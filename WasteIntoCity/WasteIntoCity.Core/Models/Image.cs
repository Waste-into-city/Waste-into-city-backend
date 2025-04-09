using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Image
    {
        private Image(Guid id, ImageName name, DateTime uploadedTime, Guid? workApplicationsId, Guid? workReportComplaintsId, Guid? workReportResultsId)
        {
            Id = id;
            Name = name;
            UploadedTime = uploadedTime;
            WorkApplicationsId = workApplicationsId;
            WorkReportComplaintsId = workReportComplaintsId;
            WorkReportResultsId = workReportResultsId;
        }

        public Guid Id { get; }

        public ImageName Name { get; }

        public DateTime UploadedTime { get; }

        public Guid? WorkApplicationsId { get; }

        public Guid? WorkReportComplaintsId { get; }

        public Guid? WorkReportResultsId { get; }

        public static Image Create(
            Guid id,
            ImageName name,
            DateTime uploadedTime,
            Guid? workApplicationsId,
            Guid? workReportComplaintsId,
            Guid? workReportResultsId
        )
        {
            return new Image(id, name, uploadedTime, workApplicationsId, workReportComplaintsId, workReportResultsId);
        }
    }
}
