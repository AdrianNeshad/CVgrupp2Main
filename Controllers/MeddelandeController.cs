using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class MeddelandeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
