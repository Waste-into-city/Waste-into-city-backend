using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Extensions;
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

        private WorkApplication(Guid id, Title title, Description description, int workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId, int workReportStatusTypesId)
        {
            Id = id;
            Title = title;
            Description = description;
            WorkComplexityTypesId = workComplexityTypesId;
            CoordinatesId = coordinatesId;
            StartedDatetime = startedDatetime;
            FromUsersId = fromUsersId;
            WorkReportStatusTypesId = workReportStatusTypesId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public int WorkComplexityTypesId { get; }

        public Guid CoordinatesId { get; }

        public Guid FromUsersId { get; }

        public int WorkReportStatusTypesId { get; }

        public static WorkApplication Create(Guid id, Title title, Description description, int workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId, int workReportStatusTypesId)
        {
            if (!Enum.IsDefined(typeof(WorkComplexityEnum), workComplexityTypesId))
            {
                throw new InvalidValueFormatException(nameof(WorkApplication),
                    $"The workComplexityTypesId should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<WorkComplexityEnum>()})");
            }

            if (!Enum.IsDefined(typeof(WorkReportStatusEnum), workReportStatusTypesId))
            {
                throw new InvalidValueFormatException(nameof(WorkApplication),
                    $"The workReportStatusTypesId should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<WorkReportStatusEnum>()})");
            }

            return new WorkApplication(id, title, description, workComplexityTypesId, coordinatesId, startedDatetime, fromUsersId, workReportStatusTypesId);
        }
    }
}
