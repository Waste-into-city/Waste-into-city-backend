namespace WasteIntoCity.Core.Models
{
    public class TrashcanOccupancyTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<TrashcanEntity> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEachMarkEntity> TrashcanPointReportEachMarkList { get; set; } = [];
    }
}
