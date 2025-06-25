using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkApplication
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private WorkApplication(Guid id, Title title, Description description, WorkComplexityEnum workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, List<TrashEnum>? trashTypesIds,
            List<ImageName>? imageNames, User? fromUser, Coordinates? coordinates)
        {
            Id = id;
            Title = title;
            Description = description;
            WorkComplexityTypesId = workComplexityTypesId;
            CoordinatesId = coordinatesId;
            StartedDatetime = startedDatetime;
            FromUsersId = fromUsersId;
            WorkReportStatusTypesId = workReportStatusTypesId;
            TrashTypesIds = trashTypesIds;
            ImageNames = imageNames;
            FromUser = fromUser;
            Coordinates = coordinates;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public WorkComplexityEnum WorkComplexityTypesId { get; }

        public Guid CoordinatesId { get; }

        public Guid FromUsersId { get; }

        public WorkReportStatusEnum WorkReportStatusTypesId { get; }

        public List<TrashEnum>? TrashTypesIds { get; }

        public List<ImageName>? ImageNames { get; }

        public User? FromUser { get; }

        public Coordinates? Coordinates { get; }

        public static WorkApplication Create(Guid id, Title title, Description description, WorkComplexityEnum workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, List<TrashEnum>? trashTypesIds,
            List<ImageName>? imageNames, User? fromUser, Coordinates? coordinates)
        {
            return new WorkApplication(id, title, description, workComplexityTypesId, coordinatesId, startedDatetime, fromUsersId,
                workReportStatusTypesId, trashTypesIds, imageNames, fromUser, coordinates);
        }
    }
}
