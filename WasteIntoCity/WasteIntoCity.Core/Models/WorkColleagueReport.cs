using WasteIntoCity.Core.Types;

namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReport
    {
        private WorkColleagueReport(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, WorkMarkEnum workMarkTypesId)
        {
            Id = id;
            FromParticipantId = fromParticipantId;
            AboutColleagueId = aboutColleagueId;
            WorksId = worksId;
            WorkMarkTypesId = workMarkTypesId;
        }

        public Guid Id { get; }

        public Guid FromParticipantId { get; }

        public Guid AboutColleagueId { get; }

        public Guid WorksId { get; }

        public WorkMarkEnum WorkMarkTypesId { get; }

        public static WorkColleagueReport Create(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, WorkMarkEnum workMarkTypesId)
        {
            return new WorkColleagueReport(id, fromParticipantId, aboutColleagueId, worksId, workMarkTypesId);
        }
    }
}
