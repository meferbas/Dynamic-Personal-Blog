using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _NavbarComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
