namespace WasteIntoCity.Persistance.Entities
{
    public class ImageEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public Guid? WorkApplicationsId { get; set; }

        public Guid? WorkReportComplaintsId { get; set; }

        public Guid? WorkReportResultsId { get; set; }

        public WorkApplicationEntity? WorkApplication { get; set; }

        public WorkReportComplaintEntity? WorkReportComplaint { get; set; }

        public WorkReportResultEntity? WorkReportResult { get; set; }
    }
}
