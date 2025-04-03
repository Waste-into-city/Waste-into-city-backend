using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;
using WasteIntoCity.Persistance.Entities;


namespace WasteIntoCity.Persistance.DefaultInitData
{
    public static class DefaultData
    {
        public static readonly RoleEntity[] roleEntities = new RoleEntity[]
        {
            new RoleEntity { Id = (int) RoleEnum.User, Name = nameof(RoleEnum.User)},
            new RoleEntity { Id = (int) RoleEnum.Moderator, Name = nameof(RoleEnum.Moderator)},
            new RoleEntity { Id = (int) RoleEnum.SuperAdmin, Name = nameof(RoleEnum.SuperAdmin)},
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

        public static readonly WorkMarkTypeEntity[] workMarkTypeEntities = new WorkMarkTypeEntity[]
        {
            new WorkMarkTypeEntity{Id = (int) WorkMarkEnum.One, Name = nameof(WorkMarkEnum.One), AdditionRanking = -2},
            new WorkMarkTypeEntity{Id = (int) WorkMarkEnum.Two, Name = nameof(WorkMarkEnum.Two), AdditionRanking = -1},
            new WorkMarkTypeEntity{Id = (int) WorkMarkEnum.Three, Name = nameof(WorkMarkEnum.Three), AdditionRanking = 0 },
            new WorkMarkTypeEntity{Id = (int) WorkMarkEnum.Four, Name = nameof(WorkMarkEnum.Four), AdditionRanking = 1 },
            new WorkMarkTypeEntity{Id = (int) WorkMarkEnum.Five, Name = nameof(WorkMarkEnum.Five), AdditionRanking = 2 },
        };

        public static readonly WorkReportStatusTypeEntity[] workReportStatusTypeEntities = new WorkReportStatusTypeEntity[]
        {
            new WorkReportStatusTypeEntity{Id = (int) WorkReportStatusEnum.Pending, Name = nameof(WorkReportStatusEnum.Pending) },
            new WorkReportStatusTypeEntity{Id = (int) WorkReportStatusEnum.Accepted, Name = nameof(WorkReportStatusEnum.Accepted) },
            new WorkReportStatusTypeEntity{Id = (int) WorkReportStatusEnum.Denied, Name = nameof(WorkReportStatusEnum.Denied) },
        };

        public static readonly WorkStatusTypeEntity[] workStatusTypeEntities = new WorkStatusTypeEntity[]
        {
            new WorkStatusTypeEntity{Id = (int) WorkStatusEnum.Avaliable, Name = nameof(WorkStatusEnum.Avaliable) },
            new WorkStatusTypeEntity{Id = (int) WorkStatusEnum.InProgress, Name = nameof(WorkStatusEnum.InProgress) },
            new WorkStatusTypeEntity{Id = (int) WorkStatusEnum.FinishedSuccessfully, Name = nameof(WorkStatusEnum.FinishedSuccessfully) },
            new WorkStatusTypeEntity{Id = (int) WorkStatusEnum.FinishedFailed, Name = nameof(WorkStatusEnum.FinishedFailed) },
            new WorkStatusTypeEntity{Id = (int) WorkStatusEnum.Closed, Name = nameof(WorkStatusEnum.Closed) },
        };

        public static readonly User[] users = new User[]
        {
            User.Create(
                Guid.NewGuid(),
                Nickname.Create("moderator"),
                Email.Create("moderator@gmail.com"),
                Password.Create(BCrypt.Net.BCrypt.EnhancedHashPassword("12345678")),
                0,
                new List<Role>
                {
                    Role.Create(RoleEnum.Moderator, roleEntities.SingleOrDefault(w => w.Id == (int)RoleEnum.Moderator)!.Name)
                },
                0,
                false
            ),
            User.Create(
                Guid.NewGuid(),
                Nickname.Create("superadmin"),
                Email.Create("superadmin@gmail.com"),
                Password.Create(BCrypt.Net.BCrypt.EnhancedHashPassword("12345678")),
                0,
                new List<Role>
                {
                    Role.Create(RoleEnum.SuperAdmin, roleEntities.SingleOrDefault(w => w.Id == (int)RoleEnum.SuperAdmin)!.Name)
                },
                0,
                false
            ),
        };
    }
}
