using Microsoft.Extensions.Options;
using WasteIntoCity.Api.Options;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _worksRepository;
        private readonly IOptions<PreparingWorksStatusOptions> _options;

        public WorksService(IWorksRepository worksRepository, IOptions<PreparingWorksStatusOptions> options)
        {
            _worksRepository = worksRepository;
            _options = options;
        }

        public async Task<List<Work>> GetAll()
        {
            return await _worksRepository.FindAllAsync();
        }

        public async Task<(List<Work>, int)> GetAllLookup(int page, int pageSize)
        {
            int total = await _worksRepository.CountAsync();

            List<Work> works = await _worksRepository.FindAllWithCoordinatesBySkipItemsAndSizeAsync(page, pageSize);

            return (works, total);
        }

        public async Task<List<Work>> GetAllOwnTakePartIn(Guid userId)
        {
            return await _worksRepository.FindAllWithCoordinatesByParticipantIdAsync(userId);
        }

        public async Task<Work> GetByIdAsync(Guid id)
        {
            return await _worksRepository.FindWithCoordinatesAndParticipantsAndImagesAndTrashTypesByIdAsync(id);
        }

        public async Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            int workStatusesId, Guid coordinatesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkComplexityEnum>(workComplexityTypesId, "work complexity");
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            Work work = Work.Create(id, Title.Create(title), Description.Create(description), startedDatetime, finishDatetime,
                (WorkComplexityEnum)workComplexityTypesId, (WorkStatusEnum)workStatusesId, coordinatesId, null, null, null, null,
                null, null, null, null, null);

            await _worksRepository.UpdateAsync(work);
        }

        public async Task UpdateWorkStatusAsync(Guid id, int workStatusesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            await _worksRepository.UpdateStatusIdByIdAsync(id, (WorkStatusEnum)workStatusesId);
        }

        public async Task TakePartInFirstSelfAsync(Guid id, Guid userId, DateTime startedDatetime)
        {
            DateTime startedDatetimeMin = DateTime.UtcNow.Add(_options.Value.MinIntervalStartAfterNow);
            DateTime startedDatetimeMax = DateTime.UtcNow.Add(_options.Value.MaxIntervalStartAfterNow);

            if (startedDatetime < startedDatetimeMin || startedDatetime > startedDatetimeMax)
            {
                throw new ValueOutOfRangeException<DateTime>("date time started", startedDatetimeMin, startedDatetimeMax, 69);
            }

            Work work = await _worksRepository.FindWithWorkComplexityTypeByIdAsync(id);

            WorkStatusForClientEnum workStatusForClienEnum = EnumOperationsExtension.TakeWorkStatusForClientEnum(work.StartedDatetime,
                work.FinishDatetime, work.WorkStatusTypesId);

            if (workStatusForClienEnum != WorkStatusForClientEnum.Avaliable)
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("work status for client enum", "Work doesn't have avaliable status", 73);
            }

            if (work.WorkComplexityType == null)
            {
                throw new NullValueServerException(43, "work complexity type", null);
            }

            DateTime finishedDatetime = startedDatetime.AddHours(work.WorkComplexityType.DurationHours);

            Work updatedWork = Work.Create(work.Id, Title.Create(work.Title.Value), Description.Create(work.Description.Value), startedDatetime,
                finishedDatetime, work.WorkComplexityTypesId, WorkStatusEnum.NotFinished, work.CoordinatesId, null, null, null, null, null, null,
                null, null, null);

            await _worksRepository.UpdateAsync(work);
            await _worksRepository.AddParticipants(work.Id, [userId]);
        }

        public async Task TakePartInSelfAsync(Guid id, Guid userId)
        {
            Work work = await _worksRepository.FindByIdAsync(id);

            WorkStatusForClientEnum workStatusForClienEnum = EnumOperationsExtension.TakeWorkStatusForClientEnum(work.StartedDatetime,
                work.FinishDatetime, work.WorkStatusTypesId);

            if (workStatusForClienEnum != WorkStatusForClientEnum.Preparing)
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("work status for client enum", "Work doesn't have preparing status", 74);
            }

            if (work.StartedDatetime < DateTime.UtcNow.Add(_options.Value.MinIntervalStartAfterNow))
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("date time started", "Date time started decision is so close. Cannot change", 75);
            }

            await _worksRepository.AddParticipants(work.Id, [userId]);
        }

        public async Task LeaveFromParticipationSelfAsync(Guid id, Guid userId)
        {
            Work work = await _worksRepository.FindByIdAsync(id);

            WorkStatusForClientEnum workStatusForClienEnum = EnumOperationsExtension.TakeWorkStatusForClientEnum(work.StartedDatetime,
                work.FinishDatetime, work.WorkStatusTypesId);

            if (workStatusForClienEnum != WorkStatusForClientEnum.Preparing)
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("work status for client enum", "Work doesn't have preparing status", 76);
            }

            if (work.StartedDatetime < DateTime.UtcNow.Add(_options.Value.MinIntervalStartAfterNow))
            {
                throw new ValueOutOfRangeException<WorkStatusForClientEnum>("date time started", "Date time started decision is so close. Cannot change", 77);
            }

            await _worksRepository.RemoveParticipants(id, [userId]);
        }
    }
}
