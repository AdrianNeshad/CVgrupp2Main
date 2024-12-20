using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class UtbildningarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
