using Microsoft.EntityFrameworkCore;
using BlogApp.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BlogApp.DAL.Context
{
    public class MyBlogContext : IdentityDbContext<User>
	{
		public MyBlogContext(DbContextOptions<MyBlogContext> options) : base(options) // Dependency Injection ile DbContextOptions<MyBlogContext> alınıyor
		{
		}

		public DbSet<Post> Posts { get; set; } // Posts tablosu
        public DbSet<PostDetail> PostDetails { get; set; } // PostDetails tablosu

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder); // Identity ile ilgili yapılandırmaları eklemek için bu satır önemli ve ilk sırada olmalı

			modelBuilder.Entity<Post>()
						.HasMany(p => p.PostDetails) // Post ve PostDetails arasında bir-çok ilişki kur
						.WithOne(pd => pd.Post) // Her PostDetail tam olarak bir Post'a bağlı
						.HasForeignKey(pd => pd.PostId); // ForeignKey tanımı
		}
		public DbSet<Footer> Footers { get; set; } // Footers tablosu
		public DbSet<Title> Titles { get; set; } // Titles tablosu



	}
}
