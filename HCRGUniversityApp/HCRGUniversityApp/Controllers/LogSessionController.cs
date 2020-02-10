using AutoMapper;
using HCRGUniversityApp.Domain.Models.LogSessionModel;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class LogSessionController : BaseController
    {
        private readonly NEPService.LogSessionService.ILogSessionService _logSessionService;
        public LogSessionController(NEPService.LogSessionService.ILogSessionService logSessionService)
        {
            _logSessionService = logSessionService;
        }


        // GET: LogSession
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DeleteSession(string browser, int MEID, int UserID)
        {
            _logSessionService.DeleteSession(browser, MEID, UserID);
            return Json(GlobalConst.Message.Success, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult LogModuleOrExam(string browser, string newurl, int MEID, int UserId)
        {
            _logSessionService.LogModuleOrExam(browser, newurl, MEID, UserId);
            return Json(GlobalConst.Message.success, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult InsertSession(string browser, int MEID, int UserID, string pageurl)
        {
            LogSession logSession = new LogSession();
            logSession.MEID = MEID;
            logSession.SessionId = UserID;
            logSession.UserId = UserID;
            logSession.PageUrl = pageurl;
            logSession.Browser = browser;
            logSession.LogCreatedDate = System.DateTime.Now;
            _logSessionService.AddSession(Mapper.Map<HCRGUniversityApp.NEPService.LogSessionService.LogSession>(logSession));
            return Json(GlobalConst.Message.Success, GlobalConst.Message.text_html);
        }
    }
}