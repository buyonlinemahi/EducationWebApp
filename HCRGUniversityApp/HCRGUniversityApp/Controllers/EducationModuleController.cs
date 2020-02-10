using AutoMapper;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using HCRGUniversityApp.Domain.ViewModels.EducationShoppingCartTempViewModel;
using HCRGUniversityApp.Infrastructure.Base;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class EducationModuleController : BaseController
    {
        //
        // GET: /EducationModule/
        private readonly NEPService.EducationModuleService.IEducationModuleService _educationModuleService;
        private readonly NEPService.ShoppingEducationService.IShoppingEducationService _shoppingeducationService;

        public EducationModuleController(NEPService.EducationModuleService.IEducationModuleService educationModuleService, NEPService.ShoppingEducationService.IShoppingEducationService shoppingeducationService)
        {
            _educationModuleService = educationModuleService;
            _shoppingeducationService = shoppingeducationService;
        }

        public ActionResult Index()
        {
            int educationID = 0;
            EducationShoppingCartTempViewModel educationshoppingModel = new EducationShoppingCartTempViewModel();
            educationshoppingModel.EducationShoppingCartTempResults = Mapper.Map<IEnumerable<EducationShoppingCart>>(_shoppingeducationService.GetEducationShoppingCartByUserID(HCRGUser.UID));
            foreach (EducationShoppingCart viewmodel in educationshoppingModel.EducationShoppingCartTempResults)
                educationID = viewmodel.TempID;
            return View();
        }
    }
}
