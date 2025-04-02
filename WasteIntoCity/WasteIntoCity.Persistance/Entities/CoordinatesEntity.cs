namespace WasteIntoCity.Persistance.Entities
{
    public class CoordinatesEntity
    {
        public Guid Id { get; set; }

        public string Lat { get; set; } = string.Empty;

        public string Lng { get; set; } = string.Empty;

        public List<TrashcanEntity> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEntity> TrashcanPointReports { get; set; } = [];

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];

        public List<WorkEntity> Works { get; set; } = [];
    }
}
