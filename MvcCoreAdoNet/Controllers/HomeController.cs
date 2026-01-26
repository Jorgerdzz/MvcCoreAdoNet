using Microsoft.AspNetCore.Mvc;

namespace MvcCoreAdoNet.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VistaEscudoLaurel()
        {
            return View();
        }

        public IActionResult VistaEscudo()
        {
            return View();
        }

        public IActionResult VistaFrenteAtletico()
        {
            return View();
        }

    }
}
