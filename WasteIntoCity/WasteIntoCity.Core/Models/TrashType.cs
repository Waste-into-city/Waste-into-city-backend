using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class TrashType
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        public TrashType(TrashEnum id, MeanText name)
        {
            Id = id;
            Name = name;
        }

        public TrashEnum Id { get; }

        public MeanText Name { get; }

        public static TrashType Create(TrashEnum id, MeanText name)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(64, nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            return new TrashType(id, name);
        }
    }
}
