using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class CVController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
