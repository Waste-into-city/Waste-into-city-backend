using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaint
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private WorkReportComplaint(Guid id, Title title, Description description, DateTime startedDatime, Guid worksId,
            Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, User? fromUser, List<ImageName>? imageNames)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedDatime = startedDatime;
            WorksId = worksId;
            FromUsersId = fromUsersId;
            WorkReportStatusTypesId = workReportStatusTypesId;
            FromUser = fromUser;
            ImageNames = imageNames;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatime { get; }

        public Guid WorksId { get; }

        public Guid FromUsersId { get; }

        public WorkReportStatusEnum WorkReportStatusTypesId { get; }

        public User? FromUser { get; }

        public List<ImageName>? ImageNames { get; }

        public static WorkReportComplaint Create(Guid id, Title title, Description description, DateTime startedDatetime,
            Guid worksId, Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, User? fromUser,
            List<ImageName>? imageNames)
        {
            return new WorkReportComplaint(id, title, description, startedDatetime, worksId, fromUsersId, workReportStatusTypesId, fromUser,
                imageNames);
        }
    }
}