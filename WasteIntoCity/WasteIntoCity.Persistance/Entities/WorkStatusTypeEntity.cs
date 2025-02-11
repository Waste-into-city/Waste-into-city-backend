namespace WasteIntoCity.Core.Models
{
    public class WorkStatusTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<WorkEntity> Works { get; set; } = [];
    }
}
