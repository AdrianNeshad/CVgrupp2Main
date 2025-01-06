using Microsoft.AspNetCore.Mvc;
using CVgrupp2Main.DatabasLager;

namespace CVgrupp2Main.Controllers
{
    public class SokController(DataContext data) : Controller
    {
        public IActionResult Sökning()
        {
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View();
        }

        public IActionResult Sök(string sökning)
        {
            // Hämta alla användare om sökning är null eller tom
            var users = string.IsNullOrWhiteSpace(sökning)
                ? data.Person.ToList()
                : data.Person.Where(p => p.Förnamn.Contains(sökning) || p.Efternamn.Contains(sökning)).ToList();

            // Om användaren inte är inloggad, hämta endast offentliga profiler
            if (User.Identity.Name == null)
            {
                users = users.Where(p => !p.Privat).ToList();
            }

            // Räkna olästa meddelanden för inloggad användare
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            // Skicka resultatet till vyn
            return View("Sökresultat", users);
        }

    }
}
