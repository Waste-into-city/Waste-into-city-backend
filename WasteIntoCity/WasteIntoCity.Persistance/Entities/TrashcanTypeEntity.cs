namespace WasteIntoCity.Core.Models
{
    public class TrashcanTypeEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<TrashcanEntity> Trashcans { get; set; } = [];
    }
}
