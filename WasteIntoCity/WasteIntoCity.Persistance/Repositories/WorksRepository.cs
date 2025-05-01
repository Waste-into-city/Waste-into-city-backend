using Microsoft.EntityFrameworkCore;
using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.InternalServer500Exceptions;
using WasteIntoCity.Core.Exceptions.NotFound404Exceptions;
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

        public async Task CreateAsync(Work work)
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

            if (workEntity.TrashTypes != null)
            {
                List<int> trashTypesIds = workEntity.TrashTypes.Select(t => (int)t.Id).ToList();

                List<WorkTrashTypeEntity> workTrashTypeEntities = trashTypesIds.Select(trashTypesId => new WorkTrashTypeEntity
                {
                    WorksId = work.Id,
                    TrashTypesId = trashTypesId
                }).ToList();

                await _mainDbContext.WorksTrashTypes.AddRangeAsync(workTrashTypeEntities); //TODO: Maybe cannot work, need to do as in usersRepository
            }

            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<List<Work>> FindAllAsync()
        {
            List<WorkEntity> workEntities = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).ToListAsync();

            List<Work> works = workEntities.Select(work =>
            {
                return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                    (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId, work.CoordinatesId, [],
                    Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat, work.Coordinates!.Lng), null, null, null, null,
                    null, null, null);
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindAllWithCoordinatesByParticipantIdAsync(Guid participantId)
        {
            List<Work> works = await _mainDbContext.WorksParticipants
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
                             null,
                             null,
                             null,
                             null
                         )
                 )
                .ToListAsync();

            return works;
        }

        public async Task<Work> FindWithCoordinatesAndParticipantsAndImagesAndTrashTypesByIdAsync(Guid id)
        {
            WorkEntity work = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).Include(w => w.Users).
                Include(w => w.Images).Include(w => w.TrashTypes).FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(Work), 8, null);

            List<User> participants = work.Users.Select(u => User.Create(u.Id, Nickname.Create(u.Nickname), Email.Create(u.Email),
                Password.Create(u.Password), u.Ranking, null, u.NegativeScore, u.IsBanned, null, null)).ToList();

            List<ImageName> imageNames = work.Images.Select(i => ImageName.Create(i.Name)).ToList();

            List<TrashType> trashTypes = work.TrashTypes.Select(t => TrashType.Create((TrashEnum)t.Id, MeanText.Create(t.Name)))
                .ToList();

            return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime,
                work.FinishDatetime, (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId,
                work.CoordinatesId, participants, Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat,
                work.Coordinates!.Lng), null, null, null, null, imageNames, null, trashTypes);
        }


        public async Task<Work> FindWithParticipantsByIdAsync(Guid id)
        {
            WorkEntity workEntity = await _mainDbContext.Works.AsNoTracking().Include(w => w.Users).FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(Work), 14, null);

            List<User> participants = workEntity.Users.Select(u => User.Create(u.Id,
                Nickname.Create(u.Nickname), Email.Create(u.Email), Password.Create(u.Password), u.Ranking, null, u.NegativeScore,
                u.IsBanned, null, null)).ToList();

            return Work.Create(workEntity.Id, Title.Create(workEntity.Title), Description.Create(workEntity.Description),
                workEntity.StartedDatetime, workEntity.FinishDatetime, (WorkComplexityEnum)workEntity.WorkComplexityTypesId,
                (WorkStatusEnum)workEntity.WorkStatusTypesId, workEntity.CoordinatesId, participants, null, null, null, null,
                null, null, null, null);
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

        public async Task UpdateStatusIdByIdAsync(Guid id, WorkStatusEnum workStatusTypesId)
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

        public async Task UpdateToAvailableWorksByIds(List<Guid> ids)
        {
            List<WorkParticipantEntity> workParticipants = await _mainDbContext.WorksParticipants
                .Where(w => ids.Contains(w.WorksId))
                .ToListAsync();

            _mainDbContext.WorksParticipants.RemoveRange(workParticipants);
            await _mainDbContext.SaveChangesAsync();

            int updatedRows = await _mainDbContext.Works
                .Where(w => ids.Contains(w.Id))
                .ExecuteUpdateAsync(w => w
                    .SetProperty(w => w.WorkStatusTypesId, (int)WorkStatusEnum.NotFinished)
                    .SetProperty(w => w.StartedDatetime, (DateTime?)null)
                    .SetProperty(w => w.FinishDatetime, (DateTime?)null)
                );
        }

        public async Task<List<Work>> FindFirstFinishedWithParticipantsAndMultiplierRankingAndWorkColleagueReportsByFinishedTimeAndClientStatuses(
            int worksAmount, TimeSpan minWorkIntervalAfterFinished)
        {
            DateTime minAppropriateFinishedWorkTime = DateTime.UtcNow.Subtract(minWorkIntervalAfterFinished);

            List<WorkEntity> workEntities = await _mainDbContext.Works
                .Where(w => w.FinishDatetime <= minAppropriateFinishedWorkTime && (w.WorkStatusTypesId == (int)WorkStatusEnum.FinishedSuccessfully
                    || w.WorkStatusTypesId == (int)WorkStatusEnum.FinishedSuccessfully))
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
                    throw new DbIsNotFoundException(nameof(WorkComplexityType), 9, null);
                }

                if (w.WorkStatusType is null)
                {
                    throw new DbIsNotFoundException(nameof(WorkStatusType), 10, null);
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
                        u.IsBanned,
                        null,
                        null
                    )).ToList();

                List<WorkColleagueReport> colleagueReports = w.WorkColleagueReports
                .Select(wc =>
                    {
                        if (wc.WorkMarkType is null)
                        {
                            throw new DbIsNotFoundException(nameof(WorkComplexityType), 16, null);
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
                    null,
                    null,
                    null,
                    null
                );
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindFirstPendingFinalizationWorksWithParticipantsByFinishedTimeAndClientStatuses(
            int worksAmount, TimeSpan minWorkIntervalAfterFinished)
        {
            DateTime minAppropriateFinishedWorkTime = DateTime.UtcNow.Subtract(minWorkIntervalAfterFinished);

            List<WorkEntity> workEntities = await _mainDbContext.Works
                .Where(w => w.FinishDatetime <= minAppropriateFinishedWorkTime && w.WorkStatusTypesId == (int)WorkStatusEnum.NotFinished)
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
                        u.IsBanned,
                        null,
                        null
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
                    null,
                    null,
                    null,
                    null
                );
            }).ToList();

            return works;
        }

        public async Task<List<Guid>> FindFirstPreparingWorksIdsByBeforeStartedTime(int worksAmount, TimeSpan minWorkIntervalBeforeStart)
        {
            DateTime minAppropriateStartedWorkTime = DateTime.UtcNow.Add(minWorkIntervalBeforeStart);

            List<WorkEntity> workEntities = await _mainDbContext.Works
                .Where(w => w.StartedDatetime >= minAppropriateStartedWorkTime && w.WorkStatusTypesId == (int)WorkStatusEnum.NotFinished)
                .Take(worksAmount)
                .ToListAsync();

            List<Guid> workIds = workEntities.Select(w =>
            {
                return w.Id;
            }).ToList();

            return workIds;
        }
    }
}
