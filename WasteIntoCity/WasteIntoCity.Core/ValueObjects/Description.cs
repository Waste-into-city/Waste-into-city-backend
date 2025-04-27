using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Description : ValueObject
    {
        public const int VALUE_LENGTH_MIN = 0;

        public const int VALUE_LENGTH_MAX = 1000;

        private Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Description Create(string value)
        {
            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException(29, nameof(Description).ToLower(), VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Description(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
