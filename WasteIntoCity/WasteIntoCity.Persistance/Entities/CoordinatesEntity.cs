namespace WasteIntoCity.Persistance.Entities
{
    public class CoordinatesEntity
    {
        public Guid Id { get; set; }

        public decimal Lat { get; set; }

        public decimal Lng { get; set; }

        public List<TrashcanEntity> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEntity> TrashcanPointReports { get; set; } = [];

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];

        public List<WorkEntity> Works { get; set; } = [];
    }
}
