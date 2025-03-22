using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaint
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private WorkReportComplaint(Guid id, Title title, Description description, Guid worksId, Guid fromUsersId, int workReportStatusTypesId)
        {
            Id = id;
            Title = title;
            Description = description;
            WorksId = worksId;
            FromUsersId = fromUsersId;
            WorkReportStatusTypesId = workReportStatusTypesId;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public Guid WorksId { get; }

        public Guid FromUsersId { get; }

        public int WorkReportStatusTypesId { get; }

        public static WorkReportComplaint Create(Guid id, Title title, Description description, Guid worksId, Guid fromUsersId, int workReportStatusTypesId)
        {
            if (!Enum.IsDefined(typeof(WorkReportStatusEnum), workReportStatusTypesId))
            {
                throw new InvalidValueFormatException(nameof(WorkApplication),
                    $"The workReportStatusTypesId should be an enum ({EnumOperationsExtension.ToStringAllValidValuesThroughComma<WorkReportStatusEnum>()})");
            }

            return new WorkReportComplaint(id, title, description, worksId, fromUsersId, workReportStatusTypesId);
        }
    }
}