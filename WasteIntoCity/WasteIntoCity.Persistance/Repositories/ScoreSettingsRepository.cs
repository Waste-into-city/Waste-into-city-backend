using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class ScoreSettingsTypeRepository : IScoreSettingsTypeRepository
    {
        private readonly MainDbContext _mainDbContext;

        public ScoreSettingsTypeRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAllIfEachNotExist(List<ScoreSettingsType> scoreSettingsTypes)
        {
            List<ScoreSettingsTypeEntity> scoreSettingsTypeEntities = scoreSettingsTypes.Select(s =>
                new ScoreSettingsTypeEntity
                {
                    Id = (int)s.Id,
                    Name = s.Name.Value,
                    Value = s.Value
                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.ScoreSettingsTypes, scoreSettingsTypeEntities,
                ScoreSettingsTypeConfiguration.TABLE_NAME);
        }

        public async Task<Dictionary<ScoreSettingsEnum, int>> FindAllValuesByIdsAsync(List<ScoreSettingsEnum> ids)
        {
            List<int> intIds = ids.Select(i => (int)i).ToList();

            List<ScoreSettingsTypeEntity> settings = await _mainDbContext.ScoreSettingsTypes
                .Where(s => intIds.Contains(s.Id))
                .ToListAsync();

            Dictionary<ScoreSettingsEnum, int> result = settings.ToDictionary(
                s => (ScoreSettingsEnum)s.Id,
                s => s.Value
            );

            return result;
        }
    }
}
