namespace BlogApp.DAL.Entities
{
	public class Post
	{
		public int PostId { get; set; }
		public virtual ICollection<PostDetail> PostDetails { get; set; } = new List<PostDetail>();
		public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

		public string? Title { get; set; }
		public string? Content { get; set; }
		public string? Path { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime DateTime { get; set; }
	}

}
