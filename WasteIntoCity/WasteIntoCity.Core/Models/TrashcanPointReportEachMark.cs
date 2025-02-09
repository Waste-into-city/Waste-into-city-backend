namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportEachMark
    {
        public Guid Id { get; set; }

        public Guid TrashcanPointReportsId { get; set; }

        public Guid TrashcansId { get; set; }

        public Guid TrashcanOccupancyTypesId { get; set; }

        public Trashcan? Trashcan {  get; set; }

        public TrashcanPointReport? TrashcanPointReport { get; set; }

        public TrashcanOccupancyType? TrashcanOccupancyType { get; set; }
    }
}
