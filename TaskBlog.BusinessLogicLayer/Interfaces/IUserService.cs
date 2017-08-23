using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskBlog.BusinessLogicLayer.Infrastructure;
using TaskBlog.BusinessLogicLayer.DTOModels;

namespace TaskBlog.BusinessLogicLayer.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}
