namespace WasteIntoCity.Persistance.Entities
{
    public class WorkStatusTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int AddingRanking { get; set; }

        public List<WorkEntity> Works { get; set; } = [];

        public List<WorkReportResultEntity> WorkReportResults { get; set; } = [];
    }
}
