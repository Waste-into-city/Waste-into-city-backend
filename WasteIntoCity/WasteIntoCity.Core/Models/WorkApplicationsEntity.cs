using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class WorkApplicationsEntity
    {
        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public string Description { get; } = string.Empty;

        public Guid WorkComplexitiesId { get; }
    }
}
