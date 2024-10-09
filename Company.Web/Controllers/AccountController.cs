using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace Company.Web.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}
		#region SginUp
		public IActionResult SginUp()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> SginUp(SignupViewModel input)
		{
			if (ModelState.IsValid)
			{
				var user = new ApplicationUser
				{
					UserName = input.Email.Split("@")[0],
					FristName = input.FristName,
					LastName = input.LastName,
					Email = input.Email,
					IsActive = true
				};
				var result = await _userManager.CreateAsync(user, input.Password);

				if (result.Succeeded)
				{
					return RedirectToAction("SignIn");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("", error.Description);
				}
			}
			return View(input);
		} 
		#endregion

		#region Login
		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{

			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var Flag = await _userManager.CheckPasswordAsync(user, model.Password);
					if (Flag)
					{
						var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
						if (result.Succeeded)
						{
							return RedirectToAction("Index", "Home");
						}
					}
				}

				ModelState.AddModelError(string.Empty, "Invalid Login!");
			}

			return View(model);
		}

		//Google Login
		//public IActionResult GoogleLogin()
		//{
		//	var prop = new AuthenticationProperties
		//	{
		//		RedirectUri = Url.Action("GoogleResponse")
		//	};
		//	return Challenge(prop, GoogleDefaults.AuthenticationScheme);


		//}

		//public async Task<IActionResult> GoogleResponse()
		//{
		//	var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

		//	var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(
		//		claim => new
		//		{
		//			claim.Issuer,
		//			claim.OriginalIssuer,
		//			claim.Type,
		//			claim.Value
		//		});

		//	return RedirectToAction("Index", "Home");
		//}


		#endregion

		#region SignOut
		public new async Task<ActionResult> SignOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction(nameof(Login));
		}
		#endregion

	}
}
