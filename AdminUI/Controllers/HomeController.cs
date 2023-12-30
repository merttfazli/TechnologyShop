using Microsoft.AspNetCore.Mvc;

namespace AdminUI.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        [Route("dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }
    }
}
