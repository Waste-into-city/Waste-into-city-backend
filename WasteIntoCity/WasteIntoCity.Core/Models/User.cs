using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class User
    {
        private User(Guid id, Nickname nickname, Title email, Password password)
        {
            Id = id;
            Nickname = nickname;
            Email = email;
            Password = password;
        }

        public Guid Id { get; }

        public Nickname Nickname { get; }

        public Title Email { get; }

        public Password Password { get; }

        public User Create(Guid id, Nickname nickname, Title email, Password password)
        {
            return new User(id, nickname, email, password);
        }
    }
}
