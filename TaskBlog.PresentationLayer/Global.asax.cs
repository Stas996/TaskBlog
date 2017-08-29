﻿using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaskBlog.BusinessLogicLayer.Mapping;

namespace TaskBlog.PresentationLayer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            MappingConfig.Configure();
        }
    }
}
