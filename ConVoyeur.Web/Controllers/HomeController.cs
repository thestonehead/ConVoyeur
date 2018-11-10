using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConVoyeur.Web.Models;
using Microsoft.AspNetCore.Authorization;
using ConVoyeur.Data;
using Microsoft.AspNetCore.Identity;
using Activity = System.Diagnostics.Activity;

namespace ConVoyeur.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ConUser> userManager;

        public HomeController(UserManager<ConUser> userManager)
        {
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";
            var user = await GetCurrentUserAsync();
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private Task<ConUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);

    }
}
