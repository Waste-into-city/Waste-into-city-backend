using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkComplexityType
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        public const int PARTICIPANTS_MIN_VALUE_MIN = 1;

        public const int PARTICIPANTS_MAX_VALUE_MAX = 9999;

        public const int DURATION_HOURS_MIN = 0;

        public const int DURATION_HOURS_MAX = 9999;

        public const int RATING_CHANGING_MIN = -9999;

        public const int RATING_CHANGING_MAX = 9999;

        public const int RADIUS_ON_MAP_MIN = 0;

        public const int RADIUS_ON_MAP_MAX = 100;


        private WorkComplexityType(WorkComplexityEnum id, MeanText name, int participantsMin, int participantsMax, int durationHours, int multiplierRanking, int radiusOnMap)
        {
            Id = id;
            Name = name;
            ParticipantsMin = participantsMin;
            ParticipantsMax = participantsMax;
            DurationHours = durationHours;
            MultiplierRanking = multiplierRanking;
            RadiusOnMap = radiusOnMap;
        }

        public WorkComplexityEnum Id { get; }

        public MeanText Name { get; }

        public int ParticipantsMin { get; }

        public int ParticipantsMax { get; }

        public int DurationHours { get; }

        public int MultiplierRanking { get; }

        public int RadiusOnMap { get; }

        public static WorkComplexityType Create(WorkComplexityEnum id, MeanText name, int participantsMin, int participantsMax, int durationHours, int multiplierRanking, int radiusOnMap)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (participantsMin < PARTICIPANTS_MIN_VALUE_MIN || participantsMin > participantsMax)
            {
                throw new ValueOutOfRangeException<int>(nameof(participantsMin), PARTICIPANTS_MIN_VALUE_MIN, participantsMax);
            }

            if (participantsMax > PARTICIPANTS_MAX_VALUE_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(participantsMax), participantsMin, PARTICIPANTS_MAX_VALUE_MAX);
            }

            if (durationHours is < DURATION_HOURS_MIN or > DURATION_HOURS_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(durationHours), DURATION_HOURS_MIN, DURATION_HOURS_MAX);
            }

            if (multiplierRanking is < RATING_CHANGING_MIN or > RATING_CHANGING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(multiplierRanking), RATING_CHANGING_MIN, RATING_CHANGING_MAX);
            }

            if (radiusOnMap is < RADIUS_ON_MAP_MIN or > RADIUS_ON_MAP_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(radiusOnMap), RADIUS_ON_MAP_MIN, RADIUS_ON_MAP_MAX);
            }

            return new WorkComplexityType(id, name, participantsMin, participantsMax, durationHours, multiplierRanking, radiusOnMap);
        }
    }
}
