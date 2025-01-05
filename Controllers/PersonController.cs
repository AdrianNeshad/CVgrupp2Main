using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CVgrupp2Main.Controllers
{
    public class PersonController(DataContext data, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) : Controller
    {
        [HttpGet]
        public IActionResult LäggTillAnvändare()
        {
            return View();
        }
        // Hanterar inlämningen av det nya personformuläret.

        [HttpPost]
        public async Task<IActionResult> LäggTillAnvändare(PersonViewModel viewModel)
        {
            if (ModelState.IsValid) // Kontrollerar om modellens data är giltig.
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = viewModel.Användarnamn;

                var result = await userManager.CreateAsync(user, viewModel.Lösenord);
                if (result.Succeeded)
                {
                    // Skapar en ny Person och tillhörande kontaktuppgifter.

                    var person = new Person()
                    {
                        Användarnamn = viewModel.Användarnamn,
                        Lösenord = viewModel.Lösenord,
                        Privat = false,
                        Förnamn = "",
                        Efternamn = "",

                    };

                    var kontaktuppgifter = new Kontaktuppgifter
                    {
                        Adress = "",
                        Telefonnummer = "",
                        Email = ""
                    };

                    data.Kontaktuppgifter.Add(kontaktuppgifter);
                    person.Kontaktuppgifter = kontaktuppgifter; // Länka kontaktuppgifter till personen

                    data.Person.Add(person);
                    data.SaveChanges();
                    await signInManager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Inloggad", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(viewModel);
        }
        // Visar formulär för att ladda upp en profilbild.

        public IActionResult LäggTillProfilbild()
        {
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            return View();
        }
        // Hanterar uppladdningen av en profilbild.

        [HttpPost]
        public async Task<IActionResult> LäggTillProfilbild(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    byte[] imageBytes = memoryStream.ToArray();


                    var person = data.Person.Where(p => p.Användarnamn == User.Identity.Name).FirstOrDefault();

                    if (person != null)
                    {
                        person.ProfilBild = imageBytes;
                        await data.SaveChangesAsync();
                    }
                }
            }

            return RedirectToAction("Profil", "Användare");
        }
        // Visar en vy med användarens profilbild.

        public IActionResult VisaProfilbild()
        {
            var personer = data.Person.ToList();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            return View(personer);
        }

        // Visar användarens profilvy.

        public IActionResult Profil()
        {
            var person = data.Person.Where(p => p.Användarnamn == User.Identity.Name).FirstOrDefault();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();

            return View(person);
        }
        // Visar formulär för att ändra lösenord.

        [Authorize]
        public IActionResult ÄndraLösenord()
        {
            LösenordÄndringViewModel model = new LösenordÄndringViewModel();
            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(model);
        }
        // Hanterar ändring av lösenord.

        [HttpPost]
        public async Task<IActionResult> ÄndraLösenord(LösenordÄndringViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await userManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.ChangePasswordAsync(user, model.GammaltLösenord, model.NyttLösenord);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }

            return RedirectToAction("Profil", "Användare");
        }
        // Hanterar ändring av kontots privatstatus.

        [HttpPost]
        public async Task<IActionResult> ÄndraStatus(bool privat)
        {
            var person = await data.Person
                .FirstOrDefaultAsync(p => p.Användarnamn == User.Identity.Name);

            if (person != null)
            {

                person.Privat = privat;

                // Spara ändringarna i databasen
                await data.SaveChangesAsync();
            }

            // Återgå till användarens profil sida eller annan relevant vy
            return RedirectToAction("Profil", "Användare");
        }
    }
}
