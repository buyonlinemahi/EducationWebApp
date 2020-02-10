using AutoMapper;
using HCRGUniversityMgtApp.Domain.ViewModels.CarouselSetUpViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.NewsViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;
using DomainModelCarousel = HCRGUniversityMgtApp.Domain.Models.CarouselSetUp;
using DomainModelModel = HCRGUniversityMgtApp.Domain.Models.NewsModel;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class CarouselSetUpController : BaseController
    {
        NEPService.NewsService.INewsService _carouselNewsService;
        private readonly NEPService.ClientService.IClientService _clientService;

        public CarouselSetUpController(NEPService.NewsService.INewsService carouselNewsService, NEPService.ClientService.IClientService clientService)
        {
            _carouselNewsService = carouselNewsService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            var carouselSearchData = _carouselNewsService.GetAllCarouselSetUp(0, HCRGCLIENT.ClientID);
            CarouselSetUpSearchViewModel objcarouselSetUp = new CarouselSetUpSearchViewModel();
            objcarouselSetUp.carouselSetupResult = Mapper.Map<IEnumerable<DomainModelCarousel.CarouselSetUp>>(carouselSearchData);
            if (HCRGCLIENT.ClientTypeID == 1)
                objcarouselSetUp.IsHCRGAdmin = true;
            else
                objcarouselSetUp.IsHCRGAdmin = false;
            return View(objcarouselSetUp);
        }
        [HttpPost]
        public JsonResult GetCarouselSetUpByOrganizationID(int organizationID)
        {
            var carouselSearchData = _carouselNewsService.GetAllCarouselSetUp(organizationID, HCRGCLIENT.ClientID);
            CarouselSetUpSearchViewModel objcarouselSetUp = new CarouselSetUpSearchViewModel();
            objcarouselSetUp.carouselSetupResult = Mapper.Map<IEnumerable<DomainModelCarousel.CarouselSetUp>>(carouselSearchData);
            return Json(objcarouselSetUp);
        }
        [HttpPost]
        public ActionResult AddCarousel(DomainModelCarousel.CarouselSetUp modelCarouselSetup)
        {
            modelCarouselSetup.OrganizationID = HCRGCLIENT.OrganizationID;
            if (modelCarouselSetup.CarouselID == 0)
            {
                modelCarouselSetup.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                var result = _carouselNewsService.AddCarouselSetup(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.CarouselSetUp>(modelCarouselSetup));
                modelCarouselSetup.CarouselID = result;
                modelCarouselSetup.NewsDescription = "Add";
            }
            else if (modelCarouselSetup.CarouselID != 0)
            {                
                var result = _carouselNewsService.UpdateCarouselSetUp(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.CarouselSetUp>(modelCarouselSetup));
                modelCarouselSetup.NewsDescription = "Update";
            }
            return Json(modelCarouselSetup);
        }
        [HttpPost]
        public ActionResult SearchNewsSection(string NewsTitle)
        {
            var getNewsDataWithNewsTitle = _carouselNewsService.GetNewsDetailsAccordingToNewsSearch(NewsTitle, HCRGCLIENT.OrganizationID);
            NewsSearchCarouselViewModel newsSectionViewModel = new NewsSearchCarouselViewModel();
            newsSectionViewModel.NewsSearchCarouselResult = Mapper.Map<IEnumerable<DomainModelModel.NewsSearchCarousel>>(getNewsDataWithNewsTitle);
            return Json(newsSectionViewModel.NewsSearchCarouselResult, GlobalConst.Message.text_html);
        }
    }
}