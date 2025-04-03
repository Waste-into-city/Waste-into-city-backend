using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Models;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;


namespace WasteIntoCity.Persistance.DefaultInitData
{
    public static class DefaultData
    {
        public static readonly Role[] Roles = new Role[]
        {
            Role.Create(RoleEnum.User, nameof(RoleEnum.User)),
            Role.Create(RoleEnum.Moderator, nameof(RoleEnum.Moderator)),
            Role.Create(RoleEnum.SuperAdmin, nameof(RoleEnum.SuperAdmin)),
        };

        public static readonly WorkComplexityType[] WorkComplexityTypes = new WorkComplexityType[]
        {
            WorkComplexityType.Create(WorkComplexityEnum.Easy, MeanText.Create(nameof(WorkComplexityEnum.Easy)), 1, 3, 3, 1, 1),
            WorkComplexityType.Create(WorkComplexityEnum.Medium, MeanText.Create(nameof(WorkComplexityEnum.Medium)), 5, 8, 5, 2, 2),
            WorkComplexityType.Create(WorkComplexityEnum.Hard, MeanText.Create(nameof(WorkComplexityEnum.Hard)), 10, 12, 24, 3, 3),
        };

        public static readonly WorkMarkType[] WorkMarkTypes = new WorkMarkType[]
        {
            WorkMarkType.Create(WorkMarkEnum.One, MeanText.Create(nameof(WorkMarkEnum.One)), -2),
            WorkMarkType.Create(WorkMarkEnum.Two, MeanText.Create(nameof(WorkMarkEnum.Two)), -1),
            WorkMarkType.Create(WorkMarkEnum.Three, MeanText.Create(nameof(WorkMarkEnum.Three)), 0),
            WorkMarkType.Create(WorkMarkEnum.Four, MeanText.Create(nameof(WorkMarkEnum.Four)), 1 ),
            WorkMarkType.Create(WorkMarkEnum.Five, MeanText.Create(nameof(WorkMarkEnum.Five)), 2 ),
        };

        public static readonly WorkReportStatusType[] WorkReportStatusTypes = new WorkReportStatusType[]
        {
            WorkReportStatusType.Create(WorkReportStatusEnum.Pending, MeanText.Create(nameof(WorkReportStatusEnum.Pending))),
            WorkReportStatusType.Create(WorkReportStatusEnum.Accepted, MeanText.Create(nameof(WorkReportStatusEnum.Accepted))),
            WorkReportStatusType.Create(WorkReportStatusEnum.Denied, MeanText.Create(nameof(WorkReportStatusEnum.Denied))),
        };

        public static readonly WorkStatusType[] WorkStatusTypes = new WorkStatusType[]
        {
            WorkStatusType.Create(WorkStatusEnum.Avaliable, MeanText.Create(nameof(WorkStatusEnum.Avaliable)), 0),
            WorkStatusType.Create(WorkStatusEnum.InProgress, MeanText.Create(nameof(WorkStatusEnum.InProgress)), 0),
            WorkStatusType.Create(WorkStatusEnum.FinishedSuccessfully, MeanText.Create(nameof(WorkStatusEnum.FinishedSuccessfully)), 0),
            WorkStatusType.Create(WorkStatusEnum.FinishedFailed, MeanText.Create(nameof(WorkStatusEnum.FinishedFailed)), 0),
            WorkStatusType.Create(WorkStatusEnum.Closed, MeanText.Create(nameof(WorkStatusEnum.Closed)), 0),
        };

        public static readonly User[] Users = new User[]
        {
            User.Create(
                Guid.NewGuid(),
                Nickname.Create("moderator"),
                Email.Create("moderator@gmail.com"),
                Password.Create(BCrypt.Net.BCrypt.EnhancedHashPassword("12345678")),
                0,
                new List<Role>
                {
                    Role.Create(RoleEnum.Moderator, Roles.SingleOrDefault(w => w.Id == RoleEnum.Moderator)!.Name)
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
                    Role.Create(RoleEnum.SuperAdmin, Roles.SingleOrDefault(w => w.Id == RoleEnum.SuperAdmin)!.Name)
                },
                0,
                false
            ),
        };
    }
}
