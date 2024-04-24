using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.DAL.Entities
{
	public class PostDetail
	{
		public int PostDetailId { get; set; }

		[ForeignKey("PostId")]
		public int PostId { get; set; } // Foreign Key
		public Post? Post { get; set; }

		public string? Title { get; set; }
		public string? Content { get; set; }
		public string? Content2 { get; set; }
		public string? Content3 { get; set; }
		public string? Content4 { get; set; }
		public string? KeyWord { get; set; }
		public string? KeyWord2 { get; set; }
		public string? KeyWord3 { get; set; }
		public string? KeyWord4 { get; set; }
		public string? Path { get; set; }
		public string? ImageUrl { get; set; }
		public DateTime DateTime { get; set; }

	}
}
