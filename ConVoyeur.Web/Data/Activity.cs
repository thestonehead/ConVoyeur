using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class Activity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public virtual ICollection<ActivityLocation> Locations { get; set; } = new HashSet<ActivityLocation>();
        public virtual ICollection<ActivityAvailabilityEntry> Availability { get; set; } = new HashSet<ActivityAvailabilityEntry>();
        public virtual ICollection<ActivityEntry> Visitors { get; set; } = new HashSet<ActivityEntry>();

        public bool Active { get; set; }


       
    }
}
