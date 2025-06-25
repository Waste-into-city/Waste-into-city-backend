namespace WasteIntoCity.Core.Models
{
    public class TrashcanPointReportEachMark
    {
        private TrashcanPointReportEachMark(Guid id, Guid trashcanPointReportsId, Guid trashcansId, Guid trashcanOccupancyTypesId)
        {
            Id = id;
            TrashcanPointReportsId = trashcanPointReportsId;
            TrashcansId = trashcansId;
            TrashcanOccupancyTypesId = trashcanOccupancyTypesId;
        }

        public Guid Id { get; }

        public Guid TrashcanPointReportsId { get; }

        public Guid TrashcansId { get; }

        public Guid TrashcanOccupancyTypesId { get; }

        public static TrashcanPointReportEachMark Create(Guid id, Guid trashcanPointReportsId, Guid trashcansId, Guid trashcanOccupancyTypesId)
        {
            return new TrashcanPointReportEachMark(id, trashcanPointReportsId, trashcansId, trashcanOccupancyTypesId);
        }
    }
}
