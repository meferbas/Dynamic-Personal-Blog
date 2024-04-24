using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _HeadComponentPartial : ViewComponent
    {    
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
