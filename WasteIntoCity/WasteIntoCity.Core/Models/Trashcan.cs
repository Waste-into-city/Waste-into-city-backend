using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

namespace WasteIntoCity.Core.Models
{
    public class Trashcan
    {
        public const int VOLUME_MIN = 120;

        public const int VOLUME_MAX = 1100;

        private Trashcan(Guid id, int volume, Guid coordinatesId, Guid trashcanTypesId, Guid? averageTrashcanOccupancyTypesId)
        {
            Id = id;
            Volume = volume;
            CoordinatesId = coordinatesId;
            TrashcanTypesId = trashcanTypesId;
            AverageTrashcanOccupancyTypesId = averageTrashcanOccupancyTypesId;
        }

        public Guid Id { get; }

        public int Volume { get; }

        public Guid CoordinatesId { get; }

        public Guid TrashcanTypesId { get; }

        public Guid? AverageTrashcanOccupancyTypesId { get; }

        public static Trashcan Create(Guid id, int volume, Guid coordinatesId, Guid trashcanTypesId, Guid? averageTrashcanOccupancyTypesId)
        {
            if (volume is < VOLUME_MIN or > VOLUME_MAX)
            {
                throw new ValueOutOfRangeException<int>("volume", VOLUME_MIN, VOLUME_MAX, 42);
            }

            return new Trashcan(id, volume, coordinatesId, trashcanTypesId, averageTrashcanOccupancyTypesId);
        }
    }
}
