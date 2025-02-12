using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class TrashcanOccupancyType
    {
        private TrashcanOccupancyType(Guid id, TrashcanOccupancyTypeName name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public TrashcanOccupancyTypeName Name { get; }

        public TrashcanOccupancyType Create(Guid id, TrashcanOccupancyTypeName name)
        {
            return new TrashcanOccupancyType(id, name);
        }
    }
}
