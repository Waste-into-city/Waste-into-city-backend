using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class TrashcanTypeName
    {
        public static readonly TrashcanTypeName Mixed = new(nameof(Mixed));

        public static readonly TrashcanTypeName Plastic = new(nameof(Plastic));

        public static readonly TrashcanTypeName Glass = new(nameof(Glass));

        public static readonly TrashcanTypeName Electronic = new(nameof(Electronic));

        public static readonly TrashcanTypeName Battaries = new(nameof(Battaries));

        private static readonly TrashcanTypeName[] _allValidValues = [Mixed, Plastic, Glass, Electronic, Battaries];

        private TrashcanTypeName(string value)
        {
            Value = value;
        }

        private static string ToStringAllValidValuesThrowComma()
        {
            return string.Join(", ", _allValidValues.Select(v => v.Value));
        }

        public string Value { get; }

        public static TrashcanTypeName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrWhiteSpaceException(nameof(TrashcanTypeName).ToLower(), null);
            }

            string convertedValue = value.Trim().ToLower();

            if (!_allValidValues.Any(t => t.Value.ToLower() == convertedValue))
            {
                throw new InvalidValueFormatException(nameof(TrashcanTypeName).ToLower(), $"The value should be enum ({ToStringAllValidValuesThrowComma()})");
            }

            return new TrashcanTypeName(value);
        }
    }
}
