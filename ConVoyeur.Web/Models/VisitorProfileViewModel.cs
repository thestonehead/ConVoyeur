using ConVoyeur.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Models
{
    public class VisitorProfileViewModel
    {
        public string Username { get; set; }
        public string Group { get; set; }

        public IEnumerable<ActivityEntryViewModel> AttendedActivities { get; set; }
    }
}
