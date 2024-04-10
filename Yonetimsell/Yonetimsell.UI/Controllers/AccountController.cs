using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Yonetimsell.Business.Mappings;
using Yonetimsell.Entity.Concrete.Identity;
using Yonetimsell.Shared.Helpers.Abstract;
using Yonetimsell.Shared.ViewModels.IdentityViewModels;
using Yonetimsell.UI.EmailServices.Abstract;

namespace Yonetimsell.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ISweetAlertService _sweetAlert;
        private readonly MapperlyConfiguration _mapperly;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ISweetAlertService sweetAlert, MapperlyConfiguration mapperly)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _sweetAlert = sweetAlert;
            _mapperly = mapperly;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = registerViewModel.UserName,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var backUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        token = tokenCode
                    });
                    await _emailSender.SendEmailAsync(
                        user.Email,
                        "Yonetimsell Üyelik Onayı",
                        $"<p>Yonetimsell uygulamasına üyeliğinizi onaylamak için aşağıdaki linke tıklayınız.</p><a href='https://localhost:7051{backUrl}'>ONAY LİNKİ</a>"
                        );
                    TempData["RegisterToast"] = _sweetAlert.TopEndNotification("success", "Onay maili gönderildi");
                    return Redirect("~/");
                }
            }
            TempData["RegisterToast"] = _sweetAlert.TopEndNotification("error", "Lütfen boş alan bırakmayınız");
            return View(registerViewModel);
        }

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
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            var user = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (user == null)
            {
                TempData["LoginToast"] = _sweetAlert.TopEndNotification("error", "Kullanıcı adı veya şifre hatalı!");
                return View(loginViewModel);
            }
            await _signInManager.SignOutAsync();
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmed)
            {
                TempData["LoginToast"] = _sweetAlert.TopEndNotification("error", "Lütfen e-postanızı onaylayıp tekrar deneyiniz.");
                Console.WriteLine("E-posta onaylanmamış.");
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);
            if (result.Succeeded)
            {
                // Başarılı giriş durumunda yapılacak işlemler
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.SetLockoutEndDateAsync(user, null);
                TempData["LoginToast"] = _sweetAlert.TopEndNotification("success", "Başarıyla giriş yapıldı");
                var returnUrl = TempData["ReturnUrl"]?.ToString();
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                if (result.IsLockedOut)
                {
                    TempData["LoginToast"] = _sweetAlert.TopEndNotification("error", "Hesabınız kilitli");
                }
                else
                {
                    TempData["LoginToast"] = _sweetAlert.TopEndNotification("error", "Kullanıcı adı veya şifre hatalı!");
                }
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["ReturnUrl"] = null;
            TempData["LogoutToast"] = _sweetAlert.TopEndNotification("success", "Başarıyla çıkış yapıldı");
            return Redirect("/");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null && token == null)
            {
                TempData["ConfirmEmailToast"] = _sweetAlert.TopEndNotification("error", "Hesabınız onaylanırken bir hata oluştu tekrar deneyiniz.");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ConfirmEmailToast"] = _sweetAlert.TopEndNotification("error", "Kullanıcı bulunamadı.");
                return View();
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData["ConfirmEmailToast"] = _sweetAlert.TopEndNotification("success", "Hesabınız onaylandı.");
                return RedirectToAction("Login");
            }
            TempData["ConfirmEmailToast"] = _sweetAlert.TopEndNotification("error", "Hesabınız onaylanırken bir hata oluştu tekrar deneyiniz.");
            return View();
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (email == null)
            {
                TempData["ForgotPasswordToast"] = _sweetAlert.TopEndNotification("error", "Bilgileri kontrol ediniz.");
                ModelState.AddModelError("", "Eposta adresi boş bırakılamaz.");
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                TempData["ForgotPasswordToast"] = _sweetAlert.TopEndNotification("error", "Bilgileri kontrol ediniz.");
                ModelState.AddModelError("", "Geçerli epostaya ait hesap bulunamadı.");
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var backUrl = Url.Action("ResetPassword", "Account", new
            {
                userId = user.Id,
                token = token
            });
            var subject = "Yonetimsell Şifre Sıfırlama";
            var htmlMessage = $"<h1>Yonetimsell Şifre Sıfırlama İşlemi</h1>" +
                $"<p>" +
                $"Lütfen şifrenizi sıfırlamak için aşağıdaki linke tıklayınız." +
                $"</p>" +
                $"<a href='https://localhost:7051{backUrl}'>Şifreyi sıfırla</a>";
            await _emailSender.SendEmailAsync(email, subject, htmlMessage);
            TempData["ForgotPasswordToast"] = _sweetAlert.TopEndNotification("success", "Mailinizi kontrol ediniz.");
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> ResetPassword(string userId, string token)
        {
            if (userId == null && token == null)
            {
                TempData["ResetPasswordToast"] = _sweetAlert.TopEndNotification("erorr", "Hata!");
                ModelState.AddModelError("", "Bir sorun oluştu");
                return View();
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ResetPasswordToast"] = _sweetAlert.TopEndNotification("erorr", "Geçerli kullanıcı bulunamadı!");
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
                TempData["ResetPasswordToast"] = _sweetAlert.TopEndNotification("erorr", "Geçerli kullanıcı bulunamadı!");
                ModelState.AddModelError("", "Böyle bir kullanıcı bulunamadı.");
                return View(resetPasswordViewModel);
            }
            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                TempData["ResetPasswordToast"] = _sweetAlert.TopEndNotification("success", "Şifreniz başarıyla değiştirildi.");
                return RedirectToAction("Login");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return View(resetPasswordViewModel);
        }
       

    }
}
