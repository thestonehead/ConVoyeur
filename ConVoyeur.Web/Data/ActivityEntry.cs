using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ActivityEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
        public int VisitorId { get; set; }
        public ConUser Visitor { get; set; }

        public DateTime ActivatedDateTime { get; set; }
        public int ActivatedUserId { get; set; }
        public ConUser ActivatedUser { get; set; }

        public ActivityEntryReview Review { get; set; }


    }
}
