using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
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
                    FristName=input.FristName,
                    LastName = input.LastName,
                    Email = input.Email,
                    IsActive=true
                };
               var result=await _userManager.CreateAsync(user,input.Password);

                if (result.Succeeded) 
                {
                    return RedirectToAction("SignIn");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("",error.Description);
                }
            }
            return View(input);
        }
    }
}
