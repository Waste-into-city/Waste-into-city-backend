using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Work
    {
        private const int NAME_LENGTH_MIN = 1;

        private const int NAME_LENGTH_MAX = 100;

        private const int DESCRIPTION_LENGTH_MIN = 0;

        private const int DESCRIPTION_LENGTH_MAX = 1000;

        private Work(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId, Guid workStatusesId)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedDatetime = startedDatetime;
            FinishDatetime = finishDatetime;
            WorkComplexityId = workComplexityId;
            WorkStatusesId = workStatusesId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime StartedDatetime { get; }

        public DateTime FinishDatetime { get; }

        public Guid WorkComplexityId { get; }

        public Guid WorkStatusesId { get; }

        public Work Create(Guid id, Title title, Description description, DateTime startedDatetime, DateTime finishDatetime, Guid workComplexityId, Guid workStatusesId)
        {
            if (title.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new ValueOutOfRangeException<int>("name", NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (description.Value.Length is < DESCRIPTION_LENGTH_MIN or > DESCRIPTION_LENGTH_MAX)
            {
                throw new ValueOutOfRangeException<int>("description", DESCRIPTION_LENGTH_MIN, DESCRIPTION_LENGTH_MAX);
            }

            return new Work(id, title, description, startedDatetime, finishDatetime, workComplexityId, workStatusesId);
        }
    }
}
