using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.ValueObjects
{
    public class MeanText : ValueObject
    {
        private MeanText(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static MeanText Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new NullOrWhiteSpaceException(nameof(MeanText).ToLower(), null);
            }

            return new MeanText(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
