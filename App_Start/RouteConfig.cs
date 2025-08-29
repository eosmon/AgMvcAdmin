using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AgMvcAdmin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Sale",
                url: "content/sales-campaigns",
                defaults: new { controller = "Content", action = "Sale" }
            );


            routes.MapRoute(
                name: "Content",
                url: "content/page-manager",
                defaults: new { controller = "Content", action = "PageMgr" }
            );


            routes.MapRoute(
                name: "HomeScroll",
                url: "content/home-scroll",
                defaults: new { controller = "Content", action = "HomeScroll" }
            );

            routes.MapRoute(
                name: "Images",
                url: "data-feed-admin/edit-images",
                defaults: new { controller = "DataAdmin", action = "Images" }
            );

            routes.MapRoute(
                name: "Manufacturers",
                url: "data-feed-admin/edit-manufacturers",
                defaults: new { controller = "DataAdmin", action = "Manufacturers" }
            );


            routes.MapRoute(
                name: "Duplicates",
                url: "data-feed-admin/manage-duplicates",
                defaults: new { controller = "DataAdmin", action = "Duplicates" }
            );

            routes.MapRoute(
                name: "DataFeedAdmin",
                url: "data-feed-admin/{seo}",
                defaults: new { controller = "DataAdmin", action = "DataFeed", seo = "" }
            );


            routes.MapRoute(
                name: "DataFeedAdminMissing",
                url: "data-feed-admin/missing-gun-specs/{link}",
                defaults: new { controller = "DataAdmin", action = "DataFeed", link = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );


        }
    }
}
