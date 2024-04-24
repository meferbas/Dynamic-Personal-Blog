using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _FooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
