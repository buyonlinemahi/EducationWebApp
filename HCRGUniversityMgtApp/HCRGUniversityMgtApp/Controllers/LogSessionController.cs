using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.LogSessionModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Web.Mvc;
namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class LogSessionController : BaseController
    {
        private readonly NEPService.LogSessionService.ILogSessionService _logsessionService;
        public LogSessionController(NEPService.LogSessionService.ILogSessionService logsessionService)
        {
            _logsessionService = logsessionService;
        }
        public ActionResult Index()
        {
            if (HCRGCLIENT != null)
            {
                if (HCRGCLIENT.ClientTypeID != 1)
                    return RedirectToAction(GlobalConst.Actions.HomeContentController.Index, GlobalConst.Controllers.Home, new { area = "" });
            }

            PagedLogSessionDetail pagedLogSessionDetail = new PagedLogSessionDetail();
            pagedLogSessionDetail = Mapper.Map<PagedLogSessionDetail>(_logsessionService.getAllLogSession(GlobalConst.Records.Skip, GlobalConst.Records.Take));
            return View(pagedLogSessionDetail);
        }
        [HttpPost]
        public ActionResult GetLogSession(int skip)
        {
            PagedLogSessionDetail pagedLogSessionDetail = new PagedLogSessionDetail();
            pagedLogSessionDetail = Mapper.Map<PagedLogSessionDetail>(_logsessionService.getAllLogSession(skip, GlobalConst.Records.Take));
            return Json(pagedLogSessionDetail, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetLogSessionByUserName(string searchtext, int? skip)
        {
            PagedLogSessionDetail pagedLogSessionDetail = new PagedLogSessionDetail();
            if (skip == null)
                pagedLogSessionDetail = Mapper.Map<PagedLogSessionDetail>(_logsessionService.GetLogSessionByUserName(searchtext == null ? "" : searchtext, GlobalConst.Records.Skip, GlobalConst.Records.Take,(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            else
                pagedLogSessionDetail = Mapper.Map<PagedLogSessionDetail>(_logsessionService.GetLogSessionByUserName(searchtext == null ? "" : searchtext, skip.Value, GlobalConst.Records.Take,(Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            return Json(pagedLogSessionDetail, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult DeleteSession(int LogSessionID)
        {
            _logsessionService.DeleteSessionByAdmin(LogSessionID);
            return Json("Log Session deleted Successfully");
        }
    }
}