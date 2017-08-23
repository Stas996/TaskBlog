using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using TaskBlog.BusinessLogicLayer.DTOModels;
using TaskBlog.BusinessLogicLayer.Interfaces;
using TaskBlog.BusinessLogicLayer.Services;

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
            kernel.Bind(typeof(IService<ArticleDTO>)).To(typeof(ArticleService));
            kernel.Bind(typeof(IService<CommentDTO>)).To(typeof(CommentService));
            kernel.Bind(typeof(IService<TagDTO>)).To(typeof(TagService));
            kernel.Bind<IUserService>().To<UserService>();
        }
    }
}