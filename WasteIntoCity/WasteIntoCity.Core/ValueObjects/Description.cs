using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Title : ValueObject
    {
        public const int VALUE_LENGTH_MIN = 1;

        public const int VALUE_LENGTH_MAX = 100;

        private Title(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Title Create(string value)
        {
            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException("value", VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Title(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
