using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance.Repositories
{
    public class WorksRepository : IWorksRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorksRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task AddAsync(Work work)
        {
            WorkEntity workEntity = new WorkEntity
            {
                Id = work.Id,
                Title = work.Title.Value,
                Description = work.Description.Value,
                StartedDatetime = work.StartedDatetime,
                FinishDatetime = work.FinishDatetime,
                WorkComplexityTypesId = work.WorkComplexityId,
                WorkStatusTypesId = work.WorkStatusesId
            };

            await _mainDbContext.Works.AddAsync(workEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<List<Work>> FindAllAsync()
        {
            List<WorkEntity> workEntities = await _mainDbContext.Works.AsNoTracking().ToListAsync();

            List<Work> works = workEntities.Select(e =>
            {
                return Work.Create(e.Id, Title.Create(e.Title), Description.Create(e.Description), e.StartedDatetime, e.FinishDatetime,
                    e.WorkComplexityTypesId, e.WorkStatusTypesId, e.CoordinatesId);
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindAllByParticipantIdAsync(Guid participantId)
        {
            List<Work> works = await _mainDbContext.WorkParticipants
                .Where(wp => wp.ParticipantsId == participantId)
                .Join(_mainDbContext.Works,
                      wp => wp.WorksId,
                      w => w.Id,
                      (wp, w) => Work.Create(w.Id, Title.Create(w.Title), Description.Create(w.Description), w.StartedDatetime, w.FinishDatetime,
                        w.WorkComplexityTypesId, w.WorkStatusTypesId, w.CoordinatesId)
                      )
                .ToListAsync();

            return works;
        }

        public async Task<Work> FindByIdAsync(Guid id)
        {
            WorkEntity work = await _mainDbContext.Works.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(User), null);

            return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                work.WorkComplexityTypesId, work.WorkStatusTypesId, work.CoordinatesId);
        }

        public async Task UpdateAsync(Work work)
        {
            int updatedRows = await _mainDbContext.Works
                .Where(w => w.Id == work.Id)
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.Title, work.Title.Value)
                    .SetProperty(w => w.Description, work.Description.Value)
                    .SetProperty(w => w.StartedDatetime, work.StartedDatetime)
                    .SetProperty(w => w.FinishDatetime, work.FinishDatetime)
                    .SetProperty(w => w.WorkComplexityTypesId, work.WorkComplexityId)
                    .SetProperty(w => w.WorkStatusTypesId, work.WorkStatusesId)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(Work), null);
            }
        }

        public async Task UpdateStatusesIdByIdAsync(Guid id, Guid statusGuid)
        {
            int updatedRows = await _mainDbContext.Works
                .Where(w => w.Id == id)
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorkStatusTypesId, statusGuid)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(Work), null);
            }
        }
    }


}
