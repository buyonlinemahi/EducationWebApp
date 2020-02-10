using AutoMapper;
namespace HCRGUniversityApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            #region userMapping
            Mapper.CreateMap<NEPService.UserService.User, HCRGUniversityApp.Domain.Models.UserModel.User>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.UserModel.User, NEPService.UserService.User>();

            Mapper.CreateMap<NEPService.UserService.UserCardDetail, HCRGUniversityApp.Domain.Models.UserModel.UserCardDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.UserModel.UserCardDetail, NEPService.UserService.UserCardDetail>();

            #endregion userMapping
            #region AboutUsMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.AboutUsService.AboutUs, HCRGUniversityApp.Domain.Models.AboutUsModel.AboutUs>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.AboutUsModel.AboutUs, HCRGUniversityApp.NEPService.AboutUsService.AboutUs>();
            #endregion AboutUsMapping
            #region CollegeMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.CollegeService.College, HCRGUniversityApp.Domain.Models.CollegeModel.College>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.CollegeModel.College, HCRGUniversityApp.NEPService.CollegeService.College>();
            #endregion CollegeMapping
            #region EducationFormatMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationFormat, HCRGUniversityApp.Domain.Models.EducationFormatModel.EducationFormat>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationFormatModel.EducationFormat, HCRGUniversityApp.NEPService.EducationService.EducationFormat>();
            #endregion EducationFormatMapping
            #region EducationMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.Education, HCRGUniversityApp.Domain.Models.EducationModel.Education>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.Education, HCRGUniversityApp.NEPService.EducationService.Education>();
            #endregion EducationMapping
            #region EducationDetailMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationDetail, HCRGUniversityApp.Domain.Models.CollegeEducationModel.EducationDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.CollegeEducationModel.EducationDetail, HCRGUniversityApp.NEPService.EducationService.EducationDetail>();
            #endregion EducationFormatMapping
            #region ProfessionMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.Profession, HCRGUniversityApp.Domain.Models.ProfessionModel.Profession>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ProfessionModel.Profession, HCRGUniversityApp.NEPService.EducationService.Profession>();
            #endregion ProfessionMapping
            #region EducationShoppingTempMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingTemp, HCRGUniversityApp.Domain.Models.EducationShoppingTempModel.EducationShoppingTemp>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationShoppingTempModel.EducationShoppingTemp, HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingTemp>();
            #endregion EducationShoppingTempMapping
            #region EducationShoppingMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShopping, HCRGUniversityApp.Domain.Models.EducationShoppingModel.EducationShopping>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationShoppingModel.EducationShopping, HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShopping>();
            #endregion EducationShoppingMapping
            #region EducationShoppingCartMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingCart, HCRGUniversityApp.Domain.Models.EducationShoppingCartModel.EducationShoppingCart>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationShoppingCartModel.EducationShoppingCart, HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingCart>();
            #endregion EducationShoppingCartMapping
            #region DiscountCouponMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon, HCRGUniversityApp.Domain.Models.DiscountCouponModel.DiscountCoupon>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.DiscountCouponModel.DiscountCoupon, HCRGUniversityApp.NEPService.DiscountCouponService.DiscountCoupon>();
            #endregion DiscountCouponMapping
            #region EducationDetailPageMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationDetailPage, HCRGUniversityApp.Domain.Models.EducationModel.EducationDetailPage>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.EducationDetailPage, HCRGUniversityApp.NEPService.EducationService.EducationDetailPage>();
            #endregion EducationDetailPageMapping
            #region EducationDetailPageMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationDetailPageWithEducation, HCRGUniversityApp.Domain.Models.EducationModel.EducationDetailPageWithEducation>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.EducationDetailPageWithEducation, HCRGUniversityApp.NEPService.EducationService.EducationDetailPageWithEducation>();
            #endregion EducationDetailPageMapping
            #region EducationTypeAvailableMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationTypesAvailable, HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable, HCRGUniversityApp.NEPService.EducationService.EducationTypesAvailable>();
            #endregion EducationTypeAvailableMapping
            #region ResourceMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.Resource, HCRGUniversityApp.Domain.Models.ResourceModel.Resource>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.Resource, HCRGUniversityApp.NEPService.NewsService.Resource>();
            #endregion ResourceMapping
            #region NewsSectionMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.NewsSection, HCRGUniversityApp.Domain.Models.NewsSectionModel.NewsSection>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.NewsSectionModel.NewsSection, HCRGUniversityApp.NEPService.NewsService.NewsSection>();
            #endregion NewsSectionMapping
            #region NewsMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.News, HCRGUniversityApp.Domain.Models.NewsModel.News>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.NewsModel.News, HCRGUniversityApp.NEPService.NewsService.News>();
            #endregion NewsMapping
            #region NewsFullDetailMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.NewsFullDetail, HCRGUniversityApp.Domain.Models.NewsModel.NewsFullDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.NewsModel.NewsFullDetail, HCRGUniversityApp.NEPService.NewsService.NewsFullDetail>();
            #endregion NewsFullDetailMapping
            #region EducationCredential
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationService.EducationCredential, HCRGUniversityApp.Domain.Models.EducationModel.EducationCredential>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.EducationCredential, HCRGUniversityApp.NEPService.EducationService.EducationCredential>();
            #endregion EducationCredential
            #region EducationModuleMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationModuleService.EducationModule, HCRGUniversityApp.Domain.Models.EducationModuleModel.EducationModule>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModuleModel.EducationModule, HCRGUniversityApp.NEPService.EducationModuleService.EducationModule>();
            #endregion EducationModuleMapping
            #region MyEducationMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.MyEducationService.MyEducationDetail, HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationDetail, HCRGUniversityApp.NEPService.MyEducationService.MyEducationDetail>();
            #endregion MyEducationMapping
            #region MyEducationModuleDetailMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.MyEducationService.MyEducationModuleDetail, HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationModuleDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationModuleDetail, HCRGUniversityApp.NEPService.MyEducationService.MyEducationModuleDetail>();
            #endregion MyEducationModuleDetailMapping
            #region MyEducationModuleMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.MyEducationService.MyEducationModule, HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationModule>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationModule, HCRGUniversityApp.NEPService.MyEducationService.MyEducationModule>();
            #endregion MyEducationModuleMapping
            #region EducationModuleFileMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationModuleService.EducationModuleFile, HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile, HCRGUniversityApp.NEPService.EducationModuleService.EducationModuleFile>();

            Mapper.CreateMap<HCRGUniversityApp.NEPService.EducationModuleService.EducationModuleFile, HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile, HCRGUniversityApp.NEPService.EducationModuleService.EducationModuleFile>();
            #endregion MyEducationModuleMapping
            #region FAQMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.FAQ, HCRGUniversityApp.Domain.Models.FAQModel.FAQ>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.FAQModel.FAQ, HCRGUniversityApp.NEPService.NewsService.FAQ>();
            #endregion FAQMapping
            #region FAQCategoryMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.NewsService.FAQCategory, HCRGUniversityApp.Domain.Models.FAQModel.FAQCategory>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.FAQModel.FAQCategory, HCRGUniversityApp.NEPService.NewsService.FAQCategory>();
            #endregion FAQCategoryMapping
            #region PreTestQuestionDetailMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PreTestQuestionDetail, HCRGUniversityApp.Domain.Models.ExamModel.PreTestQuestionDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.PreTestQuestionDetail, NEPService.ExamQuestionService.PreTestQuestionDetail>();
            #endregion PreTestQuestionDetailMapping
            #region EvaluationQuestionDetailMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.EvaluationQuestionDetail, HCRGUniversityApp.Domain.Models.ExamModel.EvaluationQuestionDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EvaluationQuestionDetail, NEPService.ExamQuestionService.EvaluationQuestionDetail>();
            #endregion EvaluationQuestionDetailMapping
            #region ExamQuestionDetailMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.ExamQuestionDetail, HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionDetail, NEPService.ExamQuestionService.ExamQuestionDetail>();
            #endregion ExamQuestionDetailMapping
            #region PreTestResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.PreTestResult, HCRGUniversityApp.Domain.Models.ExamModel.PreTestResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.PreTestResult, NEPService.ExamInternalService.PreTestResult>();
            #endregion PreTestResultMapping
            #region PreTestQuestionResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.PreTestQuestionResult, HCRGUniversityApp.Domain.Models.ExamModel.PreTestQuestionResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.PreTestQuestionResult, NEPService.ExamInternalService.PreTestQuestionResult>();
            #endregion PreTestQuestionResultMapping
            #region ExamResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.ExamResult, HCRGUniversityApp.Domain.Models.ExamModel.ExamResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.ExamResult, NEPService.ExamInternalService.ExamResult>();
            #endregion ExamResultMapping
            #region ExamQuestionResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.ExamQuestionResult, HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionResult, NEPService.ExamInternalService.ExamQuestionResult>();
            #endregion ExamQuestionResultMapping
            #region EvaluationResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.EvaluationResult, HCRGUniversityApp.Domain.Models.ExamModel.EvaluationResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EvaluationResult, NEPService.ExamInternalService.EvaluationResult>();
            #endregion EvaluationResultMapping
            #region EvaluationQuestionResultMapping
            Mapper.CreateMap<NEPService.ExamInternalService.EvaluationQuestionResult, HCRGUniversityApp.Domain.Models.ExamModel.EvaluationQuestionResult>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EvaluationQuestionResult, NEPService.ExamInternalService.EvaluationQuestionResult>();
            #endregion EvaluationQuestionResultMapping
            #region EvaluationAnserMapping
            Mapper.CreateMap<NEPService.ExamInternalService.EvaluationAnswer, HCRGUniversityApp.Domain.Models.ExamModel.EvaluationAnswer>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EvaluationAnswer, NEPService.ExamInternalService.EvaluationAnswer>();
            #endregion EvaluationAnserMapping
            #region CarouselMapping
            Mapper.CreateMap<NEPService.NewsService.CarouselSetUp, HCRGUniversityApp.Domain.Models.Carousel.CarouselDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Carousel.CarouselDetail, NEPService.NewsService.CarouselSetUp>();
            #endregion CarouselMapping
            #region FacultyMapping
            Mapper.CreateMap<HCRGUniversityApp.NEPService.CollegeService.Faculty, HCRGUniversityApp.Domain.Models.FacultyModel.Faculty>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.FacultyModel.Faculty, HCRGUniversityApp.NEPService.CollegeService.Faculty>();
            #endregion FacultyMapping
            #region HomeContent
            Mapper.CreateMap<NEPService.NewsService.HomeContent, HCRGUniversityApp.Domain.Models.HomeContentModel.HomeContent>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.HomeContentModel.HomeContent, NEPService.NewsService.HomeContent>();
            #endregion HomeContent
            #region EducationPreTestQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationPreTestQuestion, HCRGUniversityApp.Domain.Models.ExamModel.EducationPreTestQuestion>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EducationPreTestQuestion, NEPService.ExamQuestionService.EducationPreTestQuestion>();
            #endregion EducationPreTestQuestionMpping
            #region EducationExamQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationExamQuestion, HCRGUniversityApp.Domain.Models.ExamModel.EducationExamQuestion>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EducationExamQuestion, NEPService.ExamQuestionService.EducationExamQuestion>();
            #endregion EducationExamQuestionMpping
            #region EducationModuleFileMpping
            Mapper.CreateMap<NEPService.EducationModuleService.EducationModuleFile, HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile>(); 
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModuleFile.EducationModuleFile , NEPService.EducationModuleService.EducationModuleFile>();
            #endregion EducationExamQuestionMpping
            #region EducationEvaluationQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationEvaluation, HCRGUniversityApp.Domain.Models.ExamModel.EducationEvaluation>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.EducationEvaluation, NEPService.ExamQuestionService.EducationEvaluation>();
            #endregion EducationEvaluationQuestionMpping
            #region ExamQuestion
            Mapper.CreateMap<NEPService.ExamQuestionService.ExamQuestion, HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ExamModel.ExamQuestionDetail, NEPService.ExamQuestionService.ExamQuestion>();
            #endregion ExamQuestion
            #region CertificateDetail
            Mapper.CreateMap<NEPService.MyEducationService.CertificateDetail, HCRGUniversityApp.Domain.Models.CertificateDetail.CertificateDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.CertificateDetail.CertificateDetail, NEPService.MyEducationService.CertificateDetail>();
            #endregion ExamQuestion
            #region IndustryResearchContent
            Mapper.CreateMap<NEPService.NewsService.IndustryResearch, HCRGUniversityApp.Domain.Models.IndustryResearchModel.IndustryResearch>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.IndustryResearchModel.IndustryResearch, NEPService.NewsService.IndustryResearch>();
            #endregion IndustryResearchContent
            #region EventDetail
            Mapper.CreateMap<NEPService.NewsService.Event, HCRGUniversityApp.Domain.Models.Event.EventDetail>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Event.EventDetail, NEPService.NewsService.Event>();
            #endregion EventDetail
            #region MyEducationAccountHistory
            Mapper.CreateMap<HCRGUniversityApp.NEPService.MyEducationService.MyEducationAccountHistory, HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationAccountHistory>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.MyEducationDetailModel.MyEducationAccountHistory, HCRGUniversityApp.NEPService.MyEducationService.MyEducationAccountHistory>();
            #endregion MyEducationAccountHistory
            #region EducationNews
            Mapper.CreateMap<NEPService.EducationService.EducationNewsSearch, HCRGUniversityApp.Domain.Models.EducationModel.EducationNewsSearch>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationModel.EducationNewsSearch, NEPService.EducationService.EducationNewsSearch>();
            Mapper.CreateMap<NEPService.EducationService.PagedEducationNewsSearch, HCRGUniversityApp.Domain.Models.Education.PagedEducationNewsSearch>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Education.PagedEducationNewsSearch, NEPService.EducationService.PagedEducationNewsSearch>();
            #endregion EducationNews
            #region LogSession
            Mapper.CreateMap<NEPService.LogSessionService.LogSession, HCRGUniversityApp.Domain.Models.LogSessionModel.LogSession>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.LogSessionModel.LogSession, NEPService.LogSessionService.LogSession>();
            #endregion LogSession
            #region Accreditation
            Mapper.CreateMap<NEPService.NewsService.Accreditation, HCRGUniversityApp.Domain.Models.ResourceModel.Accreditation>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.Accreditation, NEPService.NewsService.Accreditation>();
            #endregion Accreditation
            #region Certification
            Mapper.CreateMap<NEPService.NewsService.Certification, HCRGUniversityApp.Domain.Models.ResourceModel.Certification>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.Certification, NEPService.NewsService.Certification>();
            #endregion Certification
            #region Training And Seminar
            Mapper.CreateMap<NEPService.NewsService.TrainingAndSeminar, HCRGUniversityApp.Domain.Models.ResourceModel.TrainingAndSeminar>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.TrainingAndSeminar, NEPService.NewsService.TrainingAndSeminar>();
            #endregion Accreditation
            #region Privacy,Policy,Legal 
            Mapper.CreateMap<NEPService.NewsService.PrivacyPolicy, HCRGUniversityApp.Domain.Models.ResourceModel.PrivacyPolicy>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.PrivacyPolicy, NEPService.NewsService.PrivacyPolicy>();

            Mapper.CreateMap<NEPService.NewsService.TermsCondition, HCRGUniversityApp.Domain.Models.ResourceModel.TermsCondition>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.TermsCondition, NEPService.NewsService.TermsCondition>();

            #endregion
            #region NewsLetter
            Mapper.CreateMap<NEPService.NewsService.NewsLetter, HCRGUniversityApp.Domain.Models.ResourceModel.NewsLetter>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ResourceModel.NewsLetter, NEPService.NewsService.NewsLetter>();
            #endregion NewsLetter
            #region Product
            Mapper.CreateMap<NEPService.ProductService.Product, HCRGUniversityApp.Domain.Models.ProductModel.Product>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ProductModel.Product, NEPService.ProductService.Product>();

            Mapper.CreateMap<NEPService.ProductService.PagedProduct, HCRGUniversityApp.Domain.Models.ProductModel.PagedProduct>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ProductModel.PagedProduct, NEPService.ProductService.PagedProduct>();

            Mapper.CreateMap<NEPService.ShoppingEducationService.ProductShopping, HCRGUniversityApp.Domain.Models.EducationShoppingModel.ProductShopping>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationShoppingModel.ProductShopping, NEPService.ShoppingEducationService.ProductShopping>();

            Mapper.CreateMap<NEPService.ShoppingEducationService.ProductShoppingTemp, HCRGUniversityApp.Domain.Models.EducationShoppingTempModel.ProductShoppingTemp>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EducationShoppingTempModel.ProductShoppingTemp, NEPService.ShoppingEducationService.ProductShoppingTemp>();

            Mapper.CreateMap<NEPService.ProductService.ProductPurchase, HCRGUniversityApp.Domain.Models.Product.ProductPurchase>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Product.ProductPurchase, NEPService.ProductService.ProductPurchase>();

            Mapper.CreateMap<NEPService.ProductService.PagedProductPurchase, HCRGUniversityApp.Domain.Models.Product.PagedProductPurchase>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Product.PagedProductPurchase, NEPService.ProductService.PagedProductPurchase>();
            #endregion Product
            #region ProductQuantity
            Mapper.CreateMap<NEPService.ProductService.ProductQuantity, HCRGUniversityApp.Domain.Models.Product.ProductQuantity>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Product.ProductQuantity, NEPService.ProductService.ProductQuantity>();

            #endregion Product
            #region Shipping Payment
            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShippingPaymentService.BillingAddress, HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress, HCRGUniversityApp.NEPService.ShippingPaymentService.BillingAddress>();

            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress, HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress, HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>();

            Mapper.CreateMap<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment, HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment, HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>();

            
            #endregion
            #region SuggestCourse
            Mapper.CreateMap<HCRGUniversityApp.NEPService.CommonService.SuggestCourse , HCRGUniversityApp.Domain.Models.SuggestCourse.SuggestCourse>();            
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.SuggestCourse.SuggestCourse, HCRGUniversityApp.NEPService.CommonService.SuggestCourse>();

            Mapper.CreateMap<HCRGUniversityApp.NEPService.CommonService.Menu, HCRGUniversityApp.Domain.Models.Common.Menu>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Common.Menu, HCRGUniversityApp.NEPService.CommonService.Menu>();

            Mapper.CreateMap<HCRGUniversityApp.NEPService.ClientService.Menu, HCRGUniversityApp.Domain.Models.Common.Menu>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Common.Menu, HCRGUniversityApp.NEPService.ClientService.Menu>();

            #endregion
            #region Common
            Mapper.CreateMap<NEPService.CommonService.State, HCRGUniversityApp.Domain.Models.Common.State>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Common.State, NEPService.CommonService.State>();

            Mapper.CreateMap<NEPService.CommonService.ShippingMethod, HCRGUniversityApp.Domain.Models.Common.ShippingMethod>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.Common.ShippingMethod, NEPService.CommonService.ShippingMethod>();

            #endregion
            #region  Certification Title Option Service

            Mapper.CreateMap<NEPService.CertificationTitleOptionService.CertificationTitleOption , HCRGUniversityApp.Domain.Models.CertificationTitleOption.CertificationTitleOption>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.CertificationTitleOption.CertificationTitleOption, NEPService.CertificationTitleOptionService.CertificationTitleOption>();

            Mapper.CreateMap<NEPService.CertificationTitleOptionService.PagedCertificationTitleOption, HCRGUniversityApp.Domain.ViewModels.CertificationTitleOptionViewModel.CertificationTitleOptionViewModel>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.ViewModels.CertificationTitleOptionViewModel.CertificationTitleOptionViewModel, NEPService.CertificationTitleOptionService.PagedCertificationTitleOption>();

            #endregion
            #region UserSubscriptionMapping

            Mapper.CreateMap<NEPService.UserService.UserSubscription, HCRGUniversityApp.Domain.Models.UserModel.UserSubscription>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.UserModel.UserSubscription, NEPService.UserService.UserSubscription>();

            #endregion UserSubscriptionMapping
            #region EnterprisePackageRegister

            Mapper.CreateMap<NEPService.UserService.EnterprisePackageRegister, HCRGUniversityApp.Domain.Models.EnterprisePackageRegisterModel.EnterprisePackageRegister>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.EnterprisePackageRegisterModel.EnterprisePackageRegister, NEPService.UserService.EnterprisePackageRegister>();

            #endregion EnterprisePackageRegister
            #region UserAllAccessPass

            Mapper.CreateMap<NEPService.UserService.UserAllAccessPass, HCRGUniversityApp.Domain.Models.UserModel.UserAllAccessPass>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.UserModel.UserAllAccessPass, NEPService.UserService.UserAllAccessPass>();
            #endregion EnterprisePackageRegister
            #region Organization
            Mapper.CreateMap<NEPService.ClientService.Organization, HCRGUniversityApp.Domain.Models.OrganizationModel.Organization>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.OrganizationModel.Organization, NEPService.ClientService.Organization>();


            Mapper.CreateMap<NEPService.ClientService.OrganizationContact, HCRGUniversityApp.Domain.Models.OrganizationModel.OrganizationContact>();
            Mapper.CreateMap<HCRGUniversityApp.Domain.Models.OrganizationModel.OrganizationContact, NEPService.ClientService.OrganizationContact>();
            #endregion Organization
        }
    }
}