using ConVoyeur.Data;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConVoyeur.Web.Controllers
{
    public class ActivityController : Controller
    {

        private readonly DEXContext context;
        private readonly UserManager<ConUser> userManager;

        public ActivityController(DEXContext context, UserManager<ConUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(int id)
        {
            Activity activity = await context.Activities
                .Include(a=>a.Visitors).ThenInclude(v=>v.Review).ThenInclude(r=>r.ActivityEntry).ThenInclude(ae=>ae.Visitor)
                .Include(a => a.Visitors).ThenInclude(v => v.Visitor)
                .Include(a=>a.Locations).ThenInclude(l=>l.Location)
                .FirstOrDefaultAsync(a=>a.Id == id);
            if (activity == null)
            {
                return NotFound();
            }

            ConUser currentUser = await userManager.GetUserAsync(HttpContext.User);
            ActivityEntry currentUserActivityEntry = currentUser == null ? null : activity.Visitors.FirstOrDefault(t => t.VisitorId == currentUser.Id);

            StringBuilder activityDetails = new StringBuilder();
            if (activity.Locations.Count == 1)
            {
                activityDetails.Append(activity.Locations.First().Location.Name);
            }

            ActivityViewModel viewModel = new ActivityViewModel()
            {
                ActivityId = activity.Id,
                ActivityName = activity.Name,
                ActivityDetails = activityDetails.ToString(),
                UserLoggedIn = currentUser != null,
                UserVisitedActivity = currentUserActivityEntry?.ActivatedDateTime,
                UsersReview = currentUserActivityEntry?.Review == null ? null : new PostReviewInput() // Get current user's review
                    {
                        ActivityId = id,
                        Grade = currentUserActivityEntry.Review.Grade,
                        Review = currentUserActivityEntry.Review.Review,
                        Access = currentUserActivityEntry.Review.Access,
                    },
                Reviews = activity.Visitors //Take all public reviews of other users
                    .Where(v => v.VisitorId != currentUser?.Id && v.Review != null && v.Review.Access == AccessSetting.Public)
                    .Select(v => new ReviewViewModel()
                        {
                            User = v.Visitor.UserName,
                            Grade = v.Review.Grade,
                            Review = v.Review.Review
                        }).ToArray(),
            };


            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PostReviewAsync([FromRoute]int id, PostReviewInput input)
        {
            Activity activity = await context.Activities
                .Include(a => a.Visitors).ThenInclude(v => v.Visitor)
                .Include(a => a.Visitors).ThenInclude(v => v.Review)
                .FirstOrDefaultAsync(a => a.Id == id);
            if (activity == null)
            {
                return NotFound("No such activity.");
            }
            
            ConUser currentUser = await userManager.GetUserAsync(HttpContext.User);
            ActivityEntry currentUserActivityEntry = activity.Visitors.FirstOrDefault(t => t.VisitorId == currentUser.Id);

            if (currentUserActivityEntry == null)
            {
                return NotFound("User hasn't attended the activity.");
            }

            if (currentUserActivityEntry.Review != null)
            {
                return Conflict("User has already written a review of this activity.");
            }

            currentUserActivityEntry.Review = new ActivityEntryReview()
            {
                Access = input.Access,
                Grade = input.Grade,
                Review = input.Review
            };

            int changes = await context.SaveChangesAsync();
            if (changes == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Couldn't save the review");
            }
            return RedirectToAction("Index", new { input.ActivityId });
        }
    }
}