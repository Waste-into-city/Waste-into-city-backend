using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Core.Interfaces.Services
{
    public interface IAdminPanelService
    {
        Task SaveAllData(List<ScoreSettingsPairStruct> scoreSettingsPairStructs);

        Task<List<ScoreSettingsType>> GetAllData();
    }
}
