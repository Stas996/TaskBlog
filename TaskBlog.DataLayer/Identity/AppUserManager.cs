using Microsoft.AspNet.Identity;

namespace TaskBlog.DataLayer
{
    public class AppUserManager : UserManager<User>
    {
        public AppUserManager(IUserStore<User> store)
                : base(store)
        {
        }
    }
}
