using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(users);

        }
        [HttpGet]
        public async Task<IActionResult> AddUser()
        {
            var adminAddUserViewModel = new AdminAddUserViewModel();
            var roles = await _roleManager.Roles.Select(r => new AssignRolesViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
            }).ToListAsync();
            adminAddUserViewModel.Roles = roles;
            return View(adminAddUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(AdminAddUserViewModel adminAddUserViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = adminAddUserViewModel.UserName,
                    Email = adminAddUserViewModel.Email,
                    FirstName = adminAddUserViewModel.FirstName,
                    LastName = adminAddUserViewModel.LastName,
                    PhoneNumber = adminAddUserViewModel.PhoneNumber,
                    Address = adminAddUserViewModel.Address,
                    City = adminAddUserViewModel.City,
                    Gender = adminAddUserViewModel.Gender,
                    DateOfBirth = adminAddUserViewModel.DateOfBirth,
                    ImageUrl = adminAddUserViewModel.ImageUrl,
                    EmailConfirmed = true
                };
                var result = await _userManager.CreateAsync(user, adminAddUserViewModel.Password);
                if (result.Succeeded)
                {
                    foreach (var role in adminAddUserViewModel.Roles)
                    {
                        if (role.IsAssigned)
                        {
                            await _userManager.AddToRoleAsync(user, role.RoleName);
                        }
                        else
                        {
                            await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                        }
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(adminAddUserViewModel);
        }
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> EditUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = await _roleManager.Roles.Select(r => new AssignRolesViewModel
            {
                RoleId = r.Id,
                RoleName = r.Name,
                IsAssigned = userRoles.Any(x => x == r.Name)
            }).ToListAsync();
            var userRolesViewModel = new UserRolesViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles,
                Address = user.Address,
                City = user.City,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                PhoneNumber = user.PhoneNumber,
            };
            return View(userRolesViewModel);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> EditUser(UserRolesViewModel userRolesViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userRolesViewModel.Id);
                foreach (var role in userRolesViewModel.Roles)
                {
                    if (role.IsAssigned)
                    {
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    }
                    else
                    {
                        await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                    }
                }
                user.FirstName = userRolesViewModel.FirstName;
                user.LastName = userRolesViewModel.LastName;
                user.UserName = userRolesViewModel.UserName;
                user.Email = userRolesViewModel.Email;
                user.Gender = userRolesViewModel.Gender;
                user.Address = userRolesViewModel.Address;
                user.City = userRolesViewModel.City;
                user.DateOfBirth = userRolesViewModel.DateOfBirth;
                user.PhoneNumber = userRolesViewModel.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    return RedirectToAction("Index");
                }
                foreach(var err in result.Errors) ModelState.AddModelError("", err.Description);
            }
            return View(userRolesViewModel);
        }

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }
    }
}
