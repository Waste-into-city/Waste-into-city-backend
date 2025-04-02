using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Work
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private Work(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, WorkComplexityEnum workComplexityTypesId,
            WorkStatusEnum workStatusTypesId, Guid coordinatesId, List<User>? participants, Coordinates? coordinates)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedDatetime = startedDatetime;
            FinishDatetime = finishDatetime;
            WorkComplexityTypesId = workComplexityTypesId;
            WorkStatusTypesId = workStatusTypesId;
            CoordinatesId = coordinatesId;
            Participants = participants;
            Coordinates = coordinates;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public DateTime FinishDatetime { get; }

        public WorkComplexityEnum WorkComplexityTypesId { get; }

        public WorkStatusEnum WorkStatusTypesId { get; }

        public Guid CoordinatesId { get; }

        public List<User>? Participants { get; }

        public Coordinates? Coordinates { get; }

        public static Work Create(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, WorkComplexityEnum workComplexityTypesId,
            WorkStatusEnum workStatusTypesId, Guid coordinatesId, List<User>? participants, Coordinates? coordinates)
        {
            if (startedDatetime > finishDatetime)
            {
                throw new ValueOutOfRangeException<DateTime>("startDatetime", DateTime.MinValue, finishDatetime);
            }

            return new Work(id, title, description, startedDatetime, finishDatetime, workComplexityTypesId, workStatusTypesId, coordinatesId, participants, coordinates);
        }
    }
}
