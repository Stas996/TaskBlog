﻿using System.Web.Mvc;
using System.Web.Routing;

namespace TaskBlog.PresentationLayer
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Admin",
            //    url: "admin/{controller}/{action}/{id}",
            //    defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            //);
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Article", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}

