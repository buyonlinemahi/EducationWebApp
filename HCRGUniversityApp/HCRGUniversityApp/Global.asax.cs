using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HCRGUniversityApp.Infrastructure.DependencyResolution;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Web;
using System;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using System.IO;
using System.Globalization;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.Infrastructure.ApplicationServices;


namespace HCRGUniversityApp
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IUnityContainer container;
        private IStorage _storageService;
        
        protected void Application_Start()
        {
            container = new UnityContainer().LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMapping();
        }

        protected void Application_End()
        {
            container.Dispose();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Response.AddHeader("x-frame-options", "DENY");
        }
        protected void Session_End(object sender, EventArgs e)
        {
            string sessionId = this.Session.SessionID;
            //your code...

            NEPService.UserService.UserServiceClient _userServiceClient = new NEPService.UserService.UserServiceClient();
            _userServiceClient.ResetUserSessionID(((HCRGUniversityApp.Domain.Models.UserModel.User)(sender)).UID);
        }


        protected void Application_Error()
        {
            var ex = Server.GetLastError();
            CreateErrorLog(ex.Message, ex.StackTrace);
            Response.Redirect("/Common/ErrorPage");
        }

        
        public void CreateErrorLog(string errorMessage, string Stacktrace)
        {
            try
            {
                _storageService = new StorageService();
                string VirtualStoragePath = _storageService.getVirtualPath();
                string path = VirtualStoragePath + "\\";

                DirectoryInfo dr = new DirectoryInfo(path);
                if (!dr.CreateSubdirectory(GlobalConst.MgmtDirectoryStructure.ErrorLog).Exists)
                    dr.CreateSubdirectory(GlobalConst.MgmtDirectoryStructure.ErrorLog);

                path += GlobalConst.MgmtDirectoryStructure.ErrorLog + "/" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";

                // check if the file for current date exist
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path).Close();
                }

                using (StreamWriter w = System.IO.File.AppendText(path))
                {
                    w.WriteLine("\r\nLog Entry : ");
                    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
                    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + Environment.NewLine;
                    err += "Error Message:" + errorMessage.ToString() + Environment.NewLine;
                    err += "Error Stacktrace:" + Stacktrace.ToString() + Environment.NewLine;
                    err += "User IP:" + System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
                    w.WriteLine(err);
                    w.WriteLine("_________________________________________________________");
                    w.Flush();
                    w.Close();
                }
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current);
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.StackTrace);
            }
        }

     
    }
}