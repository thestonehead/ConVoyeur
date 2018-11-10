using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Models
{
    public class ScannerSetupModel
    {
        public int ActivityId { get; set; }
        public int LocationId { get; set; }
        public string ScannedText { get; set; }

    }
}
