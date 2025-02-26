namespace WasteIntoCity.Core.Models
{
    public class WorkReportComplaintEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid WorksId { get; set; }

        public Guid FromUsersId { get; set; }

        public WorkEntity? Work { get; set; }

        public List<ImageEntity> Images { get; set; } = [];

        public UserEntity? FromUser { get; set; }
    }
}