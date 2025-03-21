using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Persistance.Entities;

namespace WasteIntoCity.Persistance
{
    public static class DefaultInitTypes
    {
        public static readonly RoleEntity[] roleEntities = new RoleEntity[]
        {
            new RoleEntity { Id = (int) RoleEnum.User, Name = nameof(RoleEnum.User)},
            new RoleEntity { Id = (int) RoleEnum.Moderator, Name = nameof(RoleEnum.Moderator)},
            new RoleEntity { Id = (int) RoleEnum.SuperAdmin, Name = nameof(RoleEnum.SuperAdmin)},
        };

        public static readonly WorkReportComplaintStatusTypeEntity[] workReportComplaintStatusTypeEntities = new WorkReportComplaintStatusTypeEntity[]
        {
            new WorkReportComplaintStatusTypeEntity{Id = (int) WorkReportComplaintStatusEnum.Pending, Name = nameof(WorkReportComplaintStatusEnum.Pending) },
            new WorkReportComplaintStatusTypeEntity{Id = (int) WorkReportComplaintStatusEnum.Accepted, Name = nameof(WorkReportComplaintStatusEnum.Accepted) },
            new WorkReportComplaintStatusTypeEntity{Id = (int) WorkReportComplaintStatusEnum.Denied, Name = nameof(WorkReportComplaintStatusEnum.Denied) },
        };
    }
}
