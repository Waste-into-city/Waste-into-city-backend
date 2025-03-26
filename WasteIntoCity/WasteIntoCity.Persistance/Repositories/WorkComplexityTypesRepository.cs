using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

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

            return WorkComplexityType.Create(workComplexityTypeEntity.Id, MeanText.Create(workComplexityTypeEntity.Name), workComplexityTypeEntity.ParticipantsMin,
                workComplexityTypeEntity.ParticipantsMax, workComplexityTypeEntity.DurationHours, workComplexityTypeEntity.MultiplierRanking,
                workComplexityTypeEntity.RadiusOnMap);
        }
    }
}
