using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkStatusType
    {
        public const int MULTIPLIER_RANKING_MIN = -9999;

        public const int MULTIPLIER_RANKING_MAX = 9999;

        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private WorkStatusType(Guid id, MeanText name, int multiplierRanking)
        {
            Id = id;
            Name = name;
            MultiplierRanking = multiplierRanking;
        }

        public Guid Id { get; }

        public MeanText Name { get; }

        public int MultiplierRanking { get; }

        public static WorkStatusType Create(Guid id, MeanText name, int multiplierRanking)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (multiplierRanking is < MULTIPLIER_RANKING_MIN or > MULTIPLIER_RANKING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(multiplierRanking), MULTIPLIER_RANKING_MIN, MULTIPLIER_RANKING_MAX);
            }

            return new WorkStatusType(id, name, multiplierRanking);
        }
    }
}
