namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportEachMarkEntity
    {
        public Guid Id { get; set; }

        public Guid TrashcanPointReportsId { get; set; }

        public Guid TrashcansId { get; set; }

        public Guid TrashcanOccupancyTypesId { get; set; }

        public TrashcanEntity? Trashcan {  get; set; }

        public TrashcanPointReportEntity? TrashcanPointReport { get; set; }

        public TrashcanOccupancyTypeEntity? TrashcanOccupancyType { get; set; }
    }
}
