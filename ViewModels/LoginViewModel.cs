using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels
{
	public class LoginViewModel
	{
        [Required(ErrorMessage = "Kullanıcı adı alanı gereklidir.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        public string Password { get; set; }


		public bool RememberMe { get; set; }

    }
}
