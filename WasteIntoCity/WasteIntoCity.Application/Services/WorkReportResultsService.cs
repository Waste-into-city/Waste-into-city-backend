using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class WorkReportResultsService : IWorkReportResultsService
    {
        private readonly IWorkReportResultsRepository _workReportResultsRepository;

        public WorkReportResultsService(IWorkReportResultsRepository workReportResultsRepository)
        {
            _workReportResultsRepository = workReportResultsRepository;
        }

        public async Task CreateAsync(Guid fromParticipantId, string title, string description, int workComplexityTypesId, int workStatusTypesId,
            Guid worksId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkComplexityEnum>(workComplexityTypesId, "Work comlexity type");
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusTypesId, "Work status type");

            WorkReportResult workReportResult = WorkReportResult.Create(Guid.NewGuid(), fromParticipantId, Title.Create(title),
                Description.Create(description), (WorkComplexityEnum)workComplexityTypesId, (WorkStatusEnum)workStatusTypesId, worksId, null);

            await _workReportResultsRepository.CreateAsync(workReportResult);
        }

        public async Task<WorkReportResult> GetAsync(Guid id)
        {
            return await _workReportResultsRepository.FindWithParticipantByIdAsync(id);
        }
    }
}
