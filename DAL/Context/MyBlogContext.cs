using Microsoft.EntityFrameworkCore;
using BlogApp.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogApp.DAL.Context
{
	public class MyBlogContext : IdentityDbContext<User>
	{
		public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
		{
		}

		public DbSet<Post> Posts { get; set; }
		public DbSet<PostDetail> PostDetails { get; set; }
		public DbSet<Comment> Comments { get; set; } // Yorumlar tablosu eklendi

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Post>()
						.HasMany(p => p.PostDetails)
						.WithOne(pd => pd.Post)
						.HasForeignKey(pd => pd.PostId);
		}

		public DbSet<Footer> Footers { get; set; }
		public DbSet<Title> Titles { get; set; }
	}
}
