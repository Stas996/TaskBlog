using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using TaskBlog.ViewModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;
using TaskBlog.BusinessLogicLayer.Services.Identity;

namespace TaskBlog.PresentationLayer
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind(typeof(IService<ArticleViewModel>)).To(typeof(ArticleService));
            kernel.Bind(typeof(IService<CommentViewModel>)).To(typeof(CommentService));
            kernel.Bind(typeof(IService<TagViewModel>)).To(typeof(TagService));
            kernel.Bind(typeof(IService<UserProfileViewModel>)).To(typeof(UserProfileService));
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}