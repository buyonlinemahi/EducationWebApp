using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using System.Web.Mvc;



//using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
//using HCRGUniversityMgtApp.Infrastructure.Base;
//using BRManagementApp.Infrastructure.Global;+
//using Jurisdiction = BRManagementApp.Domain.Models.JurisdictionModel.Jurisdiction;
//using BRManagementApp.Domain.ViewModels.CommonModel;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]

    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

    }
}
