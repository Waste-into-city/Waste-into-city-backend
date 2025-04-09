using System.Text.RegularExpressions;
using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Options;

namespace WasteIntoCity.Core.ValueObjects
{
    public class ImageName : ValueObject
    {
        public const int VALUE_LENGTH_MIN = 1;
        public const int VALUE_LENGTH_MAX = 255;

        private static string? _pattern;

        private ImageName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static void Configure(ImageOptions options)
        {
            if (_pattern == null)
            {
                string extensionsPattern = string.Join("|", options.MimeTypes.Keys);
                string pattern = $@"^[\w\.\-\~]*({extensionsPattern})\z";

                _pattern = pattern;
            }
        }

        public static ImageName Create(string value)
        {
            if (_pattern == null)
                throw new NullValueServerException(nameof(ImageName), "ImageName is not configured. Call Configure(options) first.");

            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(ImageName).ToLower(), VALUE_LENGTH_MIN, VALUE_LENGTH_MAX);
            }

            if (!Regex.IsMatch(value, _pattern, RegexOptions.IgnoreCase))
            {
                throw new InvalidValueFormatException(nameof(ImageName).ToLower(), null);
            }

            return new ImageName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }

}
