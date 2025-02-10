using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.Models
{
    public class Trashcan
    {
        private const int VOLUME_MIN = 120;

        private const int VOLUME_MAX = 1100;

        private Trashcan(Guid id, int volume, Guid trashcanPointsId, Guid trashcanTypesId, TrashcanOccupancyType? averageTrashcanOccupancyTypeId)
        {
            Id = id;
            Volume = volume;
            TrashcanPointsId = trashcanPointsId;
            TrashcanTypesId = trashcanTypesId;
            AverageTrashcanOccupancyTypeId = averageTrashcanOccupancyTypeId;
        }

        public Guid Id { get; }

        public int Volume { get; }

        public Guid TrashcanPointsId { get; }

        public Guid TrashcanTypesId { get; }

        public TrashcanOccupancyType? AverageTrashcanOccupancyTypeId { get; }

        public Trashcan Create(Guid id, int volume, Guid trashcanPointsId, Guid trashcanTypesId, TrashcanOccupancyType? averageTrashcanOccupancyTypeId)
        {
            if (volume is < VOLUME_MIN or > VOLUME_MAX)
            {
                throw new ValueOutOfRangeException<int>("volume", VOLUME_MIN, VOLUME_MAX);
            }

            return new Trashcan(id, volume, trashcanPointsId, trashcanTypesId, averageTrashcanOccupancyTypeId);
        }
    }
}
