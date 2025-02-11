using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Password : ValueObject
    {
        private const int PASSWORD_LENGTH_MIN = 8;

        private const int PASSWORD_LENGTH_MAX = 255;

        private Password(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Password Create(string value)
        {
            if (value.Length is < PASSWORD_LENGTH_MIN or > PASSWORD_LENGTH_MAX)
            {
                throw new InvalidLengthException("value", PASSWORD_LENGTH_MIN, PASSWORD_LENGTH_MAX);
            }

            return new Password(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
