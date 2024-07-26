using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.DAL.Entities
{
	public class Comment
	{
		[Key]
		public int CommentId { get; set; }
		[Required]
		public string Content { get; set; }
		public DateTime CreatedAt { get; set; }
		public string UserId { get; set; }
		public int PostId { get; set; }

		[ForeignKey("UserId")]
		public virtual User User { get; set; }
		[ForeignKey("PostId")]
		public virtual Post Post { get; set; }
	}
}
