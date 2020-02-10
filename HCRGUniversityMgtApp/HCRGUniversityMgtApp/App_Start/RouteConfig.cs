using System.Web.Mvc;
using System.Web.Routing;

namespace HCRGUniversityMgtApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "IndustryResearch",
            url: "IndustryResearch/",
            defaults: new { controller = "HomeContent", action = "IndustryResearch", });

            routes.MapRoute(
            name: "PrivacyPolicy",
            url: "PrivacyPolicy/",
            defaults: new { controller = "HomeContent", action = "PrivacyPolicy", });

            routes.MapRoute(
            name: "TermsCondition",
            url: "TermsCondition/",
            defaults: new { controller = "HomeContent", action = "TermsCondition", });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}