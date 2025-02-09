namespace WasteIntoCity.Core.ValueObjects
{
    public class ImageName : ValueObject
    {
        private ImageName(string value)
        {
            Value = value;
        }

        public const int VALUE_LENGTH_MIN = 20;

        public const int VALUE_LENGTH_MAX = 255;

        public string Value { get; }

        public ImageName Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Image name cannot be empty, null or white spaces.", "name");
            }

            if (value.Length is < VALUE_LENGTH_MIN or > VALUE_LENGTH_MAX)
            {
                throw new ArgumentOutOfRangeException("value", value, $"Image name length not in range [{VALUE_LENGTH_MIN,VALUE_LENGTH_MAX}].");
            }

            return new ImageName(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
