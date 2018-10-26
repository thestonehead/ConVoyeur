using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class Event
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Timestamp]
        [Column(TypeName = "TIMESTAMP")]
        public DateTime ActiveFrom { get; set; }
        public DateTime? ActiveTo { get; set; }

        public ICollection<Location> Locations { get; set; }
        public ICollection<ConUser> Users { get; set; }

        public Event()
        {
            this.Locations = new HashSet<Location>();
            this.Users = new HashSet<ConUser>();
        }
    }
}
