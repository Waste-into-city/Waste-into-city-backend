namespace WasteIntoCity.Core.Models
{
    public class WorkComplexityTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ParticipantsMin { get; set; }

        public int ParticipantsMax { get; set; }

        public int DurationHours { get; set; }

        public int MultiplierRanking { get; set; }

        public int RadiusOnMap { get; set; }

        public List<WorkApplicationEntity> WorkApplications { get; set; } = [];

        public List<WorkEntity> Works { get; set; } = [];
    }
}
