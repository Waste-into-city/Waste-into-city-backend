using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkApplication
    {
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
