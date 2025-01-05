using Microsoft.AspNetCore.Mvc;
using CVgrupp2Main.DatabasLager;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class SokController(DataContext data) : Controller
    {
        public IActionResult Sökning()
        {
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View();
        }

        // Action för att hantera sökningen
        public IActionResult Sök(string sökning)
        {
            var users = data.Person
                        .Where(p => p.Förnamn.Contains(sökning) || p.Efternamn.Contains(sökning))
                        .ToList();


            // Om användaren inte är inloggad, hämta endast offentliga profiler
            if (User.Identity.Name == null)
            {
                users = users.Where(p => !p.Privat).ToList();
            }

            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            // Skicka resultatet till vyn
            return View("Sökresultat", users);
        }
    }
}
