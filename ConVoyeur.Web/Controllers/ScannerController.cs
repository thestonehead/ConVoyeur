using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConVoyeur.Data;
using ConVoyeur.Web.Infrastructure;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConVoyeur.Web.Controllers
{
    //[Authorize]
    public class ScannerController : Controller
    {
        private readonly DEXContext context;
        private readonly TimeSpan timeDialation = TimeSpan.FromMinutes(10);

        public ScannerController(DEXContext context)
        {
            this.context = context;
        }


        public IActionResult Index()
        {
//            var activities = context.GetAvailableActivitiesAtTime(DateTime.Now).Include(a => a.Locations).ThenInclude(l => l.Location);
            var activities = context.Activities.Include(a => a.Locations).ThenInclude(l => l.Location); //TODO: change for proper data
            var model = new ScannerViewModel()
            {
                Activities = activities.Select(t => new ScannerActivityViewModel()
                {
                    ActivityId = t.Id,
                    Name = t.Name,
                    LocationIds = t.Locations.Select(l => l.LocationId)
                }),
                Locations = activities.SelectMany(a => a.Locations).Select(l=>l.Location).Distinct().Select(l => new ScannerLocationViewModel()
                {
                    LocationId = l.Id,
                    Name = l.Name
                })
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Scanning([FromBody]ScannerSetupModel request)
        {
            Uri visitorPage;
            if (!Uri.TryCreate(request.ScannedText, UriKind.Absolute, out visitorPage) || !visitorPage.Segments.Any(t => t == "Visitor/"))
            {
                return Ok(false);
            }

            int visitorId;
            if (!int.TryParse(visitorPage.Segments.Last(), out visitorId))
            {
                //TODO log error
                return Ok(false);
            }

            var visitor = await this.context.Users.FindAsync(visitorId);
            if (visitor == null)
            {
                return Ok(false);
            }

            return Ok(visitor.Name);
        }

    }
}