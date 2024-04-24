using BlogApp.DAL.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
    public class _BlogListComponentPartial : ViewComponent
    {
        private readonly MyBlogContext _context;
        
        public _BlogListComponentPartial(MyBlogContext context)
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
