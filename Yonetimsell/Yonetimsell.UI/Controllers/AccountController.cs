using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, ISweetAlertService sweetAlert)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _sweetAlert = sweetAlert;
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
                    //MAIL GÖNDERME İŞLEMİ BAŞLIYOR
                    //Token Oluşturma

                    var tokenCode = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var backUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userId = user.Id,
                        token = tokenCode
                    });

                    //Mail gönderme
                    await _emailSender.SendEmailAsync(
                        user.Email,
                        "Yonetimsell Üyelik Onayı",
                        $"<p>Yonetimsell uygulamasına üyeliğinizi onaylamak için aşağıdaki linke tıklayınız.</p><a href='https://localhost:7051{backUrl}'>ONAY LİNKİ</a>"
                        );

                    //Yukarıdaki email onayı kodlarını aktif ettiğimizde burayı sileceğiz.

                    TempData["SuccesToast"] = _sweetAlert.TopEndNotification("success","Üyeliğiniz başarıyla oluşturulmuştur. Mailinizi kontrol ederek üyeliğinizi onaylayabilirsiniz.");
                    return Redirect("~/");
                }

            }

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
                TempData["SuccessToast"] = _sweetAlert.TopEndNotification("error", "Kullanıcı adı ve şifre hatalı!");
                return View(loginViewModel);
            }
            await _signInManager.SignOutAsync();
            var isConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isConfirmed)
            {
                TempData["SuccessToast"] = _sweetAlert.TopEndNotification("error", "Lütfen e-postanızı onaylayıp tekrar deneyiniz.");
                Console.WriteLine("E-posta onaylanmamış.");
                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, loginViewModel.RememberMe, true);
            if (result.Succeeded)
            {
                // Başarılı giriş durumunda yapılacak işlemler
                await _userManager.ResetAccessFailedCountAsync(user);
                await _userManager.SetLockoutEndDateAsync(user, null);
                TempData["SuccessToast"] = _sweetAlert.TopEndNotification("success", "Başarı ile giriş yapıldı");

                // ReturnUrl varsa yönlendirme
                var returnUrl = TempData["ReturnUrl"]?.ToString();
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("Index", "Home"); // Varsayılan yönlendirme
            }
            else
            {
                // Giriş başarısız durumları
                if (result.IsLockedOut)
                {
                    TempData["SuccessToast"] = _sweetAlert.TopEndNotification("error", "Hesabınız kilitli");
                    // Hesap kilitli durumu
                    // ... (Kodunuzun bu kısmı aynı şekilde kalır) ...
                }
                else
                {
                    TempData["SuccessToast"] = _sweetAlert.TopEndNotification("error", "Kullanıcı adı ve şifre hatalı!");
                    // Diğer giriş başarısızlıkları
                    // ... (Kodunuzun bu kısmı aynı şekilde kalır) ...
                }
            }

            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["ReturnUrl"] = null;
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
            if (email == null)
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
            if (user == null)
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
                        ModelState.AddModelError("", "Şifreniz başarı ile değiştirilmiştir.");
                        return RedirectToAction("Profile");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(changePasswordViewModel);
                }
                ModelState.AddModelError("", "Geçerli şifreiz hatalıdır.");
            }
            return View();
        }
    }
}
