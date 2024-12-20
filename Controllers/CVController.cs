using CVgrupp2Main.DatabasLager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class CVController : Controller
    {
        DataContext dataContext;
        
        public CVController(DataContext data)
        {
            dataContext = data;
        }
        // Visar en privat sida för användarens CV.
        public IActionResult PrivatSidaCV()
        { // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = dataContext.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View();
        }
        // Visar ett CV för en specifik användare baserat på användarnamnet.
        public IActionResult visaCV(string användarnamn)
        {
            // Använder LINQ och Entity Framework för att hämta detaljerad information om en specifik person.
            // Inkluderar relaterad information som erfarenheter, kompetenser, projekt, etc.
            Person person = dataContext.Person.Where(p => p.Användarnamn == användarnamn)
                .Include(p => p.HarErfarenhet)
                    .ThenInclude(p => p.Erfarenheter)
                .Include(p => p.HarKompetens)
                    .ThenInclude(p => p.Kompetenser)
                .Include(p => p.HarMedverkat)
                    .ThenInclude(p => p.Projekt)
                .Include(p => p.HarUtbildning)
                    .ThenInclude(p => p.Utbildningar)
                .Include(p => p.Kontaktuppgifter)
                .Include(p => p.HarMedverkat)
                    .ThenInclude(p => p.Projekt)
                .FirstOrDefault();
            // Räknar och visar antalet olästa meddelanden för användaren.
            ViewBag.AntalMeddelanden = dataContext.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(person);
        }
    }
}
