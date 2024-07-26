using BlogApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace BlogApp.Services
{
	public static class DataSeeder
	{
		public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			// Rol oluşturma
			var roles = new List<string> { "Admin", "User" };
			foreach (var roleName in roles)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}

			// Admin kullanıcısı oluştur
			var adminEmail = "admin@admin.com";
			var adminUser = await userManager.FindByEmailAsync(adminEmail); // Admin kullanıcısı var mı kontrol et
			if (adminUser == null) // Eğer yoksa oluştur
			{
				// Admin kullanıcısı oluştur ve Admin rolüne ekle (Burada kendi bilgilerinizi girebilirsiniz)
				adminUser = new User
				{
					UserName = "your_username",
					Email = adminEmail,
					FirstName = "your_name",
					LastName = "your_lastname",
					EmailConfirmed = true
				};

				var createUserResult = await userManager.CreateAsync(adminUser, "Your_Password"); // Admin kullanıcısını oluştur (şifrenizi girin)
				if (createUserResult.Succeeded) // Eğer kullanıcı oluşturulduysa
				{
					await userManager.AddToRoleAsync(adminUser, "Admin"); // Admin rolünü ekle
				}
				else
				{
					throw new Exception("Admin kullanıcısı oluşturulamadı: " + createUserResult.Errors.FirstOrDefault()?.Description);
				}
			}
			else
			{
				// Eğer kullanıcı zaten varsa ve Admin rolüne sahip değilse role ekle
				if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
				{
					await userManager.AddToRoleAsync(adminUser, "Admin");
				}
			}
		}
	}
}
