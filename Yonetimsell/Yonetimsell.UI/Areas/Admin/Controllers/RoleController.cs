using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Yonetimsell.Entity.Concrete.Identity;

namespace Yonetimsell.UI.Areas.Admin.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin")]
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult AddRole()
        {
            var role = new Role();
            return View(role);
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(Role role)
        {
            await _roleManager.CreateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> EditRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            return View(role);
        }
        [HttpPost]
        public async Task<ActionResult> EditRole(Role role)
        {
            await _roleManager.UpdateAsync(role);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("Index");
        }
    }
}
