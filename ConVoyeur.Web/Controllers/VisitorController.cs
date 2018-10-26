using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConVoyeur.Data;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var model = new VisitorProfileViewModel()
            {
                User = user
            };

            return View(model);
        }
    }
}