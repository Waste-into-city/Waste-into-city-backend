using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class User
    {
        public const int RANKING_MIN = -9999;

        public const int RANKING_MAX = 9999;

        public const int NEGATIVE_SCORE_MIN = 0;

        public const int NEGATIVE_SCORE_MAX = 9999;

        public const int NICKNAME_LENGTH_MIN = Nickname.VALUE_LENGTH_MIN;

        public const int NICKNAME_LENGTH_MAX = Nickname.VALUE_LENGTH_MAX;

        public const int EMAIL_LENGTH_MIN = Title.VALUE_LENGTH_MAX;

        public const int EMAIL_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int PASSWORD_LENGTH_MIN = Password.VALUE_LENGTH_MIN;

        public const int PASSWORD_LENGTH_MAX = Password.VALUE_LENGTH_MAX;


        private User(Guid id, Nickname nickname, Email email, Password password, int ranking, List<Role>? roles, int negativeScore, bool isBanned)
        {
            Id = id;
            Nickname = nickname;
            Email = email;
            Password = password;
            Ranking = ranking;
            Roles = roles;
            NegativeScore = negativeScore;
            IsBanned = isBanned;
        }

        public Guid Id { get; }

        public Nickname Nickname { get; }

        public Email Email { get; }

        public Password Password { get; }

        public int Ranking { get; }

        public int NegativeScore { get; }

        public bool IsBanned { get; set; }

        public List<Role>? Roles { get; }

        public static User Create(Guid id, Nickname nickname, Email email, Password password, int ranking, List<Role>? roles, int negativeScore,
            bool isBanned)
        {
            if (ranking is < RANKING_MIN or > RANKING_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(ranking), RANKING_MIN, RANKING_MAX, 55);
            }

            if (negativeScore is < NEGATIVE_SCORE_MIN or > NEGATIVE_SCORE_MAX)
            {
                throw new ValueOutOfRangeException<int>(nameof(negativeScore), NEGATIVE_SCORE_MIN, NEGATIVE_SCORE_MAX, 56);
            }

            return new User(id, nickname, email, password, ranking, roles, negativeScore, isBanned);
        }
    }
}
