using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.AccreditorModel;
using HCRGUniversityMgtApp.Domain.Models.CollegeEducation;
using HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel;
using HCRGUniversityMgtApp.Domain.Models.EducationCredential;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatModel;
using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel;
using HCRGUniversityMgtApp.Domain.Models.EducationTypesAvailableModel;
using HCRGUniversityMgtApp.Domain.Models.EvaluationModel;
using HCRGUniversityMgtApp.Domain.Models.ExamModel;
using HCRGUniversityMgtApp.Domain.Models.PreTestModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc; 
using System.Web.UI.WebControls;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EducationController : BaseController
    {
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.EducationModuleService.IEducationModuleService _educationModuleService;
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        private readonly IStorage _storageService;
        public readonly IEncryption _encryptionService;
        public EducationController(NEPService.EducationService.IEducationService educationService, NEPService.EducationModuleService.IEducationModuleService educationModuleService, IStorage storageService, NEPService.ExamQuestionService.IExamQuestionService examService, IEncryption encryptionService)
        {
            _educationService = educationService;
            _educationModuleService = educationModuleService;
            _storageService = storageService;
            _examService = examService;
            _encryptionService = encryptionService;
        }

        public ActionResult addEducation()
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);



            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            ///modules................
            ///
            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.ClientFolder = "/richtexteditor/";
            Editor2.Width = Unit.Pixel(1146);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;

            Editor2.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor2.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor2.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor2.FullScreen = false;
            Editor2.DisabledItems = "save, help";
            string content1 = Request.Form["Editor2"];
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor Editor3 = new Editor(System.Web.HttpContext.Current, "Editor3");
            Editor3.ClientFolder = "/richtexteditor/";
            Editor3.Width = Unit.Pixel(1118);
            Editor3.Height = Unit.Pixel(660);
            Editor3.ResizeMode = RTEResizeMode.Disabled;

            Editor3.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor3.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor3.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor3.FullScreen = false;
            Editor3.DisabledItems = "save, help";
            string content2 = Request.Form["Editor3"];
            Editor3.MvcInit();
            ViewData["Editor3"] = Editor3.MvcGetString();
            // Desc Editor
            Editor Editor4 = new Editor(System.Web.HttpContext.Current, "Editor4");
            Editor4.ClientFolder = "/richtexteditor/";
            Editor4.Width = Unit.Pixel(1146);
            Editor4.Height = Unit.Pixel(660);
            Editor4.ResizeMode = RTEResizeMode.Disabled;

            Editor4.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor4.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor4.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor4.FullScreen = false;
            Editor4.DisabledItems = "save, help";
            string content3 = Request.Form["Editor4"];
            Editor4.MvcInit();
            ViewData["Editor4"] = Editor4.MvcGetString();
            ///___________________________________________________
            AddEducationViewModel addEducationViewModel = new AddEducationViewModel();
            //bInding DDl
            addEducationViewModel.EvaluationResults = Mapper.Map<IEnumerable<Evaluation>>(_examService.GetAllActiveEvaluation(HCRGCLIENT.ClientID));
            addEducationViewModel.ExamResults = Mapper.Map<IEnumerable<Exam>>(_examService.GetAllActiveExam(HCRGCLIENT.ClientID));
            addEducationViewModel.PreTestResults = Mapper.Map<IEnumerable<PreTest>>(_examService.GetAllActivePreTest(HCRGCLIENT.ClientID));
            addEducationViewModel.AccreditorResults = Mapper.Map<IEnumerable<Accreditor>>(_educationService.GetAllAccreditorsByOrganizationID(HCRGCLIENT.ClientID, HCRGCLIENT.OrganizationID));
            return View(addEducationViewModel);
        }

        public ActionResult ShowEditor()
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.FullScreen = false;
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            return View("AddEducation");
        }

        public ActionResult editEducation(string  educationID)
        {
            int _id = Convert.ToInt32(DecryptString(educationID.ToString()));
            EducationDetailPageWithEducation educationDetailPageWithEducation = new EducationDetailPageWithEducation();
            AddEducationViewModel addEducationViewModel = new AddEducationViewModel();
            addEducationViewModel.EvaluationResults = Mapper.Map<IEnumerable<Evaluation>>(_examService.GetAllActiveEvaluation(HCRGCLIENT.ClientID));
            addEducationViewModel.ExamResults = Mapper.Map<IEnumerable<Exam>>(_examService.GetAllActiveExam(HCRGCLIENT.ClientID));
            addEducationViewModel.PreTestResults = Mapper.Map<IEnumerable<PreTest>>(_examService.GetAllActivePreTest(HCRGCLIENT.ClientID));
            addEducationViewModel.AccreditorResults = Mapper.Map<IEnumerable<Accreditor>>(_educationService.GetAllAccreditorsByOrganizationID(HCRGCLIENT.ClientID, HCRGCLIENT.OrganizationID));
            addEducationViewModel.EducationCredentialResults = Mapper.Map<IEnumerable<EducationCredential>>(_educationService.GetEducationCredentialDetailByEducationID(_id));
            addEducationViewModel.Education = Mapper.Map<Education>(_educationService.GetEducationByID(_id));
            addEducationViewModel.CollegeEducation = Mapper.Map<IEnumerable<CollegeEducationSearch>>(_educationService.GetCollegeEducationByEducationID(_id));
            addEducationViewModel.ProfessionEducation = Mapper.Map<IEnumerable<EducationProfession>>(_educationService.GetProfessionEducationByEducationID(_id));
            addEducationViewModel.EducationFormatAvailable = Mapper.Map<IEnumerable<EducationFormatDetail>>(_educationService.GetEducationFormatsByEducationID(_id));
            addEducationViewModel.EducationPreTestQuestion = Mapper.Map<EducationPreTestQuestion>(_examService.GetEducationPreTestQuestionByEducationID(_id));
            addEducationViewModel.EducationExamQuestion = Mapper.Map<EducationExamQuestion>(_examService.GetEducationExamQuestionByEducationID(_id));
            addEducationViewModel.EducationEvaluation = Mapper.Map<EducationEvaluation>(_examService.GetEducationEvaluationByEducationID(_id));
            educationDetailPageWithEducation = Mapper.Map<EducationDetailPageWithEducation>(_educationService.GetEducationDetailPageContent(_id));
            EducationDetailPage educationDetailPage = new EducationDetailPage();
            educationDetailPage.EducationID = educationDetailPageWithEducation.EducationID;
            educationDetailPage.EPageID = educationDetailPageWithEducation.EPageID;
            educationDetailPage.PContent = educationDetailPageWithEducation.PContent;
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            string content = Request.Form["Editor1"];
            string decodedHTML = HttpUtility.HtmlDecode(educationDetailPageWithEducation.PContent);
            Editor1.LoadFormData(decodedHTML);
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.FullScreen = false;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.DisabledItems = "save, help";
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            ///modules................
            ///
            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.ClientFolder = "/richtexteditor/";
            Editor2.Width = Unit.Pixel(1146);
            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;

            Editor2.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor2.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor2.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor2.FullScreen = false;
            Editor2.DisabledItems = "save, help";
            string content1 = Request.Form["Editor2"];
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            Editor Editor3 = new Editor(System.Web.HttpContext.Current, "Editor3");
            Editor3.ClientFolder = "/richtexteditor/";
            Editor3.Width = Unit.Pixel(1118);
            Editor3.Height = Unit.Pixel(660);
            Editor3.ResizeMode = RTEResizeMode.Disabled;
            Editor3.FullScreen = false;

            Editor3.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor3.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor3.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor3.DisabledItems = "save, help";
            string content2 = Request.Form["Editor3"];
            Editor3.MvcInit();
            ViewData["Editor3"] = Editor3.MvcGetString();

            Editor Editor4 = new Editor(System.Web.HttpContext.Current, "Editor4");
            Editor4.ClientFolder = "/richtexteditor/";
            Editor4.Width = Unit.Pixel(1146);
            Editor4.Height = Unit.Pixel(660);
            Editor4.ResizeMode = RTEResizeMode.Disabled;


            Editor4.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor4.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor4.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor4.FullScreen = false;
            Editor4.DisabledItems = "save, help";
            string content3 = Request.Form["Editor4"];
            Editor4.MvcInit();
            ViewData["Editor4"] = Editor4.MvcGetString();
            ///_____________________________________________
            ///___________________________________________________
            educationDetailPage.PDate = educationDetailPageWithEducation.PDate;
            addEducationViewModel.EducationDetailPage = educationDetailPage;
            IEnumerable<EducationTypesAvailable> eductionTypeAvailable = Mapper.Map<IEnumerable<EducationTypesAvailable>>(_educationService.GetEducationTypeByEducationID(_id));
            if (eductionTypeAvailable.Count() > 0)
            {
                var hardcopyType = eductionTypeAvailable.Where(hp => hp.EducationTypeID == GlobalConst.EducationType.HardCopy).FirstOrDefault();
                var onlineType = eductionTypeAvailable.Where(hp => hp.EducationTypeID == GlobalConst.EducationType.Online).FirstOrDefault();
                addEducationViewModel.Education.CoursePrice = onlineType.Price;
                addEducationViewModel.Education.OnlinePriceAID = onlineType.EducationTypeAID;
            }
            //module section...rsingh
            List<EducationModule> list_educationModule = new List<EducationModule>();
            EducationModuleViewModel educationModuleModel = new EducationModuleViewModel();
            addEducationViewModel.EducationModules = Mapper.Map<PagedEducationModule>(_educationModuleService.GetAllPagedEducationModuleByEid(_id, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            foreach (EducationModule educationmodule in addEducationViewModel.EducationModules.EducationModules)
            {
                EducationModuleFile educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(educationmodule.EducationModuleID));
                educationModuleFile.ModuleFile = "blank";
                EducationModule educationModule1 = new EducationModule();
                educationModule1.EducationID = educationmodule.EducationID;
                educationModule1.EducationModuleDate = educationmodule.EducationModuleDate;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                educationModule1.EducationModuleShortDesc = regex.Replace(HttpUtility.HtmlDecode(educationmodule.EducationModuleDescription), String.Empty);
                educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Replace("&nbsp;", "");
                if (educationModule1.EducationModuleShortDesc.Length > 1000)
                    educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Substring(0, 1000);
                educationModule1.EducationModuleDescription = educationmodule.EducationModuleDescription;
                educationModule1.EducationModuleID = educationmodule.EducationModuleID;
                educationModule1.EducationModuleName = educationmodule.EducationModuleName;
                educationModule1.EducationModulePosition = educationmodule.EducationModulePosition;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.TEXT)
                    educationModule1.EducationModulePDFName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.PPT)
                    educationModule1.EducationModulePPTName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.Video)
                    educationModule1.EducationModuleVideoName = educationModuleFile.ModuleFile;
                educationModule1.EducationModuleTypeFile = educationModuleFile.FileTypeID;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                educationModule1.flag = educationmodule.flag;

                list_educationModule.Add(educationModule1);
            }
            addEducationViewModel.EducationModules.EducationModules = list_educationModule.AsEnumerable();
            addEducationViewModel.EducationModules.PagedRecords = GlobalConst.Records.Take;

            addEducationViewModel.Education.EncryptedEducationID = EncryptString(_id.ToString());  

            return View("addEducation", addEducationViewModel);
        }
        [HttpPost]
        public ActionResult addEducation(AddEducationViewModel addEducationViewModel)
        {
            addEducationViewModel.Education.CourseUploadDate = System.DateTime.Now.Date;
            addEducationViewModel.Education.IsActive = true;
           // bool updated = false;
            //save education...hp
            if (!(addEducationViewModel.Education.EducationID > 0))
            {
                if (addEducationViewModel.Education.EducationFile != null)
                    addEducationViewModel.Education.CourseFile = UpdateDocumentFile(addEducationViewModel.Education.EducationFile);
                addEducationViewModel.Education.EducationFile = null;
                addEducationViewModel.Education.ReadyToDisplay = false;
                addEducationViewModel.Education.ClientID = HCRGCLIENT.ClientID;
                addEducationViewModel.Education.EducationID = _educationService.AddEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Education>(addEducationViewModel.Education));
                addEducationViewModel.EducationDetailPage.EducationID = addEducationViewModel.Education.EducationID;
                addEducationViewModel.EducationDetailPage.PDate = DateTime.Now;
                addEducationViewModel.EducationDetailPage.EPageID = _educationService.AddEducationDetailPageContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationDetailPage>(addEducationViewModel.EducationDetailPage));

            }
            else
            {
                //updated = true;
                if (addEducationViewModel.EducationFormatAvailable.ElementAt(0).EducationFormatID == 2)
                    addEducationViewModel.Education.CourseLocation = null;
                if (addEducationViewModel.Education.EducationFile == null)
                    addEducationViewModel.Education.CourseFile = addEducationViewModel.Education.CourseFile;
                else
                {
                    addEducationViewModel.Education.CourseFile = UpdateDocumentFile(addEducationViewModel.Education.EducationFile);
                    addEducationViewModel.Education.EducationFile = null;
                }

                if (addEducationViewModel.Education.IsPublished == null)
                    addEducationViewModel.Education.IsPublished = false;

                var _res = _educationService.GetEducationByID(addEducationViewModel.Education.EducationID);
                addEducationViewModel.Education.IsPublished = _res.IsPublished;
                addEducationViewModel.Education.IsCoursePreview = _res.IsCoursePreview;
                _educationService.UpdateEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Education>(addEducationViewModel.Education));
                addEducationViewModel.EducationDetailPage.PDate = System.DateTime.Now.Date;
                _educationService.UpdateEducationDetailPageContent(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationDetailPage>(addEducationViewModel.EducationDetailPage));
            }
            //add online price...hp
            EducationTypesAvailable educationTypesAvailable = new EducationTypesAvailable();
            educationTypesAvailable.EducationID = addEducationViewModel.Education.EducationID;
            educationTypesAvailable.EducationTypeID = GlobalConst.EducationType.Online;
            educationTypesAvailable.Price = Convert.ToDecimal(addEducationViewModel.Education.CoursePrice);
            educationTypesAvailable.EducationTypeAID = addEducationViewModel.Education.OnlinePriceAID;
            if (!(addEducationViewModel.Education.OnlinePriceAID > 0))
                addEducationViewModel.Education.OnlinePriceAID = _educationService.AddEducationType(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationTypesAvailable>(educationTypesAvailable));
            else
                _educationService.UpdateEducationType(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationTypesAvailable>(educationTypesAvailable));
            //save colleges for education....hp
            if (addEducationViewModel.CollegeEducation != null)
            {
                foreach (CollegeEducationSearch _clgEdu in addEducationViewModel.CollegeEducation)
                {
                    _clgEdu.EducationID = addEducationViewModel.Education.EducationID;
                    _clgEdu.IsActive = true;
                    if (!(_clgEdu.CollegeCourseID > 0))
                        _clgEdu.CollegeCourseID = _educationService.AddCollegeEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>(_clgEdu));
                }
            }
            //save college education deleted...hp
            if (addEducationViewModel.CollegeEducationDeleted != null)
            {
                foreach (CollegeEducation _clgEdu in addEducationViewModel.CollegeEducationDeleted)
                {
                    _clgEdu.EducationID = addEducationViewModel.Education.EducationID;
                    if (_clgEdu.CollegeCourseID > 0)
                        _educationService.UpdateCollegeEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>(_clgEdu));
                }
            }

            //save Profession for education....
            if (addEducationViewModel.ProfessionEducation != null)
            {
                foreach (EducationProfession _prfEdu in addEducationViewModel.ProfessionEducation)
                {
                    _prfEdu.EducationID = addEducationViewModel.Education.EducationID;
                    _prfEdu.IsActive = true;
                    if (!(_prfEdu.EducationProfessionID > 0))
                        _prfEdu.EducationProfessionID = _educationService.AddEducationProfession(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationProfession>(_prfEdu));

                }
            }
            //save Profession education deleted...
            if (addEducationViewModel.ProfessionEducationDeleted != null)
            {
                foreach (EducationProfession _prfEdu in addEducationViewModel.ProfessionEducationDeleted)
                {
                    _prfEdu.EducationID = addEducationViewModel.Education.EducationID;
                    if (_prfEdu.EducationProfessionID > 0)
                        _prfEdu.EducationProfessionID = _educationService.UpdateEducationProfession(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationProfession>(_prfEdu));
                    // _educationService.UpdateCollegeEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>(_clgEdu));
                }
            }
            //save education format...hp
            if (addEducationViewModel.EducationFormatAvailable != null)
            {
                addEducationViewModel.EducationFormatAvailable.ElementAt(0).EducationID = addEducationViewModel.Education.EducationID;
                addEducationViewModel.EducationFormatAvailable.ElementAt(0).IsActive = true;
                if (!(addEducationViewModel.EducationFormatAvailable.ElementAt(0).EducationAvailableID > 0))
                    addEducationViewModel.EducationFormatAvailable.ElementAt(0).EducationAvailableID = _educationService.AddEducationFormatAvailable(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>(addEducationViewModel.EducationFormatAvailable.ElementAt(0)));
                else
                    _educationService.UpdateEducationFormatAvailable(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>(addEducationViewModel.EducationFormatAvailable.ElementAt(0)));
            }
            if (addEducationViewModel.CoursePreTestID == 0)
            {
                if (addEducationViewModel.EducationPreTestQuestion.PreTestID != 0)
                {
                    addEducationViewModel.EducationPreTestQuestion.EducationID = addEducationViewModel.Education.EducationID;
                    addEducationViewModel.EducationPreTestQuestion.IsActive = true;
                    addEducationViewModel.EducationPreTestQuestion.CoursePreTestID = _examService.AddEducationPreTestQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationPreTestQuestion>(addEducationViewModel.EducationPreTestQuestion));
                }
            }
            else
            {
                addEducationViewModel.EducationPreTestQuestion.EducationID = addEducationViewModel.Education.EducationID;
                addEducationViewModel.EducationPreTestQuestion.CoursePreTestID = addEducationViewModel.CoursePreTestID;
                _examService.UpdateEducationPreTestQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationPreTestQuestion>(addEducationViewModel.EducationPreTestQuestion));
            }
            //save CourseExam
            if (addEducationViewModel.CourseExamID == 0)
            {
                if (addEducationViewModel.EducationExamQuestion.ExamID != 0)
                {
                    addEducationViewModel.EducationExamQuestion.IsActive = true;
                    addEducationViewModel.EducationExamQuestion.EducationID = addEducationViewModel.Education.EducationID;
                    addEducationViewModel.EducationExamQuestion.CourseExamID = _examService.AddEducationExamQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationExamQuestion>(addEducationViewModel.EducationExamQuestion));
                }
            }
            else
            {
                addEducationViewModel.EducationExamQuestion.EducationID = addEducationViewModel.Education.EducationID;
                addEducationViewModel.EducationExamQuestion.CourseExamID = addEducationViewModel.CourseExamID;
                _examService.UpdateEducationExamQuestion(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationExamQuestion>(addEducationViewModel.EducationExamQuestion));
            }
            //save CourseEvaluation
            if (addEducationViewModel.CourseEvaluationID == 0)
            {
                if (addEducationViewModel.EducationEvaluation.EvaluationID != 0)
                {
                    addEducationViewModel.EducationEvaluation.IsActive = true;
                    addEducationViewModel.EducationEvaluation.EducationID = addEducationViewModel.Education.EducationID;
                    addEducationViewModel.EducationEvaluation.CourseEvaluationID = _examService.AddEducationEvaluation(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationEvaluation>(addEducationViewModel.EducationEvaluation));
                }
            }
            else
            {
                addEducationViewModel.EducationEvaluation.EducationID = addEducationViewModel.Education.EducationID;
                addEducationViewModel.EducationEvaluation.CourseEvaluationID = addEducationViewModel.CourseEvaluationID;
                _examService.UpdateEducationEvaluation(Mapper.Map<HCRGUniversityMgtApp.NEPService.ExamQuestionService.EducationEvaluation>(addEducationViewModel.EducationEvaluation));
            }
            _educationService.UpdateMyEducationCompletedToPassed(addEducationViewModel.Education.EducationID);

            Education edu = new Education();
            edu = Mapper.Map<Education>(_educationService.GetEducationByID(addEducationViewModel.Education.EducationID));

            addEducationViewModel.Education.ReadyToDisplay = edu.ReadyToDisplay;
            //Update Education Modules Time in admin preview course.
            if ((edu.IsTimerRequired.Value == false) || (edu.IsTimerRequired.Value == null))
                _educationService.UpdateEducationModulesTime(addEducationViewModel.Education.EducationID);

            addEducationViewModel.Education.EncryptedEducationID = EncryptString(addEducationViewModel.Education.EducationID.ToString());  

            return Json(addEducationViewModel, GlobalConst.Message.text_html);
        }
        private string UpdateDocumentFile(HttpPostedFileBase file)
        {
            var path = _storageService.SetCoursePDFStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), file.FileName);
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
        [HttpGet]
        public ActionResult GetEducationFormatsAll()
        {
            return Json(Mapper.Map<IEnumerable<EducationFormat>>(_educationService.GetEducationFormatByClientID(HCRGCLIENT.ClientID)),GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SearchEducation()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedEducation educationModel = new PagedEducation();
            educationModel = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter("", GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, _organizationID));
            educationModel.PagedRecords = GlobalConst.Records.LandingTake;
            foreach (var objResult in educationModel.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                educationModel.IsHCRGAdmin = true;
            else
                educationModel.IsHCRGAdmin = false;
            return View(educationModel);
        }
        [HttpPost]
        public JsonResult GetAllEducationOrganizationID(int OrgID)
        {
            var data = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter("", GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, OrgID));
            foreach (var objResult in data.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            return Json(data);
        }
        [HttpPost]
        public ActionResult SearchEducation(string searchText, int? skip)
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedEducation educationModel = new PagedEducation();
            if (searchText == null)
                educationModel = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter("", skip.Value, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, _organizationID));
            else
                educationModel = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter(searchText, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, _organizationID));
            educationModel.PagedRecords = GlobalConst.Records.LandingTake;
            foreach (var objResult in educationModel.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            return Json(educationModel, GlobalConst.Message.text_html);
        }
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult DisableEducation(Education education)
        {
            if (education.IsActive == false)
                education.IsActive = true;
            else
                education.IsActive = false;
            var educationid = _educationService.UpdateEducation(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Education>(education));
            education.flag = false;
            return Json(education, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult PublishEducation(int educationID)
        {
            var edu = _educationService.PublishEducation(educationID);
            return Json("Published Successfully", GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult CoursePreviewEducation(int educationID)
        {
            var edu = _educationService.CoursePreviewEducation(educationID);
            return Json("You can preview this course.", GlobalConst.Message.text_html);
        }
        public JsonResult getEducationActive()
        {
            var education = _educationService.getAllEducationActive();
            EducationViewModel educationModel = new EducationViewModel();
            educationModel.EducationResults = Mapper.Map<IEnumerable<Education>>(education);
            return Json(educationModel.EducationResults, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult AddEducationCredential(EducationCredential educationCredential)
        {
            if (educationCredential.CredentialID == 0)
            {
                educationCredential.IsActive = true;
                var credentialID = _educationService.AddEducationCredential(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationCredential>(educationCredential));
                educationCredential.CredentialID = credentialID;
                educationCredential.flag = true;
            }
            else
            {
                var credentialID = _educationService.UpdateEducationCredential(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationCredential>(educationCredential));
                educationCredential.flag = false;
            }

            return Json(educationCredential, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult ShowEducationCredential(int Educationid)
        {
            IEnumerable<EducationCredential> EducationCredentials = Mapper.Map<IEnumerable<EducationCredential>>(_educationService.GetEducationCredentialDetailByEducationID(Educationid));
            return Json(EducationCredentials, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult DeleteEducationCredentialInfo(EducationCredential educationCredential)
        {
            if (educationCredential.IsActive == true)
                educationCredential.IsActive = false;
            else
                educationCredential.IsActive = true;
            _educationService.DeleteEducationCredential(educationCredential.CredentialID, Convert.ToBoolean(educationCredential.IsActive));
            return Json("Deleted Successfully", GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetEducationByEducationName(string educationName, int? skip)
        {
            PagedEducation education = new PagedEducation();
            if (skip == null)
                education = Mapper.Map<PagedEducation>(_educationService.GetEducationByEducationNamePaged(educationName, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            else
                education = Mapper.Map<PagedEducation>(_educationService.GetEducationByEducationNamePaged(educationName, skip.Value, GlobalConst.Records.Take));
            return Json(education, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetEducationReadyToDisplayByEducationName(string educationName, int? skip)
        {
            PagedEducation education = new PagedEducation();
            if (skip == null)
                education = Mapper.Map<PagedEducation>(_educationService.GetEducationReadyToDisplayByEducationNamePaged(educationName,HCRGCLIENT.OrganizationID , GlobalConst.Records.Skip, GlobalConst.Records.Take));
            else
                education = Mapper.Map<PagedEducation>(_educationService.GetEducationReadyToDisplayByEducationNamePaged(educationName, HCRGCLIENT.OrganizationID, skip.Value, GlobalConst.Records.Take));
            return Json(education, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetEducationByID(int educationId)
        {
            Education _Education = new Education();
            _Education = Mapper.Map<Education>(_educationService.GetEducationByID(educationId));
            return Json(_Education.CourseName, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult getEducationModuleByEducationID(int educationID)
        {
            //module section...rsingh
            List<EducationModule> list_educationModule = new List<EducationModule>();
            EducationModuleViewModel educationModuleModel = new EducationModuleViewModel();
            AddEducationViewModel addEducationViewModel = new AddEducationViewModel();
            addEducationViewModel.EducationModules = Mapper.Map<PagedEducationModule>(_educationModuleService.GetAllPagedEducationModuleByEid(educationID, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            foreach (EducationModule educationmodule in addEducationViewModel.EducationModules.EducationModules)
            {
                EducationModuleFile educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(educationmodule.EducationModuleID));
                educationModuleFile.ModuleFile = "blank";
                EducationModule educationModule1 = new EducationModule();
                educationModule1.EducationID = educationmodule.EducationID;
                educationModule1.EducationModuleDate = educationmodule.EducationModuleDate;

                Regex regex = new Regex("\\<[^\\>]*\\>");
                educationModule1.EducationModuleShortDesc = regex.Replace(HttpUtility.HtmlDecode(educationmodule.EducationModuleDescription), String.Empty);
                educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Replace("&nbsp;", "");
                if (educationModule1.EducationModuleShortDesc.Length > 1000)
                    educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Substring(0, 1000);
               
                educationModule1.EducationModuleDescription = educationmodule.EducationModuleDescription;
                //string decodedHTMLDesc = HttpUtility.HtmlDecode(educationmodule.EducationModuleDescription);
                //educationModule1.EducationModuleDescription = decodedHTMLDesc;
                // educationModule1.EducationModuleDescription = educationmodule.EducationModuleDescription;
                educationModule1.EducationModuleID = educationmodule.EducationModuleID;
                educationModule1.EducationModuleName = educationmodule.EducationModuleName;
                educationModule1.EducationModulePosition = educationmodule.EducationModulePosition;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.TEXT)
                    educationModule1.EducationModulePDFName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.PPT)
                    educationModule1.EducationModulePPTName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.Video)
                    educationModule1.EducationModuleVideoName = educationModuleFile.ModuleFile;
                educationModule1.EducationModuleTypeFile = educationModuleFile.FileTypeID;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                educationModule1.flag = educationmodule.flag;

                list_educationModule.Add(educationModule1);
            }
            addEducationViewModel.EducationModules.EducationModules = list_educationModule.AsEnumerable();
            addEducationViewModel.EducationModules.PagedRecords = GlobalConst.Records.Take;

            return Json(addEducationViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult GetAllCoursesByClientID(int _clientID)
        {
            return Json(_educationService.GetEducationByClientID(_clientID), GlobalConst.Message.text_html);
        }
        public ActionResult SearchCourseCatalog()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedEducation educationModel = new PagedEducation();
            educationModel = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter("", GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, _organizationID));
            educationModel.PagedRecords = GlobalConst.Records.LandingTake;
            foreach (var objResult in educationModel.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            if (HCRGCLIENT.ClientTypeID == 1)
                educationModel.IsHCRGAdmin = true;
            else
                educationModel.IsHCRGAdmin = false;
            return View(educationModel);
        }
        [HttpPost]
        public ActionResult SearchCourseCatalog(string searchText, int? skip)
        {
            PagedEducation educationModel = new PagedEducation();
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            if (searchText == "0")
                educationModel = Mapper.Map<PagedEducation>(_educationService.GetAllPagedEducationByfilter("", GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, _organizationID));
            else
                educationModel = Mapper.Map<PagedEducation>(_educationService.GetCourseCatalogByfilter(searchText.Trim(), GlobalConst.Records.Skip, GlobalConst.Records.take500, HCRGCLIENT.ClientID, _organizationID));
            educationModel.PagedRecords = GlobalConst.Records.LandingTake;
            foreach (var objResult in educationModel.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            return Json(educationModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult GetAllCourseCatalogByOrganizationID(int OrgID, string searchText)
        {
            var data = Mapper.Map<PagedEducation>(_educationService.GetCourseCatalogByfilter(searchText.Trim(), GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID, OrgID));
            foreach (var objResult in data.Educations)
                objResult.EncryptedEducationID = EncryptString(objResult.EducationID.ToString());
            return Json(data);
        }
        #region PreviewEducation
        public ActionResult PreviewEducation(string educationID)
        {
            int _id = Convert.ToInt32(DecryptString(educationID.ToString()));
            HCRGUniversityMgtApp.Domain.ViewModels.EducationModuleFileViewModel.EducationModuleFileViewModel _educationModuleFileViewModel = new Domain.ViewModels.EducationModuleFileViewModel.EducationModuleFileViewModel();
            var data = _educationService.GetEducationByID(_id);
            _educationModuleFileViewModel.CourseName = data.CourseName;
            _educationModuleFileViewModel.EducationModules = Mapper.Map<PagedEducationModule>(_educationModuleService.GetAllPagedEducationModuleByEid(_id, GlobalConst.Records.Skip, GlobalConst.Records.Take));
            //EducationModuleFileResults = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(1176, GlobalConst.FileTypes.ModuleUploads));
            foreach (EducationModule educationmodule in _educationModuleFileViewModel.EducationModules.EducationModules)
            {
                educationmodule.EncryptedEDUModuleID = EncryptString(educationmodule.EducationModuleID.ToString());

                string decodedHTML = HttpUtility.HtmlDecode(educationmodule.ModuleFile);
              //  educationmodule.ModuleFile = decodedHTML;
                if (decodedHTML != null)
                {
               
                    educationmodule.ModuleFile = decodedHTML.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    educationmodule.ModuleFile = decodedHTML.Replace("&nbsp;", "");//.Replace("<span >", String.Empty).Replace("</span>", String.Empty).Replace(" <br />", String.Empty).Replace("<strong>", String.Empty).Replace("</strong>", String.Empty);                    
                }

                educationmodule.EducationModuleFileResults = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(educationmodule.EducationModuleID, GlobalConst.FileTypes.ModuleUploads));
                foreach (EducationModuleFile _eduModuleFile in educationmodule.EducationModuleFileResults)
                {
                    _eduModuleFile.ModuleFileExtension = Path.GetExtension(_eduModuleFile.ModuleFile);
                }
            }

            return View(_educationModuleFileViewModel);
        }

        [HttpPost]
        public ActionResult GetEducationModuleFile(string educationmoduleid)
        {

            int _id = Convert.ToInt32(DecryptString(educationmoduleid.ToString()));
            var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(_id);

            HCRGUniversityMgtApp.Domain.ViewModels.EducationModuleFileViewModel.EducationModuleFileViewModel _educationModuleFileViewModel = new Domain.ViewModels.EducationModuleFileViewModel.EducationModuleFileViewModel();

            _educationModuleFileViewModel.educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(_id));
            _educationModuleFileViewModel.EducationModuleFileResults = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(_id, GlobalConst.FileTypes.ModuleUploads));

            if (_educationModuleFileViewModel.educationModuleFile.FileTypeID == GlobalConst.FileTypes.Video)
                _educationModuleFileViewModel.educationModuleFile.ModuleFile = "http://view.vzaar.com/" + _educationModuleFileViewModel.educationModuleFile.ModuleFile + "/player";
            else if (_educationModuleFileViewModel.educationModuleFile.FileTypeID == GlobalConst.FileTypes.TEXT)
            {
                string decodedHTML = HttpUtility.HtmlDecode(_educationModuleFileViewModel.educationModuleFile.ModuleFile);
                _educationModuleFileViewModel.educationModuleFile.ModuleFile = decodedHTML;
                if (decodedHTML != null)
                {
                    _educationModuleFileViewModel.educationModuleFile.ModuleFile = decodedHTML.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                    _educationModuleFileViewModel.educationModuleFile.ModuleFile = decodedHTML.Replace("&nbsp;", "");//.Replace("<span >", String.Empty).Replace("</span>", String.Empty).Replace(" <br />", String.Empty).Replace("<strong>", String.Empty).Replace("</strong>", String.Empty); 
                }
            }
            else if (_educationModuleFileViewModel.educationModuleFile.FileTypeID == GlobalConst.FileTypes.PPT)
            {
                _educationModuleFileViewModel.educationModuleFile.UploadFilePath = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _client.ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModulePPT + GlobalConst.ConstantChar.ForwardSlash + _educationModuleFileViewModel.educationModuleFile.ModuleFile;
            }
            return Json(_educationModuleFileViewModel);
        }


        public FileResult DownloadModuleAttachment(string FileName, int MEMID)
        {
            string Extension = "";
            if (Path.GetExtension(FileName).ToString().ToLower() == ".pdf")
                Extension = GlobalConst.Message.application_pdf;
            else if ((Path.GetExtension(FileName).ToString().ToLower() == ".xlsx") || (Path.GetExtension(FileName).ToString().ToLower() == ".xls"))
                Extension = GlobalConst.Message.application_xlsx;
            else
                Extension = GlobalConst.Message.application_jpeg;
            var _data = _educationModuleService.GetOrganizationClientByEducationModuleID(MEMID);
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string filePath = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _data.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _data.ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModuleUpload + GlobalConst.ConstantChar.ForwardSlash + FileName;
            return File(filePath, Extension, FileName);
        } 
        #endregion
    }
}