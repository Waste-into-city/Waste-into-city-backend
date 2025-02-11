namespace WasteIntoCity.Core.Models
{
    public class WorkEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartedDatetime { get; set; }

        public DateTime FinishDatetime { get; set; }

        public Guid WorkComplexityId { get; set; }

        public Guid WorkStatusesId { get; set; }

        public WorkComplexityEntity? WorkComplexity { get; set; }

        public WorkStatusTypeEntity? WorkStatus { get; set; }

        public List<WorkReportComplaintEntity> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReportEntity> WorkColleagueReports { get; set; } = [];

        public ICollection<UserEntity> Users { get; set; } = [];
    }
}
