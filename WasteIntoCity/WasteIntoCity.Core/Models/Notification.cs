using WasteIntoCity.Core.Errors;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Notification
    {
        private const int TITLE_LENGTH_MIN = 1;

        private const int TITLE_LENGTH_MAX = 45;

        private const int DESCRIPTION_LENGTH_MIN = 0;

        private const int DESCRIPTION_LENGTH_MAX = 500;

        private Notification(Guid id, MeanText title, string description, Guid? fromUsersId, Guid toUsersId)
        {
            Id = id;
            Title = title;
            Description = description;
            FromUsersId = fromUsersId;
            ToUsersId = toUsersId;
        }

        public Guid Id { get; }

        public MeanText Title { get; }

        public string Description { get; }

        public Guid? FromUsersId { get; }

        public Guid ToUsersId { get; }

        public Notification Create(Guid id, MeanText title, string description, Guid? fromUsersId, Guid toUsersId)
        {
            if (title.Value.Length is < TITLE_LENGTH_MIN or > TITLE_LENGTH_MAX)
            {
                throw new InvalidLengthException("title", TITLE_LENGTH_MIN, TITLE_LENGTH_MAX);
            }

            if (description.Length is < DESCRIPTION_LENGTH_MIN or > DESCRIPTION_LENGTH_MAX)
            {
                throw new InvalidLengthException("description", DESCRIPTION_LENGTH_MIN, DESCRIPTION_LENGTH_MAX);
            }

            return new Notification(id, title, description, fromUsersId, toUsersId);
        }
    }
}
