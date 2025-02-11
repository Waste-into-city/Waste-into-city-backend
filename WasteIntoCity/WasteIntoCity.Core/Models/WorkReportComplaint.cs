using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaint
    {
        private WorkReportComplaint(Guid id, Title title, Description description, Guid worksId, Guid fromUsersId)
        {
            Id = id;
            Title = title;
            Description = description;
            WorksId = worksId;
            FromUsersId = fromUsersId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public Guid WorksId { get; }

        public Guid FromUsersId { get; }

        public WorkReportComplaint Create(Guid id, Title title, Description description, Guid worksId, Guid fromUsersId)
        {
            return new WorkReportComplaint(id, title, description, worksId, fromUsersId);
        }
    }
}