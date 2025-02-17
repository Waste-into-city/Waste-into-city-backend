namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReportEntity
    {
        public Guid Id { get; set; }

        public Guid FromParticipantId { get; set; }

        public Guid AboutColleagueId { get; set; }

        public Guid WorksId { get; set; }

        public Guid WorkMarkTypesId { get; set; }

        public WorkEntity? Work { get; set; }

        public UserEntity? UserFromParticipant { get; set; }

        public UserEntity? UserAboutColleague { get; set; }

        public WorkMarkTypeEntity? WorkMarkType { get; set; }
    }
}
