using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskBlog.DataLayer
{
    //Unit of work with identity data
    public class AppUnitOfWork : GenericUnitOfWork
    {
        AppUserManager _userManager;
        AppRoleManager _roleManager;

        public AppUnitOfWork(DbContext context)
            : base(context)
        {
            _userManager = new AppUserManager(new UserStore<User>(_dbContext));
            _roleManager = new AppRoleManager(new RoleStore<UserRole>(_dbContext));
        }

        public AppRoleManager RoleManager
        {
            get { return _roleManager; }
        }

        public AppUserManager UserManager
        {
            get { return _userManager; }
        }
    }
}
