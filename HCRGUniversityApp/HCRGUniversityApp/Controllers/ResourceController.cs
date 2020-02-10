using AutoMapper;
using HCRGUniversityApp.Domain.Models.FAQModel;
using HCRGUniversityApp.Domain.Models.ResourceModel;
using HCRGUniversityApp.Domain.ViewModels.FAQViewModel;
using HCRGUniversityApp.Domain.ViewModels.ResourceViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.NEPService.ClientService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class ResourceController : BaseController
    {

        private readonly NEPService.NewsService.INewsService _newsService;
        public readonly IEncryption _encryptionService;

        private readonly IClientService _clientService;

        public ResourceController(NEPService.NewsService.INewsService newsService, IClientService clientService, IEncryption encryptionService)
        {
            _newsService = newsService;
            _encryptionService = encryptionService;
            _clientService = clientService;

        }

        public ActionResult Index()
        {
            return RedirectToAction(GlobalConst.Actions.HomeController.Index, GlobalConst.Controllers.Home, new { area = "" });
        }

        public ActionResult Accreditation()
        {

            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("3"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }

            Accreditation accreditation = new Accreditation();
            accreditation = Mapper.Map<Accreditation>(_newsService.GetAccreditationAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (accreditation != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(accreditation.AccreditationContent);

                if (decodedHTML != null)
                {
                    accreditation.AccreditationContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    accreditation.AccreditationContent = accreditation.AccreditationContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    accreditation.AccreditationContent = accreditation.AccreditationContent.Replace("&nbsp;", "");
                }
                else
                    accreditation.AccreditationContent = decodedHTML;
            }

            return View(accreditation);
        }
        public ActionResult Certification()
        {

            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("4"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }


            Certification Certification = new Certification();
            Certification = Mapper.Map<Certification>(_newsService.GetCertificationAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (Certification != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(Certification.CertificationContent);

                if (decodedHTML != null)
                {
                    Certification.CertificationContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    Certification.CertificationContent = Certification.CertificationContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    Certification.CertificationContent = Certification.CertificationContent.Replace("&nbsp;", "");
                }
                else
                    Certification.CertificationContent = decodedHTML;
            }

            return View(Certification);
        }

        public ActionResult TrainingAndSeminar()
        {
            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("2"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }


            TrainingAndSeminar trainingAndSeminar = new TrainingAndSeminar();
            trainingAndSeminar = Mapper.Map<TrainingAndSeminar>(_newsService.GetTrainingAndSeminarAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (trainingAndSeminar != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(trainingAndSeminar.TrainingAndSeminarDesc);
                if (decodedHTML != null)
                {
                    trainingAndSeminar.TrainingAndSeminarDesc = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    trainingAndSeminar.TrainingAndSeminarDesc = trainingAndSeminar.TrainingAndSeminarDesc.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    trainingAndSeminar.TrainingAndSeminarDesc = trainingAndSeminar.TrainingAndSeminarDesc.Replace("&nbsp;", "");
                }
                else
                    trainingAndSeminar.TrainingAndSeminarDesc = decodedHTML;
            }

            return View(trainingAndSeminar);


        }

        public ActionResult FAQ()
        {
            FAQViewModel FAQModel = new FAQViewModel();
            FAQModel.FAQCategoryDetaildResults = Mapper.Map<IEnumerable<FAQCategory>>(_newsService.GetAllFAQCategoriesAccordingToOrganizationID(Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            if (FAQModel.FAQCategoryDetaildResults.Count() > 0)
                FAQModel.FAQDetailResults = Mapper.Map<IEnumerable<FAQ>>(_newsService.GetFAQByCatID(FAQModel.FAQCategoryDetaildResults.FirstOrDefault().FAQCatID));

            return View(FAQModel);
        }

        public ActionResult FAQByCatID(int faqCatID)
        {
            FAQViewModel FAQModel = new FAQViewModel();
            FAQModel.FAQDetailResults = Mapper.Map<IEnumerable<FAQ>>(_newsService.GetFAQByCatID(faqCatID));

            return Json(FAQModel.FAQDetailResults, GlobalConst.Message.text_html);
        }

        public ActionResult PrivacyPolicy()
        {
            PrivacyPolicy privacy = new PrivacyPolicy();
            privacy = Mapper.Map<PrivacyPolicy>(_newsService.GetPrivacyPolicyAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (privacy != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(privacy.PrivacyPolicyContent);
                if (decodedHTML != null)
                {
                    privacy.PrivacyPolicyContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    privacy.PrivacyPolicyContent = privacy.PrivacyPolicyContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    privacy.PrivacyPolicyContent = privacy.PrivacyPolicyContent.Replace("&nbsp;", "");
                }
                else
                    privacy.PrivacyPolicyContent = decodedHTML;
            }
            return Json(privacy);
        }

        public ActionResult TermsCondition()
        {
            TermsCondition _TermsCondition = new TermsCondition();
            _TermsCondition = Mapper.Map<TermsCondition>(_newsService.GetTermsConditionAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (_TermsCondition != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(_TermsCondition.TermsConditionContent);
                if (decodedHTML != null)
                {
                    _TermsCondition.TermsConditionContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                    _TermsCondition.TermsConditionContent = _TermsCondition.TermsConditionContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    _TermsCondition.TermsConditionContent = _TermsCondition.TermsConditionContent.Replace("&nbsp;", "");
                }
                else
                    _TermsCondition.TermsConditionContent = decodedHTML;
            }
            return Json(_TermsCondition);
        }
    }
}
