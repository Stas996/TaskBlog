using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using TaskBlog.BusinessLogicLayer.Infrastructure;
using TaskBlog.BusinessLogicLayer.DTOModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.DataLayer;

namespace TaskBlog.BusinessLogicLayer.Services
{
    public class UserService : IUserService
    {
        AppUnitOfWork _db;

        public UserService(AppUnitOfWork db)
        {
            _db = db;
        }

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            User user = await _db.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new User { Email = userDto.Email, UserName = userDto.Email };
                var result = await _db.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await _db.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                var userProfile = new UserProfile { Id = user.Id, FirstName = userDto.FirstName, LastName = userDto.LastName };
                _db.Repository<UserProfile>().Create(userProfile);
                await _db.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            User user = await _db.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await _db.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
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
            await Create(adminDto);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
