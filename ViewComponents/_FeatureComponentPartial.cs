using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _FeatureComponentPartial: ViewComponent
    {
        private readonly MyBlogContext _context;

        public _FeatureComponentPartial(MyBlogContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Posts.ToList();
            return View(values);
        }
    }
}
