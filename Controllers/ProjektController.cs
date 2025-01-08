using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class ProjektController(DataContext data) : Controller
    {
        
        public IActionResult VisaProjekt()
        {
            // Lagrar och summerar antalet meddelanden användare fått.
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            // ViewModel
            var model = new PersonProjektViewModel();

            // Hämtar olika projekt beroende på om användaren är inloggad eller ej
            model.PersonProjekt = data.PersonProjekt.ToList();
            model.Person = data.Person.ToList();
            if (User.Identity.IsAuthenticated)
            {
                model.Projekt = (from p in data.Projekt
                                 join o in data.Person on p.Skapare equals o.Användarnamn
                                 select p).ToList();
            }
            else
            {
                model.Projekt = (from p in data.Projekt
                                 join o in data.Person on p.Skapare equals o.Användarnamn
                                 where o.Privat == false
                                 select p).ToList();
            }
            return View(model);

        }
        //för att gå med i projekt
        public IActionResult AnslutTillProjekt(int projectID)
        {
            // Skapar en ny instans av PersonHarMedverkat för att länka personen med projektet
            PersonProjekt personProjekt = new PersonProjekt();
            personProjekt.Projekt = data.Projekt.Find(projectID);

            personProjekt.Person = (from p in data.Person
                                         where p.Användarnamn == User.Identity.Name
                                         select p).FirstOrDefault();

            personProjekt.ProjektID = projectID;
            personProjekt.Medverkande = personProjekt.Person.Användarnamn;
            data.PersonProjekt.Add(personProjekt);
            data.SaveChanges();


            return RedirectToAction("VisaProjekt");
        }
        //för att gå ur projekt
        public IActionResult LämnaProjekt(int project)
        {
            // Hämtar användarens profil-ID och tar bort relationen mellan personen och projektet
            string profileID = (from p in data.Person
                                where p.Användarnamn == User.Identity.Name
                                select p.Användarnamn).FirstOrDefault();


            PersonProjekt profiliP = (from p in data.PersonProjekt
                                             where p.Medverkande == profileID && p.ProjektID == project
                                             select p).FirstOrDefault();

            data.PersonProjekt.Remove(profiliP);
            data.SaveChanges();

            return RedirectToAction("VisaProjekt");
        }
        //startar nytt projekt formuläret
        [HttpGet]
        public IActionResult LäggTillProjekt()
        {
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View();
        }
        //skickar nytt projekt formuläret
        [HttpPost]
        public IActionResult LäggTillProjekt(ProjektViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var projekt = new Projekt();
                projekt.Namn = viewModel.Namn;
                projekt.Beskrivning = viewModel.Beskrivning;


                projekt.Skapare = User.Identity.Name;
                data.Projekt.Add(projekt);
                data.SaveChanges();

                // Lägger till användaren som skapade projektet i projektet
                AnslutTillProjekt(projekt.ProjektID);

                return RedirectToAction("VisaProjekt", "Projekt");
            }
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(viewModel);
        }
        //startar uppdatera projekt formuläret
        [HttpGet]
        [HttpGet]
        public IActionResult ÄndraProjekt(int projectID)
        {
            var projektToUpdate = data.Projekt.Find(projectID);
            if (projektToUpdate == null)
            {
                return NotFound();
            }

            var viewModel = new ProjektViewModel
            {
                ProjektID = projektToUpdate.ProjektID,
                Namn = projektToUpdate.Namn,
                Beskrivning = projektToUpdate.Beskrivning
            };
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(viewModel);
        }

        // Tar emot och hanterar datan från formuläret för att uppdatera ett befintligt projekt
        [HttpPost]
        public IActionResult ÄndraProjekt(int projectID, ProjektViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
                return View(viewModel);
            }

            var theProject = data.Projekt.FirstOrDefault(p => p.ProjektID == projectID);
            if (theProject == null)
            {
                return NotFound();
            }

            theProject.Namn = viewModel.Namn;
            theProject.Beskrivning = viewModel.Beskrivning;

            data.Update(theProject);
            data.SaveChanges();

            return RedirectToAction("VisaProjekt");
        }

        // Metod för att ta bort ett projekt
        public IActionResult TaBortProjekt(int id)
        {
            // Tar bort alla användarrelationer till projektet
            List<PersonProjekt> profileinProjects = data.PersonProjekt.Where(p => p.ProjektID == id).ToList();
            foreach (PersonProjekt profile in profileinProjects)
            {
                data.Remove(profile);
            }

            Projekt theProject = data.Projekt.Find(id);
            data.Remove(theProject);
            data.SaveChanges();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            return RedirectToAction("VisaProjekt");
        }
    }
}