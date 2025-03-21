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

        public static readonly WorkComplexityTypeEntity[] workComplexityTypeEntities = new WorkComplexityTypeEntity[]
        {
            new WorkComplexityTypeEntity
            {
                Id = (int) WorkComplexityEnum.Easy,
                Name = nameof(WorkComplexityEnum.Easy),
                DurationHours = 2,
                MultiplierRanking = 1,
                RadiusOnMap = 1,
                ParticipantsMin = 1,
                ParticipantsMax = 3
            },
            new WorkComplexityTypeEntity
            {
                Id = (int) WorkComplexityEnum.Medium,
                Name = nameof(WorkComplexityEnum.Medium),
                DurationHours = 8,
                MultiplierRanking = 2,
                RadiusOnMap = 2,
                ParticipantsMin = 5,
                ParticipantsMax = 8
            },
            new WorkComplexityTypeEntity
            {
                Id = (int) WorkComplexityEnum.Hard,
                Name = nameof(WorkComplexityEnum.Hard),
                DurationHours = 24,
                MultiplierRanking = 3,
                RadiusOnMap = 3,
                ParticipantsMin = 10,
                ParticipantsMax = 12
            },
        };
    }
}
