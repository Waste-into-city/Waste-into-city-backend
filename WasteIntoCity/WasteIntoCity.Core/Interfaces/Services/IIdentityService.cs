
namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(string nickname, string email, string password);

        Task<(string loginTokenValue, string registrationTokenValue)> LoginAsync(string email, string password);

        Task<(string loginTokenValue, string registrationTokenValue)> RefreshAsync(string refreshTokenValue, string accessTokenValue);
    }
}
