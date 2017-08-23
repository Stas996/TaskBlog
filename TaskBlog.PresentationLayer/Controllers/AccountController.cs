﻿using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using TaskBlog.PresentationLayer.ViewModels;
using TaskBlog.BusinessLogicLayer.DTOModels;
using System.Security.Claims;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Infrastructure;

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
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
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
                    return RedirectToAction("Index", "Manage", new { area = "Admin"});
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
                UserDTO userDto = new UserDTO
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = "user"
                };
                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                    return View("SuccessRegister");
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }
            return View(model);
        }
        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "stanislavh@ukr.net",
                UserName = "stanislavh@ukr.net",
                Password = "654321",
                FirstName = "Пахом",
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}