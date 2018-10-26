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


        public ICollection<ActivityLocation> Locations { get; set; }
        public ICollection<ActivityAvailabilityEntry> Availability { get; set; }
        public ICollection<ActivityEntry> Visitors { get; set; }

        public bool Active { get; set; }

    }
}
