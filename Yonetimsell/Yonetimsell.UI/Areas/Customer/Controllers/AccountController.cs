using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ComplexTypes;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;

namespace Yonetimsell.UI.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly MapperlyConfiguration _mapperly;
        private readonly ISweetAlertService _sweetAlert;
        private readonly IUploadService _uploadManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, MapperlyConfiguration mapperly, ISweetAlertService sweetAlert, IUploadService uploadManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapperly = mapperly;
            _sweetAlert = sweetAlert;
            _uploadManager = uploadManager;
        }

        public async Task<IActionResult> Profile()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var editUserViewModel = _mapperly.UserToEditUserViewModel(user);
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Profile(EditUserViewModel editUserViewModel)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                user.Email = editUserViewModel.Email;
                user.City = editUserViewModel.City;
                user.Address = editUserViewModel.Address;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.DateOfBirth = editUserViewModel.DateOfBirth;
                user.Gender = editUserViewModel.Gender;
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user, false);
                    TempData["ProfileToast"] = _sweetAlert.MiddleNotification("success", "Profiliniz başarıyla güncellendi");
                    return RedirectToAction("Profile");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            TempData["ProfileToast"] = _sweetAlert.MiddleNotification("error", "Lütfen bilgileri kontrol ediniz.");
            return View(editUserViewModel);
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(_userManager.GetUserId(User));
                var isVerified = await _userManager.CheckPasswordAsync(user, changePasswordViewModel.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.CurrentPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded)
                    {
                        var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, changePasswordViewModel.NewPassword, false, false);
                        TempData["ChangePasswordToast"] = _sweetAlert.MiddleNotification("success", "Şifreniz başarıyla değiştirildi.");
                        return RedirectToAction("Profile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
                TempData["ChangePasswordToast"] = _sweetAlert.MiddleNotification("error", "Geçerli şifreniz hatalıdır.");
                ModelState.AddModelError("", "Geçerli şifreniz hatalıdır.");
            }
            return View();
        }
        public IActionResult ChangePicture()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ChangePicture(IFormFile image)
        {
            if (image == null)
            {
                TempData["ChangePictureToast"] = _sweetAlert.MiddleNotification("error", "Resim yüklenirken bir hata oluştu");
                return View();
            }
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            user.ImageUrl = await _uploadManager.UploadFile(image, FolderName.Users);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.UpdateSecurityStampAsync(user);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(user, false);
                TempData["ChangePictureToast"] = _sweetAlert.MiddleNotification("success", "Resim başarıyla değiştirildi.");
                return Redirect("/Customer");
            }
            TempData["ChangePictureToast"] = _sweetAlert.MiddleNotification("error", "Resim yüklenirken bir hata oluştu");
            return View();
        }
    }
}
