using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TaskBlog.BusinessLogicLayer.Infrastructure;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity.Owin;
using AutoMapper;

namespace TaskBlog.BusinessLogicLayer.Services.Identity
{
    public class UserService : IUserService
    {
        AppUnitOfWork _db;

        public UserService(AppUnitOfWork db)
        {
            _db = db;
            var provider = new DpapiDataProtectionProvider("Sample");
            _db.UserManager.UserTokenProvider = new DataProtectorTokenProvider<User>(provider.Create("EmailConfirmation"));
        }

        public async Task<OperationDetails> Create(RegisterViewModel newUser, string role = "user")
        {
            var user = await _db.UserManager.FindByEmailAsync(newUser.Email);
            if (user == null)
            {
                user = Mapper.Map<RegisterViewModel, User>(newUser);
                var result = await _db.UserManager.CreateAsync(user, newUser.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await _db.UserManager.AddToRoleAsync(user.Id, role);
                // создаем профиль клиента
                var userProfile = new UserProfile {
                    Id = user.Id,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    Country = newUser.Country,
                    City = newUser.City
                };
                _db.Repository<UserProfile>().Create(userProfile);

                await _db.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(string userId)
        {
            var s = await _db.UserManager.GenerateEmailConfirmationTokenAsync(userId);
            return await _db.UserManager.GenerateEmailConfirmationTokenAsync(userId);
        }

        public async Task SendEmailConfirmationAsync(string userId, string callbackUrl)
        {
            await _db.UserManager.SendEmailAsync(userId, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
        }

        public async Task<ClaimsIdentity> Authenticate(LoginViewModel authUser)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            var user = await _db.UserManager.FindAsync(authUser.UserName, authUser.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
            {
                //authUser.Id = user.Id;
                claim = await _db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            }
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(RegisterViewModel admin, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await _db.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new UserRole { Name = roleName };
                    await _db.RoleManager.CreateAsync(role);
                }
            }
            await Create(admin, "admin");
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
