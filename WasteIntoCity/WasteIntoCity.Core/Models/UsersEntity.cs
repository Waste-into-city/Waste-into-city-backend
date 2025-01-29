namespace WasteIntoCity.Core.Models
{
    public class UsersEntity
    {
        public Guid Id { get; }

        public string Nickname { get; } = string.Empty;

        public string Email { get; } = string.Empty;

        public string Password { get; } = string.Empty;

    }
}
