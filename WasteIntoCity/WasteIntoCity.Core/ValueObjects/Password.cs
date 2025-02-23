using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Password : ValueObject
    {
        public const int VALUE_LENGTH_MIN = 8;

        public const int VALUE_LENGTH_MAX = 255;

        private Password(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Password Create(string value)
        {
            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(value), VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Password(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
