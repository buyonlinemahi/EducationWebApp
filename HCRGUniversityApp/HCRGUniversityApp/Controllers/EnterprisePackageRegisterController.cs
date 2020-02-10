using AutoMapper;
using HCRGUniversityApp.Domain.Models.EnterprisePackageRegisterModel;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Configuration;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class EnterprisePackageRegisterController : BaseController
    {
        // GET: EnterprisePackageRegister
        private readonly NEPService.UserService.IUserService _userService;

        public EnterprisePackageRegisterController(NEPService.UserService.IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Index()
        {
            EnterprisePackageRegister _EnterprisePackageRegister = new EnterprisePackageRegister();
            //_EnterprisePackageRegister = Mapper.Map<EnterprisePackageRegister>(_userService.getEnterprisePackageRegisterByUserID(HCRGUser.UID));
            _EnterprisePackageRegister = null;
            return View(_EnterprisePackageRegister);
        }
        [HttpPost]
        public ActionResult AddEnterprisePackageRegister(EnterprisePackageRegister EnterprisePackageRegister)
        {
            if (EnterprisePackageRegister.EPRID > 0)
            {
                _userService.updateEnterprisePackageRegister(Mapper.Map<NEPService.UserService.EnterprisePackageRegister>(EnterprisePackageRegister));
                return Json(GlobalConst.Message.UpdatedSuccessfully, GlobalConst.Message.text_html);
            }
            else
            {
                var id = _userService.addEnterprisePackageRegister(Mapper.Map<NEPService.UserService.EnterprisePackageRegister>(EnterprisePackageRegister));
                return Json(GlobalConst.Message.AddedSuccessfully, GlobalConst.Message.text_html);
            }
        }
    }
}