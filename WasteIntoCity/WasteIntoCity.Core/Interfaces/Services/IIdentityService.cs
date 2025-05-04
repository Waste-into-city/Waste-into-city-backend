
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Structs;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IIdentityService
    {
        Task RegisterAsync(string nickname, string email, string password);

        Task<UserPrepareTokensContextResponse> LoginAsync(string email, string password);

        Task<UserPrepareTokensContextResponse> RefreshAsync(string accessTokenValue, string refreshTokenValue);

        Task LogoutAsync(Guid userId);

        Task<User> GetUserInfo(Guid userId);

        Task<User> GetSelfUserInfo(Guid userId);

        Task<User> GetUserInfoForAdmin(Guid userId);

        Task<(List<User>, int)> GetLeaderboardByPage(int page, int pageSize);
    }
}
