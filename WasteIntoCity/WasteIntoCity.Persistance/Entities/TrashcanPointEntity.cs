namespace WasteIntoCity.Persistance.Entities
{
    public class TrashcanPointEntity
    {
        public Guid Id { get; set; }

        public string Lat { get; set; } = string.Empty;

        public string Lng { get; set; } = string.Empty;

        public List<TrashcanEntity> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEntity> TrashcanPointReports { get; set; } = [];
    }
}
