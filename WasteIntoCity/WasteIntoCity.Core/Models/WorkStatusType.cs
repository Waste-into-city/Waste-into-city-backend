using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkStatusType
    {
        private WorkStatusType(Guid id, WorkStatusTypeName name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public WorkStatusTypeName Name { get; }

        public WorkStatusType Create(Guid id, WorkStatusTypeName name)
        {
            return new WorkStatusType(id, name);
        }
    }
}
