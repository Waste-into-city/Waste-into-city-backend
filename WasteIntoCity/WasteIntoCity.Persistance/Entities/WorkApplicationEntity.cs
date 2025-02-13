namespace WasteIntoCity.Core.Models
{
    public class WorkApplicationEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid WorkComplexitiesId { get; set; }

        public WorkComplexityTypeEntity? WorkComplexity { get; set; }

        public List<ImageEntity> Images { get; set; } = [];
    }
}
