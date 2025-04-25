using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

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
                throw new NullOrWhiteSpaceException(nameof(MeanText).ToLower(), null, 34);
            }

            return new MeanText(value);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
