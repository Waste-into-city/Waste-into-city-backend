namespace WasteIntoCity.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Nickname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public List<WorkReportResult> WorkReportResults { get; set; } = [];

        public ICollection<Work> Works { get; set; } = [];

        public List<WorkReportComplaint> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReport> WorkColleagueReports { get; set; } = [];

        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
