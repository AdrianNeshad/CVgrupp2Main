using System.Diagnostics;
using CVgrupp2Main.Models;
using CVgrupp2Main.DatabasLager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class HomeController(ILogger<HomeController> logger, DataContext data) : Controller
    {
        private readonly ILogger<HomeController> _logger = logger;

        public IActionResult InloggadIndex()
        {
            List<Person> personer;
            // Om användaren inte är inloggad, hämta bara offentliga profiler.
            if (!User.Identity.IsAuthenticated)
            {
                personer = data.Person.Where(p => p.Privat == false)
                .OrderByDescending(p => p.KontaktID)
                .Take(3)
                .Include(p => p.HarErfarenhet)
                    .ThenInclude(p => p.Erfarenheter)
                .Include(p => p.HarKompetens)
                    .ThenInclude(p => p.Kompetenser)
                .Include(p => p.HarMedverkat)
                    .ThenInclude(p => p.Projekt)
                .Include(p => p.HarUtbildning)
                    .ThenInclude(p => p.Utbildningar)
                .ToList();
            }
            else
            {
                // Om användaren är inloggad, hämta de senaste profilerna.
                personer = data.Person
                .OrderByDescending(p => p.KontaktID)
                .Take(3)
                .Include(p => p.HarErfarenhet)
                    .ThenInclude(p => p.Erfarenheter)
                .Include(p => p.HarKompetens)
                    .ThenInclude(p => p.Kompetenser)
                .Include(p => p.HarMedverkat)
                    .ThenInclude(p => p.Projekt)
                .Include(p => p.HarUtbildning)
                    .ThenInclude(p => p.Utbildningar)
                .ToList();
            }

            bool isAuthenticated = User.Identity.IsAuthenticated;

            var projekt = data.Projekt
                .OrderByDescending(p => p.ProjektID)
                .FirstOrDefault(p => isAuthenticated || !data.Person.Any(u => u.Användarnamn == p.Skapare && u.Privat));

            ViewBag.senasteProjekt = projekt;
            // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande
                .Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            // Antag att 'personer' är en lista eller samling som ska visas i vyn
            var person = data.Person.ToList();

            return View(personer);
        }   // Visar startsidan för applikationen.
        public IActionResult Index()
        {  // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View();
        }

        // Hanterar fel och visar en anpassad felvy.

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
