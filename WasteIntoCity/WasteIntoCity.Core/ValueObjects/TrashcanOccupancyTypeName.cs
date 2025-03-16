using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class TrashcanOccupancyTypeName
    {
        public static readonly TrashcanOccupancyTypeName Empty = new(nameof(Empty));

        public static readonly TrashcanOccupancyTypeName Sparse = new(nameof(Sparse));

        public static readonly TrashcanOccupancyTypeName Medium = new(nameof(Medium));

        public static readonly TrashcanOccupancyTypeName AlmostFull = new(nameof(AlmostFull));

        private static readonly TrashcanOccupancyTypeName[] _allValidValues = [Empty, Sparse, Medium, AlmostFull];

        private TrashcanOccupancyTypeName(string value)
        {
            Value = value;
        }

        private static string ToStringAllValidValuesThrowComma()
        {
            return string.Join(", ", _allValidValues.Select(v => v.Value));
        }

        public string Value { get; }

        public static TrashcanOccupancyTypeName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrWhiteSpaceException(nameof(TrashcanOccupancyTypeName).ToLower(), null);
            }

            string convertedValue = value.Trim().ToLower();

            if (!_allValidValues.Any(t => t.Value.ToLower() == convertedValue))
            {
                throw new InvalidValueFormatException(nameof(TrashcanOccupancyTypeName).ToLower(), $"The value should be enum ({ToStringAllValidValuesThrowComma()})");
            }

            return new TrashcanOccupancyTypeName(value);
        }
    }
}
