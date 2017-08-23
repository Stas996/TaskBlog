using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskBlog.DataLayer
{
    public class AppRoleManager : RoleManager<UserRole>
    {
        public AppRoleManager(RoleStore<UserRole> store)
                    : base(store)
        { }

    }
}
