using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.UI.EmailServices.Abstract;

namespace Yonetimsell.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //RegisterViewModel'ıma daha karar vermedim sonra düzenlenecek.
        //[HttpPost]
        //public IActionResult Register()
        //{
        //    return View();
        //}
        [HttpGet]
        public IActionResult Login(string returnUrl = null) 
        {
            if (returnUrl != null)
            {
                TempData["ReturnUrl"] = returnUrl;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.Username);
                if (user != null)
                {
                    await _signInManager.SignOutAsync();
                    var isComfirmed = await _userManager.IsEmailConfirmedAsync(user);
                    if (!isComfirmed)
                    {
                        Console.WriteLine("mail onaylı değil");
                        return View(loginViewModel);
                    }
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);
                    var failedAttempCount = await _userManager.GetAccessFailedCountAsync(user);
                    var lockoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
                    var attempCount = _signInManager.Options.Lockout.MaxFailedAccessAttempts;
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);
                        await _userManager.SetLockoutEndDateAsync(user, null);
                        var returnUrl = TempData["ReturnUrl"]?.ToString();
                        if (!String.IsNullOrEmpty(returnUrl))
                        {
                            return Redirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");
                    }else if (result.IsLockedOut)
                    {
                        var timeLeft = (lockoutEndDate.Value - DateTime.Now).Seconds;
                        return View(loginViewModel);
                    }else
                    {
                        if (failedAttempCount < attempCount && !result.IsLockedOut)
                        {
                            var accessFailedCount = attempCount - failedAttempCount;
                            //_notyfService.Information($"Kalan hakkınız: {accessFailedCount}");
                        }
                        else
                        {
                            await _userManager.SetLockoutEnabledAsync(user, true);
                            //_notyfService.Information($"Hesabınız {lockoutEndDate.Value.ToString("dd-mm-yyyy")} tarihine kadar kilitlendi");
                        }
                    }
                }
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["ReturnUrl"] = null;
            return RedirectToAction("/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null && token==null) 
            {
                Console.WriteLine("userıd yada token null");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                Console.WriteLine("User bulunamadı");
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                Console.WriteLine("Başarılı");
                return RedirectToAction("Login");
            }
            Console.WriteLine("Hesabınız onaylanırken bir hata oluştu tekrar deneyiniz.");
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if(email == null)
            {
                ModelState.AddModelError("", "Eposta adresi boş bırakılamaz.");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Geçerli epostaya ait hesap bulunamadı.");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var backUrl = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = token
            });
            var subject = "MiniShopApp Şifre Sıfırlama";
            var htmlMessage = $"<h1>Yonetimsell Şifre Sıfırlama İşlemi</h1>" +
                $"<p>" +
                $"Lütfen şifrenizi sıfırlamak için aşağıdaki linke tıklayınız." +
                $"</p>" +
                $"<a href='https://localhost:59079{backUrl}'>Şifreyi sıfırla</a>";
            await _emailSender.SendEmailAsync(email, subject, htmlMessage);
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (userId == null && token == null)
            {
                ModelState.AddModelError("", "Bir sorun oluştu");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı");
                return View();
            }
            ResetPasswordViewModel resetPasswordViewModel = new ResetPasswordViewModel
            {
                UserId = userId,
                Token = token
            };
            return View(resetPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByIdAsync(resetPasswordViewModel.UserId);
            if (user == null)
            {
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı.");
                return View(resetPasswordViewModel);
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordViewModel);
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
                        ModelState.AddModelError("","Şifreniz başarı ile değiştirilmiştir.");
                        return RedirectToAction("Profile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
                ModelState.AddModelError("","Geçerli şifreiz hatalıdır.");
            }
            return View();
        }
    }
}
