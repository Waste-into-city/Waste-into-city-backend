using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class WorkStatusTypeName
    {
        public static readonly WorkStatusTypeName Pending = new(nameof(Pending));

        public static readonly WorkStatusTypeName Active = new(nameof(Active));

        public static readonly WorkStatusTypeName InProgress = new(nameof(InProgress));

        public static readonly WorkStatusTypeName Successful = new(nameof(Successful));

        public static readonly WorkStatusTypeName Unknown = new(nameof(Unknown));

        private static readonly WorkStatusTypeName[] _allValidValues = [Pending, Active, InProgress, Successful, Unknown];

        private WorkStatusTypeName(string value)
        {
            Value = value;
        }

        private string ToStringAllValidValuesThrowComma()
        {
            return string.Join(", ", _allValidValues.Select(v => v.Value));
        }

        public string Value { get; }

        public WorkStatusTypeName Create(string value)
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

            return new WorkStatusTypeName(value);
        }
    }
}
