using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HCRGUniversityApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "CertificationProgram",
            url: "Course/CourseDetail/{eid}",
            defaults: new { controller = "CertificationProgram", action = "Index", });

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            name: "Store",
            url: "Store/ProductDetail/{pid}",
            defaults: new { controller = "Store", action = "ProductDetail", });

            routes.MapRoute(
            name: "MyEducationModule",
            url: "MyEducation/MyEducationModule/{meID}",
            defaults: new { controller = "MyEducation", action = "MyEducationModule", });

            routes.MapRoute(
           name: "MyEducationModuleMedia",
           url: "MyEducation/MyEducationModuleMedia/{MEMID}",
           defaults: new { controller = "MyEducation", action = "MyEducationModuleMedia", });

            routes.MapRoute(
            name: "Pretest",
            url: "Exam/Pretest/{educationID}/{meID}",
            defaults: new { controller = "Exam", action = "Pretest" });

            routes.MapRoute(
           name: "FinalExam",
           url: "Exam/FinalExam/{educationID}/{meID}",
           defaults: new { controller = "Exam", action = "FinalExam" });

            routes.MapRoute(
            name: "ExamResult",
            url: "Exam/ExamResult/{Resultno}/{educationID}/{meID}/{totalAttempt}",
            defaults: new { controller = "Exam", action = "ExamResult" });

            routes.MapRoute(
            name: "EvaluationExam",
            url: "Exam/EvaluationExam/{educationID}/{meID}",
            defaults: new { controller = "Exam", action = "EvaluationExam" });

            routes.MapRoute(
            name: "NewsDetail",
            url: "News/NewsDetail/{NewsID}/{NewsType}",
            defaults: new { controller = "News", action = "NewsDetail" });

            routes.MapRoute(
            name: "IndustryResearch",
            url: "IndustryResearch/",
            defaults: new { controller = "Home", action = "IndustryResearch", });

            routes.MapRoute(
           name: "MyEducationAccountHistory",
           url: "MyEducationAccountHistory/",
           defaults: new { controller = "MyEducation", action = "MyEducationAccountHistory", });

            routes.MapRoute(
           name: "MyEducation",
              url: "MyEducation/MyEducationDetail/{skip}",
           defaults: new { controller = "MyEducation", action = "Index", id = UrlParameter.Optional });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}