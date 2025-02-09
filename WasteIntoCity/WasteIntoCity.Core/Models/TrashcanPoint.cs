namespace WasteIntoCity.Core.Models
{
    public class TrashcanPoint
    {
        public Guid Id { get; set; }

        public string Lat { get; set; } = string.Empty;

        public string Lng { get; set; } = string.Empty;

        public List<Trashcan> TrashcanEntities { get; set; } = [];

        public List<TrashcanPointReport> TrashcanPointReportEntities { get; set; } = [];
    }
}
