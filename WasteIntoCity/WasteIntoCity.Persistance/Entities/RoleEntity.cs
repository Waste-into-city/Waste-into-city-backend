namespace WasteIntoCity.Core.Models
{
    public class RoleEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<UserEntity> Users { get; set; } = [];
    }
}
