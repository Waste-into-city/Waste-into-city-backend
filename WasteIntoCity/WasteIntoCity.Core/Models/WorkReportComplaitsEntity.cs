namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaitsEntity
    {
        public Guid Id { get; }

        public string Title { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public Guid WorksId { get; }

        public Guid FromUsersId { get; }
    }
}
