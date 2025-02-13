using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkStatusType
    {
        private const int RATING_CHANGING_MIN = -9999;

        private const int RATING_CHANGING_MAX = 9999;

        private const int NAME_LENGTH_MIN = 1;

        private const int NAME_LENGTH_MAX = 100;

        private WorkStatusType(Guid id, MeanText name, int multiplierRanking)
        {
            Id = id;
            Name = name;
            MultiplierRanking = multiplierRanking;
        }

        public Guid Id { get; }

        public MeanText Name { get; }

        public int MultiplierRanking { get; }

        public WorkStatusType Create(Guid id, MeanText name, int multiplierRanking)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (multiplierRanking is < RATING_CHANGING_MIN or > RATING_CHANGING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(multiplierRanking), RATING_CHANGING_MIN, RATING_CHANGING_MAX);
            }

            return new WorkStatusType(id, name, multiplierRanking);
        }
    }
}
