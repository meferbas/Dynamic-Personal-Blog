using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _TitleComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
