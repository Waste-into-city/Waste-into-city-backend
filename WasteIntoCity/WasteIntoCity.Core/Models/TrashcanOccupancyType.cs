using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class TrashcanOccupancyType
    {
        public const int VALUE_MIN = 0;

        public const int VALUE_MAX = 100;

        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private TrashcanOccupancyType(Guid id, MeanText name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public Guid Id { get; }

        public MeanText Name { get; }

        public int Value { get; }

        public static TrashcanOccupancyType Create(Guid id, MeanText name, int value)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (value is < VALUE_MIN or > VALUE_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(value), VALUE_MIN, VALUE_MAX);
            }

            return new TrashcanOccupancyType(id, name, value);
        }
    }
}
