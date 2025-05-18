using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.Unauthorized401Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Structs;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Repositories;

namespace WasteIntoCity.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private const int USER_START_RANKING = 0;
        private const int USER_START_NEGATIVE_SCORE = 0;

        private readonly IUsersRepository _usersRepository;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IImagesRepository _imagesRepository;

        public IdentityService(IUsersRepository usersRepository, JwtOptions jwtOptions, TokenValidationParameters tokenValidationParameters,
            RefreshTokensRepository refreshTokensRepository, IRolesRepository rolesRepository, IImagesRepository imagesRepository)
        {
            _usersRepository = usersRepository;
            _jwtOptions = jwtOptions;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokensRepository = refreshTokensRepository;
            _rolesRepository = rolesRepository;
            _imagesRepository = imagesRepository;
        }

        private async Task<UserPrepareTokensContextResponse> CreateTokens(User user)
        {
            JwtSecurityTokenHandler jwtSecurityAccessTokenHandler = new JwtSecurityTokenHandler();
            byte[] keyBytes = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            DateTime accessTokenExpiredTimestamp = DateTime.UtcNow.Add(_jwtOptions.AccessTokenLifetime);

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email.Value),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                new Claim("id", user.Id.ToString()),
            };

            if (user.Roles is null)
            {
                throw new NullValueServerException(22, "roles", null);
            }

            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

            SecurityTokenDescriptor securityAccessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpiredTimestamp,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken accessToken = jwtSecurityAccessTokenHandler.CreateToken(securityAccessTokenDescriptor);

            DateTime refreshTokenExpiredTimestamp = DateTime.UtcNow.Add(_jwtOptions.RefreshTokenLifetime);

            RefreshTokenEntity refreshToken = new RefreshTokenEntity
            {
                Value = Guid.NewGuid(),
                JwtId = accessToken.Id,
                UserId = user.Id,
                CreationTimestamp = DateTime.UtcNow,
                ExpirationTimestamp = refreshTokenExpiredTimestamp,
            };

            await _refreshTokensRepository.AddAsync(refreshToken);

            return new UserPrepareTokensContextResponse
            {
                AccessTokenValue = jwtSecurityAccessTokenHandler.WriteToken(accessToken),
                RefreshTokenValue = refreshToken.Value.ToString(),
                AccessTokenExpiredTimestamp = accessTokenExpiredTimestamp,
                RefreshTokenExpiredTimestamp = refreshTokenExpiredTimestamp,
            };
        }

        public async Task RegisterAsync(string nickname, string email, string password)
        {
            if (await _usersRepository.IsExistByEmailAsync(email))
            {
                throw new DbIsFoundException(nameof(User), "User with this email exists", 1);
            }

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            Role role = await _rolesRepository.FindById((int)RoleEnum.User);

            User newUser = User.Create(Guid.NewGuid(), Nickname.Create(nickname), Email.Create(email),
                Password.Create(hashedPassword), USER_START_RANKING, [role], USER_START_NEGATIVE_SCORE, false, null, null);

            try
            {
                await _usersRepository.AddWithRolesAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new DbAddException(nameof(User), 1, null, ex.Message);
            }
        }

        public async Task<UserPrepareTokensContextResponse> LoginAsync(string email, string password)
        {
            User user = await _usersRepository.FindByEmailWithRolesAsync(email);

            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password.Value))
            {
                throw new LoginException(2);
            }

            if (user.IsBanned)
            {
                throw new UserWasBannedException(5);
            }

            return await CreateTokens(user);
        }

        public async Task<UserPrepareTokensContextResponse> RefreshAsync(string accessTokenValue, string refreshTokenValue)
        {
            TokenValidationParameters tokenValidationParameters = _tokenValidationParameters.Clone();
            tokenValidationParameters.ValidateLifetime = false;

            ClaimsPrincipal accessTokenClaimsPrincipal = GetPrincipalFromAccessToken(accessTokenValue, tokenValidationParameters);

            long expiryDateUnix = long.Parse(accessTokenClaimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

            DateTime expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                .AddSeconds(expiryDateUnix);

            if (expiryDateTimeUtc > DateTime.UtcNow)
            {
                throw new NotExpiredAccessTokenException(43);
            }

            string jti = accessTokenClaimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            RefreshTokenEntity refreshToken = await _refreshTokensRepository.FindByValueAsync(refreshTokenValue);

            if (DateTime.UtcNow > refreshToken.ExpirationTimestamp || refreshToken.Invalidated || refreshToken.Used || refreshToken.JwtId != jti)
            {
                throw new InvalidTokenException(5);
            }

            refreshToken.Used = true;
            await _refreshTokensRepository.UpdateAsync(refreshToken);

            User user;

            if (Guid.TryParse(accessTokenClaimsPrincipal.Claims.Single(x => x.Type == "id").Value, out Guid parsedGuid))
            {
                user = await _usersRepository.FindWithRolesByIdAsync(parsedGuid);
            }
            else
            {
                throw new InvalidTokenException(6);
            }

            if (user.IsBanned)
            {
                throw new UserWasBannedException(7);
            }

            return await CreateTokens(user);
        }

        public async Task LogoutAsync(Guid userId)
        {
            await _refreshTokensRepository.UpdateUsedByUserIdTokensAsync(true, userId);
        }

        public async Task<User> GetUserInfo(Guid userId)
        {
            return await _usersRepository.FindWithImageNameById(userId);
        }

        public async Task<User> GetSelfUserInfo(Guid userId)
        {
            return await _usersRepository.FindWithImageAndRolesById(userId);
        }

        public async Task<User> GetUserInfoForAdmin(Guid userId)
        {
            return await _usersRepository.FindWithImageNameById(userId);
        }

        public async Task<(List<User>, int)> GetLeaderboardBySkipItemsAndSize(int skipItems, int size)
        {
            int total = await _usersRepository.CountByUserRoleAsync();

            List<User> users = await _usersRepository.FindAllByRoleUserAndRankingDescendingBySkipItemsAndSizeAsync(skipItems, size);

            return (users, total);
        }

        private static ClaimsPrincipal GetPrincipalFromAccessToken(string accessTokenValue, TokenValidationParameters tokenValidationParameters)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(accessTokenValue, tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    throw new InvalidTokenException(7);
                }

                return principal;
            }
            catch
            {
                throw new InvalidTokenException(8);
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedAccessToken)
        {
            return (validatedAccessToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }

        public async Task UpdateOwnUserInfoAsync(Guid userId, string email, string? password, string? newPassword, string nickname,
            string? avatarImageName)
        {
            User user = await _usersRepository.FindWithImageNameById(userId);

            if (user.Email.Value != email)
            {
                if (await _usersRepository.IsExistByEmailAsync(email))
                {
                    throw new DbIsFoundException(nameof(User), "User with this email already exists", 64);
                }
            }

            string hashedPassword;
            if (newPassword != null && password != null)
            {
                if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password.Value))
                {
                    throw new LoginException(6);
                }

                hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(newPassword);
            }
            else if (newPassword == null && password == null)
            {
                hashedPassword = user.Password.Value;
            }
            else
            {
                throw new IncorrectRequestFormatException("Password and new password should be both null or not null", 65);
            }

            User updatedUser = User.Create(userId, Nickname.Create(nickname), Email.Create(email), Password.Create(hashedPassword), user.Ranking,
                null, user.NegativeScore, user.IsBanned, null, null);

            await _usersRepository.UpdateByIdAsync(updatedUser);

            if (user.AvatarImageName != null)
            {
                await _imagesRepository.UpdateUserIdByNameAsync(user.AvatarImageName, null);
            }

            if (avatarImageName != null)
            {
                await _imagesRepository.UpdateUserIdByNameAsync(ImageName.Create(avatarImageName), userId);
            }
        }
    }
}
