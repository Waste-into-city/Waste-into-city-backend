namespace WasteIntoCity.Persistance.Entities
{
    public class WorkEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartedDatetime { get; set; }

        public DateTime FinishDatetime { get; set; }

        public Guid WorkComplexityTypesId { get; set; }

        public Guid WorkStatusTypesId { get; set; }

        public WorkComplexityTypeEntity? WorkComplexityType { get; set; }

        public WorkStatusTypeEntity? WorkStatusType { get; set; }

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReportEntity> WorkColleagueReports { get; set; } = [];

        public ICollection<UserEntity> Users { get; set; } = [];
    }
}
