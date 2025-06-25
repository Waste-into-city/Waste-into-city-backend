namespace WasteIntoCity.Persistance.Entities
{
    public class WorkReportStatusTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];
    }
}
