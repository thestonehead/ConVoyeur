using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ActivityEntryReview
    {
        [Key]
        public int ActivityEntryId { get; set; }
        public ActivityEntry ActivityEntry { get; set; }

        public float Grade { get; set; }
        public string Review { get; set; }
        public AccessSetting Access { get; set; }
    }

    public enum AccessSetting
    {
        Private = 0,
        Public = 1
    }
}
