using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class TrashcanType
    {
        private TrashcanType(Guid id, TrashcanTypeName name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public TrashcanTypeName Name { get; }

        public TrashcanType Create(Guid id, TrashcanTypeName name)
        {
            return new TrashcanType(id, name);
        }
    }
}
