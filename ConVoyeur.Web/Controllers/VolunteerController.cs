using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConVoyeur.Data;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConVoyeur.Web.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly DEXContext context;

        public VolunteerController(DEXContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Scanner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Scanning([FromBody]ScanningRequest request)
        {
            Uri visitorPage;
            if (!Uri.TryCreate(request.ScannedText, UriKind.Absolute, out visitorPage) || !visitorPage.Segments.Any(t=>t == "Visitor/"))
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