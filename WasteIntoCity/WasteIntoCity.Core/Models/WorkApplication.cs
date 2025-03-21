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
            DateTime startedDatetime, Guid fromUsersId)
        {
            Id = id;
            Title = title;
            Description = description;
            WorkComplexityTypesId = workComplexityTypesId;
            CoordinatesId = coordinatesId;
            StartedDatetime = startedDatetime;
            FromUsersId = fromUsersId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public int WorkComplexityTypesId { get; }

        public Guid CoordinatesId { get; }

        public Guid FromUsersId { get; }

        public static WorkApplication Create(Guid id, Title title, Description description, int workComplexityTypesId, Guid coordinatesId,
            DateTime startedDatetime, Guid fromUsersId)
        {
            if (!Enum.IsDefined(typeof(WorkComplexityEnum), workComplexityTypesId))
            {
                throw new InvalidValueFormatException(nameof(WorkApplication),
                    $"The value should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<WorkComplexityEnum>()})");
            }

            return new WorkApplication(id, title, description, workComplexityTypesId, coordinatesId, startedDatetime, fromUsersId);
        }
    }
}
