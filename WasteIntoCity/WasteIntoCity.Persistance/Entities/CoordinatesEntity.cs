namespace WasteIntoCity.Persistance.Entities
{
    public class CoordinatesEntity
    {
        public Guid Id { get; set; }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public List<TrashcanEntity> Trashcans { get; set; } = [];

        public List<TrashcanPointReportEntity> TrashcanPointReports { get; set; } = [];

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];

        public List<WorkEntity> Works { get; set; } = [];
    }
}
