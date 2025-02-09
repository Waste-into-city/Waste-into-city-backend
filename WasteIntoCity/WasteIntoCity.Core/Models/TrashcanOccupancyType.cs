namespace WasteIntoCity.Core.Models
{
    public class TrashcanOccupancyType
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Trashcan> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEachMark> TrashcanPointReportEachMarkList { get; set; } = [];
    }
}
