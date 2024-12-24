using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class MeddelandeController(MeddelandeViewModel model, DataContext data) : Controller
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
        public IActionResult SkrivMeddelande(MeddelandeViewModel meddelandeView)
        {
            // Ladda användarnamn för dropdown-menyn
            ViewBag.Användare = data.Person.Select(p => new SelectListItem
            {
                Value = p.Användarnamn,
                Text = p.Användarnamn
            }).ToList();

            // Kontrollera om avsändaren finns i databasen om en användare som inte är inloggad vill skicka ett meddelande
            if (!User.Identity.IsAuthenticated)
            {
                bool avsändareFinns = data.Person.Any(p => p.Användarnamn == meddelandeView.Avsändare);
                if (!avsändareFinns)
                {
                    ModelState.AddModelError("Avsändare", "Användarnamnet finns inte");
                }
            }

            // Validera formuläret
            if (!ModelState.IsValid)
            {
                return View("NyttMeddelande", meddelandeView);
            }

            // Skapa och spara det nya meddelandet i databasen
            if (ModelState.IsValid)
            {
                Meddelande meddelande = new Meddelande();
                meddelande.Innehåll = meddelandeView.Meddelande;
                data.Meddelande.Add(meddelande);
                data.SaveChanges();

                // Lägg till avsändare och mottagare för meddelandet
                Meddelande senasteMeddelande = data.Meddelande.OrderByDescending(m => m.ID).Take(1).FirstOrDefault();
                int id = senasteMeddelande.ID;

                var harSkickat = new PersonSkickatMeddelande();
                harSkickat.MeddelandeID = id;
                harSkickat.Användarnamn = User.Identity.IsAuthenticated ? User.Identity.Name : meddelandeView.Avsändare; ;
                data.PersonSkickatMeddelande.Add(harSkickat);
                data.SaveChanges();

                var harMottagit = new PersonMottagitMeddelande();
                harMottagit.MeddelandeID = id;
                harMottagit.Användarnamn = meddelandeView.Mottagare;
                data.PersonMottagitMeddelande.Add(harMottagit);

                data.SaveChanges();
                ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

                return RedirectToAction("Inloggad", "Home");
            }

            return View(NyttMeddelande);
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
