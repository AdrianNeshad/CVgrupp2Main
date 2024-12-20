using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class ProjektController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
