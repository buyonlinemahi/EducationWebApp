using AutoMapper;
using HCRGUniversityApp.Domain.Models.ExamModel;
using HCRGUniversityApp.Domain.ViewModels.ExamViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace HCRGUniversityApp.Controllers
{
    [AuthorizedUserCheck]
    public class ExamController : BaseController
    {
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        private readonly NEPService.ExamInternalService.IExamInternalService _examInternalService;
        public readonly IEncryption _encryptionService;
        private readonly NEPService.LogSessionService.ILogSessionService _logService;
        public ExamController(NEPService.ExamQuestionService.IExamQuestionService examService, NEPService.ExamInternalService.IExamInternalService examInternalService, IEncryption encryptionService, NEPService.LogSessionService.ILogSessionService logService)
        {
            _examService = examService;
            _examInternalService = examInternalService;
            _encryptionService = encryptionService;
            _logService = logService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PreTest(string educationID, string meID)
        {

            educationID = _encryptionService.DecryptString2(educationID);
            meID = _encryptionService.DecryptString2(meID);
            PreTestQuestionDetailViewModel preTestQuestionDetailViewModel = new PreTestQuestionDetailViewModel();
            preTestQuestionDetailViewModel.PreTestAttemptByUser = _examInternalService.GetPreTestAttemptByUser(HCRGUser.UID, Convert.ToInt32(meID));
            if (preTestQuestionDetailViewModel.PreTestAttemptByUser < 1)
                preTestQuestionDetailViewModel.PreTestQuestionDetailResults = Mapper.Map<IEnumerable<PreTestQuestionDetail>>(_examService.GetAllPreTestQuestionDetailByEID(Convert.ToInt32(educationID)));
            return View(preTestQuestionDetailViewModel);
        }


        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "None")]
        public ActionResult FinalExam(string educationID, string meID)
        {
            if (Request.UrlReferrer != null)
            {
                educationID = _encryptionService.DecryptString2(educationID);
                meID = _encryptionService.DecryptString2(meID);
                /*****************************Code for  Get Already open course module and exam browser*********************************/
                var pageUrl = _logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, Convert.ToInt32(meID));
                /*****************************End*********************************/
                if (pageUrl == null)
                {
                    ExamQuestionDetailViewModel ExamQuestionDetailViewModel = new ExamQuestionDetailViewModel();
                    ExamQuestionDetailViewModel.ExamResult = Mapper.Map<IEnumerable<ExamResult>>(_examInternalService.GetExamAttempResulttByUser(HCRGUser.UID, Convert.ToInt32(meID)));
                    ExamQuestionDetailViewModel.FinalExamAttemptByUser = ExamQuestionDetailViewModel.ExamResult.Count();
                    if (ExamQuestionDetailViewModel.FinalExamAttemptByUser < 3)
                    {
                        ExamQuestionDetailViewModel.ExamQuestionDetailResults = Mapper.Map<IEnumerable<ExamQuestionDetail>>(_examService.GetAllExamQuestionDetailByEID(Convert.ToInt32(educationID)));
                        ExamQuestionDetailViewModel.EvaluationQuestionDetailResults = Mapper.Map<IEnumerable<EvaluationQuestionDetail>>(_examService.GetAllEvaluationQuestionDetailByEID(Convert.ToInt32(educationID)));
                        ExamQuestionDetailViewModel.EvaluationAnswers = Mapper.Map<IEnumerable<EvaluationAnswer>>(_examInternalService.GetAllEvaluationAnswer());
                        ExamQuestionDetailViewModel.UserID = HCRGUser.UID;
                        ExamQuestionDetailViewModel.MEID = Convert.ToInt32(meID);
                        ExamQuestionDetailViewModel.SessionTimeout = ((Session.Timeout * 60000) + 20000).ToString();
                    }
                    return View(ExamQuestionDetailViewModel);
                }
                else
                    return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
            }
            else
                return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
        }
        public ActionResult EvaluationExam(string educationID, string meID)
        {
            educationID = _encryptionService.DecryptString2(educationID);
            meID = _encryptionService.DecryptString2(meID);
            EvaluationQuestionDetailViewModel evaluationQuestionDetailViewModel = new EvaluationQuestionDetailViewModel();
            evaluationQuestionDetailViewModel.EvaluationQuestionDetailResults = Mapper.Map<IEnumerable<EvaluationQuestionDetail>>(_examService.GetAllEvaluationQuestionDetailByEID(Convert.ToInt32(educationID)));
            evaluationQuestionDetailViewModel.EvaluationAnswers = Mapper.Map<IEnumerable<EvaluationAnswer>>(_examInternalService.GetAllEvaluationAnswer());
            return View(evaluationQuestionDetailViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPreTestResult(IEnumerable<PreTestQuestionResult> PreTestQuestionResult, string MEID, IEnumerable<PreTestQuestionDetail> PreTestQuestionDetail)
        {
            int DencrptedMEID = Convert.ToInt32(_encryptionService.DecryptString2(MEID));
            int PreTestAttemptByUser = _examInternalService.GetPreTestAttemptByUser(HCRGUser.UID, DencrptedMEID);
            if (PreTestAttemptByUser < 1)
            {
                PreTestResult PreTestResult = new Domain.Models.ExamModel.PreTestResult();
                PreTestResult.UID = HCRGUser.UID;
                PreTestResult.MEID = DencrptedMEID;
                PreTestResult.PreTestID = PreTestQuestionDetail.ElementAt(0).PreTestID;
                int PreTestResultID = _examInternalService.AddPreTestResult(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.PreTestResult>(PreTestResult));
                foreach (PreTestQuestionResult pQuestionResult in PreTestQuestionResult)
                {
                    pQuestionResult.PreTestResultID = PreTestResultID;
                    if (pQuestionResult.PreTestAnswerType == GlobalConst.PreTestAnswerType.MultipleChoice)
                        pQuestionResult.PreTestAnswerTrueFalse = null;
                    else
                        pQuestionResult.PreTestAnswer = null;
                    int PreTestQuestionResultID = _examInternalService.AddPreTestQuestionResults(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.PreTestQuestionResult>(pQuestionResult));
                }
            }
            return Json(PreTestAttemptByUser, GlobalConst.Message.text_html);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddExamResult(IEnumerable<ExamQuestionResult> ExamQuestionResult, string MEID, List<ExamQuestionDetail> ExamQuestionDetail)
        {
            string QuestionCorrectAns = "";
            int DencrptedMEID = Convert.ToInt32(_encryptionService.DecryptString2(MEID));
            ExamResultViewModel ExamResultViewModel = new ExamResultViewModel();
            ExamResultViewModel.ExamResult = Mapper.Map<IEnumerable<ExamResult>>(_examInternalService.GetExamAttempResulttByUser(HCRGUser.UID, Convert.ToInt32(DencrptedMEID)));
            ExamResultViewModel.FinalExamAttemptByUser = ExamResultViewModel.ExamResult.Count();
            if (ExamResultViewModel.FinalExamAttemptByUser < 3)
            {
                ExamResult ExamResult = new Domain.Models.ExamModel.ExamResult();
                ExamResult.UID = HCRGUser.UID;
                ExamResult.MEID = DencrptedMEID;
                ExamResult.ExamID = ExamQuestionDetail.ElementAt(0).ExamID;
                int correctAnsCount = 0;
                int ExamResultID = _examInternalService.AddExamResult(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.ExamResult>(ExamResult));
                foreach (ExamQuestionResult eQuestionResult in ExamQuestionResult)
                {
                    eQuestionResult.ExamResultID = ExamResultID;
                    if (eQuestionResult.ExamAnswerType == GlobalConst.ExamAnswerType.MultipleChoice || eQuestionResult.ExamAnswerType == null)
                    {
                        eQuestionResult.ExamAnswerTrueFalse = null;
                        QuestionCorrectAns = (from ExamQuestion in ExamQuestionDetail
                                              where
                                                  ExamQuestion.ExamQuestionID == eQuestionResult.ExamQuestionID
                                              select ExamQuestion.ExamAnswer).First().ToString();
                        if (eQuestionResult.ExamAnswer == QuestionCorrectAns)
                            correctAnsCount = correctAnsCount + 1;
                    }
                    else
                    {
                        eQuestionResult.ExamAnswer = null;
                        QuestionCorrectAns = (from ExamQuestion in ExamQuestionDetail
                                              where
                                                  ExamQuestion.ExamQuestionID == eQuestionResult.ExamQuestionID
                                              select ExamQuestion.ExamAnswerTrueFalse).First().ToString();
                        if (eQuestionResult.ExamAnswerTrueFalse == Convert.ToBoolean(QuestionCorrectAns))
                            correctAnsCount = correctAnsCount + 1;
                    }
                    int ExamQuestionResultID = _examInternalService.AddExamQuestionResults(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.ExamQuestionResult>(eQuestionResult));
                }
                ExamResultViewModel.TotalPercentage = correctAnsCount * 100 / ExamQuestionResult.Count();
                ExamResultViewModel.encryptedTotalPercentage = _encryptionService.EncryptString2(ExamResultViewModel.TotalPercentage.ToString());
                if (ExamResultViewModel.TotalPercentage >= 70)
                    _examInternalService.UpdateExamResultIsPass(ExamResultID, DencrptedMEID, true);
                else
                    _examInternalService.UpdateExamResultIsPass(ExamResultID, DencrptedMEID, false);
            }
            return Json(ExamResultViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEvaluationResult(IEnumerable<EvaluationQuestionResult> EvaluationQuestionResult, string MEID, int EvaluationID, string Comments, string Suggestions)
        {
            int DencrptedMEID = Convert.ToInt32(_encryptionService.DecryptString2(MEID));
            EvaluationResult EvaluationResult = new Domain.Models.ExamModel.EvaluationResult();
            EvaluationResult.UID = HCRGUser.UID;
            EvaluationResult.MEID = DencrptedMEID;
            EvaluationResult.EvaluationID = EvaluationID;
            EvaluationResult.Comments = Comments;
            EvaluationResult.Suggestions = Suggestions;
            int EvaluationResultID = _examInternalService.AddEvaluationResult(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.EvaluationResult>(EvaluationResult));
            foreach (EvaluationQuestionResult eQuestionResult in EvaluationQuestionResult)
            {
                eQuestionResult.EvaluationResultID = EvaluationResultID;
                int EvaluationQuestionResultID = _examInternalService.AddEvaluationQuestionResults(Mapper.Map<HCRGUniversityApp.NEPService.ExamInternalService.EvaluationQuestionResult>(eQuestionResult));
            }
            return Json(GlobalConst.Message.EvaluationSubmittedSuccessfully, GlobalConst.Message.text_html);
        }
        public ActionResult ExamResult(string Resultno, string educationID, string meID,string totalAttempt)
        {
            int DencrptedMEID = Convert.ToInt32(_encryptionService.DecryptString2(meID));
            int DencrptededucationID = Convert.ToInt32(_encryptionService.DecryptString2(educationID));
            decimal DencrptedResultno = Convert.ToDecimal(_encryptionService.DecryptString2(Resultno));
            var EducationEvaluation = _examService.GetEducationEvaluationByEducationID(DencrptededucationID);
            ExamResultCard ExamResultCard = new ExamResultCard();
            ExamResultCard.EducationID = DencrptededucationID;
            if (EducationEvaluation != null)
                ExamResultCard.IsEvalutionAvailable = true;
            ExamResultCard.MEID = DencrptedMEID;
            ExamResultCard.TotalPercentage = DencrptedResultno;
            ExamResultCard.EncryptedMEID = meID;
            ExamResultCard.EncryptedEducationID = educationID;
            ExamResultCard.totalAttemptByUser = Convert.ToInt32(totalAttempt);
            return View(ExamResultCard);
        }
        public ActionResult GetExamQuestionWrongAnswered(string meID)
        {
            IEnumerable<ExamQuestionDetail> _ExamQuestionDetail;
            _ExamQuestionDetail = Mapper.Map<IEnumerable<ExamQuestionDetail>>(_examService.GetExamQuestionWrongAnswered(int.Parse(meID)));
            return Json(_ExamQuestionDetail);
        }
    }
}