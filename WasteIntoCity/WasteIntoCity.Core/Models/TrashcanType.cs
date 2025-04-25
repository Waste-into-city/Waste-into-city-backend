using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class TrashcanType
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private TrashcanType(Guid id, MeanText name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public MeanText Name { get; }

        public static TrashcanType Create(Guid id, MeanText name)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(14, nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            return new TrashcanType(id, name);
        }
    }
}
