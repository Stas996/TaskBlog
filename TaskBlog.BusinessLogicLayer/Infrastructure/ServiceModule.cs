using Ninject.Modules;
using Ninject.Web.Common;
using TaskBlog.DataLayer;
using System.Data.Entity;

namespace TaskBlog.BusinessLogicLayer.Infrastructure
{
    public class ServiceModule : NinjectModule
    {
        public ServiceModule()
        {
     
        }

        public override void Load()
        {
            Bind<DbContext>().To<BlogContext>().InRequestScope();       
        }
    }
}
