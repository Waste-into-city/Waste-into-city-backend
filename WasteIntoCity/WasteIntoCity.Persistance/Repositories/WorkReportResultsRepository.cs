using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorkReportResultsRepository : IWorkReportResultsRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkReportResultsRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task CreateAsync(WorkReportResult workReportResult)
        {
            WorkReportResultEntity workReportResultEntity = new WorkReportResultEntity
            {
                Id = workReportResult.Id,
                Title = workReportResult.Title.Value,
                Description = workReportResult.Description.Value,
                FromParticipantsId = workReportResult.FromParticipantId,
                WorkComplexityTypesId = (int)workReportResult.WorkComplexityTypesId,
                WorkStatusTypesId = (int)workReportResult.WorkStatusTypesId
            };

            await _mainDbContext.WorkReportResults.AddAsync(workReportResultEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<WorkReportResult> FindByIdAsync(Guid id)
        {
            WorkReportResultEntity workReportResultEntity = await _mainDbContext.WorkReportResults.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(WorkReportResult), 12, null);

            return WorkReportResult.Create(workReportResultEntity.Id, workReportResultEntity.FromParticipantsId, Title.Create(workReportResultEntity.Title),
                Description.Create(workReportResultEntity.Description), (WorkComplexityEnum)workReportResultEntity.WorkComplexityTypesId,
                (WorkStatusEnum)workReportResultEntity.WorkStatusTypesId);
        }
    }
}
