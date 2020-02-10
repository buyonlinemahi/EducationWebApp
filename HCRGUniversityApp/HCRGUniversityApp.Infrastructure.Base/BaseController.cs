using System;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.ApplicationServices;
using System.Web.Mvc;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace HCRGUniversityApp.Infrastructure.Base
{
    public abstract class BaseController : Controller
    {
           
        public User HCRGUser
        {
            get { return (User)Session[GlobalConst.SessionKeys.USER]; }
            set { Session[GlobalConst.SessionKeys.USER] = value; }
        }
        public  int  EducationID
        {
            get { return (int)Session[GlobalConst.SessionKeys.EDUCATIONID]; }
            set { Session[GlobalConst.SessionKeys.EDUCATIONID] = value; }
        }
        //public int ClientID
        //{
        //    get { return (int)Session[GlobalConst.SessionKeys.CLIENTID]; }
        //    set { Session[GlobalConst.SessionKeys.CLIENTID] = value; }
        //}
        protected override void OnException(ExceptionContext filterContext)
        {
            //_storageService = new StorageService();
            //Exception ex = filterContext.Exception;
            //filterContext.ExceptionHandled = true;
            //var model = new HandleErrorInfo(filterContext.Exception, "Controller", "Action");

            //filterContext.Result = new ViewResult()
            //{
            //    ViewName = "Error",
            //    ViewData = new ViewDataDictionary(model)
            //};            

            //// Create ErrorLog folder if not exist
            //string VirtualStoragePath = _storageService.getVirtualPath();
            //string path = VirtualStoragePath + "/";

            //DirectoryInfo dr = new DirectoryInfo(path);
            //if (!dr.CreateSubdirectory("ErrorLog").Exists)
            //    dr.CreateSubdirectory("ErrorLog");

            //path += GlobalConst.MgmtDirectoryStructure.ErrorLog + "/" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";
            
            //// check if the file for current date exist
            //if (!System.IO.File.Exists(path))
            //{
            //    System.IO.File.Create(path).Close();
            //}
            
            //using (StreamWriter w = System.IO.File.AppendText(path))
            //{
            //    w.WriteLine("\r\nLog Entry : ");
            //    w.WriteLine("{0}", DateTime.Now.ToString(CultureInfo.InvariantCulture));
            //    string err = "Error in: " + System.Web.HttpContext.Current.Request.Url.ToString() + Environment.NewLine;
            //    err += "Error Message:" + ex.Message.ToString() + Environment.NewLine;
            //    err += "Error Stacktrace:" + ex.StackTrace.ToString() + Environment.NewLine;
            //    err += "User IP:" + System.Web.HttpContext.Current.Request.UserHostAddress.ToString();
            //    w.WriteLine(err);
            //    w.WriteLine("_________________________________________________________");
            //    w.Flush();
            //    w.Close();
            //}
            //Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current);
        }

        
    }
}
