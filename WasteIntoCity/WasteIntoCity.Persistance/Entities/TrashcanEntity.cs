namespace WasteIntoCity.Persistance.Entities
{
    public class TrashcanEntity
    {
        public Guid Id { get; set; }

        public int Volume { get; set; }

        public int CoordinatesId { get; set; }

        public Guid TrashcanTypesId { get; set; }

        public Guid? AverageTrashcanOccupancyTypeId { get; set; }

        public CoordinatesEntity? Coordinates { get; set; }

        public TrashcanTypeEntity? TrashcanType { get; set; }

        public TrashcanOccupancyTypeEntity? AverageTrashcanOccupancyType { get; set; }

        public TrashcanPointReportEachMarkEntity? TrashcanPointReportEachMark { get; set; }
    }
}
