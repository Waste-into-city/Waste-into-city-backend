using WasteIntoCity.Core.ValueObjects;

namespace WasteIntoCity.Core.Models
{
    public class Image
    {
        private Image(Guid id, ImageName name, DateTime uploadedTime, Guid? workApplicationsId, Guid? workReportComplaintsId, Guid? workReportResultsId, Guid? usersId)
        {
            Id = id;
            Name = name;
            UploadedTime = uploadedTime;
            WorkApplicationsId = workApplicationsId;
            WorkReportComplaintsId = workReportComplaintsId;
            WorkReportResultsId = workReportResultsId;
            UsersId = usersId;
        }

        public Guid Id { get; }

        public ImageName Name { get; }

        public DateTime UploadedTime { get; }

        public Guid? WorkApplicationsId { get; }

        public Guid? WorkReportComplaintsId { get; }

        public Guid? WorkReportResultsId { get; }

        public Guid? UsersId { get; }

        public static Image Create(
            Guid id,
            ImageName name,
            DateTime uploadedTime,
            Guid? workApplicationsId,
            Guid? workReportComplaintsId,
            Guid? workReportResultsId,
            Guid? usersId
        )
        {
            return new Image(id, name, uploadedTime, workApplicationsId, workReportComplaintsId, workReportResultsId, usersId);
        }
    }
}
