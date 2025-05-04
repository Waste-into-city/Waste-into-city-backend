using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Repositories
{
    public interface IScoreSettingsTypesRepository
    {
        Task AddAllIfEachNotExist(List<ScoreSettingsType> scoreSettingsTypes);

        Task<Dictionary<ScoreSettingsEnum, int>> FindAllValuesByIdsAsync(List<ScoreSettingsEnum> ids);

        Task<List<ScoreSettingsType>> FindAllAsync();

        Task UpdateValuesAllASync(List<ScoreSettingsPairStruct> scoreSettingsPairStructs);
    }
}
