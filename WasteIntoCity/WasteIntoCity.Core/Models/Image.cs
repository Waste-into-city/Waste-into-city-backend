using WasteIntoCity.Core.Errors;

namespace WasteIntoCity.Core.Models
{
    public class Image
    {
        private const int NAME_LENGTH_MIN = 20;

        private const int NAME_LENGTH_MAX = 255;

        private Image(Guid id, string name, Guid? workApplicationsId, Guid? workReportComplaintsId, Guid? workReportResultsId)
        {
            Id = id;
            Name = name;
            WorkApplicationsId = workApplicationsId;
            WorkReportComplaintsId = workReportComplaintsId;
            WorkReportResultsId = workReportResultsId;
        }

        public Guid Id { get; }

        public string Name { get; }

        public Guid? WorkApplicationsId { get; }

        public Guid? WorkReportComplaintsId { get; }

        public Guid? WorkReportResultsId { get; }

        public static Image Create(
            Guid id,
            string name,
            Guid? workApplicationsId,
            Guid? workReportComplaintsId,
            Guid? workReportResultsId
        )
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new NullOrWhiteSpaceException("name", null);
            }

            if (name.Length is < NAME_LENGTH_MIN or > NAME_LENGTH_MAX)
            {
                throw new InvalidLengthException("name", NAME_LENGTH_MIN, NAME_LENGTH_MAX);
            }

            return new Image(id, name, workApplicationsId, workReportComplaintsId, workReportResultsId);
        }
    }
}
