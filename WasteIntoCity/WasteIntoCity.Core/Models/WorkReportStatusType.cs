using WasteIntoCity.Core.Exceptions;
using WasteIntoCity.Core.Types;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class WorkReportStatusType
    {
        public const int NAME_LENGTH_MIN = 1;

        public const int NAME_LENGTH_MAX = 100;

        private WorkReportStatusType(WorkReportStatusEnum id, MeanText name)
        {
            Id = id;
            Name = name;
        }

        public WorkReportStatusEnum Id { get; }

        public MeanText Name { get; }

        public static WorkReportStatusType Create(WorkReportStatusEnum id, MeanText name)
        {
            if (name.Value.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException(nameof(name), NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            return new WorkReportStatusType(id, name);
        }
    }
}
