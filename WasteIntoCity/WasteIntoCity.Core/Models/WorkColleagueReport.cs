namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReport
    {
        private WorkColleagueReport(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, Guid workMarkTypesId)
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

        public Guid WorkMarkTypesId { get; }

        public static WorkColleagueReport Create(Guid id, Guid fromParticipantId, Guid aboutColleagueId, Guid worksId, Guid workMarkTypesId)
        {
            return new WorkColleagueReport(id, fromParticipantId, aboutColleagueId, worksId, workMarkTypesId);
        }
    }
}
