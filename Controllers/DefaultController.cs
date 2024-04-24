using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


    }
}
