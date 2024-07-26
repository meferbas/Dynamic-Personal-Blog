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
		private readonly MyBlogContext _context; // Veritabanı işlemleri için MyBlogContext nesnesi

		public PostController(MyBlogContext context) // Constructor
		{
			_context = context; // Dependency Injection ile MyBlogContext nesnesi alınıyor
		}

		public IActionResult Detail(int id) // Post detaylarını gösteren Action
		{
			var currentPost = _context.Posts
									  .Include(p => p.PostDetails) // Post ile ilişkili detaylar yükleniyor
									  .Include(p => p.Comments) // Post ile ilişkili yorumlar yükleniyor
									  .ThenInclude(c => c.User) // Yorum yapan kullanıcılar yükleniyor
									  .FirstOrDefault(p => p.PostId == id); // Sadece belirli bir ID'ye sahip post

			if (currentPost == null)
			{
				return NotFound($"Post with ID {id} not found."); // Post bulunamazsa 404 hatası döndür
			}

			var postViewModel = new PostViewModel // PostViewModel nesnesi oluşturuluyor
			{
				CurrentPost = currentPost,
				Comments = currentPost.Comments.ToList(),
				PostDetails = currentPost.PostDetails.ToList(),
				Posts = _context.Posts.ToList() // Diğer tüm postlar yükleniyor
			};

			return View(postViewModel); // PostViewModel ile view döndürülüyor
		}

		[HttpGet]
        [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar bu işlemi yapabilir
        public IActionResult List()
        {
            var posts = _context.Posts.Include(p => p.PostDetails).ToList();
            return View(posts);
        }

        [HttpGet]
		[Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar bu işlemi yapabilir
        public IActionResult Create()
		{
			return View();  // Create için bir form göster
		}

       
        [HttpPost] // Sadece POST isteklerine yanıt verecek
        [ValidateAntiForgeryToken] // CSRF saldırılarına karşı koruma
        public IActionResult Create(Post post, PostDetail postDetail) // Formdan gelen verileri alacak
		{
			if (ModelState.IsValid) // Formdan gelen veriler doğruysa
			{
                post.PostDetails.Add(postDetail); // Post'a ait detayları ekliyoruz
                _context.Posts.Add(post); // Post'u ekliyoruz
				_context.SaveChanges(); // Değişiklikleri kaydediyoruz
				return RedirectToAction("Index", "Default"); // Index sayfasına yönlendiriyoruz
			}
			return View(post); // Hatalı verilerle tekrar formu göster
		
		}
		[HttpGet] // Sadece GET isteklerine yanıt verecek
        [Authorize(Roles = "Admin")] // Sadece Admin rolüne sahip kullanıcılar bu işlemi yapabilir
        public IActionResult Delete() // Silme işlemi için bir form göster
		{
            return View(); 
        }

        [HttpPost] // Sadece POST isteklerine yanıt verecek
        public IActionResult Delete(int id) // Formdan gelen verileri alacak
        {
            var post = _context.Posts.Include(p => p.PostDetails).FirstOrDefault(p => p.PostId == id); // ID'ye göre postu bul
            if (post == null) // Post bulunamazsa
            {
                return NotFound($"Post with ID {id} not found."); // 404 hatası döndür
            }
            _context.Posts.Remove(post); // Postu sil
            _context.SaveChanges(); // Değişiklikleri kaydet
            return RedirectToAction("List"); // List sayfasına yönlendir
        }


    }
}
