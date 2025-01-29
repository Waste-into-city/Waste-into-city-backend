using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class WorksEntity
    {
        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public Guid WorkComplexityId { get; }

        public string Description { get; } = string.Empty;

        public Guid WorkStatusesId { get; }

        public DateTime StartedDatetime { get; }
    
        public DateTime FinishDatetime { get; }
    }
}
