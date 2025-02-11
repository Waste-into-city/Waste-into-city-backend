using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Description : ValueObject
    {
        private const int VALUE_LENGTH_MIN = 0;

        private const int VALUE_LENGTH_MAX = 1000;

        private Description(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Description Create(string value)
        {
            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException("value", VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Description(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
