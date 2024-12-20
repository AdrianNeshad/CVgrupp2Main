using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
