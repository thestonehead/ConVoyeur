using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ConUser : IdentityUser<int>
    {
        [Required]
        public string Name { get; set; }

        public bool Active { get; set; }

        public ICollection<ActivityEntry> Activities { get; set; }

        //public ICollection<ConUserRole> UserRoles { get; set; }

        public int? EventId { get; set; }
        public Event Event { get; set; }

        public ConUser() : base()
        {
            this.Activities = new HashSet<ActivityEntry>();
        }

        public ConUser(string userName, Event @event) : base(userName)
        {
            this.Activities = new HashSet<ActivityEntry>();
            this.Event = @event;
        }
    }
}
