using BlogApp.DAL.Entities;

namespace BlogApp.ViewModels
{
		public class PostViewModel
		{
			public Post CurrentPost { get; set; }  
			public List<Post> Posts { get; set; } 
			public List<PostDetail> PostDetails { get; set; }  
		}

}
