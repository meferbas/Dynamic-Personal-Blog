using System;
using System.Linq;
using BlogApp.DAL.Context;
using BlogApp.DAL.Entities;
using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class CommentController : Controller
	{
		private readonly MyBlogContext _context;
		private readonly UserManager<User> _userManager;

		public CommentController(MyBlogContext context, UserManager<User> userManager) // Constructor
		{
			_context = context; // Dependency Injection ile MyBlogContext nesnesi alınıyor
			_userManager = userManager;
		}

		[HttpPost]
		[Authorize]
		public IActionResult AddComment(int postId, string content)
		{
			var userId = _userManager.GetUserId(User); // Kullanıcı ID'sini alır
			var comment = new Comment
			{
				Content = content,
				CreatedAt = DateTime.Now,
				PostId = postId,
				UserId = userId
			};

			_context.Comments.Add(comment);
			_context.SaveChanges();

			return RedirectToAction("Detail", "Post", new { id = postId });
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
