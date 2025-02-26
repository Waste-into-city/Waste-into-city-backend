namespace WasteIntoCity.Core.Models
{
    public class TrashcanEntity
    {
        public Guid Id { get; set; }

        public int Volume { get; set; }

        public Guid TrashcanPointsId { get; set; }

        public Guid TrashcanTypesId { get; set; }

        public Guid? AverageTrashcanOccupancyTypeId { get; set; }

        public TrashcanPointEntity? TrashcanPoint { get; set; }

        public TrashcanTypeEntity? TrashcanType { get; set; }

        public TrashcanOccupancyTypeEntity? AverageTrashcanOccupancyType { get; set; }

        public TrashcanPointReportEachMarkEntity? TrashcanPointReportEachMark { get; set; }
    }
}
