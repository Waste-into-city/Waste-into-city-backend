namespace WasteIntoCity.Persistance.Entities
{
    public class WorkApplicationEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartedDatetime { get; set; }

        public int WorkComplexityTypesId { get; set; }

        public int WorkReportStatusTypesId { get; set; }

        public Guid CoordinatesId { get; set; }

        public Guid FromUsersId { get; set; }

        public WorkComplexityTypeEntity? WorkComplexityType { get; set; }

        public WorkReportStatusTypeEntity? WorkReportStatusType { get; set; }

        public List<ImageEntity> Images { get; set; } = [];

        public CoordinatesEntity? Coordinates { get; set; }

        public UserEntity? FromUser { get; set; }
    }
}
