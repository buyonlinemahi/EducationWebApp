using AutoMapper;
using HCRGUniversityApp.Domain.Models.NewsModel;
using HCRGUniversityApp.Domain.Models.NewsSectionModel;
using HCRGUniversityApp.Domain.Models.ResourceModel;
using HCRGUniversityApp.Domain.ViewModels.NewsDetailViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.NEPService.ClientService;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
namespace HCRGUniversityApp.Controllers
{
    public class NewsController : BaseController
    {
        private readonly NEPService.NewsService.INewsService _newsService;
        private readonly IEncryption _encryptionService;
        private readonly IClientService _clientService;

        public NewsController(NEPService.NewsService.INewsService newsService, IClientService clientService, IEncryption encryptionService)
        {
            _newsService = newsService;
            _encryptionService = encryptionService;
            _clientService = clientService;
        }
        public ActionResult Index(string SectionId)
        {
            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("7"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage , GlobalConst.Controllers.User, new { area = "" });
                }
            }
            //return Json(_res, JsonRequestBehavior.AllowGet);

            NewsDetailViewModel NewsDetail = new NewsDetailViewModel();
            ViewBag.OrganizationID = _encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]);
            ViewBag.StoragePath = GlobalConst.Message.SlashStoragePath + GlobalConst.ConstantChar.ForwardSlash;
            NewsDetail.NewsSectionResults = Mapper.Map<IEnumerable<NewsSection>>(_newsService.getAllNewsSection(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            int? sectionId = null;
            if (SectionId != null)
                sectionId = int.Parse(_encryptionService.DecryptString2(SectionId.ToString()));
             
            if (sectionId == null)
            {
                var _newsData = _newsService.GetAllNewsDetail("All", 0, GlobalConst.Records.Take,HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                NewsDetail.NewsResults = Mapper.Map<IEnumerable<NewsFullDetail>>(_newsData.NewsFullDetailRecords);
                foreach (NewsFullDetail _newsFullDetail in NewsDetail.NewsResults)
                    _newsFullDetail.EncryptedNewsID = _encryptionService.EncryptString2(_newsFullDetail.NewsID.ToString());
                NewsDetail.TotalCount = _newsData.TotalCount;
            }
            else
            {
                var _newsData = _newsService.GetAllNewsDetailByNewsSectionIDAndType("All", sectionId.Value, 0, GlobalConst.Records.Take, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                NewsDetail.NewsResults = Mapper.Map<IEnumerable<NewsFullDetail>>(_newsData.NewsFullDetailRecords);
                foreach (NewsFullDetail _newsFullDetail in NewsDetail.NewsResults)
                    _newsFullDetail.EncryptedNewsID = _encryptionService.EncryptString2(_newsFullDetail.NewsID.ToString());
                NewsDetail.TotalCount = _newsData.TotalCount;
                NewsDetail.SectionID = sectionId.Value;
            }


            NewsDetail.newsLetter = Mapper.Map<NewsLetter>(_newsService.GetNewsLetterAll(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            if (NewsDetail.newsLetter != null)
            {
                string decodedHTML = HttpUtility.HtmlDecode(NewsDetail.newsLetter.NewsLetterContent);
                if (decodedHTML != null)
                    NewsDetail.newsLetter.NewsLetterContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                else
                    NewsDetail.newsLetter.NewsLetterContent = decodedHTML;
            }

            foreach (NewsFullDetail viewmodel in NewsDetail.NewsResults)
            {
                viewmodel.NewsDescription = HttpUtility.HtmlDecode(viewmodel.NewsDescription);
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.NewsDescription = regex.Replace(viewmodel.NewsDescription, String.Empty);
                bool IsVideo = viewmodel.NewsDescription.Contains("Video");
                if (IsVideo)
                    viewmodel.NewsDescription = "Video";
                if (viewmodel.NewsDescription.Length > 1000)
                    viewmodel.NewsDescription = regex.Replace(viewmodel.NewsDescription, String.Empty).Substring(0, 1000);
            }
            return View(NewsDetail);
        }

        public ActionResult NewsDetail(string NewsID, string NewsType)
        {
            ViewBag.OrganizationID = _encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]);
            ViewBag.StoragePath = GlobalConst.Message.SlashStoragePath + GlobalConst.ConstantChar.ForwardSlash;
            NewsFullDetailViewModel NewsDetail = new NewsFullDetailViewModel();
            NewsDetail.NewsSectionResults = Mapper.Map<IEnumerable<NewsSection>>(_newsService.getAllNewsSection(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])))));
            foreach (NewsSection _newsSection in NewsDetail.NewsSectionResults)
            {
                _newsSection.EncryptedNewsSectionID = _encryptionService.EncryptString2(_newsSection.NewsSectionID.ToString());
            }
            NewsDetail.NewsResults = Mapper.Map<IEnumerable<NewsFullDetail>>(_newsService.GetNewsDetailByNewsID(int.Parse(_encryptionService.DecryptString2(NewsID.ToString())), NewsType));
            foreach (NewsFullDetail viewmodel in NewsDetail.NewsResults)
            {
                //  EducationTypesAvailableViewModel.EducationDetailPageResults.PContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.StoragePath]);

                viewmodel.NewsDescription = HttpUtility.HtmlDecode(viewmodel.NewsDescription);
                viewmodel.NewsDescription = viewmodel.NewsDescription.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                viewmodel.NewsDescription = viewmodel.NewsDescription.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");                
                viewmodel.NewsDescription = viewmodel.NewsDescription.Replace("&nbsp;", "");
            }
            return View(NewsDetail);
        }
        public ActionResult GetFilterNews(string filtertype, int sectionid, int skip)
        {
            NewsDetailViewModel NewsDetail = new NewsDetailViewModel();
            if (sectionid == 0)
            {
                var _newsData = _newsService.GetAllNewsDetail(filtertype, skip, GlobalConst.Records.Take, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                NewsDetail.NewsResults = Mapper.Map<IEnumerable<NewsFullDetail>>(_newsData.NewsFullDetailRecords);
                foreach (NewsFullDetail _newsFullDetail in NewsDetail.NewsResults)
                    _newsFullDetail.EncryptedNewsID= _encryptionService.EncryptString2(_newsFullDetail.NewsID.ToString());
                NewsDetail.TotalCount = _newsData.TotalCount;
            }

            else
            {
                var _newsData = _newsService.GetAllNewsDetailByNewsSectionIDAndType(filtertype, sectionid, skip, GlobalConst.Records.Take, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                NewsDetail.NewsResults = Mapper.Map<IEnumerable<NewsFullDetail>>(_newsData.NewsFullDetailRecords);
                foreach (NewsFullDetail _newsFullDetail in NewsDetail.NewsResults)
                    _newsFullDetail.EncryptedNewsID = _encryptionService.EncryptString2(_newsFullDetail.NewsID.ToString());
                NewsDetail.TotalCount = _newsData.TotalCount;
                NewsDetail.SectionID = sectionid;
            }
            foreach (NewsFullDetail viewmodel in NewsDetail.NewsResults)
            {
                viewmodel.NewsDescription = HttpUtility.HtmlDecode(viewmodel.NewsDescription);
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.NewsDescription = regex.Replace(viewmodel.NewsDescription, String.Empty);
                if (viewmodel.NewsDescription.Length > 1000)
                {
                    viewmodel.NewsDescription = regex.Replace(viewmodel.NewsDescription, String.Empty).Substring(0, 1000);
                    viewmodel.NewsDescription = viewmodel.NewsDescription.Replace("&nbsp;", "");
                }
                //  viewmodel.NewsDescription = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, System.Configuration.ConfigurationManager.AppSettings[GlobalConst.Message.StoragePath]);
            }
            return Json(NewsDetail);
        }


    }
}
