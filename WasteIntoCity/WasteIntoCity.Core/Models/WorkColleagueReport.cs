namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReport
    {
        private WorkColleagueReport(Guid id, Guid fromParticipantid, Guid aboutColleagueId, Guid worksId, Guid workMarkTypesId)
        {
            Id = id;
            FromParticipantid = fromParticipantid;
            AboutColleagueId = aboutColleagueId;
            WorksId = worksId;
            WorkMarkTypesId = workMarkTypesId;
        }

        public Guid Id { get; }

        public Guid FromParticipantid { get; }

        public Guid AboutColleagueId { get; }

        public Guid WorksId { get; }

        public Guid WorkMarkTypesId { get; }

        public WorkColleagueReport Create(Guid id, Guid fromParticipantid, Guid aboutColleagueId, Guid worksId, Guid workMarkTypesId)
        {
            return new WorkColleagueReport(id, fromParticipantid, aboutColleagueId, worksId, workMarkTypesId);
        }
    }
}
