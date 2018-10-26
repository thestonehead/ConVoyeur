using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ActivityAvailabilityEntry
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int AvailabilityEntryId { get; set; }
        public AvailabilityEntry AvailabilityEntry { get; set; }
    }
}
