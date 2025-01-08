using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class MeddelandeController(DataContext data) : Controller
    {

        // Visar formuläret för att skapa ett nytt meddelande
        public IActionResult NyttMeddelande(string användarnamn)
        {
            // Sätt användarnamnet som mottagare av meddelandet
            ViewBag.Användarnamn = användarnamn;

            // Skapa en ny instans av MeddelandeViewModel
            var meddelandeViewModel = new MeddelandeViewModel();

            // Ladda lista med användare för dropdown-menyn
            ViewBag.Användare = data.Person.Select(p => new SelectListItem
            {
                Value = p.Användarnamn,
                Text = p.Användarnamn
            }).ToList();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(meddelandeViewModel);
        }

        // Hanterar POST-begäran för att skapa ett nytt meddelande
        [HttpPost]
        public IActionResult SkrivMeddelande(MeddelandeViewModel meddelandeView, bool anonym = false)
        {
            // Ladda användarnamn för dropdown-menyn
            ViewBag.Användare = data.Person.Select(p => new SelectListItem
            {
                Value = p.Användarnamn,
                Text = p.Användarnamn
            }).ToList();

            // Kontrollera anonymitet
            string avsändare = anonym ? "Anonym" : (User.Identity.IsAuthenticated ? User.Identity.Name : meddelandeView.Avsändare);

            // Om anonymitet inte är markerad och avsändarnamn saknas
            if (!anonym && string.IsNullOrWhiteSpace(avsändare))
            {
                ModelState.AddModelError("Avsändare", "Användarnamn för avsändare krävs om det inte skickas anonymt.");
            }

            // Validera formuläret
            if (!ModelState.IsValid)
            {
                return View("NyttMeddelande", meddelandeView);
            }

            // Skapa och spara det nya meddelandet i databasen
            Meddelande meddelande = new()
            {
                Innehåll = meddelandeView.Meddelande
            };
            data.Meddelande.Add(meddelande);
            data.SaveChanges();

            // Lägg till avsändare och mottagare för meddelandet
            var senasteMeddelande = data.Meddelande.OrderByDescending(m => m.ID).FirstOrDefault();
            if (senasteMeddelande != null)
            {
                if (!anonym)
                {
                    var harSkickat = new PersonSkickatMeddelande
                    {
                        MeddelandeID = senasteMeddelande.ID,
                        Användarnamn = avsändare
                    };
                    data.PersonSkickatMeddelande.Add(harSkickat);
                }

                var harMottagit = new PersonMottagitMeddelande
                {
                    MeddelandeID = senasteMeddelande.ID,
                    Användarnamn = meddelandeView.Mottagare
                };
                data.PersonMottagitMeddelande.Add(harMottagit);
                data.SaveChanges();
            }

            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande
                .Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs)
                .Count();

            return RedirectToAction("Inloggad", "Home");
        }




        // Visar en lista över alla meddelanden för den inloggade användaren
        public IActionResult VisaMeddelanden()
        {
            var användare = User.Identity.Name;

            // Hämta alla meddelanden som är relevanta för den inloggade användaren
            var meddelandeLista = data.Meddelande.Include(m => m.HarSkickat)
                .Where(m => m.HarMottagit.Any(Hm => Hm.Användarnamn == användare)).ToList();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(meddelandeLista);
        }

        // Hanterar POST-begäran för att markera ett meddelande som läst
        [HttpPost]
        public IActionResult MarkeraSomLäst(int meddelandeId)
        {
            var meddelande = data.Meddelande.Where(m => m.ID == meddelandeId).FirstOrDefault();

            // Kontrollera om meddelandet finns och markera det som läst
            if (meddelande != null)
            {
                meddelande.HarLästs = true;
                data.SaveChanges();
            }

            return RedirectToAction("VisaMeddelanden", "Meddelande");
        }
    }
}
