namespace WasteIntoCity.Persistance.Entities
{
    public class WorkMarkTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AdditionRanking { get; set; }

        public List<WorkColleagueReportEntity> WorkColleagueReports { get; set; } = [];
    }
}
