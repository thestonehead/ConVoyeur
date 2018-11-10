using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Models
{
    public class ActivityViewModel
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime? UserVisitedActivity { get; set; }
        public PostReviewInput UsersReview { get; set; }
        public IEnumerable<ReviewViewModel> Reviews { get; set; }
        public bool UserLoggedIn { get; internal set; }
        public string ActivityDetails { get; internal set; }
    }
}
