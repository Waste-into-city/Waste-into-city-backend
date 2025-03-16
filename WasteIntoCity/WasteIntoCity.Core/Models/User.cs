using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class User
    {
        public const int RANKING_MIN = -9999;

        public const int RANKING_MAX = 9999;

        public const int NICKNAME_LENGTH_MIN = Nickname.VALUE_LENGTH_MIN;

        public const int NICKNAME_LENGTH_MAX = Nickname.VALUE_LENGTH_MAX;

        public const int EMAIL_LENGTH_MIN = Title.VALUE_LENGTH_MAX;

        public const int EMAIL_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int PASSWORD_LENGTH_MIN = Password.VALUE_LENGTH_MIN;

        public const int PASSWORD_LENGTH_MAX = Password.VALUE_LENGTH_MAX;


        private User(Guid id, Nickname nickname, Email email, Password password, int ranking, List<Role> roles)
        {
            Id = id;
            Nickname = nickname;
            Email = email;
            Password = password;
            Ranking = ranking;
            Roles = roles;
        }

        public Guid Id { get; }

        public Nickname Nickname { get; }

        public Email Email { get; }

        public Password Password { get; }

        public int Ranking { get; }

        public List<Role> Roles { get; }

        public static User Create(Guid id, Nickname nickname, Email email, Password password, int ranking, List<Role> roles)
        {
            if (ranking is < RANKING_MIN or > RANKING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(ranking), RANKING_MIN, RANKING_MAX);
            }

            return new User(id, nickname, email, password, ranking, roles);
        }
    }
}
