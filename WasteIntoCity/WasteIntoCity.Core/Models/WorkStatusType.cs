using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkStatusType
    {
        public const int MULTIPLIER_RANKING_MIN = -9999;

        public const int MULTIPLIER_RANKING_MAX = 9999;

        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private WorkStatusType(WorkStatusEnum id, MeanText name, int addingRanking)
        {
            Id = id;
            Name = name;
            AddingRanking = addingRanking;
        }

        public WorkStatusEnum Id { get; }

        public MeanText Name { get; }

        public int AddingRanking { get; }

        public static WorkStatusType Create(WorkStatusEnum id, MeanText name, int addingRanking)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(28, nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (addingRanking is < MULTIPLIER_RANKING_MIN or > MULTIPLIER_RANKING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(addingRanking), MULTIPLIER_RANKING_MIN, MULTIPLIER_RANKING_MAX, 67);
            }

            return new WorkStatusType(id, name, addingRanking);
        }
    }
}