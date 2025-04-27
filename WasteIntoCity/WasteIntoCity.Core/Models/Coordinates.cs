using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

namespace WasteIntoCity.Core.Models
{
    public class Coordinates
    {
        public const int LAT_MIN = -9999999;

        public const int LAT_MAX = 9999999;

        public const int LNG_MIN = -9999999;

        public const int LNG_MAX = 9999999;

        private Coordinates(Guid id, double lat, double lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }

        public Guid Id { get; }

        public double Lat { get; }

        public double Lng { get; }

        public static Coordinates Create(Guid id, double lat, double lng)
        {
            if (lat is < LAT_MIN or > LAT_MAX)
            {
                throw new ValueOutOfRangeException<double>($"{nameof(lat)} of {nameof(Coordinates)}", LAT_MIN, LAT_MAX, 19);
            }

            if (lng is < LNG_MIN or > LNG_MAX)
            {
                throw new ValueOutOfRangeException<double>($"{nameof(lng)} of {nameof(Coordinates)}", LNG_MIN, LNG_MAX, 20);
            }

            return new Coordinates(id, lat, lng);
        }
    }
}
