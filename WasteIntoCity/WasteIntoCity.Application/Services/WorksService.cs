using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Interfaces.Services;
using WasteIntoCity.Core.Models;
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

        public async Task CreateAsync(string title, string description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId,
            Guid workStatusesId, int coordinatesId)
        {
            Work work = Work.Create(Guid.NewGuid(), Title.Create(title), Description.Create(description), startedDatetime, finishDatetime,
                workComplexityId, workStatusesId, coordinatesId);

            await _worksRepository.AddAsync(work);
        }

        public async Task<List<Work>> GetAll()
        {
            return await _worksRepository.FindAllAsync();
        }

        public async Task<List<Work>> GetAllOwnTakePartIn(Guid userId)
        {
            return await _worksRepository.FindAllByParticipantIdAsync(userId);
        }

        public async Task<Work> GetById(Guid id)
        {
            return await _worksRepository.FindByIdAsync(id);
        }

        public async Task UpdateAsync(Guid id, string title, string description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId,
            Guid workStatusesId, int coordinatesId)
        {
            Work work = Work.Create(id, Title.Create(title), Description.Create(description), startedDatetime, finishDatetime,
                workComplexityId, workStatusesId, coordinatesId);

            await _worksRepository.UpdateAsync(work);
        }

        public async Task UpdateWorkStatusAsync(Guid id, Guid workStatusesId)
        {
            await _worksRepository.UpdateStatusesIdByIdAsync(id, workStatusesId);
        }
    }
}
