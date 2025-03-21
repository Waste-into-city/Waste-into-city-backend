namespace WasteIntoCity.Persistance.Entities
{
    public class WorkReportComplaintStatusTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];
    }
}
