using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LanguageSchoolApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "AllCourses",
                url: "Courses/AllCourses",
                defaults: new { controller = "Courses", action = "AllCourses", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Courses",
                url: "Courses/{id}",
                defaults: new { controller = "Courses", action = "ById", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "404-PageNotFound",
                url: "{*url}",
                defaults: new { controller = "Home", action = "PageNotFound" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "home", action = "index", id = UrlParameter.Optional }
            );

        }
    }
}
