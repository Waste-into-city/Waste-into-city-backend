using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IScoreSettingsTypeRepository
    {
        Task AddAllIfEachNotExist(List<ScoreSettingsType> scoreSettingsTypes);

        Task<Dictionary<ScoreSettingsEnum, int>> FindAllValuesByIdsAsync(List<ScoreSettingsEnum> ids);
    }
}
