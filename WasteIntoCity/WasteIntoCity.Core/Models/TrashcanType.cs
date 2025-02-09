namespace WasteIntoCity.Core.Models
{
    public class TrashcanType
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Trashcan> Trashcans { get; set; } = [];
    }
}
