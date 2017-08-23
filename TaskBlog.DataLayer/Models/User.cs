using Microsoft.AspNet.Identity.EntityFramework;

namespace TaskBlog.DataLayer
{
    public class User : IdentityUser
    {
        public virtual UserProfile ClientProfile { get; set; }
    }
}
