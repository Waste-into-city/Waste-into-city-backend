using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class WorkApplication
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Guid WorkComplexitiesId { get; set; }

        public WorkComplexity? WorkComplexity { get; set; }

        public List<Image> Images { get; set; } = [];
    }
}
