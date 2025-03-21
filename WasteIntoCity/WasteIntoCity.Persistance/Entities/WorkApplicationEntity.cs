namespace WasteIntoCity.Persistance.Entities
{
    public class WorkApplicationEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartedDatetime { get; set; }

        public Guid WorkComplexitiesId { get; set; }

        public int CoordinatesId { get; }

        public WorkComplexityTypeEntity? WorkComplexityType { get; set; }

        public List<ImageEntity> Images { get; set; } = [];

        public CoordinatesEntity? Coordinates { get; set; }
    }
}
