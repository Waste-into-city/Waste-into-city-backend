
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(string nickname, string email, string password);

        Task<UserPrepareTokensContextResponse> LoginAsync(string email, string password);

        Task<UserPrepareTokensContextResponse> RefreshAsync(string accessTokenValue, string refreshTokenValue);

        Task LogoutAsync(Guid userId);
    }
}
