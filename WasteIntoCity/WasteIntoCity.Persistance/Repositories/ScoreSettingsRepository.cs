using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Application.Structs;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class ScoreSettingsTypeRepository : IScoreSettingsTypesRepository
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

        public async Task<List<ScoreSettingsType>> FindAllAsync()
        {
            List<ScoreSettingsTypeEntity> scoreSettingsTypeEntities = await _mainDbContext.ScoreSettingsTypes.AsNoTracking().ToListAsync();

            return scoreSettingsTypeEntities.Select(s => ScoreSettingsType.Create((ScoreSettingsEnum)s.Id, MeanText.Create(s.Name), s.Value))
                .ToList();
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

        public async Task UpdateValuesAllASync(List<ScoreSettingsPairStruct> scoreSettingsPairStructs)
        {
            foreach (var item in scoreSettingsPairStructs)
            {
                ScoreSettingsTypeEntity scoreSettingsTypeEntity = await _mainDbContext.ScoreSettingsTypes.FindAsync(item.Id)
                    ?? throw new DbIsNotFoundException(nameof(ScoreSettingsType), 19, null);

                scoreSettingsTypeEntity.Value = item.Value;
            }

            await _mainDbContext.SaveChangesAsync();
        }
    }
}
