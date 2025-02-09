namespace WasteIntoCity.Core.Models
{
    public class Trashcan
    {
        public Guid Id { get; set; }

        public int Volume { get; set; }

        public Guid TrashcanPointsId { get; set; }

        public Guid TrashcanTypesId { get; set; }

        public Guid? AverageTrashcanOccupancyTypeId { get; set; }

        public TrashcanPoint? TrashcanPoint { get; set; }

        public TrashcanType? TrashcanType { get; set; } 

        public TrashcanOccupancyType? TrashcanOccupancyType { get; set; }

        public List<TrashcanPointReportEachMark> TrashcanPointReportEachMarkList { get; set; } = [];
    }
}
