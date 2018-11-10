using ConVoyeur.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Models
{
    public class PostReviewInput
    {
        public int ActivityId { get; set; }
        public float Grade { get; set; }
        public string Review { get; set; }
        public AccessSetting Access { get; set; }

        public PostReviewInput()
        {
        }

        public PostReviewInput(int activityId)
        {
            this.ActivityId = activityId;
        }
    }
}
