using WasteIntoCity.Core.Exceptions.BadRequest400Exceptions;

namespace WasteIntoCity.Core.Models
{
    public class Coordinates
    {
        public const int LAT_PRECISION = 20;

        public const int LAT_SCALE = 17;

        public const int LNG_PRECISION = 20;

        public const int LNG_SCALE = 17;

        private Coordinates(Guid id, decimal lat, decimal lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }

        public Guid Id { get; }

        public decimal Lat { get; }

        public decimal Lng { get; }

        private static bool IsValidDecimal(decimal value, int precision, int scale)
        {
            // Absolute value to handle negative numbers
            value = Math.Abs(value);

            // Split integer and fractional parts
            decimal integerPart = Math.Truncate(value);
            decimal fractionalPart = value - integerPart;

            // Count digits
            int integerDigits = integerPart == 0 ? 1 : (int)Math.Floor(Math.Log10((double)integerPart) + 1);
            int fractionalDigits = fractionalPart.ToString().TrimEnd('0').Length - 2; // Remove "0."

            return (integerDigits + fractionalDigits) <= precision && fractionalDigits <= scale;
        }

        public static Coordinates Create(Guid id, decimal lat, decimal lng)
        {
            if (!IsValidDecimal(lat, LAT_PRECISION, LAT_SCALE))
            {
                throw new ValueOutOfRangeException<decimal>($"{nameof(lat)} of {nameof(Coordinates)}",
                    $"precision = {LAT_PRECISION}, scale = {LAT_SCALE}", 19);
            }

            if (!IsValidDecimal(lng, LNG_PRECISION, LNG_SCALE))
            {
                throw new ValueOutOfRangeException<decimal>($"{nameof(lng)} of {nameof(Coordinates)}",
                    $"precision = {LNG_PRECISION}, scale = {LNG_SCALE}", 20);
            }

            return new Coordinates(id, lat, lng);
        }
    }
}
