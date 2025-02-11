using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportResult
    {
        private WorkReportResult(Guid id, Guid fromParticipantId, Title title, Description description, Guid workStatusesId)
        {
            Id = id;
            FromParticipantId = fromParticipantId;
            Title = title;
            Description = description;
            WorkStatusesId = workStatusesId;
        }

        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        public Title Title { get; }

        public Description Description { get; }

        public Guid WorkStatusesId { get; }

        public WorkReportResult Create(Guid id, Guid fromParticipantId, Title title, Description description, Guid workStatusesId)
        {
            return new WorkReportResult(id, fromParticipantId, title, description, workStatusesId);
        }

    }
}
