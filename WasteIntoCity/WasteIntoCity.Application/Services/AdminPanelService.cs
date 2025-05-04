using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;

namespace WasteIntoCity.Application.Services
{
    public class AdminPanelService : IAdminPanelService
    {
        private readonly IScoreSettingsTypesRepository _scoreSettingsTypesRepository;

        public AdminPanelService(IScoreSettingsTypesRepository scoreSettingsTypesRepository)
        {
            _scoreSettingsTypesRepository = scoreSettingsTypesRepository;
        }

        public async Task<List<ScoreSettingsType>> GetAllData()
        {
            return await _scoreSettingsTypesRepository.FindAllAsync();
        }

        public async Task SaveAllData(List<ScoreSettingsPairStruct> scoreSettingsPairStructs)
        {
            await _scoreSettingsTypesRepository.UpdateValuesAllASync(scoreSettingsPairStructs);
        }
    }
}
