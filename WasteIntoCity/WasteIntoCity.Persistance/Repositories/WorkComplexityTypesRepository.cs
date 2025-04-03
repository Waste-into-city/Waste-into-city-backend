using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Configurations;
using WasteIntoCity.Persistance.Entities;
using WasteIntoCity.Persistance.Extensions;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkComplexityTypesRepository : IWorkComplexityTypesRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkComplexityTypesRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task<WorkComplexityType> FindById(int id)
        {
            WorkComplexityTypeEntity workComplexityTypeEntity = await _mainDbContext.WorkComplexityTypes.AsNoTracking().
                            FirstOrDefaultAsync(u => u.Id == id) ?? throw new DbIsNotFoundException(nameof(WorkComplexityType), null);

            return WorkComplexityType.Create((WorkComplexityEnum)workComplexityTypeEntity.Id, MeanText.Create(workComplexityTypeEntity.Name), workComplexityTypeEntity.ParticipantsMin,
                workComplexityTypeEntity.ParticipantsMax, workComplexityTypeEntity.DurationHours, workComplexityTypeEntity.MultiplierRanking,
                workComplexityTypeEntity.RadiusOnMap);
        }

        public async Task AddAllIfEachNotExist(List<WorkComplexityType> workComplexityTypes)
        {
            List<WorkComplexityTypeEntity> workComplexityTypeEntities = workComplexityTypes.Select(w =>
                new WorkComplexityTypeEntity
                {
                    Id = (int)w.Id,
                    Name = w.Name.Value,
                    ParticipantsMin = w.ParticipantsMin,
                    ParticipantsMax = w.ParticipantsMax,
                    DurationHours = w.DurationHours,
                    MultiplierRanking = w.MultiplierRanking,
                    RadiusOnMap = w.RadiusOnMap
                }
            ).ToList();

            await _mainDbContext.AddToDbTypesBasedEntitiesIfEachNotExistById(_mainDbContext.WorkComplexityTypes, workComplexityTypeEntities,
                WorkComplexityTypeConfiguration.TABLE_NAME);
        }
    }
}
