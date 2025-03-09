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

            User newUser = User.Create(Guid.NewGuid(), Nickname.Create(nickname), Email.Create(email), Password.Create(password), USER_RANKING);
            try
            {
                await _userRepository.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                throw new DbAddException(nameof(User), null, ex.Message);
            }
        }

        public Task<(string loginTokenValue, string registrationTokenValue)> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<(string loginTokenValue, string registrationTokenValue)> RefreshAsync(string refreshTokenValue, string accessTokenValue)
        {
            throw new NotImplementedException();
        }
    }
}
