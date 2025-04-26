using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkMarkType
    {
        public const int RATING_CHANGING_MIN = -9999;

        public const int RATING_CHANGING_MAX = 9999;

        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private WorkMarkType(WorkMarkEnum id, MeanText name, int additionRanking)
        {
            Id = id;
            Name = name;
            AdditionRanking = additionRanking;
        }

        public WorkMarkEnum Id { get; }

        public MeanText Name { get; }

        public int AdditionRanking { get; }

        public static WorkMarkType Create(WorkMarkEnum id, MeanText name, int additionRanking)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(26, nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (additionRanking is < RATING_CHANGING_MIN or > RATING_CHANGING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(additionRanking), RATING_CHANGING_MIN, RATING_CHANGING_MAX, 63);
            }

            return new WorkMarkType(id, name, additionRanking);
        }
    }
}
