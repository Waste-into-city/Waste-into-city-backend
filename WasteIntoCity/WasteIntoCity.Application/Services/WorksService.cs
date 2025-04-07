using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Application.Services
{
    public class WorksService : IWorksService
    {
        private readonly IWorksRepository _worksRepository;

        public WorksService(IWorksRepository worksRepository)
        {
            _worksRepository = worksRepository;
        }

        public async Task<List<Work>> GetAll()
        {
            return await _worksRepository.FindAllAsync();
        }

        public async Task<List<Work>> GetAllOwnTakePartIn(Guid userId)
        {
            return await _worksRepository.FindAllWithCoordinatesByParticipantIdAsync(userId);
        }

        public async Task<Work> GetById(Guid id)
        {
            return await _worksRepository.FindByIdAsync(id);
        }

        public async Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, int workComplexityTypesId,
            int workStatusesId, Guid coordinatesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkComplexityEnum>(workComplexityTypesId, "work complexity");
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            Work work = Work.Create(id, Title.Create(title), Description.Create(description), startedDatetime, finishDatetime,
                (WorkComplexityEnum)workComplexityTypesId, (WorkStatusEnum)workStatusesId, coordinatesId, null, null, null, null, null, null);

            await _worksRepository.UpdateAsync(work);
        }

        public async Task UpdateWorkStatusAsync(Guid id, int workStatusesId)
        {
            EnumOperationsExtension.CheckEnumIntValue<WorkStatusEnum>(workStatusesId, "work status");

            await _worksRepository.UpdateStatusesIdByIdAsync(id, (WorkStatusEnum)workStatusesId);
        }
    }
}
