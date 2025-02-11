using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.Models
{
    public class Trashcan
    {
        private const int VOLUME_MIN = 120;

        private const int VOLUME_MAX = 1100;

        private Trashcan(Guid id, int volume, Guid trashcanPointsId, TrashcanType trashcanTypes, TrashcanOccupancyType? averageTrashcanOccupancyType)
        {
            Id = id;
            Volume = volume;
            TrashcanPointsId = trashcanPointsId;
            TrashcanTypes = trashcanTypes;
            AverageTrashcanOccupancyType = averageTrashcanOccupancyType;
        }

        public Guid Id { get; }

        public int Volume { get; }

        public Guid TrashcanPointsId { get; }

        public TrashcanType TrashcanTypes { get; }

        public TrashcanOccupancyType? AverageTrashcanOccupancyType { get; }

        public Trashcan Create(Guid id, int volume, Guid trashcanPointsId, TrashcanType trashcanTypes, TrashcanOccupancyType? averageTrashcanOccupancyType)
        {
            if (volume is < VOLUME_MIN or > VOLUME_MAX)
            {
                throw new ValueOutOfRangeException<int>("volume", VOLUME_MIN, VOLUME_MAX);
            }

            return new Trashcan(id, volume, trashcanPointsId, trashcanTypes, averageTrashcanOccupancyType);
        }
    }
}
