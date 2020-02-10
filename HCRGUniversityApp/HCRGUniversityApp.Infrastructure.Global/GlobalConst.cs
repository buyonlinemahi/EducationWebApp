
namespace HCRGUniversityApp.Infrastructure.Global
{
    /*
     * important notes....
     * College is renamed to subject matter
     * Resource is renamed to WorkerComp News
     * Education is courses
     */

    public class GlobalConst
    {

        public struct SessionKeys
        {
            public const string USERID = "UserID";
            public const string USER = "User";
            public const string EDUCATIONID = "EducationID";
            public const string CLIENTID = "ClientID";

        }
        public struct Records
        {
            public const int Skip = 0;
            public const int Take = 10;
            public const int Take5 = 5;
            public const int LandingTake = 25;
            public const int Take20 = 20;
        }

        public struct Message
        {

            public const string DeletedSuccessfully = "Deleted Successfully";

            public const string TimeExpired = "One hour time duration expired to reset password.Try again with new password!";

            public const string AlReadyDeletedSuccessfully = "AlReadyDeletedSuccessfully";

            public const string CannotDeleted = "Item cannot be delete , as it is already in process.";

            public const string AddedSuccessfully = "Added Successfully";
            
            public const string UpdatedSuccessfully = "Updated Successfully";
            public const string EvaluationSubmittedSuccessfully = "Evaluation Submitted Successfully.";

            public const string EmailSentSuccessfully = "Email Sent Successfully.";

            public const string PasswordUpdatedSuccessfully = "Password Updated Successfully";
            public const string PasswordNotcorrect = "Password is not correct";
            public const string UserSuccessfullyAdded = "User Successfully Added";
            public const string Success = "Success";
            public const string success = "success";

            public const string UnauthoriseUser = "Unauthorise";

            public const string allowedLetterChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ";
            public const string allowedNumberChars = "23456789";
            

            public const string OpenInAnotherResource = "Exam or Module open in another browser or tab or else contact system administrator";
            public const string LockCourse = "Course is locked for 24 Hours. Please contact our support team for any issue";

            public const string text_html = "text/html";
            public const string image_jpeg = "image/jpeg";
            public const string ReSetUrl = "ReSetUrl";
            public const string LogScheduleTime = "LogScheduleTime";

            public const string AlreadyAddToCart = "Already Add to Cart";
            public const string CourseAlreadyInProgress = "This Course is already in Progress in your account.";
            public const string CoupanNotValid = "Coupan not valid";

            public const string ResetShippingOrderQuentityStock = "ResetShippingOrderQuentityStock";


            public const string application_pdf = "application/pdf";
            public const string application_xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            public const string application_jpeg = "image/jpeg";
            


            public const string StoragePath = "/Storage/";
            public const string SlashStoragePath = "/Storage";
            public const string TimeSpanWithZero = "00:00:00";
            public const string Download = "Download";

            public const string StoragePath_Certificate = "/Storage/Certificate/";

            public const string GoToMyEducation = "GoToMyEducation";
            public const string UserSubscriptionsTimeComplate = "UserSubscriptionsTimeComplate";

            public const string ProductOutOfStock = "ProductOutOfStock";
            public const string ProductInStock = "ProductInStock";
            public const string ItemAlreadyInProcess = "Item(s) are already being in Process.";
            
        }

        public struct BraintreeGateway
        {
            public const string MerchantId = "MerchantId";
            public const string PublicKey = "PublicKey";
            public const string PrivateKey = "PrivateKey";
            public const string CustomerId = "CustomerId";
            public const string BrainTreeEnviorment = "BrainTreeEnviorment";
            public const string Production = "Production";

 
        }

        public struct EducationType
        {
            public const int HardCopy = 1;
            public const int Online = 2;
        }
        public struct routeedURL
        {
            public const string courseDetailPage = "/Course/CourseDetail/";
            public const string newsDetailPage = "/news/newsDetail/";
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

        public struct partialViewPaths
        {
            public const string shoppingCartItemCountPartial = "ShopingCart/_shoppingCartItemCountPartial";
        }

        public struct Controllers
        {
            public const string User = "User";
            public const string Home = "Home";
            public const string Login = "Login";
            public const string CollegeEducation = "CollegeEducation";
            public const string ShoppingCart = "ShoppingCart";
            public const string MyEducation = "MyEducation";
            public const string ExamController = "Exam";
            public const string College = "College";
            public const string CertificationProgram = "CertificationProgram";
            public const string Calendar = "Calendar";
            public const string News = "News";
            public const string Resource = "Resource";
            public const string ShippingPayment = "ShippingPayment";
            public const string UserSubscription = "UserSubscription";
            public const string EnterprisePackageRegister = "EnterprisePackageRegister";
            public const string SuggestCourse = "SuggestCourse";
            public const string StoreController = "Store";           
            
        }

        public struct Actions
        {
            public struct ResourceController
            {
                public const string FAQ = "FAQ";
            }
            public struct CertificationProgramController
            {
                public const string Index = "Index";
            }
            public struct NewsController
            {
                public const string NewsDetail = "NewsDetail";
            }
            public struct CalendarController
            {
                public const string Index = "Index";
            }
            public struct UserSubscriptionController
            {
                public const string Index = "Index";
                public const string AddEnterprisePackageRegistor = "AddEnterprisePackageRegistor";
            }

            public struct EnterprisePackageRegisterController
            {
                public const string Index = "Index";
                public const string AddEnterprisePackageRegister = "AddEnterprisePackageRegister";
            }

            public struct UserController
            {
                public const string Add = "Add";
                public const string Index = "Index";
                public const string LogOff = "LogOff";
                public const string ResetPassword = "ResetPassword";
                public const string ChangePassword = "ChangePassword";
                public const string Unauthorise = "Unauthorise";
                public const string UnauthorisePage = "UnauthorisePage";
            }
            public struct LoginController
            {
                public const string Index = "Index";
            }

            public struct HomeController
            {
                public const string Index = "Index";
            }
            public struct CollegeEducationController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }

            public struct ShoppingCartController
            {
                public const string Index = "Index";
                public const string Add = "Add";
            }
            public struct MyEducationController
            {
                public const string Index = "Index";
            }
            public struct CollegeController
            {
                public const string JoinFaculty = "JoinFaculty";
            }
            public struct SuggestCourseController
            {
                public const string Index = "Index";
                public const string AddSuggestCourse = "AddSuggestCourse";
            }

            public struct ExamController
            {
                public const string AddPreTestResult = "AddPreTestResult";
                public const string AddExamResult = "AddExamResult";
                public const string AddEvaluationResult = "AddEvaluationResult";
            }
            public struct ShippingPaymentController
            {
                public const string SaveShiipingAddress = "SaveShiipingAddress";
                public const string SaveShiipingMethod = "SaveShiipingMethod";
                public const string SaveBillingAddress = "SaveBillingAddress";
            }
            public struct StoreController
            {
                public const string ProductDetail = "ProductDetail";
            }
        }

         

        public struct Views
        {

            public struct User
            {
                public const string Index = "Index";
                public const string Login = "Login";
            }

            public struct Student
            {
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
            public const string ErrorLog = "ErrorLog";
        }
        public struct FileTypes
        {
            public struct FileTypesName
            {
                public const string TEXT = "Text";
                public const string PPT = "PPT";
                public const string Video = "Video";
                public const string ModuleUploads = "ModuleUploads";
            }
            public struct FileTypeID
            {
                public const int ModuleUploadID = 4;
            }
        }

        public struct  Variable
        {

            public static int EducationID
            {
                get;
                set;
            }

            public struct MyEdudationModuleZeroTime
            {
                public const string ZeroTime = "00:00";
                
            }

        }
        public struct Extension
        {
            public const string pdf = "pdf";
            public const string xls = "xls";
            public const string xlt = "xlt";
            public const string jpg = "jpg";
            public const string na = "na";
            public const string http = "http";
        }

        public struct CartType
        {
            public const string Course = "Course";
            public const string Product = "Product";
            public const string AllAccessPass = "AllAccessPass";
        }

        public struct ConstantChar
        {
            public const string Colon = ":";
            public const int ClientID = 0;
            public const string ForwardSlash = "/";
            public const string DoubleBackSlash = @"\";
            public const decimal ProductTaxPercentage = 8;
            public const decimal ZeroTaxPercentage = 0;
            public const decimal CourserTaxPercentage = 0;
        }

        public struct FolderName
        {
            public const string Org = "Org-";
            public const string NewsPhotos = "NewsPhotos";
            public const string ModulePDF = "ModulePDF";
            public const string ModuleVideo = "ModuleVideo";
            public const string ModulePPT = "ModulePPT";
            public const string ModuleUpload = "ModuleUploads";
            public const string CoursePDF = "CoursePDF";
            public const string ModuleImages = "ModuleImages";
            public const string Certificate = "Certificate";
            public const string LogoImage = "LogoImage";
            public const string Storage = "/Storage";
        }



        public struct Menu
        {
            public const string DefaultMenuID = "1";
        }
    }
}
