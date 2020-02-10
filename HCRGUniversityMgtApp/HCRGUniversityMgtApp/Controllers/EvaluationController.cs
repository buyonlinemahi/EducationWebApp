using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EvaluationModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EvaluationViewModel;
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
    public class EvaluationController : BaseController
    {
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        public readonly IEncryption _encryptionService;
        public EvaluationController(NEPService.ExamQuestionService.IExamQuestionService examService, IEncryption encryptionService)
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
            PagedEvaluation pagedEvaluationModel = new PagedEvaluation();
            pagedEvaluationModel = Mapper.Map<PagedEvaluation>(_examService.GetAllPagedEvaluation("", GlobalConst.Records.Skip, GlobalConst.Records.take500, HCRGCLIENT.ClientID, _organizationID));
            pagedEvaluationModel.PagedRecords = GlobalConst.Records.Take;
            foreach (var objResult in pagedEvaluationModel.Evaluations)
                objResult.EncryptedEvaluationID = EncryptString(objResult.EvaluationID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                pagedEvaluationModel.IsHCRGAdmin = true;
            else
                pagedEvaluationModel.IsHCRGAdmin = false;
            return View(pagedEvaluationModel);
        }

        [HttpPost]
        public JsonResult GetAllEvaluationByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<PagedEvaluation>(_examService.GetAllPagedEvaluation("", GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID,OrgID));
            foreach (var objResult in data.Evaluations)
                objResult.EncryptedEvaluationID = EncryptString(objResult.EvaluationID.ToString());
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
            PagedEvaluation pagedEvaluationModel = new PagedEvaluation();
            pagedEvaluationModel = Mapper.Map<PagedEvaluation>(_examService.GetAllPagedEvaluation(searchText, GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID, _organizationID));
            foreach (var objResult in pagedEvaluationModel.Evaluations)
                objResult.EncryptedEvaluationID = EncryptString(objResult.EvaluationID.ToString());
            pagedEvaluationModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedEvaluationModel);
        }
        [HttpGet]
        public ActionResult EditEvaluation(string EvaluationID)
        {
            int _Id = Convert.ToInt32(DecryptString(EvaluationID.ToString()));
            PagedEvaluationQuestionDetail pagedEvaluationQuestionDetail = new PagedEvaluationQuestionDetail();
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion = Mapper.Map<PagedEvaluationQuestion>(_examService.GetAllPagedEvaluationQuestionByEvaluationID(_Id, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake));
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion.PagedRecords = GlobalConst.Records.LandingTake;
            pagedEvaluationQuestionDetail.Evaluation = Mapper.Map<Evaluation>(_examService.GetEvaluationByID(_Id));
            return View("Add", pagedEvaluationQuestionDetail);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View("Add");
        }
        [HttpPost]
        public ActionResult Add(EvaluationDetailViewModel EvaluationDetailViewModel)
        {
            if (EvaluationDetailViewModel.Evaluation.EvaluationID == null || EvaluationDetailViewModel.Evaluation.EvaluationID == 0)
            {
                EvaluationDetailViewModel.Evaluation.EvaluationStatus = true;
                EvaluationDetailViewModel.Evaluation.ClientID = HCRGCLIENT.ClientID;

                EvaluationDetailViewModel.Evaluation.EvaluationID = _examService.AddEvaluation(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.Evaluation>(EvaluationDetailViewModel.Evaluation));
            }
            EvaluationDetailViewModel.EvaluationQuestion.EvaluationID = EvaluationDetailViewModel.Evaluation.EvaluationID;
            if (EvaluationDetailViewModel.EvaluationQuestion.EvaluationQuestionID == null || EvaluationDetailViewModel.EvaluationQuestion.EvaluationQuestionID == 0)
            {
                EvaluationDetailViewModel.EvaluationQuestion.IsStatus = true;
                EvaluationDetailViewModel.EvaluationQuestion.EvaluationQuestionID = _examService.AddEvaluationQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EvaluationQuestion>(EvaluationDetailViewModel.EvaluationQuestion));
                EvaluationDetailViewModel.EvaluationQuestion.flag = true;
                TempData["message"] = GlobalConst.Message.EvaluationSave;
            }
            else
            {
                _examService.UpdateEvaluationQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EvaluationQuestion>(EvaluationDetailViewModel.EvaluationQuestion));
                EvaluationDetailViewModel.EvaluationQuestion.flag = false;
                TempData["message"] = GlobalConst.Message.EvaluationUpdated;
            }
            return Json(EvaluationDetailViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult AddPredefinedQuestions(string EvaluationName, int EvaluationID)
        {
            Evaluation objEvaluation = new Evaluation();
            objEvaluation.EvaluationName = EvaluationName;
            objEvaluation.ClientID = HCRGCLIENT.ClientID;
            objEvaluation.EvaluationStatus = true;
            if (EvaluationID == 0)
                objEvaluation.EvaluationID = _examService.AddEvaluation(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.Evaluation>(objEvaluation));
            else 
                objEvaluation.EvaluationID = EvaluationID;
            int _evaluationQuestionID = _examService.AddEvaluationQuestionsFromEvaluationPredefinedQuestions(objEvaluation.EvaluationID);
            PagedEvaluationQuestionDetail pagedEvaluationQuestionDetail = new PagedEvaluationQuestionDetail();
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion = Mapper.Map<PagedEvaluationQuestion>(_examService.GetAllPagedEvaluationQuestionByEvaluationID(objEvaluation.EvaluationID, 0, GlobalConst.Records.Take));
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion.PagedRecords = GlobalConst.Records.Take;
            pagedEvaluationQuestionDetail.Evaluation = Mapper.Map<Evaluation>(_examService.GetEvaluationByID(objEvaluation.EvaluationID));
            return Json(pagedEvaluationQuestionDetail, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult UpdateEvalQuestions(EvaluationQuestion _evaluationQuestions)
        {
            if (_evaluationQuestions.IsStatus == true)
                _evaluationQuestions.IsStatus = false;
            else
                _evaluationQuestions.IsStatus = true;
            int i = _examService.UpdateEvaluationQuestionStatus(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EvaluationQuestion>(_evaluationQuestions));
            return Json(_evaluationQuestions, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult GetAllPagedEvaluation(int skip, string searchText)
        {
            PagedEvaluation pagedEvaluationModel = new PagedEvaluation();
            pagedEvaluationModel = Mapper.Map<PagedEvaluation>(_examService.GetAllPagedEvaluation(searchText == null ? "" : searchText, skip, GlobalConst.Records.Take, HCRGCLIENT.ClientID,HCRGCLIENT.OrganizationID));
            pagedEvaluationModel.PagedRecords = GlobalConst.Records.Take;
            return Json(pagedEvaluationModel, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public JsonResult GetAllActiveEvaluation()
        {
            EvaluationDetailViewModel EvaluationModel = new EvaluationDetailViewModel();
            EvaluationModel.EvaluationResults = Mapper.Map<IEnumerable<Evaluation>>(_examService.GetAllActiveEvaluation(HCRGCLIENT.ClientID));
            return Json(EvaluationModel.EvaluationResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllPagedEvaluationQuestionByEvaluationID(int EvaluationID, int skip)
        {
            PagedEvaluationQuestionDetail pagedEvaluationQuestionDetail = new PagedEvaluationQuestionDetail();
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion = Mapper.Map<PagedEvaluationQuestion>(_examService.GetAllPagedEvaluationQuestionByEvaluationID(EvaluationID, skip, GlobalConst.Records.Take));
            pagedEvaluationQuestionDetail.pagedEvaluationQuestion.PagedRecords = GlobalConst.Records.Take;
            pagedEvaluationQuestionDetail.Evaluation = Mapper.Map<Evaluation>(_examService.GetEvaluationByID(EvaluationID));
            return Json(pagedEvaluationQuestionDetail, GlobalConst.Message.text_html);
        }
    }
}