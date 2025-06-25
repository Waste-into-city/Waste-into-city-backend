using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class ScoreSettingsType
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        public const int VALUE_MIN = -9999;

        public const int VALUE_MAX = 9999;

        private ScoreSettingsType(ScoreSettingsEnum id, MeanText name, int value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        public ScoreSettingsEnum Id { get; set; }

        public MeanText Name { get; set; }

        public int Value { get; set; }

        public static ScoreSettingsType Create(ScoreSettingsEnum id, MeanText name, int value)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(22, $"{nameof(name)} of {nameof(ScoreSettingsType)}", NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            if (value is < VALUE_MIN or > VALUE_MAX)
            {
                throw new ValueOutOfRangeException<int>($"{nameof(value)} of {nameof(ScoreSettingsType)}", VALUE_MIN, VALUE_MAX, 52);
            }

            return new ScoreSettingsType(id, name, value);
        }
    }
}
