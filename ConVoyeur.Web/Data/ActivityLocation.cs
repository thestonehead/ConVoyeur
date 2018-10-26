using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ActivityLocation
    {
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
