namespace WasteIntoCity.Core.Models
{
    public class WorkStatus
    {
        public Guid Id { get; set; }

        public string Name { get; } = string.Empty;

        public List<Work> Works { get; set; } = [];
    }
}
