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

      
    }
}