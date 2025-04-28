using WasteIntoCity.Core.Types;
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
            DateTime startedDatetime, Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, TrashType[]? trashTypes)
        {
            Id = id;
            Title = title;
            Description = description;
            WorkComplexityTypesId = workComplexityTypesId;
            CoordinatesId = coordinatesId;
            StartedDatetime = startedDatetime;
            FromUsersId = fromUsersId;
            WorkReportStatusTypesId = workReportStatusTypesId;
            TrashTypes = trashTypes;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public WorkComplexityEnum WorkComplexityTypesId { get; }

        public Guid CoordinatesId { get; }

        public Guid FromUsersId { get; }

        public WorkReportStatusEnum WorkReportStatusTypesId { get; }

        public TrashType[]? TrashTypes { get; }

        public static WorkApplication Create(Guid id, Title title, Description description, WorkComplexityEnum workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId, WorkReportStatusEnum workReportStatusTypesId, TrashType[]? trashTypes)
        {
            return new WorkApplication(id, title, description, workComplexityTypesId, coordinatesId, startedDatetime, fromUsersId, workReportStatusTypesId, trashTypes);
        }
    }
}
