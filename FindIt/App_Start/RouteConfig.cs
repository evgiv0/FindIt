using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace FindIt
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


           // routes.MapRoute("", "{controller}/{action}/{city}");
            routes.MapRoute("", "{controller}/{action}/{filter}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Notice",
                    action = "List",
                    id = UrlParameter.Optional
                });



        }
    }
}