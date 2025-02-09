using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class WorkComplexity
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int ParticipantsMin { get; set; }

        public int ParticipantsMax { get; set; }

        public int DurationHours {  get; set; }

        public List<WorkApplication> WorkApplications { get; set; } = [];

        public List<Work> Works { get; set; } = [];
    }
}
