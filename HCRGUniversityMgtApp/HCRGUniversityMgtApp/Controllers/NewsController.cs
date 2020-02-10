using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.NewsModel;
using HCRGUniversityMgtApp.Domain.Models.NewsPhotoModel;
using HCRGUniversityMgtApp.Domain.Models.NewsSectionModel;
using HCRGUniversityMgtApp.Domain.Models.NewsVideoModel;
using HCRGUniversityMgtApp.Domain.ViewModels.NewsPhotoViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.NewsSectionViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.NewsViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class NewsController : BaseController
    {
        //
        // GET: /News/
        private readonly NEPService.NewsService.INewsService _NewsService;
        private readonly IStorage _storageService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public NewsController(NEPService.NewsService.INewsService newsService, IStorage storageService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _storageService = storageService;
            _clientService = clientService;
        }
       
        [HttpPost]
        public JsonResult GetNewsByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<News>>(_NewsService.GetAllNewsByOrganizationID(OrgID, HCRGCLIENT.ClientID));
            foreach (News _newsContent in data)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _newsContent.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_newsContent.NewsDescription), String.Empty);
                _newsContent.NewsDescriptionShort = _newsContent.NewsDescriptionShort.Replace("&nbsp;", "");
                if (_newsContent.NewsDescriptionShort.Length > 1000)
                    _newsContent.NewsDescriptionShort = _newsContent.NewsDescriptionShort.Substring(0, 1000);
            }
            return Json(data);
        }

        public ActionResult Index()
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            NewsViewModel newsViewmodel = new NewsViewModel();
            newsViewmodel.NewsResults = Mapper.Map<IEnumerable<News>>(_NewsService.GetAllNewsByOrganizationID(0, HCRGCLIENT.ClientID));
            foreach (News viewmodel in newsViewmodel.NewsResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.NewsDescription), String.Empty);
                viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.NewsDescriptionShort.Length > 1000)
                {
                    viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Substring(0, 1000);

                }

            }
            if (HCRGCLIENT.ClientTypeID == 1)
                newsViewmodel.IsHCRGAdmin = true;
            else
                newsViewmodel.IsHCRGAdmin = false;
            newsViewmodel.NewsSectionResults = Mapper.Map<IEnumerable<NewsSection>>(_NewsService.getAllNewsSection(HCRGCLIENT.OrganizationID));
            return View(newsViewmodel);
        }

        public ActionResult ShowEditor()
        {  
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.FullScreen = false;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            return View();
        }
       
        [HttpPost]
        public JsonResult AddNews(string newsTitle, string newsSectionID, string newsDescription, string newsEditorPick, string newsType, string newsVideo, int newsID, string newsAuthor, string organizationName, int organizationID)
        {

            News news = new News();
            if (newsID == 0)
            {
                NewsVideo newsVideoobj = new NewsVideo();
                news.NewsSectionID = Convert.ToInt32(newsSectionID);
                news.NewsTitle = newsTitle;
                news.NewsType = newsType;
                news.NewsDate = DateTime.Now;
                news.NewsDescription = newsDescription;
                news.NewsAuthor = newsAuthor;
                news.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                news.NewsEditorPick = Convert.ToBoolean(newsEditorPick);
                Regex regex = new Regex("\\<[^\\>]*\\>");
                news.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(newsDescription), String.Empty);
                news.NewsDescriptionShort = news.NewsDescriptionShort.Replace("&nbsp;", "");
                if (news.NewsDescriptionShort.Length > 1000)
                    news.NewsDescriptionShort = news.NewsDescriptionShort.Substring(0, 1000);
                news.OrganizationID = HCRGCLIENT.OrganizationID;
                news.NewsID = _NewsService.AddNews(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.News>(news));
                if (newsType == "Video")
                {
                    newsVideoobj.NewsID = news.NewsID;
                    newsVideoobj.NewsVideos = newsVideo;
                    var NewsVideoID = _NewsService.AddNewsVideo(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsVideo>(newsVideoobj));
                }
                news.msg = news.NewsID.ToString() + "," + "Added";
            }
            else
            {
                news.NewsSectionID = Convert.ToInt32(newsSectionID);
                news.NewsTitle = newsTitle;
                news.NewsDescription = newsDescription;
                news.NewsAuthor = newsAuthor;
                news.NewsEditorPick = Convert.ToBoolean(newsEditorPick);
                news.NewsID = Convert.ToInt32(newsID);
                news.NewsType = newsType;
                news.NewsDate = DateTime.Now;
                news.OrganizationName = organizationName;
                news.OrganizationID = organizationID;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                news.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(newsDescription), String.Empty);
                news.NewsDescriptionShort = news.NewsDescriptionShort.Replace("&nbsp;", "");
                if (news.NewsDescriptionShort.Length > 1000)
                    news.NewsDescriptionShort = news.NewsDescriptionShort.Substring(0, 1000);
                _NewsService.UpdateNews(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.News>(news));
                news.msg = news.NewsID.ToString() + "," + "Updated";
            }
            return Json(news, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UploadNewsPhotos(string[] photoFiles, string[] photoNames, int id)
        {

            var _res = _NewsService.GetNewsByID(id);

            for (int i = 0; i < photoFiles.Length; i++)
            {
                byte[] bytes = Convert.FromBase64String(photoFiles[i].Split(',')[1]);
                var path = _storageService.SetNewsPhotoStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _res.OrganizationID), photoNames[i]);
                using (System.IO.FileStream stream = new System.IO.FileStream(path, System.IO.FileMode.Create))
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Flush();
                }
                NewsPhoto newsPhotoobj = new NewsPhoto();
                newsPhotoobj.NewsID = id;
                newsPhotoobj.NewsPhotos = Path.GetFileName(path);
                var NewsPhotoID = _NewsService.AddNewsPhoto(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsPhoto>(newsPhotoobj));
            }

            return Json("Photos added Successfully", GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult AddNewsSection(NewsSection newsSection)
        {
            newsSection.OrganizationID = HCRGCLIENT.OrganizationID;
            if (newsSection.NewsSectionID == null || newsSection.NewsSectionID == 0)
            {
                newsSection.NewsSectionID = _NewsService.AddNewsSection(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsSection>(newsSection));
                newsSection.flag = true;
            }
            else
            {
                _NewsService.UpdateNewsSection(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsSection>(newsSection));
                newsSection.flag = false;
            }
            return Json(newsSection, GlobalConst.Message.text_html);
        }
   
        public ActionResult GetAllNews()
        {
            var news = _NewsService.getAllNews(HCRGCLIENT.OrganizationID);
            NewsViewModel newsViewModel = new NewsViewModel();
            newsViewModel.NewsResults = Mapper.Map<IEnumerable<News>>(news);
            foreach (News viewmodel in newsViewModel.NewsResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.NewsDescription), String.Empty);
                viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.NewsDescriptionShort.Length > 1000)
                    viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Substring(0, 1000);
            }
            return Json(newsViewModel.NewsResults, GlobalConst.Message.text_html);
        }

        public ActionResult GetAllNewsSection()
        {
            var newsSection = _NewsService.getAllNewsSection(HCRGCLIENT.OrganizationID);
            NewsSectionViewModel newsSectionViewModel = new NewsSectionViewModel();
            newsSectionViewModel.NewsSectionResults = Mapper.Map<IEnumerable<NewsSection>>(newsSection);
            return Json(newsSectionViewModel.NewsSectionResults, GlobalConst.Message.text_html);
        }
        public ActionResult GetAllPagedNews(int skip, int? take)
        {
            PagedNews newsModel = new PagedNews();
            if (take == null)
                newsModel = Mapper.Map<PagedNews>(_NewsService.GetAllPagedNews(skip, GlobalConst.Records.Take, HCRGCLIENT.OrganizationID));
            else
                newsModel = Mapper.Map<PagedNews>(_NewsService.GetAllPagedNews(skip, take.Value, HCRGCLIENT.OrganizationID));
            newsModel.PagedRecords = GlobalConst.Records.Take;
            foreach (News viewmodel in newsModel.NewsRecords)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.NewsDescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.NewsDescription), String.Empty);
                viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.NewsDescriptionShort.Length > 1000)
                    viewmodel.NewsDescriptionShort = viewmodel.NewsDescriptionShort.Substring(0, 1000);
            }
            return Json(newsModel, GlobalConst.Message.text_html);
        }
        
        [HttpPost]
        public JsonResult getPhotosByNewsID(int newsID)
        {
            var _res = _NewsService.GetNewsByID(newsID);

            var newsphotos = _NewsService.getAllNewsPhotoByNewsID(newsID);
            NewsPhotoViewModel newsPhotoViewModel = new NewsPhotoViewModel();
            newsPhotoViewModel.NewsPhotoResults = Mapper.Map<IEnumerable<NewsPhoto>>(newsphotos);
            foreach (var _r in newsPhotoViewModel.NewsPhotoResults)
                _r.NewsPhotos = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _res.OrganizationID + "/NewsPhotos/" + _r.NewsPhotos;
            return Json(newsPhotoViewModel.NewsPhotoResults, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult UpdateNewsVideo(int newsVideoID, String newsVideos, int newsID)
        {
            NewsVideo newsVideoobj = new NewsVideo();
            newsVideoobj.NewsVideoID = newsVideoID;
            newsVideoobj.NewsVideos = newsVideos;
            newsVideoobj.NewsID = newsID;
            var NewsVideoID = _NewsService.UpdateNewsVideo(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.NewsVideo>(newsVideoobj));
            return Json("Updated Successfully", GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult getVideoByNewsID(string newsID)
        {
            NewsVideo newsVideo = new NewsVideo();
            newsVideo = Mapper.Map<NewsVideo>(_NewsService.GetNewsVideoByNewsID(Convert.ToInt32(newsID)));
            return Json(newsVideo, GlobalConst.Message.text_html);
        }
       
        [HttpPost]
        public JsonResult DeleteNewsSection(string newsSectionID)
        {
            _NewsService.DeleteNewsSection(Convert.ToInt32(newsSectionID));
            return Json("NewsSection deleted Successfully");
        }
        private string UpdateDocumentFile(string photofiles)
        {
            return "";// fileName;
        }

        [HttpPost]
        public JsonResult DeletePhoto(string newsPhotoID)
        {
            _NewsService.DeleteNewsPhoto(Convert.ToInt32(newsPhotoID));
            return Json("Photo deleted Successfully");
        }
        [HttpPost]
        public JsonResult DeleteNews(string newsID)
        {
            _NewsService.DeleteNews(Convert.ToInt32(newsID));
            return Json("News deleted Successfully");
        }

        
    }
}
