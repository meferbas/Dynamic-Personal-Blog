using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogApp.DAL.Entities; // User sınıfınızın olduğu namespace
using BlogApp.ViewModels; // View modellerinizin olduğu namespace
using BlogApp.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

public class UserController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly EmailSettings _emailSettings;

    public UserController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<EmailSettings> emailSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailSettings = emailSettings.Value;
    }

    public IActionResult Index()
	{
		return View();
	}

	[HttpGet]
	public IActionResult Login()
	{
		return View();
	}
    [HttpPost]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (!ModelState.IsValid)
		{
			if (string.IsNullOrWhiteSpace(model.UserName))
			{
				ModelState.AddModelError(nameof(model.UserName), "Kullanıcı adı boş olamaz.");
			}
			if (string.IsNullOrWhiteSpace(model.Password))
			{
				ModelState.AddModelError(nameof(model.Password), "Şifre boş olamaz.");
			}
			return View(model);
		}

		var user = await _userManager.FindByNameAsync(model.UserName);
		if (user != null && !await _userManager.IsEmailConfirmedAsync(user))
		{
			ModelState.AddModelError(string.Empty, "Giriş yapabilmeniz için email doğrulaması yapmanız gerekmektedir.");
			return View(model);
		}

		var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);

		if (!result.Succeeded)
		{
			ModelState.AddModelError(string.Empty, "Hatalı şifre veya kullanıcı adı.");
			return View(model);
		}

		return RedirectToAction("Index", "Default");
	}




	[HttpGet]
	public IActionResult Register()
	{
		return View();
	}

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new User
            {
                UserName = model.UserName, // Kullanıcı adı olarak e-posta kullanılıyor
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                // Token oluştur
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                // Geri arama URL'si oluştur
                var callbackUrl = Url.Action("ConfirmEmail", "User",
                    new { userId = user.Id, token = token }, protocol: Request.Scheme);

                // E-posta gönder
                await SendConfirmationEmail(user.Email, callbackUrl);

                // Kayıt sonrası bilgilendirme veya başka bir sayfaya yönlendirme
                return RedirectToAction("Login", "User");
            }

            AddErrors(result);
        }

        return View(model);
    }


    private async Task SendConfirmationEmail(string email, string callbackUrl)
    {
        using (var client = new SmtpClient("smtp.example.com", 587))
        {

            client.Host = _emailSettings.MailServer;
            client.Port = _emailSettings.MailPort;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(_emailSettings.Sender, _emailSettings.Password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Sender, _emailSettings.SenderName),
                Subject = "Confirm your email",
                Body = $"Please confirm your email by clicking here: <a href='{callbackUrl}'>link</a>",
                IsBodyHtml = true
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }

    [HttpGet]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if (userId == null || token == null)
        {
            return RedirectToAction("Error", "Home");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
            return View("Error");
        }

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return View("ConfirmEmailSuccess");
        }

        ViewBag.ErrorTitle = "Email cannot be confirmed";
        return View("Error");
    }


    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index","Default");
    }

    private void AddErrors(IdentityResult result)
	{
		foreach (var error in result.Errors)
		{
			ModelState.AddModelError(string.Empty, error.Description);
		}
	}

	private IActionResult RedirectToLocal(string returnUrl)
	{
		if (Url.IsLocalUrl(returnUrl))
		{
			return Redirect(returnUrl);
		}
		else
		{
			return RedirectToAction(nameof(Index), "Home");
		}
	}
}
