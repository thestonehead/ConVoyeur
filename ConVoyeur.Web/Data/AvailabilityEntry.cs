using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class AvailabilityEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }

        public ICollection<ActivityAvailabilityEntry> Activities { get; set; }

    }
}
