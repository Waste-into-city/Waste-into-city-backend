using System.Text.RegularExpressions;
using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class ImageName : ValueObject
    {
        private const string VALUE_PATTERN = @"^[\w\.\-\~]*[\.](jpg|jpeg|png)\z";

        private const int VALUE_LENGTH_MIN = 1;

        private const int VALUE_LENGTH_MAX = 255;

        private ImageName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static ImageName Create(string value)
        {
            if (Regex.IsMatch(value, VALUE_PATTERN))
            {
                throw new InvalidValueFormatException(nameof(ImageName).ToLower(), null);
            }

            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(ImageName).ToLower(), VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            return new ImageName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
