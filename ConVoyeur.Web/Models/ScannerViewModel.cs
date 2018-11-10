using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Models
{
    public class ScannerViewModel
    {
        public ScannerSetupModel Setup { get; set; }
        public IEnumerable<ScannerActivityViewModel> Activities { get; set; }
        public IEnumerable<ScannerLocationViewModel> Locations { get; set; }
    }

    public class ScannerActivityViewModel
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public IEnumerable<int> LocationIds { get; set; }
    }

    public class ScannerLocationViewModel
    {
        public int LocationId { get; set; }
        public string Name { get; set; }
    }
}
