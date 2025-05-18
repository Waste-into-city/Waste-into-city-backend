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
    public class WorkReportComplaintsRepository : IWorkReportComplaintsRepository
    {
        private readonly MainDbContext _mainDbContext;

        public WorkReportComplaintsRepository(MainDbContext mainDbContext)
        {
            _mainDbContext = mainDbContext;
        }

        public async Task CreateAsync(WorkReportComplaint workReportComplaint)
        {
            WorkReportComplaintEntity workReportComplaintEntity = new WorkReportComplaintEntity
            {
                Id = workReportComplaint.Id,
                Title = workReportComplaint.Title.Value,
                Description = workReportComplaint.Description.Value,
                StartedDatetime = workReportComplaint.StartedDatime,
                WorksId = workReportComplaint.WorksId,
                FromUsersId = workReportComplaint.FromUsersId,
                WorkReportStatusTypesId = (int)workReportComplaint.WorkReportStatusTypesId
            };

            await _mainDbContext.WorkReportComplaints.AddAsync(workReportComplaintEntity);
            await _mainDbContext.SaveChangesAsync();
        }

        public async Task<WorkReportComplaint> FindByIdAsync(Guid id)
        {
            WorkReportComplaintEntity workReportComplaintEntity = await _mainDbContext.WorkReportComplaints.AsNoTracking().
                FirstOrDefaultAsync(w => w.Id == id) ?? throw new DbIsNotFoundException(nameof(WorkReportComplaint), 13, null);

            return WorkReportComplaint.Create(workReportComplaintEntity.Id, Title.Create(workReportComplaintEntity.Title),
                Description.Create(workReportComplaintEntity.Description), workReportComplaintEntity.StartedDatetime,
                workReportComplaintEntity.WorksId, workReportComplaintEntity.FromUsersId,
                (WorkReportStatusEnum)workReportComplaintEntity.WorkReportStatusTypesId, null, null);
        }

        public async Task<WorkReportComplaint> FindPendingWithFromUserAndImageNamesByStartedDatetimeAscending()
        {
            WorkReportComplaintEntity workReportComplaintEntity = await _mainDbContext.WorkReportComplaints.AsNoTracking().Include(w => w.FromUser)
                .Include(w => w.Images).OrderBy(w => w.StartedDatetime).
                FirstOrDefaultAsync(w => w.WorkReportStatusTypesId == (int)WorkReportStatusEnum.Pending)
                ?? throw new DbIsNotFoundException(nameof(WorkApplication), 17, null);

            List<ImageName> imageNames = workReportComplaintEntity.Images.Select(i => ImageName.Create(i.Name)).ToList();

            if (workReportComplaintEntity.FromUser == null)
            {
                throw new NullValueServerException(42, "from user", null);
            }

            UserEntity fromUserEntity = workReportComplaintEntity.FromUser;
            User fromUser = User.Create(fromUserEntity.Id, Nickname.Create(fromUserEntity.Nickname), Email.Create(fromUserEntity.Email),
                Password.Create(fromUserEntity.Password), fromUserEntity.Ranking, null, fromUserEntity.NegativeScore, fromUserEntity.IsBanned,
                null, null);

            return WorkReportComplaint.Create(workReportComplaintEntity.Id, Title.Create(workReportComplaintEntity.Title),
                Description.Create(workReportComplaintEntity.Description), workReportComplaintEntity.StartedDatetime,
                workReportComplaintEntity.WorksId, workReportComplaintEntity.FromUsersId,
                (WorkReportStatusEnum)workReportComplaintEntity.WorkReportStatusTypesId, fromUser, imageNames);
        }

        public async Task UpdateWorkReportStatusTypesIdByIdAsync(Guid id, WorkReportStatusEnum workReportStatusTypesId)
        {
            int updatedRows = await _mainDbContext.WorkReportComplaints
                .Where(w => w.Id == id)
                .ExecuteUpdateAsync(n => n
                    .SetProperty(w => w.WorkReportStatusTypesId, (int)workReportStatusTypesId)
                );

            if (updatedRows == 0)
            {
                throw new DbUpdateCustomException(nameof(Work), 28, null);
            }
        }
    }
}
