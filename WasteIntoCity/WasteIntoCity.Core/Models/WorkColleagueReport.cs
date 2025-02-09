namespace WasteIntoCity.Core.Models
{
    public class WorkColleagueReport
    {
        public Guid Id { get; set; }

        public Guid FromParticipantid { get; set; }

        public Guid AboutColleagueId { get; set; }

        public Guid WorksId { get; set; }

        public Guid WorkMarkTypesId { get; set; }

        public Work? Work { get; set; }

        public User? UserFromParticipant { get; set; }

        public User? UserAboutColleague { get; set; }

        public WorkMarkType? WorkMarkType { get; set; }
    }
}
