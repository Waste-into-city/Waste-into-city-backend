using WasteIntoCity.Core.Enums;

namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReport
    {
        private WorkColleagueReport(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, WorkMarkEnum workMarkTypesId,
            WorkMarkType? workMarkType, User? aboutColleague)
        {
            Id = id;
            FromParticipantId = fromParticipantId;
            AboutColleagueId = aboutColleagueId;
            WorksId = worksId;
            WorkMarkTypesId = workMarkTypesId;
            WorkMarkType = workMarkType;
            AboutColleague = aboutColleague;
        }

        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        public Guid AboutColleagueId { get; }

        public Guid WorksId { get; }

        public WorkMarkEnum WorkMarkTypesId { get; }

        public WorkMarkType? WorkMarkType { get; }

        public User? AboutColleague { get; }

        public static WorkColleagueReport Create(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, WorkMarkEnum workMarkTypesId,
            WorkMarkType? workMarkType, User? aboutColleague)
        {
            return new WorkColleagueReport(id, fromParticipantId, aboutColleagueId, worksId, workMarkTypesId, workMarkType, aboutColleague);
        }
    }
}
