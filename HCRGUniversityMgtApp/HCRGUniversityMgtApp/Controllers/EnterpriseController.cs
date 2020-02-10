using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Common;
using HCRGUniversityMgtApp.Domain.Models.Enterprise;
using HCRGUniversityMgtApp.Domain.ViewModels.EnterpriseViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EnterpriseController : BaseController
    {
        // GET: Enterprise

        private readonly NEPService.CommonService.ICommonService _commonService;

        public EnterpriseController(NEPService.CommonService.ICommonService commonService)
        {
            _commonService = commonService;
        }

        public ActionResult Index()
        {
            PagedEnterprise pagedEnterpriseModel = new PagedEnterprise();
            pagedEnterpriseModel = Mapper.Map<PagedEnterprise>(_commonService.getEnterpriseByEnterpriseClientName("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            pagedEnterpriseModel.PagedRecords = GlobalConst.Records.Take;
            return View(pagedEnterpriseModel);
        }

        [HttpPost]
        public ActionResult Index(string searchText)
        {
            PagedEnterprise pagedEnterpriseModel = new PagedEnterprise();
            if (searchText.Trim() == "")
                pagedEnterpriseModel = Mapper.Map<PagedEnterprise>(_commonService.getEnterpriseByEnterpriseClientName("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            else
                pagedEnterpriseModel = Mapper.Map<PagedEnterprise>(_commonService.getEnterpriseByEnterpriseClientName(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            pagedEnterpriseModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedEnterpriseModel);
        }

        [HttpPost]
        public ActionResult GetAllPagedEnterprise(int skip, string searchText)
        {
            PagedEnterprise pagedEnterpriseModel = new PagedEnterprise();
            if (searchText.Trim() == "")
                pagedEnterpriseModel = Mapper.Map<PagedEnterprise>(_commonService.getEnterpriseByEnterpriseClientName("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            else
                pagedEnterpriseModel = Mapper.Map<PagedEnterprise>(_commonService.getEnterpriseByEnterpriseClientName(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID));
            pagedEnterpriseModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedEnterpriseModel, GlobalConst.Message.text_html);
        }

        public IEnumerable<State> getAllState()
        {
            return Mapper.Map<IEnumerable<State>>(_commonService.getAllState());
        }

        [HttpGet]
        public ActionResult EditEnterprise(int EnterpriseID)
        {
            EnterpriseViewModel _enterpriseViewModel = new EnterpriseViewModel();
            _enterpriseViewModel.StateResults = getAllState();

            _enterpriseViewModel.Enterprise = Mapper.Map<Enterprise>(_commonService.getEnterpriseByID(EnterpriseID));
            return View("Add", _enterpriseViewModel);
        }

        [HttpGet]
        public ActionResult Add()
        {
            EnterpriseViewModel _enterpriseViewModel = new EnterpriseViewModel();
            _enterpriseViewModel.StateResults = getAllState();
            return View(_enterpriseViewModel);
        }

        [HttpPost]
        public ActionResult Add(Enterprise _enterprise)
        {
            _enterprise.CreatedBy = HCRGCLIENT.ClientID;
            _enterprise.OrganizationID = HCRGCLIENT.OrganizationID;
            _enterprise.CreatedOn = DateTime.Now;
            if (_enterprise.EnterpriseID == 0)
            {
                _enterprise.EnterpriseID = _commonService.addEnterprise(Mapper.Map<NEPService.CommonService.Enterprise>(_enterprise));
                return Json(GlobalConst.Message.AddSucessfully, GlobalConst.Message.text_html);
            }
            else
            {
                _enterprise.EnterpriseID = _commonService.updateEnterprise(Mapper.Map<NEPService.CommonService.Enterprise>(_enterprise));
                return Json(GlobalConst.Message.UpdateSucessfully, GlobalConst.Message.text_html);
            }

        }
    }
}