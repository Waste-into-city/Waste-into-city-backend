using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkMarkType
    {
        private WorkMarkType(Guid id, WorkMarkTypeName name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; }

        public WorkMarkTypeName Name { get; }

        public WorkMarkType Create(Guid id, WorkMarkTypeName name)
        {
            return new WorkMarkType(id, name);
        }
    }
}
