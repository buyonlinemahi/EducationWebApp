using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.PreTestModel;
using HCRGUniversityMgtApp.Domain.ViewModels.PreTestViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class PreTestController : BaseController
    {
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        public readonly IEncryption _encryptionService;
        public PreTestController(NEPService.ExamQuestionService.IExamQuestionService examService, IEncryption encryptionService)
        {
            _examService = examService;
            _encryptionService = encryptionService;
        }
        public ActionResult Index()
        {
            ViewBag.Message = TempData["message"];
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedPreTest pagedPreTestModel = new PagedPreTest();
            pagedPreTestModel = Mapper.Map<PagedPreTest>(_examService.GetAllPagedPreTest("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, _organizationID));
            pagedPreTestModel.PagedRecords = GlobalConst.Records.Take;
            foreach (var objResult in pagedPreTestModel.PreTests)
                objResult.EncryptedPreTestID = EncryptString(objResult.PreTestID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                pagedPreTestModel.IsHCRGAdmin = true;
            else
                pagedPreTestModel.IsHCRGAdmin = false;
            return View(pagedPreTestModel);
        }

        [HttpPost]
        public JsonResult GetAllPreTestByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<PagedPreTest>(_examService.GetAllPagedPreTest("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, OrgID));
            foreach (var objResult in data.PreTests)
                objResult.EncryptedPreTestID = EncryptString(objResult.PreTestID.ToString());
            return Json(data);
        }

        [HttpPost]
        public ActionResult Index(string searchText)
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedPreTest pagedPreTestModel = new PagedPreTest();
            pagedPreTestModel = Mapper.Map<PagedPreTest>(_examService.GetAllPagedPreTest(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, _organizationID));       
            pagedPreTestModel.PagedRecords = GlobalConst.Records.Take;
            foreach (var objResult in pagedPreTestModel.PreTests)
                objResult.EncryptedPreTestID = EncryptString(objResult.PreTestID.ToString());
            return Json(pagedPreTestModel);
        }
        [HttpGet]
        public ActionResult EditPreTest(string PreTestID)
        {
            int _preID = Convert.ToInt32(DecryptString(PreTestID.ToString()));
            PagedPreTestQuestionDetail pagedPreTestQuestionDetail = new PagedPreTestQuestionDetail();
            pagedPreTestQuestionDetail.pagedPreTestQuestion = Mapper.Map<PagedPreTestQuestion>(_examService.GetAllPagedPreTestQuestionByPreTestID(_preID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake));
            pagedPreTestQuestionDetail.pagedPreTestQuestion.PagedRecords = GlobalConst.Records.LandingTake;
            pagedPreTestQuestionDetail.preTest = Mapper.Map<PreTest>(_examService.GetPreTestByID(_preID));
            return View("Add",pagedPreTestQuestionDetail);
        }
        [HttpGet]
        public ActionResult Add() 
        {
            return View("Add");
        }
        [HttpPost]
        public ActionResult Add(PreTestDetailViewModel preTestDetailViewModel)
        {
            if (preTestDetailViewModel.preTest.PreTestID == null || preTestDetailViewModel.preTest.PreTestID == 0)
            {
                preTestDetailViewModel.preTest.PreTestStatus = true;
                preTestDetailViewModel.preTest.ClientID = HCRGCLIENT.ClientID;
                preTestDetailViewModel.preTest.PreTestID = _examService.AddPreTest(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.PreTest>(preTestDetailViewModel.preTest));
            }
            preTestDetailViewModel.preTestQuestion.PreTestID = preTestDetailViewModel.preTest.PreTestID;
            if (preTestDetailViewModel.preTestQuestion.PreTestAnswerType == GlobalConst.PreTestAnswerType.MultipleChoice)
                preTestDetailViewModel.preTestQuestion.PreTestAnswerTrueFalse = null;
            else
            {
                preTestDetailViewModel.preTestQuestion.PreTestOptionA = null;
                preTestDetailViewModel.preTestQuestion.PreTestOptionB = null;
                preTestDetailViewModel.preTestQuestion.PreTestOptionC = null;
                preTestDetailViewModel.preTestQuestion.PreTestOptionD = null;
                preTestDetailViewModel.preTestQuestion.PreTestAnswer = null;
            }
            if (preTestDetailViewModel.preTestQuestion.PreTestQuestionID == null || preTestDetailViewModel.preTestQuestion.PreTestQuestionID == 0)
            {
                preTestDetailViewModel.preTestQuestion.PreTestQuestionID = _examService.AddPreTestQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.PreTestQuestion>(preTestDetailViewModel.preTestQuestion));
                preTestDetailViewModel.preTestQuestion.flag = true;
                TempData["message"] = GlobalConst.Message.PretestSave;
            }
            else
            {           
                _examService.UpdatePreTestQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.PreTestQuestion>(preTestDetailViewModel.preTestQuestion));
                preTestDetailViewModel.preTestQuestion.flag = false;
                TempData["message"] = GlobalConst.Message.PretestUpdated;
            }
            return Json(preTestDetailViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetAllPagedPreTest(int skip, string searchText)
        {
            PagedPreTest pagedPreTestModel = new PagedPreTest();
            pagedPreTestModel = Mapper.Map<PagedPreTest>(_examService.GetAllPagedPreTest(searchText == null ? "" : searchText, skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, HCRGCLIENT.OrganizationID));
            pagedPreTestModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedPreTestModel, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public JsonResult GetAllActivePreTest()
        {
            PreTestDetailViewModel preTestModel = new PreTestDetailViewModel();
            preTestModel.PreTestResults = Mapper.Map<IEnumerable<PreTest>>(_examService.GetAllActivePreTest(HCRGCLIENT.ClientID));
            return Json(preTestModel.PreTestResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

         [HttpPost]
        public ActionResult GetAllPagedPreTestQuestionByPreTestID(int PreTestID,int skip)
        {
            PagedPreTestQuestionDetail pagedPreTestQuestionDetail = new PagedPreTestQuestionDetail();
            pagedPreTestQuestionDetail.pagedPreTestQuestion = Mapper.Map<PagedPreTestQuestion>(_examService.GetAllPagedPreTestQuestionByPreTestID(PreTestID, skip, GlobalConst.Records.Take));
            pagedPreTestQuestionDetail.pagedPreTestQuestion.PagedRecords = GlobalConst.Records.Take;
            pagedPreTestQuestionDetail.preTest = Mapper.Map<PreTest>(_examService.GetPreTestByID(PreTestID));
            return Json(pagedPreTestQuestionDetail, GlobalConst.Message.text_html);
        }
    }
}