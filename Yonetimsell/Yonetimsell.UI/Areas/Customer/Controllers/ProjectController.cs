using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Abstract;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.Extensions;
using Yonetimsell.Shared.ViewModels;
using Yonetimsell.Shared.ViewModels.ProjectViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class ProjectController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IProjectService _projectManager;

        public ProjectController(UserManager<User> userManager, IProjectService projectManager)
        {
            _userManager = userManager;
            _projectManager = projectManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var projects = await _projectManager.GetProjectsByUserIdAsync(userId);
            if (!projects.IsSucceeded)
            {
                return View();
            }
            foreach (var p in projects.Data)
            {
                var designatedDays = (int)Math.Round((p.EndDate - p.CreatedDate).TotalMinutes);
                designatedDays = Math.Max(designatedDays, 1);
                var passingDays = (int)Math.Round((DateTime.Now - p.CreatedDate).TotalMinutes);
                var remainingDays = (int)Math.Round((p.EndDate - DateTime.Now).TotalMinutes);
                double progressedDaysPercentage = 0; // İlerleme yüzdesi başlangıçta sıfır olarak ayarlanır
                if (designatedDays != 0)
                {
                    progressedDaysPercentage = (passingDays / designatedDays) * 100;
                }
                var result = new ProgressTime
                {
                    DesignatedDays = designatedDays,
                    PassingDays = passingDays,
                    RemainingDays = remainingDays,
                    ProgressedDaysPercentage = progressedDaysPercentage
                };
                p.ProgressTime = result;
            }
            return View(projects.Data);
        }
        public IActionResult Create()
        {
            return View(new AddProjectViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddProjectViewModel addProjectViewModel)
        {
            addProjectViewModel.UserId = _userManager.GetUserId(User);
            addProjectViewModel.EndDate = DateTime.Now;
            var result = await _projectManager.CreateAsync(addProjectViewModel);
            if (result.IsSucceeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "hata var");
            return View(result);
        }
    }
}
