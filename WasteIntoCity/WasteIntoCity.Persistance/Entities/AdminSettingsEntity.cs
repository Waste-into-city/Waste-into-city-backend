namespace WasteIntoCity.Persistance.Entities
{
    public class AdminSettingsEntity
    {
        public Guid Id { get; set; }

        public int TrueComplaintToAdditionRanking { get; set; }

        public int FalseComplaintFromAdditionRanking { get; set; }

        public int TrueComplaintFromAdditionRanking { get; set; }

        public int AcceptableDifferenceReportTrashcanOccupancy { get; set; }

        public int FalseReportTrashcansOccupancyAdditionRanking { get; set; }
    }
}
