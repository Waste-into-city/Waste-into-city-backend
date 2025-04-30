using AutoMapper;
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

        public WorkReportComplaintsRepository(MainDbContext mainDbContext, IMapper mapper)
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
                StartedDatime = workReportComplaint.StartedDatime,
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
                Description.Create(workReportComplaintEntity.Description), workReportComplaintEntity.StartedDatime,
                workReportComplaintEntity.WorksId, workReportComplaintEntity.FromUsersId,
                (WorkReportStatusEnum)workReportComplaintEntity.WorkReportStatusTypesId);
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
