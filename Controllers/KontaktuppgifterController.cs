    using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class KontaktuppgifterController(DataContext data) : Controller
    {
        // Visar ett redigeringsformulär för användarens kontaktuppgifter.
        public IActionResult ÄndraKontaktuppgifter()
        {
            var användarnamn = User.Identity.Name;
            // Hämtar den inloggade användarens personliga data.
            var person = data.Person.FirstOrDefault(p => p.Användarnamn == användarnamn);
            // Hämtar kontaktuppgifter för användaren, eller skapar nya om de inte finns.
            var kontakt = data.Kontaktuppgifter.Find(person.KontaktID) ?? new Kontaktuppgifter();

            // Förbereder en ViewModel med användarens kontaktuppgifter och namn.
            var viewModel = new KontaktuppgifterViewModel
            {
                KontaktID = kontakt.KontaktID,
                Adress = kontakt.Adress,
                Telefonnummer = kontakt.Telefonnummer,
                Email = kontakt.Email,
                Förnamn = person.Förnamn,
                Efternamn = person.Efternamn
            };
            // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View("~/Views/CV/ÄndraKontaktuppgifter.cshtml", viewModel);       // Returnerar vyn för att redigera kontaktuppgifter.
        }
        // Hanterar inlämningen av redigerade kontaktuppgifter.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ÄndraKontaktuppgifter(KontaktuppgifterViewModel viewModel)
        {  // Kontrollerar om modellens data är giltig.
            if (ModelState.IsValid)
            { // Skapar eller uppdaterar kontaktuppgifterna baserat på inlämnade data.
                var kontakt = new Kontaktuppgifter
                {
                    KontaktID = viewModel.KontaktID,
                    Adress = viewModel.Adress,
                    Telefonnummer = viewModel.Telefonnummer,
                    Email = viewModel.Email
                };

                // Uppdaterar den inloggade användarens namnuppgifter.
                var person = data.Person.FirstOrDefault(p => p.Användarnamn == User.Identity.Name);
                if (person != null)
                {
                    person.Förnamn = viewModel.Förnamn;
                    person.Efternamn = viewModel.Efternamn;
                    data.Update(person);
                }

                // Sparar ändringarna i databasen.
                data.Entry(kontakt).State = EntityState.Modified;
                data.SaveChanges();

                // Återgår till användarens profilsida efter uppdatering.
                return RedirectToAction("Profil", "Användare");
            }
            //kollar meddelanden,
            //Återvisar formuläret med eventuella felmeddelanden om valideringen misslyckas.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View("~/Views/CV/ÄndraKontaktuppgifter.cshtml", viewModel);
        }
    }
}
