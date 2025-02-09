using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class Work
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public DateTime StartedDatetime { get; set; }

        public DateTime FinishDatetime { get; set; }

        public Guid WorkComplexityId { get; set; }

        public Guid WorkStatusesId { get; set; }

        public WorkComplexity? WorkComplexity { get; set; }

        public WorkStatus? WorkStatus { get; set; }

        public List<WorkReportComplaint> WorkReportComplaints { get; set; } = [];

        public List<WorkColleagueReport> WorkColleagueReports { get; set; } = [];

        public ICollection<User> Users { get; set; } = [];
    }
}
