using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.Models
{
    public class Coordinates
    {
        public const int LAT_LEGTH_MIN = 1;

        public const int LAT_LEGTH_MAX = 255;

        public const int LNG_LEGTH_MIN = 1;

        public const int LNG_LEGTH_MAX = 255;

        private Coordinates(Guid id, string lat, string lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }

        public Guid Id { get; }

        public string Lat { get; }

        public string Lng { get; }

        public static Coordinates Create(Guid id, string lat, string lng)
        {
            if (lat.Length is < LAT_LEGTH_MIN or > LAT_LEGTH_MAX)
            {
                throw new InvalidLengthException(nameof(lat), LAT_LEGTH_MIN, LAT_LEGTH_MAX);
            }

            if (lng.Length is < LNG_LEGTH_MIN or > LNG_LEGTH_MAX)
            {
                throw new InvalidLengthException(nameof(lng), LNG_LEGTH_MIN, LNG_LEGTH_MAX);
            }

            return new Coordinates(id, lat, lng);
        }
    }
}
