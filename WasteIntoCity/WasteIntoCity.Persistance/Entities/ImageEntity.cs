namespace WasteIntoCity.Core.Models
{
    public class ImageEntity
    {
        public Guid Id { get; }

        public Guid? WorkApplicationsId { get; }

        public Guid? WorkReportResultsId { get; }

        public string Name { get; } = string.Empty;
    }
}
