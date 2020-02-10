namespace HCRGUniversityMgtApp.Infrastructure.Global
{
    public class GlobalConst
    {
        public struct Controllers
        {
            public const string Home = "Home";
            public const string Login = "Login";
            public const string AboutUs = "AboutUs";
            public const string CarouselSetUp = "CarouselSetUp";
            public const string College = "College";
            public const string Education = "Education";
            public const string Profession = "Profession";
            public const string EducationFormat = "EducationFormat";
            public const string EducationLink = "EducationLink";
            public const string DiscountCoupon = "DiscountCoupon";
            public const string User = "User";
            public const string News = "News";
            public const string EducationModule = "EducationModule";
            public const string FAQ = "FAQ";
            public const string PreTest = "PreTest";
            public const string Exam = "Exam";
            public const string Evaluation = "Evaluation";
            public const string HomeContent = "HomeContent";
            public const string Accreditor = "Accreditor";
            public const string EventController = "Event";
            public const string TrainingAndSeminar = "TrainingAndSeminar";
            public const string Accreditation = "Accreditation";
            public const string Certification = "Certification";
            public const string NewsLetter = "NewsLetter";
            public const string Product = "Product";
            public const string UserSubscription = "UserSubscription";
            public const string Enterprise = "Enterprise";
            public const string CertificationTitleOption = "CertificationTitleOption";
            public const string Common = "Common";
            public const string Client = "Client";
            public const string Organization = "Organization";
            public const string Plan = "Plan";
            public const string Group = "Group";
        }
        public struct EducationType
        {
            public const int HardCopy = 1;
            public const int Online = 2;
        }
        public struct FileTypes
        {
            public const int TEXT = 1;
            public const int PPT = 2;
            public const int Video = 3;
            public const int ModuleUploads = 4;
            
        }
        public struct CouponType
        {
            public const string Course = "Course";
            public const string Product = "Product";
        }
        public struct PreTestAnswerType
        {
            public const string TrueorFalse = "True or False";
            public const string MultipleChoice = "Multiple Choice";
        }
        public struct ExamAnswerType
        {
            public const string TrueorFalse = "True or False";
            public const string MultipleChoice = "Multiple Choice";
        }
        public struct Actions
        {
            public struct AboutUsController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string Detail = "Detail";
                public const string Update = "Update";
                public const string getAll = "getAll";
                public const string ShowEditor = "ShowEditor";
            }
            public struct LoginController
            {
                public const string Index = "Index";
                public const string Login = "Login";
                public const string LogOff = "LogOff";
                public const string ResetPassword = "ResetPassword";
            }
            public struct HomeController
            {
                public const string Index = "Index";
            }
            public struct CarouselSetUpController
            {
                public const string Index = "Index";
                public const string AddCarousel = "AddCarousel";
                public const string GetCarouselGrid = "GetCarouselGrid";
            }
            public struct CollegeController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
            }

            public struct CertificationTitleOptionController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string ShowEditor = "ShowEditor";

            }
   
            public struct ProfessionController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
            }
            public struct EducationController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
                public const string addEducation = "addEducation";
                public const string ShowEditor = "ShowEditor";
                public const string AddEducationCredential = "AddEducationCredential";
                public const string getAllEducationCredential = "getAllEducationCredential";
            }
            public struct EducationFormatController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
            }
       
            public struct DiscountCouponController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
            }
            public struct UserController
            {
                public const string Index = "Index";
                public const string AddUser = "AddUser";
                public const string UpdateUser = "UpdateUser";
                public const string LogOff = "LogOff";
                public const string ResetPassword = "ResetPassword";
            }

            public struct UserSubscriptionController
            {
                public const string Index = "Index";
                public const string AddUserSubscription = "AddUserSubscription";
            }
            public struct NewsController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string getAll = "getAll";
                public const string ShowEditor = "ShowEditor";
                //AddNewsSection
                public const string AddNewsSection = "AddNewsSection";
            }
            public struct EducationModuleController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string AddSubModule = "AddSubModule";
                public const string UpdateEducationModuleFile = "UpdateEducationModuleFile";
                public const string ShowEditorModule = "ShowEditorModule";
                public const string ShowEditEditorModule = "ShowEditEditorModule";
                public const string UploadMultipleModuleFiles = "UploadMultipleModuleFiles";
            }
            public struct PlanController
            {
                public const string Index = "Index";
                public const string AddPlan = "AddPlan";
            }
            public struct FAQController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string AddFAQCategory = "AddFAQCategory";
            }

            public struct EnterpriseController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct PreTestController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct ExamController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct EvaluationController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct HomeContentController
            {
                public const string Index = "Index";
                public const string Update = "Update";
                public const string UpdateIndustryResearchContent = "UpdateIndustryResearchContent";
                public const string IndustryResearch = "IndustryResearch";
                public const string PrivacyPolicy = "PrivacyPolicy";
                public const string UpdatePrivacyPolicy = "UpdatePrivacyPolicy";
                public const string TermsCondition = "TermsCondition";
                public const string UpdateTermsCondition = "UpdateTermsCondition";
                public const string NewsLetter = "NewsLetter";
                public const string UpdateNewsLetter = "UpdateNewsLetter";
                public const string ReturnPolicy = "ReturnPolicy";
                public const string DeliveryTerm = "DeliveryTerm";
                public const string UpdateReturnPolicy = "UpdateReturnPolicy";
                public const string UpdateDeliveryTerm = "UpdateDeliveryTerm";
            }

            public struct GroupController
            {
                public const string Index = "Index";
                public const string AddGroup = "AddGroup";
            }


            public struct AccreditationController
            {
                public const string Index = "Index";
                public const string Update = "Update";               
            }

            public struct CertificationController
            {
                public const string Index = "Index";
                public const string Update = "Update"; 
            }

            public struct NewsLetterController
            {
                public const string Index = "Index";
                public const string Update = "Update";
            }

            public struct TrainingAndSeminarController
            {
                public const string Index = "Index";
                public const string AddTrainingAndSeminar = "AddTrainingAndSeminar";
            }
            public struct AccreditorController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct EventController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct ProductController
            {
                public const string Index = "Index";
                public const string Add = "Add";
                public const string Addproductshippingdetail = "Addproductshippingdetail";
            }
            public struct CommonController
            {
                public const string ContactUs = "ContactUs";
            }

            public struct ClientController
            {
                public const string ClientEdit = "ClientEdit";
                public const string SaveClient = "SaveClient";
            }

            public struct OrganizationController
            {
                public const string Index = "Index";
                public const string AddOrganization = "AddOrganization";
            }
        }
        public struct Views
        {
            public struct User
            {
                public const string Index = "Index";
                public const string Login = "Login";
            }
            public struct Home
            {
                public const string Index = "Index";
            }
            public struct College
            {
                public const string DetailFaculty = "../College/DetailFaculties";
            }
            public struct Client
            {
                public const string ClientEdit = "ClientEdit";
                public const string Index = "Index";
            }
        }
        public struct MgmtDirectoryStructure
        {
            public const string ModulePDF = "ModulePDF";
            public const string NewsPhotos = "NewsPhotos";
            public const string ModuleVideo = "ModuleVideo";
            public const string ModulePPT = "ModulePPT";
            public const string CoursePDF = "CoursePDF";
            public const string ModuleImages = "ModuleImages";
            public const string Resume = "Resume";
            public const string ErrorLogMgt = "ErrorLogMgt";
        }
        public struct VirtualDirectoryPath
        {
            public const string VirtualPath = "VirtualPath";
        }
        public struct SessionKeys
        {
            public const string CLIENTID = "ClientID";
            public const string CLIENT = "Client";
            public const string ClientTypeID = "ClientTypeID";
            public const int  ClientTypeIdValue = 3;
            public const int ClientTypeIDTwo = 2;
            public const int ClientTypeIDOne = 1;
        }
        public struct Records
        {
            public const int Skip = 0;
            public const int Take = 10;
            public const int Take15 = 15;
            public const int LandingTake = 50;
            public const int LandingTakeTest = 4;
            public const int Take5 = 5;
            public const int take500 = 500;
            public const int take5000 = 5000;
        }

        public struct CommonValues
        {
            public const int Zero = 0;
            public const int One = 1;
        }


        public struct Message
        {
            public const string text_html = "text/html";
            public const string LoginUrl = "Login Url";
            public const string StdLoginUrl = "StdLoginUrl";
            

            public const string UserIsLockMessage = "User is Lock.";
            public const string InvalidCredentials = "Invalid Credentials";
            public const string InvalidPassword = "Invalid User Password";
            public const string ValidLogin = "ValidLogin";
            public const string UserMgmtAccess = "User don't have Management Access.";

            public const string application_pdf = "application/pdf";
            public const string application_xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string application_jpeg = "image/jpeg";

            public const string ReSetUrl = "ReSetUrl";
            public const string AddSucessfully = "Add Sucessfully.";
            public const string UpdateSucessfully = "Update Sucessfully.";
            public const string EmailSentSuccessfully = "Email Sent Successfully.";
            public const string ErrorOccouredWhileSendingEmail = "Error occoured while sending email";
            public const string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            public const string allowedNumberChars = "23456789";
            public const string EmailExist = "The email you are trying to use already exists. We cannot create this account. If you think this is a mistake, please contact us at support@allgatelearning.com";
            public const string UserPlanAdded = "User Plan Added Successfully";
            public const string CourcePlanAdded = "Cource Plan Added Successfully";
            public const string UserPlanExist = "User Plan already exist";
            public const string CourcePlanExist = "Cource Plan Already Exist";
            public const string ResetPassword = "Password Reset Sucessfully.";
            public const string NotAuthorized = "You are not authorized to add Home Content";
            public const string PretestSave = "PreTest Question Insert Successfully";
            public const string PretestUpdated = "PreTest Question Updated Successfully";
            public const string EvaluationSave = "Evaluation Question Insert Successfully";
            public const string EvaluationUpdated = "Evaluation Question Updated Successfully";
        }

        public struct FolderName
        {
            public const string Org  = "Org-";
            public const string LogoImage = "LogoImage";
            public const string FaviconImage = "FaviconImage";
            public const string Storage = "/Storage";
            public const string RTEUpload = "RTEUpload";
            public const string ModulePPT = "ModulePPT";
            public const string ModuleUpload = "ModuleUploads";
        }

        public struct SubFolderName
        {
            public const string ClientID = "ClientID";
        }
        public struct ConstantChar
        {
           public const string DoubleBackSlash = @"\";
           public const string ForwardSlash = "/";
           public const string ALL = "All";
           public const string Blank_String = "";
           public const string Colon = ":";

        }
        public struct Extension
        {
            public const string doc = ".doc";
            public const string EXCEL = "EXCEL";
            public const string http = "http://";
            public const string pdf = ".pdf";
            public const string PDF = "PDF";
            public const string WORD = "WORD";
            public const string xlsx = ".xls";
            public const string xml = ".xml";
        }

       
    }
}
