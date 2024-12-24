using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;
namespace CVgrupp2Main.Controllers
{
    public class ErfarenheterController(DataContext data) : Controller
    {
        [HttpGet]
        public IActionResult ÄndraErfarenhet()
        {
            var användarnamn = User.Identity.Name;

            // Använder LINQ för att hämta erfarenheter relaterade till den inloggade användaren.
            var erfarenhet = data.PersonErfarenheter
                                .Where(phk => phk.PersonID == användarnamn) // Filtrera på användarens ID.
                                .Select(phk => phk.Erfarenheter) // Välj endast erfarenhetsobjekt.
                                .ToList(); // Konvertera resultatet till en lista.

            // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande
                                        .Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs)
                                        .Count();

            return View(erfarenhet);
        }

        // Visar en vy för att lägga till en ny erfarenhet.
        [HttpGet]
        public IActionResult LäggTillErfarenhet()
        {
            var viewModel = new ErfarenheterViewModel
            {
                ID = User.Identity.Name // Sätt användarens ID i ViewModel
            };

            // Hämtar och visar en lista över befintliga erfarenheter.
            ViewBag.ExistingErfarenheter = data.Erfarenheter.ToList();

            return View(viewModel);
        }

        // Hanterar inskickad data för att skapa och spara en ny erfarenhet.
        [HttpPost]
        public IActionResult LäggTillErfarenhet(ErfarenheterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                // Skapa en ny erfarenhet baserat på data från formuläret.
                var erfarenhet = new Erfarenheter
                {
                    Titel = viewModel.Titel,
                    Beskrivning = viewModel.Beskrivning
                };

                // Lägger till den nya erfarenheten i databasen.
                data.Erfarenheter.Add(erfarenhet);
                data.SaveChanges();

                // Skapa en relation mellan den inloggade användaren och den nya erfarenheten.
                var personErfarenheter = new PersonErfarenheter
                {
                    PersonID = viewModel.ID,
                    Erfarenheter = erfarenhet
                };

                data.PersonErfarenheter.Add(personErfarenheter);
                data.SaveChanges();

                return RedirectToAction("ÄndraErfarenhet");
            }

            // Om formuläret inte är korrekt ifyllt, visa samma vy igen för korrigering.
            return View(viewModel);
        }

        // Tar bort en specifik erfarenhet baserat på dess unika ID.
        [HttpPost]
        public IActionResult TaBortErfarenhet(int erfarenhetId)
        {
            // Söker efter och hittar den specifika erfarenheten.
            var erfarenhet = data.Erfarenheter.FirstOrDefault(e => e.ID == erfarenhetId);
            if (erfarenhet == null)
            {
                return NotFound(); // Om erfarenheten inte finns, returnera 'NotFound'.
            }

            // Hittar alla relationer mellan användare och den specifika erfarenheten.
            var personErfarenheterEntries = data.PersonErfarenheter
                .Where(pe => pe.Erfarenheter.ID == erfarenhetId).ToList();

            // Tar bort dessa relationer och själva erfarenheten från databasen.
            data.PersonErfarenheter.RemoveRange(personErfarenheterEntries);
            data.Erfarenheter.Remove(erfarenhet);
            data.SaveChanges();

            return RedirectToAction("ÄndraErfarenhet");
        }
    }
}
