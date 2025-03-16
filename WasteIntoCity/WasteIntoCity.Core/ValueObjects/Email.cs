using System.Text.RegularExpressions;
using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class Email : ValueObject
    {
        private const string VALUE_PATTERN = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
            + "@"
            + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))\z";

        private const int VALUE_LENGTH_MIN = 6;

        private const int VALUE_LENGTH_MAX = 45;

        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Email Create(string value)
        {
            if (!Regex.IsMatch(value, VALUE_PATTERN))
            {
                throw new InvalidValueFormatException(nameof(Email).ToLower(), null);
            }

            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(Email).ToLower(), VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new Email(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
