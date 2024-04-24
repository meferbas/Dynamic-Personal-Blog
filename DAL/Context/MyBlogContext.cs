using Microsoft.EntityFrameworkCore;
using BlogApp.DAL.Entities;

namespace BlogApp.DAL.Context
{
    public class MyBlogContext : DbContext
    {
        public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options)
        {
        }

        public DbSet<Post> Posts { get; set; }
        public DbSet<PostDetail> PostDetails { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Post>()
						.HasMany(p => p.PostDetails) // Post ve PostDetails arasında bir-çok ilişki kur
						.WithOne(pd => pd.Post) // Her PostDetail tam olarak bir Post'a bağlı
						.HasForeignKey(pd => pd.PostId); // ForeignKey tanımı

			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Footer> Footers { get; set; }
		public DbSet<Title> Titles { get; set; }



	}
}
