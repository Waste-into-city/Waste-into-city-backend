namespace WasteIntoCity.Core.Models
{
    public class TrashcanPoint
    {
        // TODO: Add constraints for Lat and Lng

        private TrashcanPoint(Guid id, string lat, string lng)
        {
            Id = id;
            Lat = lat;
            Lng = lng;
        }

        public Guid Id { get; }

        public string Lat { get; }

        public string Lng { get; }

        public TrashcanPoint Create(Guid id, string lat, string lng)
        {
            return new TrashcanPoint(Id, Lat, Lng);
        }
    }
}
