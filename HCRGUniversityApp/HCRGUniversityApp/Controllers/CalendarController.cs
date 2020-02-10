using HCRGUniversityApp.Domain.Models.Calander;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class CalendarController : BaseController
    {
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.NewsService.INewsService _newsService;
        public readonly IEncryption _encryptionService;

        public CalendarController(NEPService.EducationService.IEducationService educationService, NEPService.NewsService.INewsService newsService, IEncryption encryptionService)
        {
            _educationService = educationService;
            _encryptionService = encryptionService;
            _newsService = newsService;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult getEvents(double start, double end)
        {
            var startDate = ConvertFromUnixTimestamp(start);
            var endDate = ConvertFromUnixTimestamp(end);

            var coursesList = _educationService.GetOnSiteEducationByStartDate(startDate, endDate);
            var eventsList = _newsService.GetEventsByEventDateRange(startDate, endDate, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            int _orgID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));

            var filterEventsList1 = (from evnt in eventsList
                                     where !coursesList.Any(hp => (hp.EducationID == evnt.EducationID) && (hp.CourseStartDate == evnt.EventDate) )
                                     select evnt).ToList();


            var eventList1 = (from e in coursesList
                              select new
                              {
                                  id = e.EducationID,
                                  title = e.CourseName,
                                  start = e.CourseStartDate.Value.ToString("s"),
                                  eventDesc = "",
                                  CourseLocation = (e.CourseLocation == null) ? "" : e.CourseLocation,
                                  courseStartTime = (e.CourseStartTime == null) ? "" : e.CourseStartTime,
                                  CoursePresenterName = (e.CoursePresenterName == null) ? "" : e.CoursePresenterName,
                                  coursePageUrl = ".." + GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(e.EducationID.ToString()),
                                  eventType = "OnSiteCourse",
                                  EventCourseNews = e.CourseName,
                                  OrganizationID = _orgID
                              }).ToList();

            var eventList2 = (from e in filterEventsList1.Where(hp => hp.NewsID != null)
                              select new
                              {
                                  id = (int)e.NewsID,
                                  title = e.EventName,
                                  start = e.EventDate.ToString("s"),
                                  eventDesc = e.EventDescription,
                                  CourseLocation = "",
                                  courseStartTime = "",
                                  CoursePresenterName = "",
                                  coursePageUrl = ".." + GlobalConst.routeedURL.newsDetailPage + _encryptionService.EncryptString2(e.NewsID.ToString()) + "/" + e.NewsType,
                                  eventType = "News",
                                  EventCourseNews = e.NewsTitle,
                                  OrganizationID = e.OrganizationID
                              }).ToList();


            var eventList3 = (from e in filterEventsList1.Where(hp => hp.EducationID != null)
                              select new
                              {
                                  id = (int)e.EducationID,
                                  title = e.EventName,
                                  start = e.EventDate.ToString("s"),
                                  eventDesc = e.EventDescription,
                                  CourseLocation = "",
                                  courseStartTime = "",
                                  CoursePresenterName = "",
                                  coursePageUrl = ".." + GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(e.EducationID.ToString()),
                                  eventType = "EventCourse",
                                  EventCourseNews = e.CourseName,
                                  OrganizationID = e.OrganizationID
                              }).ToList();

            var finalResult = eventList1.Union(eventList2.Union(eventList3));
            return Json(finalResult.ToArray(), JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult GetEventData(string start, string end)
        {
            var startDate = Convert.ToDateTime(start);
            var endDate = Convert.ToDateTime(end);

            var coursesList = _educationService.GetOnSiteEducationByStartDate(startDate, endDate);
            var eventsList = _newsService.GetEventsByEventDateRange(startDate, endDate, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));

            int _orgID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));

            var filterEventsList1 = (from evnt in eventsList
                                     where !coursesList.Any(hp => (hp.EducationID == evnt.EducationID) && (hp.CourseStartDate == evnt.EventDate))
                                     select evnt).ToList();


            var eventList1 = (from e in coursesList
                              select new
                              {
                                  id = (int)e.EducationID,
                                  title = e.CourseName,
                                  start = e.CourseStartDate.Value.ToString("s"),
                                  eventDesc = "",
                                  CourseLocation = (e.CourseLocation == null) ? "" : e.CourseLocation,
                                  courseStartTime = (e.CourseStartTime == null) ? "" : e.CourseStartTime,
                                  CoursePresenterName = (e.CoursePresenterName == null) ? "" : e.CoursePresenterName,
                                  coursePageUrl = ".." + GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(e.EducationID.ToString()),
                                  eventType = "OnSiteCourse",
                                  EventCourseNews = e.CourseName,
                                  OrganizationID = _orgID
                              }).ToList();

            var eventList2 = (from e in filterEventsList1.Where(hp => hp.NewsID != null)
                              select new
                              {
                                  id = (int)e.NewsID,
                                  title = e.EventName,
                                  start = e.EventDate.ToString("s"),
                                  eventDesc = e.EventDescription,
                                  CourseLocation = "",
                                  courseStartTime = "",
                                  CoursePresenterName = "",
                                  coursePageUrl = ".." + GlobalConst.routeedURL.newsDetailPage + _encryptionService.EncryptString2(e.NewsID.ToString()) + "/" + e.NewsType,
                                  eventType = "News",
                                  EventCourseNews = e.NewsTitle,
                                  OrganizationID = e.OrganizationID
                              }).ToList();


            var eventList3 = (from e in filterEventsList1.Where(hp => hp.EducationID != null)
                              select new
                              {
                                  id = (int)e.EducationID,
                                  title = e.EventName,
                                  start = e.EventDate.ToString("s"),
                                  eventDesc = e.EventDescription,
                                  CourseLocation = "",
                                  courseStartTime = "",
                                  CoursePresenterName = "",
                                  coursePageUrl = ".." + GlobalConst.routeedURL.courseDetailPage + _encryptionService.EncryptString2(e.EducationID.ToString()),
                                  eventType = "EventCourse",
                                  EventCourseNews = e.CourseName,
                                  OrganizationID = e.OrganizationID
                              }).ToList();

            var finalResult = eventList1.Union(eventList2.Union(eventList3)).ToArray();
            object[] obj = { finalResult, finalResult.Count() };
            return Json(obj);
        }

        [HttpPost]
        public ActionResult GetEventDataPagng(IEnumerable<Calander> calenderData, int skip)
        {    
            var _orgID = calenderData.Select(mahi => new { mahi.OrganizationID }).ToList();

            int _OrganizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_OrganizationID == _orgID[0].OrganizationID)
            {
                var finalResult = calenderData.OrderBy(preet => preet.start).Skip(skip).Take(GlobalConst.Records.Take5).ToList();
                return Json(finalResult);
            }
            else{
                Calander obj = new Calander();
                return Json(obj);
            }
        }

        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}