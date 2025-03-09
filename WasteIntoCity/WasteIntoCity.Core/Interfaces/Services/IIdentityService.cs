
namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(string nickname, string email, string password);

        Task<(string accessTokenValue, string refreshTokenValue)> LoginAsync(string email, string password);

        Task<(string accessTokenValue, string refreshTokenValue)> RefreshAsync(string refreshTokenValue, string accessTokenValue);
    }
}
