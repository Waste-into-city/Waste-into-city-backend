using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Interfaces.Repositories;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
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
                WorkComplexityTypesId = (int)work.WorkComplexityTypesId,
                WorkStatusTypesId = (int)work.WorkStatusTypesId,
                CoordinatesId = work.CoordinatesId
            };

            await _mainDbContext.Works.AddAsync(workEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<List<Work>> FindAllAsync()
        {
            List<WorkEntity> workEntities = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).ToListAsync();

            List<Work> works = workEntities.Select(work =>
            {
                return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                    (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId, work.CoordinatesId, [],
                    Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat, work.Coordinates!.Lng), null, null, null, null);
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindAllWithCoordinatesByParticipantIdAsync(Guid participantId)
        {
            List<Work> works = await _mainDbContext.WorkParticipants
                 .Where(wp => wp.ParticipantsId == participantId)
                 .Join(_mainDbContext.Works.Include(w => w.Coordinates),
                     wp => wp.WorksId,
                     w => w.Id,
                     (wp, w) =>
                         Work.Create(
                             w.Id,
                             Title.Create(w.Title),
                             Description.Create(w.Description),
                             w.StartedDatetime,
                             w.FinishDatetime,
                             (WorkComplexityEnum)w.WorkComplexityTypesId,
                             (WorkStatusEnum)w.WorkStatusTypesId,
                             w.CoordinatesId,
                             null,
                             Coordinates.Create(w.Coordinates!.Id, w.Coordinates!.Lat, w.Coordinates!.Lng),
                             null,
                             null,
                             null,
                             null
                         )
                 )
                .ToListAsync();

            return works;
        }

        public async Task<Work> FindByIdAsync(Guid id)
        {
            WorkEntity work = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(Work), 3, null);

            return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId, work.CoordinatesId, [],
                Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat, work.Coordinates!.Lng), null, null, null, null);
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
                    .SetProperty(w => w.WorkComplexityTypesId, (int)work.WorkComplexityTypesId)
                    .SetProperty(w => w.WorkStatusTypesId, (int)work.WorkStatusTypesId)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(Work), 6, null);
            }
        }

        public async Task UpdateStatusesIdByIdAsync(Guid id, WorkStatusEnum workStatusTypesId)
        {
            int updatedRows = await _mainDbContext.Works
                .Where(w => w.Id == id)
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorkStatusTypesId, (int)workStatusTypesId)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(Work), 7, null);
            }
        }

        public async Task<List<Work>> FindFirstByFinishedTimeAndStatusesWithParticipantsAndMultiplierRankingAndWorkColleagueReportsAndWorkStatus(
            int worksAmount, TimeSpan minWorkIntervalAfterFinished, WorkStatusEnum[] workStatuses)
        {
            DateTime minAppropriateFinishedWorkTime = DateTime.UtcNow.Subtract(minWorkIntervalAfterFinished);

            List<WorkEntity> workEntities = await _mainDbContext.Works
                .Where(w => w.FinishDatetime <= minAppropriateFinishedWorkTime && workStatuses.Contains((WorkStatusEnum)w.WorkStatusTypesId))
                .Include(w => w.WorkComplexityType)
                .Include(w => w.Users)
                .Include(w => w.WorkStatusType)
                .Include(w => w.WorkColleagueReports)
                .ThenInclude(wcr => wcr.WorkMarkType)
                .Take(worksAmount)
                .ToListAsync();

            List<Work> works = workEntities.Select(w =>
            {
                if (w.WorkComplexityType is null)
                {
                    throw new DbIsNotFoundException(nameof(WorkComplexityType), 4, null);
                }

                if (w.WorkStatusType is null)
                {
                    throw new DbIsNotFoundException(nameof(WorkStatusType), 5, null);
                }

                List<User> participants = w.Users.Select(u =>
                    User.Create(
                        u.Id,
                        Nickname.Create(u.Nickname),
                        Email.Create(u.Email),
                        Password.Create(u.Password),
                        u.Ranking,
                        null,
                        u.NegativeScore,
                        u.IsBanned
                    )).ToList();

                List<WorkColleagueReport> colleagueReports = w.WorkColleagueReports
                .Select(wc =>
                    {
                        if (wc.WorkMarkType is null)
                        {
                            throw new DbIsNotFoundException(nameof(WorkComplexityType), 6, null);
                        }

                        return WorkColleagueReport.Create(
                            wc.Id,
                            wc.FromParticipantId,
                            wc.AboutColleagueId,
                            wc.WorksId,
                            (WorkMarkEnum)wc.WorkMarkTypesId,
                            WorkMarkType.Create((WorkMarkEnum)wc.WorkMarkType.Id, MeanText.Create(wc.WorkMarkType.Name), wc.WorkMarkType.AdditionRanking)
                        );
                    }
                ).ToList();

                WorkComplexityType complexity = WorkComplexityType.Create(
                    (WorkComplexityEnum)w.WorkComplexityType.Id,
                    MeanText.Create(w.WorkComplexityType.Name),
                    w.WorkComplexityType.ParticipantsMin,
                    w.WorkComplexityType.ParticipantsMax,
                    w.WorkComplexityType.DurationHours,
                    w.WorkComplexityType.MultiplierRanking,
                    w.WorkComplexityType.RadiusOnMap
                );

                WorkStatusType workStatusType = WorkStatusType.Create(
                    (WorkStatusEnum)w.WorkStatusType.Id,
                    MeanText.Create(w.WorkStatusType.Name),
                    w.WorkStatusType.AddingRanking
                );

                return Work.Create(
                    w.Id,
                    Title.Create(w.Title),
                    Description.Create(w.Description),
                    w.StartedDatetime,
                    w.FinishDatetime,
                    (WorkComplexityEnum)w.WorkComplexityTypesId,
                    (WorkStatusEnum)w.WorkStatusTypesId,
                    w.CoordinatesId,
                    participants,
                    null,
                    complexity,
                    colleagueReports,
                    workStatusType,
                    null
                );
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindFirstByFinishedTimeAndStatusesWithParticipants(int worksAmount, TimeSpan minWorkIntervalAfterFinished, WorkStatusForClientEnum[] workStatusesForClient)
        {
            DateTime minAppropriateFinishedWorkTime = DateTime.UtcNow.Subtract(minWorkIntervalAfterFinished);

            List<WorkEntity> workEntities = await _mainDbContext.Works
                .Where(w => w.FinishDatetime <= minAppropriateFinishedWorkTime && EnumOperationsExtension.TakeWorkStatusForClientEnum(w.StartedDatetime, w, FinishDatetime, workStatusTypesId))
                .Include(w => w.Users)
                .Take(worksAmount)
                .ToListAsync();

            List<Work> works = workEntities.Select(w =>
            {
                List<User> participants = w.Users.Select(u =>
                    User.Create(
                        u.Id,
                        Nickname.Create(u.Nickname),
                        Email.Create(u.Email),
                        Password.Create(u.Password),
                        u.Ranking,
                        null,
                        u.NegativeScore,
                        u.IsBanned
                    )).ToList();

                return Work.Create(
                    w.Id,
                    Title.Create(w.Title),
                    Description.Create(w.Description),
                    w.StartedDatetime,
                    w.FinishDatetime,
                    (WorkComplexityEnum)w.WorkComplexityTypesId,
                    (WorkStatusEnum)w.WorkStatusTypesId,
                    w.CoordinatesId,
                    participants,
                    null,
                    null,
                    null,
                    null,
                    null
                );
            }).ToList();

            return works;
        }
    }
}
