namespace WasteIntoCity.Core.Models
{
    public class TrashcansEntity
    {
        public Guid Id { get; }

        public string TrashcanPointsId { get; } = string.Empty;

        public string TrashcanTypesId { get; } = string.Empty;

        public int Volume { get; }

        public Guid? AverageTrashcanOccupancyTypeId { get; }
    }
}
