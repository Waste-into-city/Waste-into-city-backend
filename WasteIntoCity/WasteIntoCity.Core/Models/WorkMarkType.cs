namespace WasteIntoCity.Core.Models
{
    public class WorkMarkType
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<WorkColleagueReport> WorkColleagueReports { get; set; } = [];
    }
}
