using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class WorkMarkTypeName
    {
        public static readonly WorkMarkTypeName One = new(nameof(One));

        public static readonly WorkMarkTypeName Two = new(nameof(Two));

        public static readonly WorkMarkTypeName Three = new(nameof(Three));

        public static readonly WorkMarkTypeName Four = new(nameof(Four));

        public static readonly WorkMarkTypeName Five = new(nameof(Five));

        private static readonly WorkMarkTypeName[] _allValidValues = [One, Two, Three, Four, Five];

        private WorkMarkTypeName(string value)
        {
            Value = value;
        }

        private string ToStringAllValidValuesThrowComma()
        {
            return string.Join(", ", _allValidValues.Select(v => v.Value));
        }

        public string Value { get; }

        public WorkMarkTypeName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrWhiteSpaceException(nameof(value), null);
            }

            string convertedValue = value.Trim().ToLower();

            if (!_allValidValues.Any(t => t.Value.ToLower() == convertedValue))
            {
                throw new InvalidValueFormatException(nameof(value), $"The value should be enum ({ToStringAllValidValuesThrowComma()})");
            }

            return new WorkMarkTypeName(value);
        }
    }
}
