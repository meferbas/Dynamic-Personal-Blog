using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using BlogApp.DAL.Entities; // User sınıfınızın olduğu namespace
using BlogApp.ViewModels; // View modellerinizin olduğu namespace

public class UserController : Controller
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;

	public UserController(UserManager<User> userManager, SignInManager<User> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
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
            return View(model);
        }

        if (string.IsNullOrWhiteSpace(model.UserName))
        {
            ModelState.AddModelError(string.Empty, "Kullanıcı adı boş olamaz.");
            return View(model); // Kullanıcıyı aynı sayfaya geri yönlendirerek hata mesajını gösterin
        }

        var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Default"); // Giriş başarılıysa, başka bir sayfaya yönlendirin
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        return View(model); // Başarısız giriş denemesi durumunda kullanıcıya hata mesajını gösterin
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
                UserName = model.Email, // Kullanıcı adı olarak e-posta kullanılıyor
                Email = model.Email,
                FirstName = model.FirstName, // FirstName ekleniyor
                LastName = model.LastName   // LastName ekleniyor
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index","Default");
            }

            AddErrors(result);
        }

        return View(model);
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
