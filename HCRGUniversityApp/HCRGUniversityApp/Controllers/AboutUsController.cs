using AutoMapper;
using HCRGUniversityApp.Domain.Models.AboutUsModel;
using HCRGUniversityApp.Domain.ViewModels.AboutUsViewModel;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;

namespace HCRGUniversityApp.Controllers
{
    public class AboutUsController : BaseController
    {
        public readonly IEncryption _encryptionService;
        private readonly NEPService.AboutUsService.IAboutUsService _aboutusService;
        public AboutUsController(NEPService.AboutUsService.IAboutUsService aboutusService, IEncryption encryptionService)
        {
            _aboutusService = aboutusService;
            _encryptionService = encryptionService;

        }
        public ActionResult Index()
        {

            AboutUsViewModel aboutusModel = new AboutUsViewModel();

            aboutusModel.AboutUSResults = Mapper.Map<IEnumerable<AboutUs>>(_aboutusService.getAllRecords((HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))))));
            foreach (AboutUs viewmodel in aboutusModel.AboutUSResults)
            {
                string decodedHTML = HttpUtility.HtmlDecode(viewmodel.Description);
                if (decodedHTML != null)
                {
                    viewmodel.Description = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    viewmodel.Description = viewmodel.Description.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    viewmodel.Description = viewmodel.Description.Replace("&nbsp;", "");
                }
                else
                    viewmodel.Description = decodedHTML;
            }
            return View(aboutusModel);
        }

    }
}
