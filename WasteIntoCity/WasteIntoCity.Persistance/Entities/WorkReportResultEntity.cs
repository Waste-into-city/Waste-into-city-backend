namespace WasteIntoCity.Persistance.Entities
{
    public class WorkReportResultEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid FromParticipantsId { get; set; }

        public int WorkComplexityTypesId { get; set; }

        public int WorkStatusTypesId { get; set; }

        public Guid WorksId { get; set; }

        public UserEntity? FromParticipant { get; set; }

        public WorkStatusTypeEntity? WorkStatusType { get; set; }

        public WorkComplexityTypeEntity? WorkComplexityType { get; set; }

        public List<ImageEntity> Images { get; set; } = [];

        public WorkEntity? Work { get; set; }
    }
}
