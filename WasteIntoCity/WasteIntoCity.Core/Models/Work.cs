using WasteIntoCity.Core.Enums;
using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;
using WasteIntoCity.Core.Extensions;
using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Work
    {
        public const int TITLE_LENGTH_MIN = Title.VALUE_LENGTH_MIN;

        public const int TITLE_LENGTH_MAX = Title.VALUE_LENGTH_MAX;

        public const int DESCRIPTION_LENGTH_MIN = Description.VALUE_LENGTH_MIN;

        public const int DESCRIPTION_LENGTH_MAX = Description.VALUE_LENGTH_MAX;

        private Work(Guid id, Title title, Description description, DateTime? startedDatetime, DateTime? finishDatetime, WorkComplexityEnum workComplexityTypesId,
            WorkStatusEnum workStatusTypesId, Guid coordinatesId, List<User>? participants, Coordinates? coordinates,
            WorkComplexityType? workComplexityType, List<WorkColleagueReport>? workColleagueReports, WorkStatusType? workStatusType,
            WorkReportResult? workReportResult)
        {
            Id = id;
            Title = title;
            Description = description;
            StartedDatetime = startedDatetime;
            FinishDatetime = finishDatetime;
            WorkComplexityTypesId = workComplexityTypesId;
            WorkStatusTypesId = workStatusTypesId;
            CoordinatesId = coordinatesId;
            Participants = participants;
            Coordinates = coordinates;
            WorkComplexityType = workComplexityType;
            WorkColleagueReports = workColleagueReports;
            WorkStatusType = workStatusType;
            WorkReportResult = workReportResult;
            WorkStatusForClient = null;
        }

        public Guid Id { get; }

        public Title Title { get; }

        public Description Description { get; }

        public DateTime? StartedDatetime { get; }

        public DateTime? FinishDatetime { get; }

        public WorkComplexityEnum WorkComplexityTypesId { get; }

        public WorkStatusEnum WorkStatusTypesId { get; }

        public Guid CoordinatesId { get; }

        public List<User>? Participants { get; }

        public Coordinates? Coordinates { get; }

        public WorkComplexityType? WorkComplexityType { get; }

        public List<WorkColleagueReport>? WorkColleagueReports { get; }

        public WorkStatusType? WorkStatusType { get; }

        public WorkReportResult? WorkReportResult { get; }

        public WorkStatusForClientEnum? WorkStatusForClient { get; }

        public WorkStatusForClientEnum TakeStatusForClientEnum()
        {
            return EnumOperationsExtension.TakeWorkStatusForClientEnum(StartedDatetime, FinishDatetime, WorkStatusTypesId);
        }

        public static Work Create(Guid id, Title title, Description description, DateTime? startedDatetime, DateTime? finishDatetime,
            WorkComplexityEnum workComplexityTypesId, WorkStatusEnum workStatusTypesId, Guid coordinatesId, List<User>? participants,
            Coordinates? coordinates, WorkComplexityType? workComplexityType, List<WorkColleagueReport>? workColleagueReports, WorkStatusType? workStatusType,
            WorkReportResult? workReportResult)
        {
            if (startedDatetime > finishDatetime)
            {
                throw new ValueOutOfRangeException<DateTime>("startDatetime", DateTime.MinValue, (DateTime)finishDatetime, 57);
            }

            if ((startedDatetime is null && finishDatetime is not null) || (startedDatetime is not null && finishDatetime is null))
            {
                throw new NullOrWhiteSpaceException(nameof(Work),
                    "finish datetime and startedDatetime should be null or both should be not null", 44);
            }

            return new Work(id, title, description, startedDatetime, finishDatetime, workComplexityTypesId, workStatusTypesId, coordinatesId,
                participants, coordinates, workComplexityType, workColleagueReports, workStatusType, workReportResult);
        }
    }
}
