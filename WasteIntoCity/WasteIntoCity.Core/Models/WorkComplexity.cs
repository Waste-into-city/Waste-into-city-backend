using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkComplexity
    {
        private const int NAME_LENGTH_MIN = 1;

        private const int NAME_LENGTH_MAX = 45;

        private const int PARTICIPANTS_MIN_VALUE_MIN = 1;

        private const int PARTICIPANTS_MAX_VALUE_MAX = 9999;

        private const int DURATION_HOURS_MIN = 0;

        private const int DURATION_HOURS_MAX = 9999;

        private WorkComplexity(Guid id, MeanText name, int participantsMin, int participantsMax, int durationHours)
        {
            Id = id;
            Name = name;
            ParticipantsMin = participantsMin;
            ParticipantsMax = participantsMax;
            DurationHours = durationHours;
        }

        public Guid Id { get; }

        public MeanText Name { get; }

        public int ParticipantsMin { get; }

        public int ParticipantsMax { get; }

        public int DurationHours { get; }

        public WorkComplexity Create(Guid id, MeanText name, int participantsMin, int participantsMax, int durationHours)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException("name", NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (participantsMin < PARTICIPANTS_MIN_VALUE_MIN || participantsMin > participantsMax)
            {
                throw new ValueOutOfRangeException<int>("participantsMin", PARTICIPANTS_MIN_VALUE_MIN, participantsMax);
            }

            if (participantsMax > PARTICIPANTS_MAX_VALUE_MAX)
            {
                throw new ValueOutOfRangeException<int>("participantsMax", participantsMin, PARTICIPANTS_MAX_VALUE_MAX);
            }

            if (durationHours is < DURATION_HOURS_MIN or > DURATION_HOURS_MAX)
            {
                throw new ValueOutOfRangeException<int>("durationHours", DURATION_HOURS_MIN, DURATION_HOURS_MAX);
            }

            return new WorkComplexity(id, name, participantsMin, participantsMax, durationHours);
        }
    }
}
