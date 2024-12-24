using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CVgrupp2Main.Models;
using CVgrupp2Main.DatabasLager;

namespace CVgrupp2Main.Controllers
{
    [Authorize]
    public class KompetenserController(DataContext data) : Controller
    {
        [HttpGet]
        public IActionResult VisaKompetenser()
        {
            var användarnamn = User.Identity.Name;
            // Använder LINQ för att hämta kompetenser associerade med den inloggade användaren.
            var kompetenser = data.PersonKompetenser
                                 .Where(phk => phk.PersonID == användarnamn)
                                .Select(phk => phk.Kompetenser)
            .ToList();
            // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(kompetenser);
        }



        // Visar formulär för att lägga till en ny kompetens.
        [HttpGet]
        public IActionResult LäggTillKompetens()
        {
            KompetenserViewModel kompetens = new KompetenserViewModel();

            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(kompetens);
        }
        // Hanterar inskickad data från formuläret för att skapa en ny kompetens.
        [HttpPost]
        public IActionResult LäggTillKompetens(KompetenserViewModel model)
        {
            var användarnamn = User.Identity.Name;
            if (användarnamn == null)
            {
                return Unauthorized(); // Användaren är inte inloggad.
            }

            if (!ModelState.IsValid)
            { // Kontrollerar om modellens data är giltig.

                ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
                return View(model);
            }// Skapar ett nytt Kompetenser-objekt baserat på formulärdata.
            Kompetenser nyKompetens = new Kompetenser();
            nyKompetens.Titel = model.Titel;
            nyKompetens.Beskrivning = model.Beskrivning;


            data.Kompetenser.Add(nyKompetens);
            data.SaveChanges();



            // Skapar en relation mellan användaren och den nya kompetensen.
            PersonKompetenser personKompetenser = new PersonKompetenser();


            personKompetenser.Kompetenser = nyKompetens;

            personKompetenser.Person = (from p in data.Person
                                         where p.Användarnamn == User.Identity.Name
                                         select p).FirstOrDefault();

            personKompetenser.ID = nyKompetens.ID;
            personKompetenser.PersonID = personKompetenser.Person.Användarnamn;
            data.PersonKompetenser.Add(personKompetenser);
            data.SaveChanges();

            Person person = data.Person.FirstOrDefault(p => p.Användarnamn == User.Identity.Name);
            return RedirectToAction("VisaKompetenser");
        }

        // Tar bort en specifik kompetens baserat på dess ID.
        [HttpPost]
        public IActionResult TaBortKompetens(Kompetenser kompetens)
        {
            var användarnamn = User.Identity.Name;
            if (användarnamn == null)
            {
                return Unauthorized(); // Användaren är inte inloggad.
            }

            var kompetensToRemove = data.Kompetenser.Find(kompetens.ID);
            if (kompetensToRemove == null)
            {
                return NotFound();  // Kompetensen hittades inte.
            }
            // Hittar relationen mellan användaren och den specifika kompetensen.
            var personHarKompetens = data.PersonKompetenser
                .Where(phk => phk.ID == kompetens.ID && phk.PersonID == User.Identity.Name).ToList();
            // Tar bort relationen om den existerar.
            if (personHarKompetens != null && personHarKompetens.Any())
            {
                data.PersonKompetenser.RemoveRange(personHarKompetens);
                data.SaveChanges();
            }
            // Tar bort kompetensen om ingen annan användare är relaterad till den.
            if (!data.PersonKompetenser.Any(phk => phk.ID == kompetens.ID))
            {
                data.Kompetenser.Remove(kompetensToRemove);
                data.SaveChanges();
            }
            //Skickar tillbaka till kompetens Vyn
            return RedirectToAction("VisaKompetenser");
        }
    }
}
