using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteIntoCity.Core.Models
{
    public class WorkComplexitiesEntity
    {
        public Guid Id { get; }

        public string Name { get; } = string.Empty;

        public int ParticipantsMin { get; }

        public int ParticipantsMax { get; }

        public int DurationHours {  get; }
    }
}
