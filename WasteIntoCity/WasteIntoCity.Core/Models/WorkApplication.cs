using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkApplication
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private WorkApplication(Guid id, Title title, Description description, Guid workComplexitiesId)
        {
            Id = id;
            Title = title;
            Description = description;
            WorkComplexitiesId = workComplexitiesId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public Guid WorkComplexitiesId { get; }

        public WorkApplication Create(Guid id, Title title, Description description, Guid workComplexitiesId)
        {
            return new WorkApplication(id, title, description, workComplexitiesId);
        }
    }
}
