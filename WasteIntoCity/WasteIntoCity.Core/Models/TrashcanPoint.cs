namespace WasteIntoCity.Core.Models
{
    public class TrashcanPoint
    {
        private TrashcanPoint(Guid id, string lat, string lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }

        public Guid Id { get; }

        public string Lat { get; }

        public string Lng { get; }


    }
}
