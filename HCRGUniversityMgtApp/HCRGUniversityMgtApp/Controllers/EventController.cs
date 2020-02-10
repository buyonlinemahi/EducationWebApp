using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EventModel;
using HCRGUniversityMgtApp.Domain.Models.NewsModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EventController : BaseController
    {
        private readonly NEPService.NewsService.INewsService _NewsService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public EventController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;
        }
        public ActionResult Index(int? skip)
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedEvents eventModel = new PagedEvents();
            if (skip == null)
            {
                eventModel = Mapper.Map<PagedEvents>(_NewsService.getAllEventsPaged(GlobalConst.Records.Skip, GlobalConst.Records.take5000, HCRGCLIENT.ClientID, _organizationID));
                if (HCRGCLIENT.ClientTypeID == 1)
                    eventModel.IsHCRGAdmin = true;
                else
                    eventModel.IsHCRGAdmin = false;
                return View(eventModel);
            }
            else
            {
                eventModel = Mapper.Map<PagedEvents>(_NewsService.getAllEventsPaged(skip.Value, GlobalConst.Records.take5000, HCRGCLIENT.ClientID, _organizationID));
                return Json(eventModel, GlobalConst.Message.text_html);
            }
        }
        [HttpPost]
        public JsonResult GetAllEventsByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<PagedEvents>(_NewsService.getAllEventsPaged(GlobalConst.Records.Skip, GlobalConst.Records.take5000, HCRGCLIENT.ClientID, OrgID));
            return Json(data);
        }
        [HttpPost]
        public ActionResult Add(Event _Event)
        {
            if (_Event.EventID == 0)
            {
                _Event.OrganizationID = HCRGCLIENT.OrganizationID;
                _Event.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                _Event.EventID = _NewsService.AddEvent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Event>(_Event));
                _Event.flag = true;
            }
            else
            {
                _Event.OrganizationID = _Event.OrganizationID;
                _Event.OrganizationName = (_clientService.GetOrganizationByID(_Event.OrganizationID)).OrganizationName;
                int EventID = _NewsService.UpdateEvent(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Event>(_Event));
                _Event.flag = false;
            }
            return Json(_Event, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetNewsByNewsTitle(string newsTitle, int? skip)
        {
            PagedNews pagedNews = new PagedNews();
            if (skip == null)
                pagedNews = Mapper.Map<PagedNews>(_NewsService.GetNewsByNewsTitlePaged(newsTitle, HCRGCLIENT.OrganizationID, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            else
                pagedNews = Mapper.Map<PagedNews>(_NewsService.GetNewsByNewsTitlePaged(newsTitle, HCRGCLIENT.OrganizationID, skip.Value, GlobalConst.Records.Take));

            return Json(pagedNews, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetNewsdetailByID(int newsID)
        {
            News news = new News();
            news = Mapper.Map<News>(_NewsService.GetNewsByID(newsID));
            return Json(news.NewsTitle, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult DeleteEvent(Event deleteEvent)
        {
            _NewsService.DeleteEvent(deleteEvent.EventID);
            return Json("Event Deleted Successfully");
        }
    }
}