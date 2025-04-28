using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportResult
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private WorkReportResult(Guid id, Guid fromParticipantId, Title title, Description description, WorkComplexityEnum workComplexityTypesId,
            WorkStatusEnum workStatusesId)
        {
            Id = id;
            FromParticipantId = fromParticipantId;
            Title = title;
            Description = description;
            WorkComplexityTypesId = workComplexityTypesId;
        }

        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        public Title Title { get; }

        public Description Description { get; }

        public WorkComplexityEnum WorkComplexityTypesId { get; }

        public WorkStatusEnum WorkStatusTypesId { get; }

        public static WorkReportResult Create(Guid id, Guid fromParticipantId, Title title, Description description, WorkComplexityEnum workComplexityTypesId,
            WorkStatusEnum workStatusesId)
        {
            return new WorkReportResult(id, fromParticipantId, title, description, workComplexityTypesId, workStatusesId);
        }

    }
}
