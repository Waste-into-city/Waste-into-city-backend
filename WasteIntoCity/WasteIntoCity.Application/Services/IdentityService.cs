using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions;
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

        private readonly IUsersRepository _userRepository;
        private readonly RefreshTokensRepository _refreshTokensRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public IdentityService(IUsersRepository usersRepository, JwtOptions jwtOptions, TokenValidationParameters tokenValidationParameters,
            RefreshTokensRepository refreshTokensRepository, IRolesRepository rolesRepository)
        {
            _userRepository = usersRepository;
            _jwtOptions = jwtOptions;
            _tokenValidationParameters = tokenValidationParameters;
            _refreshTokensRepository = refreshTokensRepository;
            _rolesRepository = rolesRepository;
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
            if (await _userRepository.IsExistByEmailAsync(email))
            {
                throw new DbIsFoundException(nameof(User), "User with this email exists");
            }

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            Role role = await _rolesRepository.FindById((int)RoleEnum.User);

            User newUser = User.Create(Guid.NewGuid(), Nickname.Create(nickname), Email.Create(email),
                Password.Create(hashedPassword), USER_START_RANKING, [role], USER_START_NEGATIVE_SCORE);

            try
            {
                await _userRepository.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new DbAddException(nameof(User), null, ex.Message);
            }
        }

        public async Task<UserPrepareTokensContextResponse> LoginAsync(string email, string password)
        {
            User user = await _userRepository.FindByEmailWithRolesAsync(email);

            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password.Value))
            {
                throw new LoginException();
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
                throw new NotExpiredAccessTokenException();
            }

            string jti = accessTokenClaimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

            RefreshTokenEntity refreshToken = await _refreshTokensRepository.FindByValueAsync(refreshTokenValue);

            if (DateTime.UtcNow > refreshToken.ExpirationTimestamp || refreshToken.Invalidated || refreshToken.Used || refreshToken.JwtId != jti)
            {
                throw new InvalidTokenException();
            }

            refreshToken.Used = true;
            await _refreshTokensRepository.UpdateAsync(refreshToken);

            User user = await _userRepository.FindByIdWithRolesAsync(accessTokenClaimsPrincipal.Claims.Single(x => x.Type == "id").Value);

            return await CreateTokens(user);
        }

        public async Task LogoutAsync(Guid userId)
        {
            await _refreshTokensRepository.UpdateUsedByUserIdTokensAsync(true, userId);
        }

        private static ClaimsPrincipal GetPrincipalFromAccessToken(string accessTokenValue, TokenValidationParameters tokenValidationParameters)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            try
            {
                ClaimsPrincipal principal = jwtSecurityTokenHandler.ValidateToken(accessTokenValue, tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    throw new InvalidTokenException();
                }

                return principal;
            }
            catch
            {
                throw new InvalidTokenException();
            }
        }

        private static bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedAccessToken)
        {
            return (validatedAccessToken is JwtSecurityToken jwtSecurityToken) &&
                jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
