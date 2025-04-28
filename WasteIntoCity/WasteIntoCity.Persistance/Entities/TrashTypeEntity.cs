namespace WasteIntoCity.Persistance.Entities
{
    public class TrashTypeEntity
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<WorkEntity> Works { get; set; } = [];

        public ICollection<WorkApplicationEntity> WorkApplications { get; set; } = [];
    }
}
