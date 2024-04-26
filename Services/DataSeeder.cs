using BlogApp.DAL.Entities;
using Microsoft.AspNetCore.Identity;

namespace BlogApp.Services
{
	public static class DataSeeder
	{
		public static async Task SeedSuperAdminAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
		{
			// Admin rolü oluştur
			var adminRole = new IdentityRole("Admin");
			if (!await roleManager.RoleExistsAsync(adminRole.Name))
			{
				await roleManager.CreateAsync(adminRole);
			}

			// User rolü oluştur
			var userRole = new IdentityRole("User");
			if (!await roleManager.RoleExistsAsync(userRole.Name))
			{
				await roleManager.CreateAsync(userRole);
			}

			// Admin kullanıcıyı oluştur
			// Bu kod örnek bir admin kullanıcı oluşturmak için kullanılır.
			// Gerçek bir sistemde, güvenli ve gizli tutulmalıdır.
			// Kullanmadan önce yer tutucu bilgileri gerçek bilgilerle değiştirin.

			/*
			var adminUser = new User
			{
				UserName = "admin_username",
				Email = "admin@admin.com",
				FirstName = "Admins_Name",
				LastName = "Admins_LastName",
				EmailConfirmed = true
			};
			// Burada adminUser kullanıcı oluşturma ve role atama işlemleri yapılabilir.
			*/


			// Admin kullanıcısı yoksa oluştur

			//if (userManager.Users.All(u => u.Id != adminUser.Id))
			//{
			//	var user = await userManager.FindByEmailAsync(adminUser.Email);
			//	if (user == null)
			//	{
			//		await userManager.CreateAsync(adminUser, "Your_Password");
			//		await userManager.AddToRolesAsync(adminUser, new[] { adminRole.Name });
			//	}
			//}
		}
	}

}
