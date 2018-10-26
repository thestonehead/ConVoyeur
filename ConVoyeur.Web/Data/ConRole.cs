using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Data
{
    public class ConRole : IdentityRole<int>
    {
        //public ICollection<ConUserRole> UserRoles { get; set; }
        //public string BaseName { get; set; }

        public ConRole() { }

        public ConRole(string roleName) : base(roleName) {
           // this.BaseName = roleName;
        }

        //public ConRole(string roleName, Event @event) : this($"{@event.Name}[{@event.Id}]:{roleName}")
        //{
        //    this.Event = @event;
        //    this.BaseName = roleName;
        //}

    }
}
