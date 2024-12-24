using CVgrupp2Main.DatabasLager;
using CVgrupp2Main.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class LoginController(DataContext data, SignInManager<ApplicationUser> signInManager) : Controller
    {
        // Visar inloggningsformuläret.
        [HttpGet]
        public IActionResult LoggaIn()
        {
            var loginViewModel = new LoginViewModel();
            // Räknar och visar antalet olästa meddelanden för användaren.

            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(loginViewModel);
        }
        // Hanterar inloggningen baserat på de uppgifter användaren skickar in.
        [HttpPost]
        public async Task<IActionResult> LoggaIn(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid) // Kontrollerar om formuläret är korrekt ifyllt.
            {
                // Försöker logga in användaren med angivna uppgifter.

                var result = await signInManager.PasswordSignInAsync(loginViewModel.Användarnamn, loginViewModel.Lösenord, loginViewModel.Komihåg, lockoutOnFailure: false);
                if (result.Succeeded) // Om inloggningen lyckas, navigera till startsidan.
                {

                    return RedirectToAction("Inloggad", "Home");
                }
                else
                {
                    // Om inloggningen misslyckas, visa ett felmeddelande.

                    ModelState.AddModelError(string.Empty, "Ogiltigt inlogg");

                    ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
                    return View(loginViewModel);
                }
            }

            ViewBag.AntalMeddelanden = data.PersonMottagitMeddelande.Where(m => m.Användarnamn == User.Identity.Name && !m.Meddelande.HarLästs).Count();
            return View(loginViewModel);         // Om modellstaten inte är giltig, visa inloggningsformuläret igen.

        }
        // Hanterar utloggningen.

        [HttpPost]
        public async Task<IActionResult> LoggaUt()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
