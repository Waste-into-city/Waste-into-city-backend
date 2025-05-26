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

            await _mainDbContext.SaveChangesAsync();

            if (work.TrashTypesIds != null)
            {
                List<int> trashTypesIds = work.TrashTypesIds.Select(t => (int)t).ToList();

                List<WorkTrashTypeEntity> workTrashTypeEntities = trashTypesIds.Select(trashTypesId => new WorkTrashTypeEntity
                {
                    WorksId = work.Id,
                    TrashTypesId = trashTypesId
                }).ToList();

                await _mainDbContext.WorksTrashTypes.AddRangeAsync(workTrashTypeEntities); //TODO: Maybe cannot work, need to do as in usersRepository
            }

            await _mainDbContext.SaveChangesAsync();
        }


        public async Task AddParticipants(Guid id, List<Guid> participantsIds)
        {
            List<WorkParticipantEntity> participants = participantsIds
                .Select(p => new WorkParticipantEntity
                {
                    WorksId = id,
                    ParticipantsId = p
                }).ToList();

            await _mainDbContext.WorksParticipants.AddRangeAsync(participants);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _mainDbContext.Works.AsNoTracking().CountAsync();
        }

        public async Task<int> CountByParticipantIdAsync(Guid userId)
        {
            List<WorkEntity> works = await _mainDbContext.Works.AsNoTracking().Include(w => w.Users).Where(w => w.Users.Any(u => u.Id == userId))
                .ToListAsync();

            return await _mainDbContext.Works.AsNoTracking().Include(w => w.Users).Where(w => w.Users.Any(u => u.Id == userId)).
                CountAsync();
        }

        public async Task<List<Work>> FindAllWithCoordinatesByNotClosedAsync()
        {
            List<WorkEntity> workEntities = await _mainDbContext.Works.AsNoTracking().Where(w => w.WorkStatusTypesId != (int)WorkStatusEnum.Closed)
                .Include(w => w.Coordinates).ToListAsync();

            List<Work> works = workEntities.Select(work =>
            {
                return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                    (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId, work.CoordinatesId, null,
                    Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat, work.Coordinates!.Lng), null, null, null, null,
                    null, null, null);
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindAllAsync()
        {
            List<WorkEntity> workEntities = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).ToListAsync();

            List<Work> works = workEntities.Select(work =>
            {
                return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime, work.FinishDatetime,
                    (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId, work.CoordinatesId, null,
                    Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat, work.Coordinates!.Lng), null, null, null, null,
                    null, null, null);
            }).ToList();

            return works;
        }

        public async Task<List<Work>> FindAllWithCoordinatesAndTrashTypesIdsByParticipantIdAndSkipItemsAsync(Guid participantId, int skipItems, int size)
        {
            List<Work> works = await _mainDbContext.WorksParticipants
                 .Where(wp => wp.ParticipantsId == participantId)
                 .Join(_mainDbContext.Works.Include(w => w.Coordinates).Include(w => w.TrashTypes),
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
                             w.TrashTypes.Select(t => (TrashEnum)t.Id).ToList()
                         )
                 )
                 .Skip(skipItems)
                 .Take(size)
                .ToListAsync();

            return works;
        }

        public async Task<Work> FindByIdAsync(Guid id)
        {
            WorkEntity workEntity = await _mainDbContext.Works.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(Work), 16, null);

            return Work.Create(workEntity.Id, Title.Create(workEntity.Title), Description.Create(workEntity.Description), workEntity.StartedDatetime,
                workEntity.FinishDatetime, (WorkComplexityEnum)workEntity.WorkComplexityTypesId, (WorkStatusEnum)workEntity.WorkStatusTypesId,
                workEntity.CoordinatesId, null, null, null, null, null, null, null, null, null);
        }

        public async Task<Work> FindWithWorkComplexityTypeByIdAsync(Guid id)
        {
            WorkEntity workEntity = await _mainDbContext.Works.AsNoTracking().Include(w => w.WorkComplexityType)
                .FirstOrDefaultAsync(w => w.Id == id) ?? throw new DbIsNotFoundException(nameof(Work), 16, null);

            if (workEntity.WorkComplexityType == null)
            {
                throw new NullValueServerException(53, "work complexity type", null);
            }

            WorkComplexityTypeEntity workComplexityTypeEntity = workEntity.WorkComplexityType;

            WorkComplexityType workComplexityType = WorkComplexityType.Create((WorkComplexityEnum)workComplexityTypeEntity.Id,
                MeanText.Create(workComplexityTypeEntity.Name), workComplexityTypeEntity.ParticipantsMin, workComplexityTypeEntity.ParticipantsMax,
                workComplexityTypeEntity.DurationHours, workComplexityTypeEntity.MultiplierRanking, workComplexityTypeEntity.RadiusOnMap);

            return Work.Create(workEntity.Id, Title.Create(workEntity.Title), Description.Create(workEntity.Description), workEntity.StartedDatetime,
                workEntity.FinishDatetime, (WorkComplexityEnum)workEntity.WorkComplexityTypesId, (WorkStatusEnum)workEntity.WorkStatusTypesId,
                workEntity.CoordinatesId, null, null, workComplexityType, null, null, null, null, null, null);
        }

        public async Task<Work> FindWithCoordinatesAndParticipantsAndAvatarImageNameAndImagesAndTrashTypesByIdAsync(Guid id)
        {
            WorkEntity work = await _mainDbContext.Works.AsNoTracking().Include(w => w.Coordinates).Include(w => w.Users).
                Include(w => w.Images).Include(w => w.TrashTypes).FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new DbIsNotFoundException(nameof(Work), 8, null);

            List<User> participants = work.Users.Select(u => User.Create(u.Id, Nickname.Create(u.Nickname), Email.Create(u.Email),
                Password.Create(u.Password), u.Ranking, null, u.NegativeScore, u.IsBanned,
                u.Image != null ? ImageName.Create(u.Image.Name) : null, null)).ToList();

            List<ImageName> imageNames = work.Images.Select(i => ImageName.Create(i.Name)).ToList();

            List<TrashEnum> trashTypesIds = work.TrashTypes.Select(t => (TrashEnum)t.Id).ToList();

            return Work.Create(work.Id, Title.Create(work.Title), Description.Create(work.Description), work.StartedDatetime,
                work.FinishDatetime, (WorkComplexityEnum)work.WorkComplexityTypesId, (WorkStatusEnum)work.WorkStatusTypesId,
                work.CoordinatesId, participants, Coordinates.Create(work.Coordinates!.Id, work.Coordinates!.Lat,
                work.Coordinates!.Lng), null, null, null, null, imageNames, null, trashTypesIds);
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
                    .SetProperty(w => w.CoordinatesId, work.CoordinatesId)
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
                            throw new DbIsNotFoundException(nameof(WorkComplexityType), 21, null);
                        }

                        return WorkColleagueReport.Create(
                            wc.Id,
                            wc.FromParticipantId,
                            wc.AboutColleagueId,
                            wc.WorksId,
                            (WorkMarkEnum)wc.WorkMarkTypesId,
                            WorkMarkType.Create((WorkMarkEnum)wc.WorkMarkType.Id, MeanText.Create(wc.WorkMarkType.Name), wc.WorkMarkType.AdditionRanking),
                            null
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

        public async Task<List<Guid>> FindFirstPreparingWorksIdsByBeforeStartedTimeAndNotEnoughParticipants(
            int worksAmount, TimeSpan minWorkIntervalBeforeStart,
            Dictionary<WorkComplexityEnum, WorkComplexityType> workComplexityValues)
        {
            DateTime currentDatetime = DateTime.UtcNow;
            DateTime minAppropriateStartedWorkTime = currentDatetime.Add(minWorkIntervalBeforeStart);

            // started - range <= current <= started

            List<WorkEntity> candidateWorks = await _mainDbContext.Works
                .Include(w => w.Users)
                .Where(w =>
                    w.StartedDatetime <= minAppropriateStartedWorkTime &&
                    currentDatetime <= w.StartedDatetime &&
                    w.WorkStatusTypesId == (int)WorkStatusEnum.NotFinished)
                .ToListAsync();

            List<Guid> filteredWorks = candidateWorks
                .Where(w =>
                    workComplexityValues.TryGetValue((WorkComplexityEnum)w.WorkComplexityTypesId, out var type) &&
                    w.Users.Count < type.ParticipantsMin)
                .Take(worksAmount)
                .Select(w => w.Id)
                .ToList();

            return filteredWorks;
        }

        public async Task RemoveParticipants(Guid id, List<Guid> participantsIds)
        {
            List<WorkParticipantEntity> participants = participantsIds
                .Select(p => new WorkParticipantEntity
                {
                    WorksId = id,
                    ParticipantsId = p
                }).ToList();

            _mainDbContext.WorksParticipants.RemoveRange(participants);
            await _mainDbContext.SaveChangesAsync();
        }
    }
}

