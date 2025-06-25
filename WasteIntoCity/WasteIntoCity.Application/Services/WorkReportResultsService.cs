using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
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
        private readonly IWorksRepository _worksRepository;

        public WorkReportResultsService(IWorkReportResultsRepository workReportResultsRepository,
            IWorksRepository worksRepository)
        {
            _workReportResultsRepository = workReportResultsRepository;
            _worksRepository = worksRepository;
        }

        public async Task CreateAsync(Guid fromParticipantId, string title, string description, int workComplexityTypesId, int newWorkStatusTypesForClientId,
            Guid worksId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkComplexityEnum>(workComplexityTypesId, "Work comlexity type");

            Work work = await _worksRepository.FindWithParticipantsByIdAsync(worksId);

            if (work.Participants == null)
            {
                throw new NullValueServerException(49, "participants", null);
            }

            if (!work.Participants.Any(p => p.Id == fromParticipantId))
            {
                throw new DbIsNotFoundException("participants", 20, $"Participant {fromParticipantId} " +
                    $"didn't took part in work {worksId}");
            }

            WorkStatusForClientEnum currentworkStatusForClient = EnumOperationsExtension.TakeWorkStatusForClientEnum(work.StartedDatetime, work.FinishDatetime, work.WorkStatusTypesId);

            if (currentworkStatusForClient != WorkStatusForClientEnum.InProgress && currentworkStatusForClient != WorkStatusForClientEnum.PendingFinalization)
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("work status types for client current id",
                    WorkStatusForClientEnum.InProgress, WorkStatusForClientEnum.PendingFinalization, 67);
            }

            WorkStatusEnum newWorkStatusTypesId;
            if (newWorkStatusTypesForClientId == (int)WorkStatusForClientEnum.FinishedSuccessfully)
            {
                newWorkStatusTypesId = WorkStatusEnum.FinishedSuccessfully;
            }
            else if (newWorkStatusTypesForClientId == (int)WorkStatusForClientEnum.FinishedFailed)
            {
                newWorkStatusTypesId = WorkStatusEnum.FinishedFailed;
            }
            else
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("work status for client types new id",
                    WorkStatusForClientEnum.FinishedSuccessfully, WorkStatusForClientEnum.FinishedSuccessfully, 66);
            }


            WorkReportResult workReportResult = WorkReportResult.Create(Guid.NewGuid(), fromParticipantId, Title.Create(title),
            Description.Create(description), (WorkComplexityEnum)workComplexityTypesId, newWorkStatusTypesId, worksId, null);

            await _workReportResultsRepository.CreateAsync(workReportResult);
            await _worksRepository.UpdateStatusIdByIdAsync(work.Id, newWorkStatusTypesId);
        }

        public async Task<(WorkReportResult, Work)> GetAsync(Guid id)
        {
            WorkReportResult workReportResult = await _workReportResultsRepository.FindWithParticipantByIdAsync(id);

            Work work = await _worksRepository.FindByIdAsync(workReportResult.WorksId);

            return (workReportResult, work);
        }
    }
}
