using BlogApp.DAL.Context;
using BlogApp.DAL.Entities;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		private readonly MyBlogContext _context;

		public PostController(MyBlogContext context)
		{
			_context = context;
		}

		public IActionResult Detail(int id)
		{
			var currentPost = _context.Posts
									  .Include(p => p.PostDetails) // Post ile ilişkili detaylar yükleniyor
									  .FirstOrDefault(p => p.PostId == id); // Sadece belirli bir ID'ye sahip post

			if (currentPost == null)
			{
				return NotFound($"Post with ID {id} not found.");
			}

			var postViewModel = new PostViewModel
			{
				PostDetails = currentPost.PostDetails.ToList(), 
				Posts = _context.Posts.ToList()
			};

			return View(postViewModel);
		}


        [Authorize(Roles = "Admin")]
        [HttpGet]
		public IActionResult Create()
		{
			return View();  // Create için bir form göster
		}
        [Authorize(Roles = "Admin")]
        [HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Post post)
		{
			if (ModelState.IsValid)
			{
				_context.Posts.Add(post);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(post);
		}

	}
}
