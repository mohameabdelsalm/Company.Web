using Company.Data.Entites;
using Company.web.Entites;
using Company.Web.Helper;
using Company.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

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
					return RedirectToAction("Login");
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

		
		#region ForgetPassword
		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}


		[HttpPost]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					// Generate Token

					var token = await _userManager.GeneratePasswordResetTokenAsync(user);

					// create URL Which Send in Body of The Email

					var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

					// https://localhost:44384/Account/ResetPassword?email=ahmed@gmail.com&token=

					// Create Email

					var email = new SendEmail()
					{
						To = model.Email,
						Subject = "Reset Password",
						Body = url,
					};

					// Send Email

					EmailSetting.SendEmail(email);
					//_mailService.SendMail(email);

					return RedirectToAction(nameof(CheckYourInbox));
				}

				ModelState.AddModelError(string.Empty, "Invalid Email!");

			}

			return View(nameof(ForgetPassword), model);
		}

		//send sms
		//[HttpPost]
		//public async Task<IActionResult> SendSms(ForgetPasswordViewModel model)
		//{
		//	if (ModelState.IsValid)
		//	{
		//		var user = await _userManager.FindByEmailAsync(model.Email);
		//		if (user is not null)
		//		{
		//			// Generate Token

		//			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

		//			// create URL Which Send in Body of The Email

		//			var url = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme);

		//		//https://localhost:44384/Account/ResetPassword?email=ahmed@gmail.com&token=

		//			// Create Email

		//			//var sms = new SmsMessage()
		//			//{
		//			//	PhoneNumber = user.PhoneNumber,
		//			//	Body = url,
		//			//};

		//			// Send Email

		//			//EmailSettings.SendEmail(email);
		//			// _mailService.SendMail(email);
		//			//_smsService.Send(sms);

		//			//return RedirectToAction(nameof(CheckYourInbox));
		//			return Ok("Check Your Phone");
		//		}

		//		ModelState.AddModelError(string.Empty, "Invalid Email!");

		//	}

		//	return View(nameof(ForgetPassword), model);
		//}




		public IActionResult CheckYourInbox()
		{
			return View();
		}

		#endregion
		

	}
}
