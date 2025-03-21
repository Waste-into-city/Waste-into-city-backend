using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Work
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private Work(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId, Guid workStatusesId, int coordinatesId)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedDatetime = startedDatetime;
            FinishDatetime = finishDatetime;
            WorkComplexityId = workComplexityId;
            WorkStatusesId = workStatusesId;
            CoordinatesId = coordinatesId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public DateTime FinishDatetime { get; }

        public Guid WorkComplexityId { get; }

        public Guid WorkStatusesId { get; }

        public int CoordinatesId { get; }

        public static Work Create(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId, Guid workStatusesId, int coordinatesId)
        {
            if (startedDatetime > finishDatetime)
            {
                throw new ValueOutOfRangeException<DateTime>("startDatetime", DateTime.MinValue, finishDatetime);
            }

            return new Work(id, title, description, startedDatetime, finishDatetime, workComplexityId, workStatusesId, coordinatesId);
        }
    }
}
