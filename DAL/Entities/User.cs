using Microsoft.AspNetCore.Identity;

namespace BlogApp.DAL.Entities
{
	public class User : IdentityUser
	{
		public string FirstName { get; set; }

		public string LastName { get; set; }
		public UserRole Role { get; set; } // Kullanıcı rolü
	}

	public enum UserRole
	{
		Admin,
		User
	}

}
