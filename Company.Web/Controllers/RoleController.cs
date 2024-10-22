using Company.Data.Entites;
using Company.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Library.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RoleController> _logger;

        // Get, GetAll, Add, Update , Delete
        // Index, Create, Details, Edit, Delete

        public RoleController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager,ILogger<RoleController> logger)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchInput)
        {
            var roles = Enumerable.Empty<IdentityRole>();


            if (string.IsNullOrEmpty(searchInput))
            {
                roles = await _roleManager.Roles.ToListAsync();
            }
            else
            {

                roles = await _roleManager.Roles.Where(U => U.Name
                                  .ToLower()
                                  .Contains(searchInput.ToLower()))
                                  .Select(R => new IdentityRole()
                                  {
                                      Id = R.Id,
                                      Name = R.Name
                                  }).ToListAsync();

            }

            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RoleViewModel model)
        {
            if (ModelState.IsValid)

 
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
                };

                await _roleManager.CreateAsync(role);
                return RedirectToAction(nameof(Index));
            }


            return View(model);
        }

        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(); // 404

               var roleViewmodel = new RoleViewModel
               {
                   Id = role.Id,
                   Name = role.Name,

               };

               return View(ViewName, roleViewmodel);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {

            return await Details(id, "Update");
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, RoleViewModel roleModel)
        {
            if (roleModel.Id != id)
                return NotFound(); // 404

            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManager.FindByIdAsync(id);


                    if (role is null)
                        return NotFound();

                    role.Name = roleModel.Name;
                    role.NormalizedName = roleModel.Name.ToUpper();

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Role Update Successfully");
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
            return View(roleModel);
        }
        public async Task<IActionResult> Delete(string id)
        {


            var role = await _roleManager.FindByIdAsync(id);
            if (role is null)
                return NotFound(); // 404


            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)

                return RedirectToAction(nameof(Index));

            foreach (var item in result.Errors)
                _logger.LogError(item.Description);


            return View("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            ViewData["RoleId"] = roleId;

            var usersInRole = new List<UsersInRoleViewModel>();
            var users = await _userManager.Users.ToListAsync();

            foreach (var user in users)
            {
                var userInRole = new UsersInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;

                }
                usersInRole.Add(userInRole);
            }

            return View(usersInRole);

        }
        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(string roleId, List<UsersInRoleViewModel> users)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                foreach (var user in users)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if (appUser is not null)
                    {
                        if (user.IsSelected && !await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        }
                        else if (!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                        {
                            await _userManager.RemoveFromRoleAsync(appUser, role.Name);
                        }
                    }

                }
                return RedirectToAction(nameof(Update), new { id = roleId });
            }


            return View(users);


        }
    }
}
