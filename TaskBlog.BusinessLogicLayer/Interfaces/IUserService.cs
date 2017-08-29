using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskBlog.DataLayer;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Infrastructure;

namespace TaskBlog.BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(RegisterViewModel user, string role = "user");
        Task<ClaimsIdentity> Authenticate(LoginViewModel user);
        Task SetInitialData(RegisterViewModel admin, List<string> roles);
        Task SendEmailConfirmationAsync(string userId, string callbackUrl);
        Task<string> GenerateEmailConfirmationTokenAsync(string userId);
    }
}
