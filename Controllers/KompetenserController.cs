using Microsoft.AspNetCore.Mvc;

namespace CVgrupp2Main.Controllers
{
    public class KompetenserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
