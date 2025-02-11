using System.Text.RegularExpressions;
using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Nickname : ValueObject
    {
        private const string VALUE_PATTERN = @"^[\w]+\z";

        private const int VALUE_LENGTH_MIN = 1;

        private const int VALUE_LENGTH_MAX = 45;


        private Nickname(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public Nickname Create(string value)
        {
            if (Regex.IsMatch(value, VALUE_PATTERN))
            {
                throw new InvalidValueFormatException("value", null);
            }

            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException("value", VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Nickname(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
