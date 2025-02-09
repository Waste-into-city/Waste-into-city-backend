namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaint
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid WorksId { get; set; }

        public Guid FromUsersId { get; set; }

        public Work? Work { get; set; }

        public List<Image> Images { get; set; } = [];
    }
}