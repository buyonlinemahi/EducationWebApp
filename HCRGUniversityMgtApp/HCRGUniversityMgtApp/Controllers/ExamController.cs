using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.ExamModel;
using HCRGUniversityMgtApp.Domain.ViewModels.ExamViewModel;
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
    public class ExamController : BaseController
    {
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        public readonly IEncryption _encryptionService;
        public ExamController(NEPService.ExamQuestionService.IExamQuestionService examService, IEncryption encryptionService)
        {
            _examService = examService;
            _encryptionService = encryptionService;
        }
        public ActionResult Index()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedExam pagedExamModel = new PagedExam();
            pagedExamModel = Mapper.Map<PagedExam>(_examService.GetAllPagedExam("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, _organizationID));
            pagedExamModel.PagedRecords = GlobalConst.Records.Take;
            foreach (var objResult in pagedExamModel.Exams )
                objResult.EncryptedExamID = EncryptString(objResult.ExamID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                pagedExamModel.IsHCRGAdmin = true;
            else
                pagedExamModel.IsHCRGAdmin = false;
            return View(pagedExamModel);
        }

        [HttpPost]
        public JsonResult GetAllExamByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<PagedExam>(_examService.GetAllPagedExam("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, OrgID));
            foreach (var objResult in data.Exams)
                objResult.EncryptedExamID = EncryptString(objResult.ExamID.ToString());
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
            PagedExam pagedExamModel = new PagedExam();
            pagedExamModel = Mapper.Map<PagedExam>(_examService.GetAllPagedExam(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, _organizationID));
            foreach (var objResult in pagedExamModel.Exams)
                objResult.EncryptedExamID = EncryptString(objResult.ExamID.ToString());
            pagedExamModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedExamModel);
        }

        [HttpGet]
        public ActionResult EditExam(string ExamID)
        {
            int _Id = Convert.ToInt32(DecryptString(ExamID.ToString()));
            PagedExamQuestionDetail pagedExamQuestionDetail = new PagedExamQuestionDetail();
            pagedExamQuestionDetail.pagedExamQuestion = Mapper.Map<PagedExamQuestion>(_examService.GetAllPagedExamQuestionByExamID(_Id, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake));
            pagedExamQuestionDetail.pagedExamQuestion.PagedRecords = GlobalConst.Records.LandingTake;
            pagedExamQuestionDetail.Exam = Mapper.Map<Exam>(_examService.GetExamByID(_Id));
            return View("Add",pagedExamQuestionDetail);
        }
        [HttpGet]
        public ActionResult Add() 
        {
            return View("Add");
        }
        [HttpPost]
        public ActionResult Add(ExamDetailViewModel ExamDetailViewModel)
        {
            if (ExamDetailViewModel.Exam.ExamID == 0)
            {
                ExamDetailViewModel.Exam.ExamStatus = true;
                ExamDetailViewModel.Exam.ClientID = HCRGCLIENT.ClientID;
                ExamDetailViewModel.Exam.ExamID = _examService.AddExam(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.Exam>(ExamDetailViewModel.Exam));
            }
            if (ExamDetailViewModel.ExamQuestion.ExamAnswerType == GlobalConst.ExamAnswerType.MultipleChoice)
                ExamDetailViewModel.ExamQuestion.ExamAnswerTrueFalse = null;
            else
            {
                ExamDetailViewModel.ExamQuestion.ExamOptionA = null;
                ExamDetailViewModel.ExamQuestion.ExamOptionB = null;
                ExamDetailViewModel.ExamQuestion.ExamOptionC = null;
                ExamDetailViewModel.ExamQuestion.ExamOptionD = null;
                ExamDetailViewModel.ExamQuestion.ExamAnswer = null;
            }
            ExamDetailViewModel.ExamQuestion.ExamID = ExamDetailViewModel.Exam.ExamID;
            if (ExamDetailViewModel.ExamQuestion.ExamQuestionID == 0)
            {
                ExamDetailViewModel.ExamQuestion.ExamQuestionID = _examService.AddExamQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.ExamQuestion>(ExamDetailViewModel.ExamQuestion));
                ExamDetailViewModel.ExamQuestion.flag = true;
            }
            else
            {
                _examService.UpdateExamQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.ExamQuestion>(ExamDetailViewModel.ExamQuestion));
                ExamDetailViewModel.ExamQuestion.flag = false;
            }
            return Json(ExamDetailViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetAllPagedExam(int skip, string searchText)
        {
            PagedExam pagedExamModel = new PagedExam();
            pagedExamModel = Mapper.Map<PagedExam>(_examService.GetAllPagedExam(searchText == null ? "" : searchText, skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, HCRGCLIENT.OrganizationID));
            pagedExamModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedExamModel, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public JsonResult GetAllActiveExam()
        {
            ExamDetailViewModel examModel = new ExamDetailViewModel();
            examModel.ExamResults = Mapper.Map<IEnumerable<Exam>>(_examService.GetAllActiveExam(HCRGCLIENT.ClientID));
            return Json(examModel.ExamResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

       
         [HttpPost]
        public ActionResult GetAllPagedExamQuestionByExamID(int ExamID,int skip)
        {
            PagedExamQuestionDetail pagedExamQuestionDetail = new PagedExamQuestionDetail();
            pagedExamQuestionDetail.pagedExamQuestion = Mapper.Map<PagedExamQuestion>(_examService.GetAllPagedExamQuestionByExamID(ExamID, skip, GlobalConst.Records.Take));
            pagedExamQuestionDetail.pagedExamQuestion.PagedRecords = GlobalConst.Records.Take;
            pagedExamQuestionDetail.Exam = Mapper.Map<Exam>(_examService.GetExamByID(ExamID));
            return Json(pagedExamQuestionDetail, GlobalConst.Message.text_html);
        }
    }
}