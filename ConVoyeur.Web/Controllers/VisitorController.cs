using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConVoyeur.Data;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConVoyeur.Web.Controllers
{
    [Route("[controller]")]
    public class VisitorController : Controller
    {
        private readonly DEXContext context;

        public VisitorController(DEXContext context)
        {
            this.context = context;
        }

        [Route("{id}")]
        public async Task<IActionResult> Index([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await context.Users
                .Include(u=>u.Activities).ThenInclude(ae=>ae.Activity)
                .Include(u => u.Activities).ThenInclude(ae => ae.Review)
                .Include(u => u.Activities).ThenInclude(ae => ae.ActivatedLocation)
                .FirstOrDefaultAsync(u=>u.Id == id);
            if (user == null)
            {
                return NotFound();
            }



            var model = new VisitorProfileViewModel()
            {
                Username = user.Name,
                Group = "N/A",
                AttendedActivities = user.Activities.Select(a => new ActivityEntryViewModel()
                {
                    ActivityId = a.ActivityId,
                    ActivityName = a.Activity?.Name ?? "N/A",
                    ActivityActivated = a.ActivatedDateTime,
                    LocationName = a.ActivatedLocation?.Name ?? "N/A",
                    Review = a.Review == null ? null : new ReviewViewModel()
                    {
                        User = a.Visitor.UserName,
                        Grade = a.Review.Grade,
                        Review = a.Review.Review
                    }
                })
            };

            return View(model);
        }
    }

}