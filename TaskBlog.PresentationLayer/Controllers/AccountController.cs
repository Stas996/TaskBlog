using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using TaskBlog.ViewModels;
using System.Security.Claims;
using TaskBlog.BusinessLogicLayer.Interfaces;

namespace TaskBlog.PresentationLayer.Controllers
{
    public class AccountController : Controller
    {
        IUserService UserService { get; set; }
        IAuthenticationManager SignInManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        public AccountController(IUserService userService)
        {
            UserService = userService;           
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                ClaimsIdentity claim = await UserService.Authenticate(model);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    SignInManager.SignOut();
                    SignInManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("Index", "Article");
                }
            }
            return View(model);
        }

        public ActionResult LogOff()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "Article");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var operationDetails = await UserService.Create(model);
                if (operationDetails.Succedeed)
                {
                    //send email confirmation code
                    //var code = await UserService.GenerateEmailConfirmationTokenAsync(userDto.Id);
                    //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = userDto.Id, code = code }, protocol: Request.Url.Scheme);
                    //await UserService.SendEmailConfirmationAsync(userDto.Id, callbackUrl);

                    return await Login(new LoginViewModel() {
                            UserName = model.UserName,
                            Password = model.Password });
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }

        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new RegisterViewModel
            {
                Email = "stanislavh@ukr.net",
                UserName = "Admin",
                Password = "654321",
                FirstName = "Вася",
                LastName = "Пупкин",
            }, new List<string> { "user", "admin" });
        }
    }
}