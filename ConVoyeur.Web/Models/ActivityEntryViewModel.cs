using System;

namespace ConVoyeur.Web.Models
{
    public class ActivityEntryViewModel
    {
        public string ActivityName { get; set; }
        public string LocationName { get; set; }
        public int ActivityId { get; set; }
        public DateTime ActivityActivated { get; set; }
        public ReviewViewModel Review { get; set; }
    }
}
