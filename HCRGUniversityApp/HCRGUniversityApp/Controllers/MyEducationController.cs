using AutoMapper;
using HCRGUniversityApp.Domain.Models.CertificateDetail;
using HCRGUniversityApp.Domain.Models.EducationModuleFile;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using HCRGUniversityApp.Domain.Models.ExamModel;
using HCRGUniversityApp.Domain.Models.LogSessionModel;
using HCRGUniversityApp.Domain.Models.MyEducationDetailModel;
using HCRGUniversityApp.Domain.Models.Product;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Domain.ViewModels.MyEducationDetailViewModel;
using HCRGUniversityApp.NEPService.CertificationTitleOptionService;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Services;
using System.Web.Services;
using TheArtOfDev.HtmlRenderer.WinForms;
namespace HCRGUniversityApp.Controllers
{
    [AuthorizedUserCheck]
    public class MyEducationController : BaseController
    {
        private readonly NEPService.MyEducationService.IMyEducationService _myEducationService;
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examQuestionService;
        private readonly NEPService.EducationModuleService.IEducationModuleService _educationModuleService;
        private readonly NEPService.ExamInternalService.IExamInternalService _examInternalService;
        private readonly NEPService.ExamQuestionService.IExamQuestionService _examService;
        private readonly NEPService.LogSessionService.ILogSessionService _logService;
        private readonly NEPService.ProductService.IProductService _productService;
        private readonly NEPService.ClientService.IClientService _clientService;
        private readonly NEPService.UserService.IUserService _userService;
        private readonly NEPService.CertificationTitleOptionService.ICertificationTitleOptionService _CertificationTitleOptionService;
        private readonly NEPService.ShoppingEducationService.IShoppingEducationService _ShoppingEducationService;

        private readonly IStorage _storageService;
        public readonly IEMail _mailService;
        public readonly IEncryption _encryptionService;
        //
        // GET: /MyEducation/
        public MyEducationController(NEPService.ExamQuestionService.IExamQuestionService examService, NEPService.MyEducationService.IMyEducationService myEducationService, IEncryption encryptionService, NEPService.ExamInternalService.IExamInternalService examInternalService, IStorage storageService, NEPService.LogSessionService.ILogSessionService logService, NEPService.ClientService.IClientService clientService,
            NEPService.EducationModuleService.IEducationModuleService educationModuleService, NEPService.ProductService.IProductService productService, NEPService.UserService.IUserService userService, NEPService.CertificationTitleOptionService.ICertificationTitleOptionService CertificationTitleOptionService, NEPService.ExamQuestionService.IExamQuestionService examQuestionService, IEMail mailService, NEPService.ShoppingEducationService.IShoppingEducationService shoppingEducationService)
        {
            _myEducationService = myEducationService;
            _examInternalService = examInternalService;
            _storageService = storageService;
            _encryptionService = encryptionService;
            _examService = examService;
            _logService = logService;
            _educationModuleService = educationModuleService;
            _productService = productService;
            _userService = userService;
            _CertificationTitleOptionService = CertificationTitleOptionService;
            _examQuestionService = examQuestionService;
            _mailService = mailService;
            _ShoppingEducationService = shoppingEducationService;
            _clientService = clientService;
        }
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(int skip = 0)
        {
            if (skip != 0)
                skip = Convert.ToInt32(_encryptionService.DecryptString2(skip.ToString()));
            //check and update if course is expired...hp
            _myEducationService.UpdateMyEducationExpiredByUserID(HCRGUser.UID);
            MyEducationDetailViewModel myEducationDetailViewModel = new MyEducationDetailViewModel();
            myEducationDetailViewModel = GetMyEducationCompletedByUserID(GlobalConst.Records.Skip);
            var MyEducationInProgress = GetMyEducationInProgressByUserIDPaged(skip);
            myEducationDetailViewModel.MyEducationDetailResults = MyEducationInProgress.MyEducationDetailResults;
            foreach (MyEducationDetail _MyEducationDetail in myEducationDetailViewModel.MyEducationDetailResults)
            {
                _MyEducationDetail.DaysLeft = Convert.ToInt32((Convert.ToDateTime(_MyEducationDetail.ExpiryDate) - Convert.ToDateTime(DateTime.Now.ToShortDateString())).TotalDays);

                _MyEducationDetail.EncryptedMEID = _encryptionService.EncryptString2(_MyEducationDetail.MEID.ToString());

                var _res = _CertificationTitleOptionService.GetCertificationTitleOptionsByEducationID(_MyEducationDetail.EducationID);
                if (_res != null)
                {
                    _MyEducationDetail.IsCertificationTitleOption = true;
                    _MyEducationDetail.CertificationTitleOptionID = _res.CertificationTitleOptionID;
                }
                else
                {
                    _MyEducationDetail.IsCertificationTitleOption = null;
                    _MyEducationDetail.CertificationTitleOptionID = 0;
                }

                //Certificatepath = Server.MapPath(GlobalConst.Message.StoragePath + GlobalConst.FolderName.Org + (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Certificate + GlobalConst.ConstantChar.ForwardSlash) + certificateURL;

            }
            myEducationDetailViewModel.TotalInprocessItemCount = MyEducationInProgress.TotalInprocessItemCount;
            myEducationDetailViewModel.Skip = skip;
            myEducationDetailViewModel.ProductPurchaseDetails = Mapper.Map<PagedProductPurchase>(_productService.GetPagedProductByProductType(GlobalConst.Message.Download, 0, skip, GlobalConst.Records.Take5));
            //myEducationDetailViewModel.ProductPurchaseDetails = Mapper.Map<PagedProductPurchase>(_productService.GetPagedProductByProductType(GlobalConst.Message.Download, HCRGUser.UID, skip, GlobalConst.Records.Take5));
            var _result = Mapper.Map<UserAllAccessPass>(_userService.getUserAllAccessPassByUserID(HCRGUser.UID));
            if (_result != null)
            {
                _result.AllAccessValidEndDate = _result.AllAccessEndDate.Value.ToString("MM/dd/yyyy");
                if (_result.AllAccessEndDate.Value >= DateTime.Now)
                    myEducationDetailViewModel.UserAllAccessPassResults = _result;
            }
            else
                myEducationDetailViewModel.UserAllAccessPassResults = null;

            return View(myEducationDetailViewModel);
        }
        [HttpPost]
        public ActionResult GetMyEducationCompletedByUserIDPaged(int skip)
        {
            MyEducationDetailViewModel myEducationDetailViewModel = new MyEducationDetailViewModel();
            myEducationDetailViewModel = GetMyEducationCompletedByUserID(skip);
            return Json(myEducationDetailViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult GetMyEducationInProgressByUserID(int skip)
        {
            MyEducationDetailViewModel myEducationDetailViewModel = new MyEducationDetailViewModel();
            myEducationDetailViewModel = GetMyEducationInProgressByUserIDPaged(skip);
            foreach (MyEducationDetail _MyEducationDetail in myEducationDetailViewModel.MyEducationDetailResults)
            {
                _MyEducationDetail.DaysLeft = Convert.ToInt32((Convert.ToDateTime(_MyEducationDetail.ExpiryDate) - DateTime.Now).TotalDays);

                var _res = _CertificationTitleOptionService.GetCertificationTitleOptionsByEducationID(_MyEducationDetail.EducationID);
                if (_res != null)
                {
                    _MyEducationDetail.IsCertificationTitleOption = true;
                    _MyEducationDetail.CertificationTitleOptionID = _res.CertificationTitleOptionID;
                }
                else
                {
                    _MyEducationDetail.IsCertificationTitleOption = null;
                    _MyEducationDetail.CertificationTitleOptionID = 0;
                }
            }
            return Json(myEducationDetailViewModel, GlobalConst.Message.text_html);
        }
        public MyEducationDetailViewModel GetMyEducationCompletedByUserID(int skip)
        {
            MyEducationDetailViewModel myEducationDetailViewModel = new MyEducationDetailViewModel();
            var MyEducationDetailCompleted = _myEducationService.GetMyEducationCompletedByUserIDPaged(HCRGUser.UID, skip, GlobalConst.Records.Take5);
            myEducationDetailViewModel.MyEducationDetailCompletedResults = Mapper.Map<IEnumerable<MyEducationDetail>>(MyEducationDetailCompleted.myeducation);
            myEducationDetailViewModel.TotalItemCount = MyEducationDetailCompleted.TotalCount;
            foreach (MyEducationDetail myEducationDetail in myEducationDetailViewModel.MyEducationDetailCompletedResults)
            {
                /*****************************Code for Get Already open course module and exam browser*********************************/
                var logsession = _logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, myEducationDetail.MEID);
                if (logsession != null)
                {
                    myEducationDetail.PageUrl = logsession.PageUrl;
                    myEducationDetail.LogErrorMsg = GlobalConst.Message.OpenInAnotherResource;
                }
                /*****************************ENd*********************************/
                myEducationDetail.EncryptedMEID = _encryptionService.EncryptString2(myEducationDetail.MEID.ToString());
                myEducationDetail.EncryptedEducationID = _encryptionService.EncryptString2(myEducationDetail.EducationID.ToString());
                var EducationExamQuestion = _examService.GetEducationExamQuestionByEducationID(myEducationDetail.EducationID);
                var EducationEvaluation = _examService.GetEducationEvaluationByEducationID(myEducationDetail.EducationID);
                if (EducationExamQuestion != null)
                {
                    myEducationDetail.ExamResult = Mapper.Map<IEnumerable<ExamResult>>(_examInternalService.GetExamAttempResulttByUser(HCRGUser.UID, myEducationDetail.MEID));
                    myEducationDetail.IsExamAvailable = true;
                    myEducationDetail.FinalExamAttemptByUser = myEducationDetail.ExamResult.Count();
                    foreach (ExamResult _ExamResult in myEducationDetail.ExamResult)
                    {
                        myEducationDetail.IsExamPass = _ExamResult.IsPass;
                        if (myEducationDetail.IsExamPass)
                            break;
                    }
                }
                if (EducationEvaluation != null)
                    myEducationDetail.IsEvalutionAvailable = true;
            }
            return myEducationDetailViewModel;
        }
        public FileResult Download(string FileName, string MEID)
        {
            int _ClientID = _myEducationService.GetMyEducationByID(int.Parse(_encryptionService.DecryptString2(MEID)));
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string filePath = Server.MapPath(GlobalConst.Message.StoragePath + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.CoursePDF + GlobalConst.ConstantChar.ForwardSlash) + FileName;
            return File(client.DownloadData(filePath), GlobalConst.Message.application_pdf, FileName);
        }

        public FileResult DownloadModuleAttachment(string FileName, string MEMID)
        {
            string Extension = "";
            if (Path.GetExtension(FileName).ToString().ToLower() == ".pdf")
                Extension = GlobalConst.Message.application_pdf;
            else if ((Path.GetExtension(FileName).ToString().ToLower() == ".xlsx") || (Path.GetExtension(FileName).ToString().ToLower() == ".xls"))
                Extension = GlobalConst.Message.application_xlsx;
            else
                Extension = GlobalConst.Message.application_jpeg;

            int _mEID = _myEducationService.GetMyEducationModulesDetailByMEMID(int.Parse(_encryptionService.DecryptString2(MEMID))).MEID;
            int _ClientID = _myEducationService.GetMyEducationByID(_mEID);
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            string filePath = Server.MapPath(GlobalConst.Message.StoragePath + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModuleUpload + GlobalConst.ConstantChar.ForwardSlash) + FileName;
            return File(client.DownloadData(filePath), Extension, FileName);
        }


        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "None")]
        public ActionResult MyEducationModule(string meID)
        {
            bool IsRevisitCase = false;
            LogSession logsession = new LogSession();
            if (Request.UrlReferrer != null)
            {

                meID = _encryptionService.DecryptString2(meID);
                /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
                IEnumerable<MyEducationModuleDetail> _myEducation = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(meID), HCRGUser.UID));
                foreach (MyEducationModuleDetail myEducation in _myEducation)
                {
                    myEducation.EducationModuleFileDetail = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(Convert.ToInt32(myEducation.EducationModuleID), 4));
                    IsRevisitCase = myEducation.Completed;

                }

                if (IsRevisitCase)
                {
                    logsession = Mapper.Map<LogSession>(_logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, Convert.ToInt32(meID)));
                    if (logsession == null)
                        logsession = new LogSession();
                }

                /*****************************End*********************************/
                if (logsession.PageUrl == null)
                {

                    MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
                    myEducationModuleDetailViewModel.MyEducationModuleDetailResults = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(meID), HCRGUser.UID));
                    myEducationModuleDetailViewModel.MyEducationModuleDetail = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                                where ss.Completed == false
                                                                                select ss).FirstOrDefault();
                    //If All Modules Are completed then select last
                    if (myEducationModuleDetailViewModel.MyEducationModuleDetail == null)
                    {
                        myEducationModuleDetailViewModel.MyEducationModuleDetail = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                                    where ss.Completed == true
                                                                                    select ss).LastOrDefault();
                    }
                    myEducationModuleDetailViewModel.TotalModules = myEducationModuleDetailViewModel.MyEducationModuleDetailResults.Count();
                    myEducationModuleDetailViewModel.CompletedModules = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                         where ss.Completed == true
                                                                         select ss).Count();
                    int completeprcentage = (myEducationModuleDetailViewModel.CompletedModules * 100) / myEducationModuleDetailViewModel.TotalModules;
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.percent = completeprcentage.ToString() + "%";
                    myEducationModuleDetailViewModel.UserID = HCRGUser.UID;
                    myEducationModuleDetailViewModel.IsRevisit = IsRevisitCase;
                    myEducationModuleDetailViewModel.SessionTimeout = ((Session.Timeout * 60000) + 20000).ToString();
                    foreach (MyEducationModuleDetail myEducationmodule in myEducationModuleDetailViewModel.MyEducationModuleDetailResults)
                    {

                        string decodedHTML = HttpUtility.HtmlDecode(myEducationmodule.EducationModuleDescription);
                        if (decodedHTML != null)
                        {
                            myEducationmodule.EducationModuleDescription = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                            myEducationmodule.EducationModuleDescription = myEducationmodule.EducationModuleDescription.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                            myEducationmodule.EducationModuleDescription = myEducationmodule.EducationModuleDescription.Replace("&nbsp;", "");
                        }
                        else
                            myEducationmodule.EducationModuleDescription = decodedHTML;
                    }

                    return View(myEducationModuleDetailViewModel);
                }
                else
                    return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
            }
            else
                return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
        }

        [HttpPost]
        public ActionResult EducationModuleChildNode(int EMID)
        {
            // EducationModuleFileDetail
            MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
            //   myEducationModuleDetailViewModel.EducationModuleFileDetail = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(EMID, GlobalConst.FileTypes.FileTypeID.ModuleUploadID));
            return Json(myEducationModuleDetailViewModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult FinishMyCourse(int _educationID, int _mEID)
        {
            var EducationExamQuestion = _examService.GetEducationExamQuestionByEducationID(_educationID);
            var EducationEvaluation = _examService.GetEducationEvaluationByEducationID(_educationID);
            var EncryptedMEID = _encryptionService.EncryptString2(_mEID.ToString());
            var EncryptedEducationID = _encryptionService.EncryptString2(_educationID.ToString());

            if (EducationExamQuestion != null)
            {
                var _ExamResult = Mapper.Map<IEnumerable<ExamResult>>(_examInternalService.GetExamAttempResulttByUser(HCRGUser.UID, _mEID));
                if (_ExamResult.Count() != 0)
                {
                    foreach (ExamResult _examResult in _ExamResult)
                    {
                        if (!_examResult.IsPass)
                        {
                            Tuple<string, string, string> data = new Tuple<string, string, string>("/Exam/FinalExam/", EncryptedEducationID.ToString(), EncryptedMEID);
                            return Json(data);//return Json("/Exam/FinalExam/" + EncryptedEducationID + "/" + EncryptedMEID);
                        }
                        else
                            break;
                    }
                }
                else
                {
                    Tuple<string, string, string> data = new Tuple<string, string, string>("/Exam/FinalExam/", EncryptedEducationID.ToString(), EncryptedMEID);
                    return Json(data); //return Json("/Exam/FinalExam/" + EncryptedEducationID + "/" + EncryptedMEID); 
                }
            }
            if (EducationEvaluation != null)
            {
                var _EvaluationResult = _examInternalService.GetEvaluationAttemptByUserAndMEID(HCRGUser.UID, _mEID);
                if (_EvaluationResult == null)
                {
                    Tuple<string, string, string> data = new Tuple<string, string, string>("/Exam/EvaluationExam/", EncryptedEducationID.ToString(), EncryptedMEID);
                    return Json(data); //return Json("/Exam/EvaluationExam/" + EncryptedEducationID + "/" + EncryptedMEID);

                }
            }
            return Json("/MyEducation/");
        }

        public void sendPaymentEmail(int shippingPaymentID, string transactionID, string userEmailID)
        {
            IEnumerable<EducationShoppingCart> _EducationShoppingCart = Mapper.Map<IEnumerable<EducationShoppingCart>>(_ShoppingEducationService.GetShoppingDetailsByShippingPaymentID(shippingPaymentID));

            string coursesName = "";
            string courseData = "";
            string booksName = "";
            string bookData = "";
            string allAccessPassData = "";
            string allAccessPass = "";
            decimal totalPrice = 0;
            decimal taxPrice = 0;
            decimal GtotalPrice = 0;
            string productName = "";
            string productCategoryNames = "";
            string msg = "";
            foreach (EducationShoppingCart edsc in _EducationShoppingCart)
            {
                if (edsc.CartType == "Course")
                {
                    if (coursesName != "")
                        coursesName += ", " + edsc.PName;
                    else
                        coursesName += edsc.PName;
                }
                else if (edsc.CartType == "Product")
                    booksName += ", " + edsc.PName;
                else if (edsc.CartType == "AllAccessPass")
                    allAccessPass += ", All Access Pass";
                totalPrice += edsc.Price;
                taxPrice += edsc.TaxPercentage.Value;

                // course detail data
                if (edsc.CartType == "Course")
                    courseData += @"Course Name: " + edsc.PName + @"</p><p>Payment Amount: " + "$" + String.Format("{0:0.00}", edsc.Price) + @"</p><br />";
                else if (edsc.CartType == "Product")
                {
                    if (edsc.PType == "Hard Copy")
                        bookData += @"<p>Book Name: " + edsc.PName + @"</p><p>Payment Amount: " + "$" + String.Format("{0:0.00}", edsc.Price) + @"</p><br />";
                    else if (edsc.PType == "Download")
                        bookData += @"<p>Download Name: " + edsc.PName + @"</p><p>Payment Amount: " + "$" + String.Format("{0:0.00}", edsc.Price) + @"</p><br />";
                }
                else if (edsc.CartType == "AllAccessPass")
                    allAccessPassData += @"<p>All Access Pass</p><p>Payment Amount: " + "$" + String.Format("{0:0.00}", edsc.Price) + @"</p><br />";
            }

            string url = GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/Content/img/email_logo.jpg";
            productCategoryNames = ((coursesName != "" ? "course(s)" : "") + (booksName != "" ? ", book(s)" : "") + (allAccessPass != "" ? ", All Access Pass" : "")).TrimStart(',').Trim();
            if (productCategoryNames.LastIndexOf(",") >= 0)
                productCategoryNames = productCategoryNames.Remove(productCategoryNames.LastIndexOf(","), 1).Insert(productCategoryNames.LastIndexOf(","), " and ");

            productName = (coursesName + booksName + allAccessPass).TrimStart(',').Trim();
            if (productName.LastIndexOf(",") >= 0)
                productName = productName.Remove(productName.LastIndexOf(","), 1).Insert(productName.LastIndexOf(","), " and ");
            GtotalPrice = totalPrice + taxPrice;
            msg = @"<img src='" + url + @"' />
                            <p>Thank you for purchasing " + productName + @"</p>
                            " + (courseData != "" ? "<p>The course content in now available in your student portal.</p><br /><p>" : "") + courseData + bookData + allAccessPassData + @"
                            <p>Payment Date: " + System.DateTime.Today.ToString("MM/dd/yyyy") + @"</p>
                            <p>Transaction Number #: " + transactionID + @"</p>
                            <p>Purchase       Total: " + "$" + String.Format("{0:0.00}", totalPrice) + @"</p>
                            <p>Tax: " + "$" + String.Format("{0:0.00}", taxPrice) + @"</p>
                            <p>Purchase Grand Total: " + "$" + String.Format("{0:0.00}", GtotalPrice) + @"</p>
                            <p>Please keep this email as confirmation of " + productCategoryNames + @" purchase.</p>
                            <p>Thanks!</p>";

            _mailService.SendEmail("Your Name Here Payment Confirmation", msg, userEmailID);
        }

        [OutputCache(NoStore = true, Duration = 1, VaryByParam = "None")]
        public ActionResult MyEducationModuleMedia(string MEMID)
        {
            LogSession logsession = new LogSession();
            bool IsRevisitCase = false;
            if (Request.UrlReferrer != null)
            {

                MEMID = _encryptionService.DecryptString2(MEMID);


                int _ClientID = 0;
                MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
                myEducationModuleDetailViewModel.MyEducationModuleDetail = Mapper.Map<MyEducationModuleDetail>(_myEducationService.GetMyEducationModulesDetailByMEMID(Convert.ToInt32(MEMID)));

                myEducationModuleDetailViewModel.MyEducationModuleDetail.EnCryptMEID = _encryptionService.EncryptString2(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID.ToString());
                if (myEducationModuleDetailViewModel.MyEducationModuleDetail != null)
                    _ClientID = _myEducationService.GetMyEducationByID(Convert.ToInt32(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID));

                // myEducationModuleDetailViewModel.MyEducationModuleDetail.CourseFilePath = "/Storage/CoursePDF/" + myEducationModuleDetailViewModel.MyEducationModuleDetail.CourseFile;
                myEducationModuleDetailViewModel.MyEducationModuleDetail.CourseFilePath = myEducationModuleDetailViewModel.MyEducationModuleDetail.CourseFile; //GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.CoursePDF + GlobalConst.ConstantChar.ForwardSlash + myEducationModuleDetailViewModel.MyEducationModuleDetail.CourseFile;
                if (myEducationModuleDetailViewModel.MyEducationModuleDetail.Completed == false && myEducationModuleDetailViewModel.MyEducationModuleDetail.IsTimerRequired == false)
                {
                    UpdateMyEducationModuleToComplete(Convert.ToInt32(MEMID));
                    //rebind again 
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.Completed = true;
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.CompletedDate = DateTime.Now;
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.TimeLeft = GlobalConst.Message.TimeSpanWithZero;
                }
                /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/

                IEnumerable<MyEducationModuleDetail> _myEducation = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID, HCRGUser.UID));

                foreach (MyEducationModuleDetail myEducation in _myEducation)
                    IsRevisitCase = myEducation.Completed;

                if (IsRevisitCase)
                {
                    logsession = Mapper.Map<LogSession>(_logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, Convert.ToInt32(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID)));
                    if (logsession == null)
                        logsession = new LogSession();
                }

                /*****************************End*********************************/
                if (logsession.PageUrl == null)
                {
                    if (myEducationModuleDetailViewModel.MyEducationModuleDetail.FileTypeName == GlobalConst.FileTypes.FileTypesName.TEXT)
                    {
                        string decodedHTML = HttpUtility.HtmlDecode(myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile);
                        if (decodedHTML != null)
                            myEducationModuleDetailViewModel.MyEducationModuleDetail.FilePath = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                        else
                            myEducationModuleDetailViewModel.MyEducationModuleDetail.FilePath = decodedHTML;
                    }
                    else if (myEducationModuleDetailViewModel.MyEducationModuleDetail.FileTypeName == GlobalConst.FileTypes.FileTypesName.Video)
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.FilePath = "http://view.vzaar.com/" + myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile + "/player";  //GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModuleVideo + GlobalConst.ConstantChar.ForwardSlash + myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile;
                    else if (myEducationModuleDetailViewModel.MyEducationModuleDetail.FileTypeName == GlobalConst.FileTypes.FileTypesName.PPT)
                    {
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.FilePath = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModulePPT + GlobalConst.ConstantChar.ForwardSlash + myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile;
                        MyEducationModuleDetailViewModel myEducationModuleDetailViewModel1 = new MyEducationModuleDetailViewModel();
                        myEducationModuleDetailViewModel1.MyEducationModuleDetail = Mapper.Map<MyEducationModuleDetail>(_myEducationService.GetMyEducationModulesDetailByMEMID(Convert.ToInt32(MEMID)));
                        MyEducationModule myeducationModule = new MyEducationModule();
                        myeducationModule.Completed = true;
                        myeducationModule.CompletedDate = DateTime.Now;
                        myeducationModule.EducationModuleID = myEducationModuleDetailViewModel1.MyEducationModuleDetail.EducationModuleID;
                        myeducationModule.MEID = myEducationModuleDetailViewModel1.MyEducationModuleDetail.MEID;
                        myeducationModule.MEMID = Convert.ToInt32(MEMID);
                        myeducationModule.TimeLeft = GlobalConst.Message.TimeSpanWithZero;
                        var MyeducationModuleID = _myEducationService.UpdateMyEducationModule(Mapper.Map<HCRGUniversityApp.NEPService.MyEducationService.MyEducationModule>(myeducationModule));

                    }
                    string[] filearray = myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile.Split('/');
                    string fileID = filearray[filearray.Length - 1].ToString();
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.ModuleFile = fileID;
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModulePosition = myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModulePosition + 1;
                    myEducationModuleDetailViewModel.MyEducationModuleDetailResults = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID), HCRGUser.UID));//

                    foreach (var _myEducationModuleDetailResults in myEducationModuleDetailViewModel.MyEducationModuleDetailResults)
                    {


                        _myEducationModuleDetailResults.EducationModuleFileDetail = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(Convert.ToInt32(_myEducationModuleDetailResults.EducationModuleID), 4));
                        foreach (var _EducationModuleFileDetail in _myEducationModuleDetailResults.EducationModuleFileDetail)
                        {
                            _EducationModuleFileDetail.ModuleFile = _EducationModuleFileDetail.ModuleFile;// GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.ModuleUpload + GlobalConst.ConstantChar.ForwardSlash + _EducationModuleFileDetail.ModuleFile;
                            _EducationModuleFileDetail.EnCryptMEMID = _encryptionService.EncryptString2(MEMID);

                            if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.pdf) != -1)
                                _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.pdf;
                            else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.jpg) != -1)
                                _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.jpg;
                            else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.xls) != -1)
                                _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.xls;
                            else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.xlt) != -1)
                                _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.xlt;
                            else
                                _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.na;
                        }
                    }

                    myEducationModuleDetailViewModel.MyEducationModuleDetail.MEMPositiontoStart = 1;
                    string decodedHTML1 = HttpUtility.HtmlDecode(myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription);
                    if (decodedHTML1 != null)
                    {
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription = decodedHTML1.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription = myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription = myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription.Replace("&nbsp;", "");
                    }
                    else
                        myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleDescription = decodedHTML1;
                    foreach (var item in myEducationModuleDetailViewModel.MyEducationModuleDetailResults)
                    {
                        //string[] filearray1 = item.ModuleFile.Split('/');
                        //string fileID1 = filearray1[filearray1.Length - 1].ToString();
                        //item.ModuleFile = fileID1; 

                        if (myEducationModuleDetailViewModel.MyEducationModuleDetail.MEMID == item.MEMID)
                        {
                            myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModulePosition = item.EducationModulePosition;
                            myEducationModuleDetailViewModel.MyEducationModuleDetail.Completed = item.Completed;
                        }
                        if (item.Completed == true)
                            myEducationModuleDetailViewModel.MyEducationModuleDetail.MEMPositiontoStart = item.EducationModulePosition + 1;


                        string decodedHTML = HttpUtility.HtmlDecode(item.EducationModuleDescription);
                        if (decodedHTML != null)
                        {
                            item.EducationModuleDescription = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                            item.EducationModuleDescription = item.EducationModuleDescription.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                            item.EducationModuleDescription = item.EducationModuleDescription.Replace("&nbsp;", "");
                        }
                        else
                            item.EducationModuleDescription = decodedHTML;
                    }
                    if (myEducationModuleDetailViewModel.MyEducationModuleDetail.FileTypeName != GlobalConst.FileTypes.FileTypesName.PPT)
                    {
                        string[] ClientSideTime = myEducationModuleDetailViewModel.MyEducationModuleDetail.TimeLeft.Split(':');
                        double seconds = TimeSpan.Parse(myEducationModuleDetailViewModel.MyEducationModuleDetail.TimeLeft).TotalSeconds;
                        myEducationModuleDetailViewModel.Hours = Convert.ToInt32(ClientSideTime[0]);
                        myEducationModuleDetailViewModel.Minutes = Convert.ToInt32(ClientSideTime[1]);
                        myEducationModuleDetailViewModel.Seconds = Convert.ToInt32(ClientSideTime[2]);
                    }
                    myEducationModuleDetailViewModel.UserID = HCRGUser.UID;
                    myEducationModuleDetailViewModel.IsRevisit = IsRevisitCase;
                    myEducationModuleDetailViewModel.SessionTimeout = ((Session.Timeout * 60000) + 20000).ToString();
                    return View(myEducationModuleDetailViewModel);
                }
                else
                    return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
            }
            else
                return RedirectToAction(GlobalConst.Actions.UserController.Unauthorise, GlobalConst.Controllers.User);
        }

        [HttpPost]
        public JsonResult GetMyEducationModulesDetailsByMEID(int MEMID)
        {
            MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
            myEducationModuleDetailViewModel.MyEducationModuleDetail = Mapper.Map<MyEducationModuleDetail>(_myEducationService.GetMyEducationModulesDetailByMEMID(MEMID));
            myEducationModuleDetailViewModel.MyEducationModuleDetailResults = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID), HCRGUser.UID));//

            foreach (var _myEducationModuleDetailResults in myEducationModuleDetailViewModel.MyEducationModuleDetailResults)
            {
                _myEducationModuleDetailResults.EducationModuleFileDetail = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(Convert.ToInt32(_myEducationModuleDetailResults.EducationModuleID), 4));
                foreach (var _EducationModuleFileDetail in _myEducationModuleDetailResults.EducationModuleFileDetail)
                {
                    if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.pdf) != -1)
                        _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.pdf;
                    else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.jpg) != -1)
                        _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.jpg;
                    else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.xls) != -1)
                        _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.xls;
                    else if (_EducationModuleFileDetail.ModuleFile.ToLower().IndexOf(GlobalConst.Extension.xlt) != -1)
                        _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.xlt;
                    else
                        _EducationModuleFileDetail.ModuleFileExtension = GlobalConst.Extension.na;
                }
            }

            return Json(myEducationModuleDetailViewModel.MyEducationModuleDetailResults, GlobalConst.Message.text_html);
        }

        public void UpdateMyEducationModuleToComplete(int meMID)
        {

            MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
            myEducationModuleDetailViewModel.MyEducationModuleDetail = Mapper.Map<MyEducationModuleDetail>(_myEducationService.GetMyEducationModulesDetailByMEMID(meMID));
            MyEducationModule myeducationModule = new MyEducationModule();
            myeducationModule.Completed = true;
            myeducationModule.CompletedDate = DateTime.Now;
            myeducationModule.EducationModuleID = myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleID;
            myeducationModule.MEID = myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID;
            myeducationModule.MEMID = meMID;
            myeducationModule.TimeLeft = GlobalConst.Message.TimeSpanWithZero;
            var MyeducationModuleID = _myEducationService.UpdateMyEducationModule(Mapper.Map<HCRGUniversityApp.NEPService.MyEducationService.MyEducationModule>(myeducationModule));


        }

        [WebMethod]
        [ScriptMethod]
        public ActionResult UpdateTimer(int meMID, string updatedTime)
        {
            bool Status;
            if (updatedTime.Replace(" ", String.Empty) == GlobalConst.Message.TimeSpanWithZero)
            {
                MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
                myEducationModuleDetailViewModel.MyEducationModuleDetail = Mapper.Map<MyEducationModuleDetail>(_myEducationService.GetMyEducationModulesDetailByMEMID(meMID));
                MyEducationModule myeducationModule = new MyEducationModule();
                myeducationModule.Completed = true;
                Status = true;
                myeducationModule.CompletedDate = DateTime.Now;
                myeducationModule.EducationModuleID = myEducationModuleDetailViewModel.MyEducationModuleDetail.EducationModuleID;
                myeducationModule.MEID = myEducationModuleDetailViewModel.MyEducationModuleDetail.MEID;
                myeducationModule.MEMID = meMID;
                myeducationModule.TimeLeft = GlobalConst.Message.TimeSpanWithZero;
                var MyeducationModuleID = _myEducationService.UpdateMyEducationModule(Mapper.Map<HCRGUniversityApp.NEPService.MyEducationService.MyEducationModule>(myeducationModule));
            }
            else
            {
                Status = false;
                _myEducationService.UpdateMyEducationModuleTimeLeft(meMID, updatedTime.Replace(" ", String.Empty));
            }
            return Json(Status, GlobalConst.Message.text_html);
        }
        public FileResult PrintCertificate(int meID, bool? certificatePrinted, string certificateURL)
        {
            int _ClientID = _myEducationService.GetMyEducationByID(meID);

            CertificateDetail certdetail = new CertificateDetail();
            string Certificatepath = "";
            // certificatePrinted=null;
            if (certificatePrinted == null)
            {
                string CUnit = "";
                certdetail = Mapper.Map<CertificateDetail>(_myEducationService.GetCertificateDetailByMEID(meID));
                var _org = _clientService.GetOrganizationByID((HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                string imgServerpath = Server.MapPath("../Content/img/").Replace("\\", "/");
                string imgServerpath1 = Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.LogoImage + GlobalConst.ConstantChar.ForwardSlash + _org.LogoImage).Replace("\\", "/");
                string fontServerpath = Server.MapPath("../Content/fonts/").Replace("\\", "/");
                if (certdetail.AccreditorName != null)
                    certdetail.AccreditorName = "This course is approved by " + certdetail.AccreditorName;
                if (certdetail.CredentialUnit != null)
                    CUnit = certdetail.CredentialUnit + @" CEUs (" + certdetail.CredentialProgram + @")";
                string htmlCertificate = @"<!DOCTYPE html>
                                                            <html lang='en' xmlns='http://www.w3.org/1999/xhtml'>
                                                            <head>
                                                                <meta charset='utf-8' />
                                                                <title></title>                                                           
                                                                <style>
                                                                    .bg {
                                                                        width: 100%;
                                                                        height: 2100px;
                                                                        background-image: url('" + imgServerpath + @"bg.png');
                                                                        padding: 20px;
                                                                        text-align: center;
                                                                    }
                                                                    .text {
                                                                        width: 2700px;
                                                                        height: 1500px;
                                                                        padding: 20px;
                                                                        text-align: center;
                                                                        margin-top: 9% !important;
                                                                        font-size: 32px;
                                                                        font-family:AGaramond LT;
                                                                    }
                                                                    .uni_logo {
                                                                        font-size:124px;
                                                                        top: 20%;
                                                                        color: #b99a49;
                                                                        text-align: center;
                                                                        font-family:Open Sans;
                                                                    }
                                                                    .slogan {
                                                                        font-size: 31px;
                                                                        font-family:Gotham Book;
                                                                        height:70px;
                                                                    }
                                                                    .italic_text {
                                                                        font-size: 52px;
                                                                       font-family:AGaramond RegularSC;
                                                                        font-style:italic;
                                                                    }
                                                                    .stu_name {
                                                                        font-size: 80px;
                                                                          font-family:AGaramond LT Bold;
                                                                    }
                                                                    .stamp {                                                                   
                                                                        width: 250px;  
                                                                    }
                                                                    .course_name {
                                                                        height: 75px;
                                                                        font-size: 64px;
                                                                        font-family:AGaramond LT;
                                                                    }
                                                                    .left_side {
                                                                        width: 390px;
                                                                         font-family:AGaramond LT;
                                                                        font-size:42px;
                                                                       text-align:center;
                                                                       margin-left:10% !important;
                                                                    }
                                                                    .right_side {
                                                                        width: 580px;
                                                                        font-family:AGaramond LT;
                                                                        font-size:42px;
                                                                    }
                                                                    .lower_part {
                                                                        height: 280px;
                                                                        width: 2700px;
                                                                    }
                                                                      .message {
                                                                        height: 180px;
                                                                        width: 2375px;
                                                                       font-family:AGaramond LT;
                                                                        font-size: 38px;
                                                                        margin-left:20% !important;
                                                                        text-align:justify;
                                                                    }
                                                                    .nurses {                                                                     
                                                                       width:1100px;
                                                                       font-size: 38px;
                                                                        font-family:Garamond ;
                                                                    }
                                                                    .footer_text {
                                                                        font-size: 40px;
                                                                       font-family:AGaramond LT;
                                                                    }
                                                                    .linetm{
                                                                  font-size:124px;
                                                                        top: 20%;
                                                                        color: #b99a49;
                                                                        text-align: center;
                                                                        font-family:Sansation Light;
                                                                    }
                                                                </style>
                                                            </head>
                                                            <body>
                                                                <div class='bg'>
                                                                    <div class='text'>
                                                                        <div class='uni_logo'> <img  src='" + imgServerpath1 + @"'/></div>
                                                                        <br/>

                                                                        <div class='italic_text'><i>has awarded this</i></div>
                                                                        <div class='italic_text'><i>Certificate</i></div>
                                                                        <div class='italic_text'><i>to</i></div>
                                                                        <div class='stu_name'>" + certdetail.Name + @"</div><br />
                                                                        <div class='italic_text'><i>for&nbsp;successful completion of the course</i></div> <br /><br />
                                                                        <div class='course_name'>" + certdetail.CourseName + @" </div> <br />
                                                              <div class='message'><i>" + certdetail.CertificateMessage + @" <i></div>
                                                                        <div class='lower_part'>
                                                                              <table width='100%'  style='text-align:center'>
                                                                                  <tr>
                                                                                    <td style='width:10%'>&nbsp;</td>
                                                                                    <td style='width:90%'>
                                                                        <table  width='100%' align='center'  style='text-align:center'>
                                                                        <tr>
                                                                        <td  class='left_side'>
                                                                              <div style='width:100%;text-align:center;margin-left:20% !important' >
                                                                                                                 <div style='height:60px'>" + certdetail.CompletedDate.ToString("dddd, MMMM dd, yyyy") + @"</div>
                                                                                                               <img height='4px' width='600px'  src='" + imgServerpath1 + @"line.png'/>
                                                                                                                                   <br/>  
                                                                                                                        <span>    Date of Completion <span/>
                                                                                </div>
                                                                        </td>
                                                      <td   class='stamp'> <div width='100%' style='text-align:right'> <img height='400px' width='400px' src='" + imgServerpath + @"stamp.png'/>  </div> </td>
                                                                        <td  class='right_side'>   
                                                                                                                                                          
                                                                                                                                                   </td>
                                                                        </tr>
                                                                        </table>
                                                                                        </td>
                                                                                <td style='width:1%'>&nbsp;</td>
                                                                                  </tr>
                                                                                </table>
                                                                          <div class='nurses'>" + CUnit + @"</div>
                                                                        </div>
                                                                      <br /><br />
                                                                       <br>
                                                                    </div>
                                                                </div>
                                                            </body>
                                                            </html>";
                System.Drawing.Image image = HtmlRender.RenderToImage(htmlCertificate, new Size(2750, 2125));
                Certificatepath = Server.MapPath(GlobalConst.Message.StoragePath + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash);
                string filename = UploadCertificate(image, Guid.NewGuid().ToString(), Certificatepath);
                Certificatepath = Certificatepath + GlobalConst.FolderName.Certificate + GlobalConst.ConstantChar.ForwardSlash + filename;
                _myEducationService.UpdateMyEducationForCertificateByMEID(meID, true, filename);
            }
            else
                Certificatepath = Server.MapPath(GlobalConst.Message.StoragePath + GlobalConst.FolderName.Org + (HCRGUser != null ? HCRGUser.OrganizationID.ToString() : (_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))) + GlobalConst.ConstantChar.ForwardSlash + _ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Certificate + GlobalConst.ConstantChar.ForwardSlash) + certificateURL;
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultNetworkCredentials;
            return File(client.DownloadData(Certificatepath), GlobalConst.Message.image_jpeg, "Certificate.jpeg");
        }
        private string UploadCertificate(Image file, string filename, string Certificatepath)
        {
            var path = _storageService.SetCertificateStoragePath(Certificatepath, filename + "." + ImageFormat.Jpeg);
            var fileName = Path.GetFileName(path);
            file.Save(path);
            return fileName;
        }
        public ActionResult MyEducationAccountHistory()
        {
            MyEducationAccountHistoryViewModel myEducationAccountHistoryViewModel = new MyEducationAccountHistoryViewModel();
            var MyEducationAccountHistory = _myEducationService.GetMyEducationDetailByUserIDPaged(HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake);
            myEducationAccountHistoryViewModel.myEducationAccountHistory = Mapper.Map<IEnumerable<MyEducationAccountHistory>>(MyEducationAccountHistory.accountHistory);
            myEducationAccountHistoryViewModel.TotalItemCount = MyEducationAccountHistory.TotalCount;
            myEducationAccountHistoryViewModel.UserAllAccessPassResults = Mapper.Map<UserAllAccessPass>(_userService.getUserAllAccessPassByUserID(HCRGUser.UID));


            return View(myEducationAccountHistoryViewModel);
        }
        [HttpPost]
        public ActionResult MyEducationAccountHistory(int skip)
        {
            MyEducationAccountHistoryViewModel myEducationAccountHistoryViewModel = new MyEducationAccountHistoryViewModel();
            var MyEducationAccountHistory = _myEducationService.GetMyEducationDetailByUserIDPaged(HCRGUser.UID, skip, GlobalConst.Records.LandingTake);
            myEducationAccountHistoryViewModel.myEducationAccountHistory = Mapper.Map<IEnumerable<MyEducationAccountHistory>>(MyEducationAccountHistory.accountHistory);
            myEducationAccountHistoryViewModel.TotalItemCount = MyEducationAccountHistory.TotalCount;
            return Json(myEducationAccountHistoryViewModel, GlobalConst.Message.text_html);
        }
        // new
        [HttpPost]
        public ActionResult MyEducationAccountHistoryPrint(int Totalcount)
        {
            MyEducationAccountHistoryViewModel myEducationAccountHistoryViewModel = new MyEducationAccountHistoryViewModel();
            var MyEducationAccountHistory = _myEducationService.GetMyEducationDetailByUserIDPaged(HCRGUser.UID, 0, Totalcount);
            myEducationAccountHistoryViewModel.myEducationAccountHistory = Mapper.Map<IEnumerable<MyEducationAccountHistory>>(MyEducationAccountHistory.accountHistory);
            myEducationAccountHistoryViewModel.TotalItemCount = MyEducationAccountHistory.TotalCount;
            return Json(myEducationAccountHistoryViewModel, GlobalConst.Message.text_html);
        }
        public MyEducationDetailViewModel GetMyEducationInProgressByUserIDPaged(int skip)
        {
            MyEducationDetailViewModel myEducationDetailViewModel = new MyEducationDetailViewModel();
            var GetMyEducationInProgress = _myEducationService.GetMyEducationInProgressByUserIDPaged(HCRGUser.UID, skip, GlobalConst.Records.Take5);
            myEducationDetailViewModel.MyEducationDetailResults = Mapper.Map<IEnumerable<MyEducationDetail>>(GetMyEducationInProgress.myeducation);
            myEducationDetailViewModel.TotalInprocessItemCount = GetMyEducationInProgress.TotalCount;
            foreach (MyEducationDetail myEducationDetail in myEducationDetailViewModel.MyEducationDetailResults)
            {
                /*****************************Code for Get Already open course module and exam browser*********************************/
                var logsession = _logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, myEducationDetail.MEID);
                if (logsession != null)
                {
                    myEducationDetail.PageUrl = logsession.PageUrl;
                    myEducationDetail.LogErrorMsg = GlobalConst.Message.OpenInAnotherResource;
                }
                /*****************************End*********************************/
                myEducationDetail.EncryptedMEID = _encryptionService.EncryptString2(myEducationDetail.MEID.ToString());
                myEducationDetail.EncryptedEducationID = _encryptionService.EncryptString2(myEducationDetail.EducationID.ToString());
                var EducationPreTestQuestion = _examService.GetEducationPreTestQuestionByEducationID(myEducationDetail.EducationID);
                var EducationExamQuestion = _examService.GetEducationExamQuestionByEducationID(myEducationDetail.EducationID);
                var EducationEvaluation = _examService.GetEducationEvaluationByEducationID(myEducationDetail.EducationID);
                if (EducationPreTestQuestion != null)
                {
                    myEducationDetail.PreTestAttemptByUser = _examInternalService.GetPreTestAttemptByUser(HCRGUser.UID, myEducationDetail.MEID);
                    myEducationDetail.IsPreTestAvailable = true;
                }
                if (EducationExamQuestion != null)
                {
                    myEducationDetail.ExamResult = Mapper.Map<IEnumerable<ExamResult>>(_examInternalService.GetExamAttempResulttByUser(HCRGUser.UID, myEducationDetail.MEID));
                    myEducationDetail.IsExamAvailable = true;
                    myEducationDetail.FinalExamAttemptByUser = myEducationDetail.ExamResult.Count();
                    foreach (ExamResult _ExamResult in myEducationDetail.ExamResult)
                    {
                        myEducationDetail.IsExamPass = _ExamResult.IsPass;
                        if (myEducationDetail.IsExamPass)
                            break;
                    }
                }
                if (EducationEvaluation != null)
                    myEducationDetail.IsEvalutionAvailable = true;
            }
            return myEducationDetailViewModel;
        }

        [HttpPost]
        public ActionResult GetProductPurchasePaged(int skip)
        {
            PagedProductPurchase _products = new PagedProductPurchase();
            _products = Mapper.Map<PagedProductPurchase>(_productService.GetPagedProductByProductType(GlobalConst.Message.Download, HCRGUser.UID, skip, GlobalConst.Records.Take5));
            return Json(_products);
        }

        [HttpPost]
        public ActionResult GetCertificationContentByID(int _certificationTitleOptionID)
        {
            CertificationTitleOption _certificationTitleOption = new CertificationTitleOption();
            _certificationTitleOption = Mapper.Map<CertificationTitleOption>(_CertificationTitleOptionService.GetCertificationTitleOptionsByID(_certificationTitleOptionID));

            string decodedHTML = HttpUtility.HtmlDecode(_certificationTitleOption.CertificationContent);
            if (decodedHTML != null)
            {
                decodedHTML = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                decodedHTML = decodedHTML.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                decodedHTML = decodedHTML.Replace("&nbsp;", "");
            }

            _certificationTitleOption.CertificationContent = decodedHTML;

            return Json(_certificationTitleOption);
        }

        [HttpPost]
        public ActionResult MyEducationModuleMediaSkipByMethod(string meID)
        {
            bool IsRevisitCase = false;
            LogSession logsession = new LogSession();
            if (Request.UrlReferrer != null)
            {

                meID = _encryptionService.DecryptString2(meID);
                /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
                IEnumerable<MyEducationModuleDetail> _myEducation = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(meID), HCRGUser.UID));
                foreach (MyEducationModuleDetail myEducation in _myEducation)
                {
                    myEducation.EducationModuleFileDetail = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(Convert.ToInt32(myEducation.EducationModuleID), 4));
                    IsRevisitCase = myEducation.Completed;

                }

                if (IsRevisitCase)
                {
                    logsession = Mapper.Map<LogSession>(_logService.getLogSessionByUserIDAndMEID(HCRGUser.UID, Convert.ToInt32(meID)));
                    if (logsession == null)
                        logsession = new LogSession();
                }

                /*****************************End*********************************/
                if (logsession.PageUrl == null)
                {

                    MyEducationModuleDetailViewModel myEducationModuleDetailViewModel = new MyEducationModuleDetailViewModel();
                    myEducationModuleDetailViewModel.MyEducationModuleDetailResults = Mapper.Map<IEnumerable<MyEducationModuleDetail>>(_myEducationService.GetMyEducationModulesDetailsByMEID(Convert.ToInt32(meID), HCRGUser.UID));
                    myEducationModuleDetailViewModel.MyEducationModuleDetail = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                                where ss.Completed == false
                                                                                select ss).FirstOrDefault();
                    //If All Modules Are completed then select last
                    if (myEducationModuleDetailViewModel.MyEducationModuleDetail == null)
                    {
                        myEducationModuleDetailViewModel.MyEducationModuleDetail = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                                    where ss.Completed == true
                                                                                    select ss).LastOrDefault();
                    }
                    myEducationModuleDetailViewModel.TotalModules = myEducationModuleDetailViewModel.MyEducationModuleDetailResults.Count();
                    myEducationModuleDetailViewModel.CompletedModules = (from ss in myEducationModuleDetailViewModel.MyEducationModuleDetailResults
                                                                         where ss.Completed == true
                                                                         select ss).Count();
                    int completeprcentage = (myEducationModuleDetailViewModel.CompletedModules * 100) / myEducationModuleDetailViewModel.TotalModules;
                    myEducationModuleDetailViewModel.MyEducationModuleDetail.percent = completeprcentage.ToString() + "%";
                    myEducationModuleDetailViewModel.UserID = HCRGUser.UID;
                    myEducationModuleDetailViewModel.IsRevisit = IsRevisitCase;
                    myEducationModuleDetailViewModel.SessionTimeout = ((Session.Timeout * 60000) + 20000).ToString();
                    foreach (MyEducationModuleDetail myEducationmodule in myEducationModuleDetailViewModel.MyEducationModuleDetailResults)
                    {

                        string decodedHTML = HttpUtility.HtmlDecode(myEducationmodule.EducationModuleDescription);
                        if (decodedHTML != null)
                        {
                            myEducationmodule.EducationModuleDescription = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                            myEducationmodule.EducationModuleDescription = myEducationmodule.EducationModuleDescription.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                            myEducationmodule.EducationModuleDescription = myEducationmodule.EducationModuleDescription.Replace("&nbsp;", "");
                        }
                        else
                            myEducationmodule.EducationModuleDescription = decodedHTML;

                    }

                    return Json(myEducationModuleDetailViewModel);
                }
                else
                {
                    return Json(GlobalConst.Message.UnauthoriseUser);
                }
            }
            else
            {
                return Json(GlobalConst.Message.UnauthoriseUser);
            }
        }
    }
}
