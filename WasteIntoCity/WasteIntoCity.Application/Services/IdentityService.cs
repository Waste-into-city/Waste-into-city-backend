using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WasteIntoCity.Application.Options;
using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class IdentityService : IIdentityService
    {
        private const int USER_RANKING = 0;

        private readonly IUsersRepository _userRepository;
        private readonly JwtOptions _jwtOptions;

        public IdentityService(IUsersRepository usersRepository, JwtOptions jwtOptions)
        {
            _userRepository = usersRepository;
            _jwtOptions = jwtOptions;
        }

        public async Task RegisterAsync(string nickname, string email, string password)
        {
            bool isExistUser = true;

            try
            {
                User existingUser = await _userRepository.FindByEmailAsync(email);
            }
            catch
            {
                isExistUser = false;
            }

            if (isExistUser)
            {
                throw new DbIsFoundException(nameof(User), "User with this email exists");
            }

            string hashedPassword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);

            User newUser = User.Create(Guid.NewGuid(), Nickname.Create(nickname), Email.Create(email), Password.Create(hashedPassword), USER_RANKING);
            try
            {
                await _userRepository.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new DbAddException(nameof(User), null, ex.Message);
            }
        }

        public async Task<(string accessTokenValue, string refreshTokenValue)> LoginAsync(string email, string password)
        {
            User user = await _userRepository.FindByEmailAsync(email);

            JwtSecurityTokenHandler jwtSecurityAccessTokenHandler = new JwtSecurityTokenHandler();
            byte[] keyBytes = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            SecurityTokenDescriptor securityAccessTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email.Value),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email.Value),
                    new Claim("id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(2), //TODO: change lifetime
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            if (!BCrypt.Net.BCrypt.EnhancedVerify(password, user.Password.Value))
            {
                throw new LoginException();
            }

            SecurityToken securityAccessToken = jwtSecurityAccessTokenHandler.CreateToken(securityAccessTokenDescriptor);

            return (jwtSecurityAccessTokenHandler.WriteToken(securityAccessToken), ""); //TODO: add resfresh token
        }

        public Task<(string accessTokenValue, string refreshTokenValue)> RefreshAsync(string refreshTokenValue, string accessTokenValue)
        {
            throw new NotImplementedException();
        }
    }
}
