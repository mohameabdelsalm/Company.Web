using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Web.Controllers
{

    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger _logger;

        public UserController(UserManager<ApplicationUser> userManager, ILogger<UserController> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            //var users = Enumerable.Empty<RoleUpdateViewModel>();
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(searchInput))
                users = await _userManager.Users.ToListAsync();
            else
                users = await _userManager.Users.Where(U => U.NormalizedEmail.Trim()
                .Contains(searchInput.ToUpper().Trim())).ToListAsync();
     

            return View(users);
        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound(); // 404

            if (ViewName == "Update")
            {
                var userviewmdel = new UserViewModel
                {
                    Id= user.Id,
                    UserName= user.UserName,
                    //Email=userFromDb.Email

                };
                return View(ViewName, userviewmdel);
            }

            else
                return View(ViewName, user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, UserViewModel _user)
        {
            if (_user.Id != id)
                return NotFound(); // 404

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManager.FindByIdAsync(id);
                    if (user is null)
                        return NotFound();

                    user.UserName = _user.UserName;
                    user.NormalizedUserName = _user.UserName.ToUpper();
                    //user.FristName = _user.FristName;
                    //user.LastName = _user.LastName;
                    //user.Email = _user.Email;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User Update Successfully");
                        return RedirectToAction("Index");
                    }
                    foreach (var item in result.Errors)
                        _logger.LogError(item.Description);
                    



                }
                catch (Exception ex)
                {

                    _logger.LogError(ex.Message);
                }

            }
            return View(_user);
        }
        public async Task<IActionResult> Delete(string id)
        {
            
            
                var userFromDb = await _userManager.FindByIdAsync(id);
                if (userFromDb is null)
                    return NotFound(); // 404


               var result= await _userManager.DeleteAsync(userFromDb);
               
            if (result.Succeeded) 
            
                return RedirectToAction(nameof(Index));

            foreach (var item in result.Errors)
                _logger.LogError(item.Description);




            return View("Index");
        }
    }
}
