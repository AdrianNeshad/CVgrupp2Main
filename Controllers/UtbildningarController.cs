using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class UtbildningarController(DataContext data) : Controller
    {
        // Hämtar en lista över utbildningar för den inloggade användaren och visar dem.
        [HttpGet]
        public IActionResult ÄndraUtbildning()
        {
            var användarnamn = User.Identity.Name;

            // Använder LINQ för att hämta utbildningar för den inloggade användaren.
            var utbildningar = data.PersonUtbildningar
                                .Where(phu => phu.Person.Användarnamn == användarnamn) // Filtrera baserat på användarnamnet.
                                .Select(phu => phu.Utbildningar) // Välj endast utbildningsinformation.
                                .ToList(); // Konvertera resultatet till en lista.

            // Hämtar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande
                                        .Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(utbildningar);
        }

        // Förbereder och visar en vy för att lägga till en ny utbildning.
        [HttpGet]
        public IActionResult LäggTillUtbildning()
        {
            var utbildningViewModel = new UtbildningarViewModel
            {
                PersonID = User.Identity.Name // Sätt Personid i ViewModel till den inloggade användarens ID.
            };

            // Hämtar lista över befintliga utbildningar för att visa i vyn.
            ViewBag.ExistingUtbildningar = data.Utbildningar.ToList();

            return View(utbildningViewModel);
        }

        // Hanterar data inskickad från formuläret för att lägga till en ny utbildning.
        [HttpPost]
        public IActionResult LäggTillUtbildning(UtbildningarViewModel utbildningViewModel)
        {
            if (ModelState.IsValid)
            {
                // Skapar ett nytt Utbildning-objekt med data från vyn.
                var utbildning = new Utbildningar
                {
                    Namn = utbildningViewModel.Namn,
                    Beskrivning = utbildningViewModel.Beskrivning
                };

                // Lägger till det nya Utbildning-objektet i databasen.
                data.Utbildningar.Add(utbildning);
                data.SaveChanges();

                // Skapar och lägger till en relation mellan användaren och den nya utbildningen.
                var personUtbildningar = new PersonUtbildningar
                {
                    PersonID = utbildningViewModel.PersonID,
                    UtbildningID = utbildning.UtbildningID
                };

                data.PersonUtbildningar.Add(personUtbildningar);
                data.SaveChanges();

                return RedirectToAction("ÄndraUtbildning");
            }

            // Om modellstaten inte är giltig, återvänd till samma vy.
            return View(utbildningViewModel);
        }

        // Tar bort en specifik utbildning baserat på dess ID.
        [HttpPost]
        public IActionResult TaBortUtbildning(int utbildningID)
        {
            // Hittar den specifika utbildningen baserat på dess ID.
            var utbildning = data.Utbildningar.FirstOrDefault(u => u.UtbildningID == utbildningID);
            if (utbildning == null)
            {
                return NotFound(); // Om utbildningen inte hittas, returnera NotFound.
            }

            // Hittar alla relationer mellan användare och den specifika utbildningen.
            var personUtbildningarEntries = data.PersonUtbildningar
                .Where(pu => pu.Utbildningar.UtbildningID == utbildningID).ToList();

            // Tar bort dessa relationer från databasen.
            data.PersonUtbildningar.RemoveRange(personUtbildningarEntries);
            data.Utbildningar.Remove(utbildning); // Tar även bort själva utbildningen.
            data.SaveChanges();

            return RedirectToAction("ÄndraUtbildning");
        }
    }
}
