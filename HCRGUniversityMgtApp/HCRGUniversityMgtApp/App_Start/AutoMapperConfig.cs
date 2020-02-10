using AutoMapper;
namespace HCRGUniversityMgtApp
{
    public class AutoMapperConfig
    {
        public static void RegisterMapping()
        {
            #region AboutUsMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.AboutUsService.AboutUs, HCRGUniversityMgtApp.Domain.Models.AboutUsModel.AboutUs>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.AboutUsModel.AboutUs, HCRGUniversityMgtApp.NEPService.AboutUsService.AboutUs>();
            #endregion AboutUsMapping

            #region AboutUsPagedMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.AboutUsService.PagedAboutUs, HCRGUniversityMgtApp.Domain.Models.AboutUsModel.PagedAboutUs>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.AboutUsModel.PagedAboutUs, HCRGUniversityMgtApp.NEPService.AboutUsService.PagedAboutUs>();
            #endregion AboutUsPagedMapping

            #region CarouselSetup
            Mapper.CreateMap<NEPService.NewsService.CarouselSetUp, HCRGUniversityMgtApp.Domain.Models.CarouselSetUp.CarouselSetUp>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CarouselSetUp.CarouselSetUp, NEPService.NewsService.CarouselSetUp>();

            #endregion CarouselSetup

            #region CollegeMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CollegeService.College, HCRGUniversityMgtApp.Domain.Models.CollegeModel.College>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CollegeModel.College, HCRGUniversityMgtApp.NEPService.CollegeService.College>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CollegeService.PagedFaculty, HCRGUniversityMgtApp.Domain.Models.Faculty.PagedFaculty>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Faculty.PagedFaculty, HCRGUniversityMgtApp.NEPService.CollegeService.PagedFaculty>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CollegeService.Faculty, HCRGUniversityMgtApp.Domain.Models.Faculty.Faculty>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Faculty.Faculty, HCRGUniversityMgtApp.NEPService.CollegeService.Faculty>();
            #endregion CollegeMapping

            #region EducationMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.Education, HCRGUniversityMgtApp.Domain.Models.EducationModel.Education>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.Education, HCRGUniversityMgtApp.NEPService.EducationService.Education>(); ;
            #endregion EducationMapping

            #region ProfessionMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.Profession, HCRGUniversityMgtApp.Domain.Models.ProfessionModel.Profession>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProfessionModel.Profession, HCRGUniversityMgtApp.NEPService.EducationService.Profession>(); ;
            #endregion ProfessionMapping

            #region ProfessionPagedMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.PagedProfession, HCRGUniversityMgtApp.Domain.Models.ProfessionModel.PagedProfession>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProfessionModel.PagedProfession, HCRGUniversityMgtApp.NEPService.EducationService.PagedProfession>(); ;
            #endregion ProfessionPagedMapping

            #region EducationFormatMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormat, HCRGUniversityMgtApp.Domain.Models.EducationFormatModel.EducationFormat>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationFormatModel.EducationFormat, HCRGUniversityMgtApp.NEPService.EducationService.EducationFormat>(); ;
            #endregion EducationFormatMapping

            #region EducationFormatDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatDetail, HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel.EducationFormatDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel.EducationFormatDetail, HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel.EducationFormatDetail, HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>();
            #endregion

            #region CollegeEducationSearchMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducationSearch, HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel.CollegeEducationSearch>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel.CollegeEducationSearch, HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducationSearch>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducationSearch, HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel.CollegeEducationSearch>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel.CollegeEducationSearch, HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>();
            #endregion CollegeEducationSearchMapping

            #region CollegeEducationMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation, HCRGUniversityMgtApp.Domain.Models.CollegeEducation.CollegeEducation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CollegeEducation.CollegeEducation, HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>(); ;
            #endregion CollegeEducationMapping

            #region CollegeEducationDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation, HCRGUniversityMgtApp.Domain.Models.EducationDetailModel.EducationDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationDetailModel.EducationDetail, HCRGUniversityMgtApp.NEPService.EducationService.CollegeEducation>(); ;
            #endregion CollegeEducationMapping

            #region EducationDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationDetail, HCRGUniversityMgtApp.Domain.Models.EducationDetailModel.EducationDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationDetailModel.EducationDetail, HCRGUniversityMgtApp.NEPService.EducationService.EducationDetail>(); ;
            #endregion EducationDetailMapping

            #region EducationProfessionMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationProfession, HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel.EducationProfession>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel.EducationProfession, HCRGUniversityMgtApp.NEPService.EducationService.EducationProfession>(); ;
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationProfessionDetail, HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel.EducationProfession>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationProfessionModel.EducationProfession, HCRGUniversityMgtApp.NEPService.EducationService.EducationProfessionDetail>(); ;
            #endregion EducationProfessionMapping

            #region EducationProfessionDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationProfessionDetail, HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel.EducationProfessionDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel.EducationProfessionDetail, HCRGUniversityMgtApp.NEPService.EducationService.EducationProfessionDetail>(); ;
            #endregion EducationProfessionDetailMapping

            #region EducationProfessionDetailPagedMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.PagedEducationProfession, HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel.PagedEducationProfession>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel.PagedEducationProfession, HCRGUniversityMgtApp.NEPService.EducationService.PagedEducationProfession>(); ;
            #endregion EducationProfessionDetailPagedMapping

            #region EducationTypesAvailableMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationTypesAvailable, HCRGUniversityMgtApp.Domain.Models.EducationTypesAvailableModel.EducationTypesAvailable>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationTypesAvailableModel.EducationTypesAvailable, HCRGUniversityMgtApp.NEPService.EducationService.EducationTypesAvailable>(); ;
            #endregion EducationProfessionDetail1Mapping

            #region DiscountCouponMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.DiscountCouponService.DiscountCoupon, HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel.DiscountCoupon>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel.DiscountCoupon, HCRGUniversityMgtApp.NEPService.DiscountCouponService.DiscountCoupon>();
            #endregion DiscountCouponMapping

            #region DiscountCouponForCoursesMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.DiscountCouponService.DiscountCouponForCourses, HCRGUniversityMgtApp.Domain.Models.DiscountCouponForCoursesModel.DiscountCouponForCourses>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.DiscountCouponForCoursesModel.DiscountCouponForCourses, HCRGUniversityMgtApp.NEPService.DiscountCouponService.DiscountCouponForCourses>();
            #endregion DiscountCouponMapping

            #region PagedDiscountCouponMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.DiscountCouponService.PagedDiscountCoupon, HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel.PagedDiscountCoupon>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel.PagedDiscountCoupon, HCRGUniversityMgtApp.NEPService.DiscountCouponService.PagedDiscountCoupon>();
            #endregion PagedDiscountCouponMapping

            #region EducationFormatDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatDetail, HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel.EducationFormatDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel.EducationFormatDetail, HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatDetail>(); ;
            #endregion EducationFormatDetailMapping

            #region EducationFormatDetailMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable, HCRGUniversityMgtApp.Domain.Models.EducationFormatAvailableModel.EducationFormatAvailable>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationFormatAvailableModel.EducationFormatAvailable, HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>(); ;
            #endregion EducationFormatDetailMapping

            #region userMapping
            Mapper.CreateMap<NEPService.UserService.User, HCRGUniversityMgtApp.Domain.Models.UserModel.User>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.UserModel.User, NEPService.UserService.User>();

            Mapper.CreateMap<NEPService.UserService.ClientAndUserbySearchCriterias, HCRGUniversityMgtApp.Domain.Models.UserModel.ClientAndUserbySearchCriterias>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.UserModel.ClientAndUserbySearchCriterias, NEPService.UserService.ClientAndUserbySearchCriterias>();
            #endregion userMapping

            #region userPagedMapping
            Mapper.CreateMap<NEPService.UserService.PagedUser, HCRGUniversityMgtApp.Domain.Models.UserModel.PagedUser>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.UserModel.PagedUser, NEPService.UserService.PagedUser>();
            #endregion userPagedMapping

            #region UserSubscriptionMapping

            Mapper.CreateMap<NEPService.UserService.UserSubscription, HCRGUniversityMgtApp.Domain.Models.UserModel.UserSubscription>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.UserModel.UserSubscription, NEPService.UserService.UserSubscription>();

            #endregion UserSubscriptionMapping

            #region newsSectionMapping
            Mapper.CreateMap<NEPService.NewsService.NewsSection, HCRGUniversityMgtApp.Domain.Models.NewsSectionModel.NewsSection>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsSectionModel.NewsSection, NEPService.NewsService.NewsSection>();
            #endregion newsSectionMapping

            #region newsMapping
            Mapper.CreateMap<NEPService.NewsService.News, HCRGUniversityMgtApp.Domain.Models.NewsModel.News>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsModel.News, NEPService.NewsService.News>();
            Mapper.CreateMap<NEPService.NewsService.NewsSearchCarousel, HCRGUniversityMgtApp.Domain.Models.NewsModel.NewsSearchCarousel>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsModel.NewsSearchCarousel, NEPService.NewsService.NewsSearchCarousel>();
            #endregion newsMapping

            #region newsPagedMapping
            Mapper.CreateMap<NEPService.NewsService.PagedNews, HCRGUniversityMgtApp.Domain.Models.NewsModel.PagedNews>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsModel.PagedNews, NEPService.NewsService.PagedNews>();
            #endregion newsPagedMapping

            #region newsVideoMapping
            Mapper.CreateMap<NEPService.NewsService.NewsVideo, HCRGUniversityMgtApp.Domain.Models.NewsVideoModel.NewsVideo>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsVideoModel.NewsVideo, NEPService.NewsService.NewsVideo>();
            #endregion newsVideoMapping

            #region newsPhotoMapping
            Mapper.CreateMap<NEPService.NewsService.NewsPhoto, HCRGUniversityMgtApp.Domain.Models.NewsPhotoModel.NewsPhoto>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsPhotoModel.NewsPhoto, NEPService.NewsService.NewsPhoto>();
            #endregion newsPhotoMapping

            #region EducationModuleMapping
            Mapper.CreateMap<NEPService.EducationModuleService.EducationModule, HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationModule>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationModule, NEPService.EducationModuleService.EducationModule>();
            #endregion EducationModuleMapping

            #region PagedEducationModuleMapping
            Mapper.CreateMap<NEPService.EducationModuleService.PagedEducationModule, HCRGUniversityMgtApp.Domain.Models.EducationModel.PagedEducationModule>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.PagedEducationModule, NEPService.EducationModuleService.PagedEducationModule>();
            #endregion PagedEducationModuleMapping

            #region EducationSearchResultMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationSearchResult, HCRGUniversityMgtApp.Domain.Models.Education.EducationSearchResult>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Education.EducationSearchResult, HCRGUniversityMgtApp.NEPService.EducationService.EducationSearchResult>();
            #endregion EducationSearchResultMapping

            #region EducationPagedMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.PagedEducation, HCRGUniversityMgtApp.Domain.Models.EducationModel.PagedEducation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.PagedEducation, HCRGUniversityMgtApp.NEPService.EducationService.PagedEducation>();
            #endregion EducationPagedMapping

            #region EducationModuleDetailMapping
            Mapper.CreateMap<NEPService.EducationModuleService.EducationModuleFile, HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationModuleFile>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationModuleFile, NEPService.EducationModuleService.EducationModuleFile>();
            #endregion EducationModuleDetailMapping

            #region EducationDetailPageMapping
            Mapper.CreateMap<NEPService.EducationService.EducationDetailPage, HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationDetailPage>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationDetailPage, NEPService.EducationService.EducationDetailPage>();
            #endregion EducationDetailPageMapping

            #region EducationDetailPageMapping
            Mapper.CreateMap<NEPService.EducationService.EducationDetailPageWithEducation, HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationDetailPageWithEducation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationModel.EducationDetailPageWithEducation, NEPService.EducationService.EducationDetailPageWithEducation>();
            #endregion EducationDetailPageMapping

            #region FAQMapping
            Mapper.CreateMap<NEPService.NewsService.FAQ, HCRGUniversityMgtApp.Domain.Models.FAQModel.FAQ>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.FAQModel.FAQ, NEPService.NewsService.FAQ>();
            #endregion FAQMapping

            #region PagedFAQMapping
            //Mapper.CreateMap<NEPService.NewsService.PagedFAQ, HCRGUniversityMgtApp.Domain.Models.FAQModel.PagedFAQ>();
            //Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.FAQModel.PagedFAQ, NEPService.NewsService.PagedFAQ>();
            #endregion PagedFAQMapping

            #region FAQCategoryMapping
            Mapper.CreateMap<NEPService.NewsService.FAQCategory, HCRGUniversityMgtApp.Domain.Models.FAQModel.FAQCategory>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.FAQModel.FAQCategory, NEPService.NewsService.FAQCategory>();
            #endregion FAQCategoryMapping

            #region PreTestQuestionMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PreTestQuestion, HCRGUniversityMgtApp.Domain.Models.PreTestModel.PreTestQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PreTestModel.PreTestQuestion, NEPService.ExamQuestionService.PreTestQuestion>();
            #endregion PreTestQuestionMapping

            #region PreTestMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PreTest, HCRGUniversityMgtApp.Domain.Models.PreTestModel.PreTest>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PreTestModel.PreTest, NEPService.ExamQuestionService.PreTest>();
            #endregion PreTestMapping

            #region PagedPreTestQuestionPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedPreTestQuestion, HCRGUniversityMgtApp.Domain.Models.PreTestModel.PagedPreTestQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PreTestModel.PagedPreTestQuestion, NEPService.ExamQuestionService.PagedPreTestQuestion>();
            #endregion PagedPreTestQuestionPagedMapping

            #region PreTestPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedPreTest, HCRGUniversityMgtApp.Domain.Models.PreTestModel.PagedPreTest>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PreTestModel.PagedPreTest, NEPService.ExamQuestionService.PagedPreTest>();
            #endregion PreTestPagedMapping

            #region ExamQuestionMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.ExamQuestion, HCRGUniversityMgtApp.Domain.Models.ExamModel.ExamQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ExamModel.ExamQuestion, NEPService.ExamQuestionService.ExamQuestion>();
            #endregion ExamQuestionMapping

            #region ExamMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.Exam, HCRGUniversityMgtApp.Domain.Models.ExamModel.Exam>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ExamModel.Exam, NEPService.ExamQuestionService.Exam>();
            #endregion ExamMapping

            #region PagedExamQuestionPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedExamQuestion, HCRGUniversityMgtApp.Domain.Models.ExamModel.PagedExamQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ExamModel.PagedExamQuestion, NEPService.ExamQuestionService.PagedExamQuestion>();
            #endregion PagedExamQuestionPagedMapping

            #region ExamPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedExam, HCRGUniversityMgtApp.Domain.Models.ExamModel.PagedExam>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ExamModel.PagedExam, NEPService.ExamQuestionService.PagedExam>();
            #endregion ExamPagedMapping

            #region EvaluationMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.Evaluation, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.Evaluation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.Evaluation, NEPService.ExamQuestionService.Evaluation>();
            #endregion EvaluationMapping

            #region PagedEvaluationQuestionPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedEvaluationQuestion, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.PagedEvaluationQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.PagedEvaluationQuestion, NEPService.ExamQuestionService.PagedEvaluationQuestion>();
            #endregion PagedEvaluationQuestionPagedMapping

            #region EvaluationPagedMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.PagedEvaluation, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.PagedEvaluation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.PagedEvaluation, NEPService.ExamQuestionService.PagedEvaluation>();
            #endregion EvaluationPagedMapping

            #region EvaluationMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.Evaluation, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.Evaluation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.Evaluation, NEPService.ExamQuestionService.Evaluation>();
            #endregion EvaluationMapping

            #region EvaluationQuestionMapping
            Mapper.CreateMap<NEPService.ExamQuestionService.EvaluationQuestion, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.EvaluationQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.EvaluationQuestion, NEPService.ExamQuestionService.EvaluationQuestion>();
            #endregion EvaluationQuestionMapping

            #region EducationPreTestQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationPreTestQuestion, HCRGUniversityMgtApp.Domain.Models.PreTestModel.EducationPreTestQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PreTestModel.EducationPreTestQuestion, NEPService.ExamQuestionService.EducationPreTestQuestion>();
            #endregion EducationPreTestQuestionMpping

            #region EducationExamQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationExamQuestion, HCRGUniversityMgtApp.Domain.Models.ExamModel.EducationExamQuestion>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ExamModel.EducationExamQuestion, NEPService.ExamQuestionService.EducationExamQuestion>();
            #endregion EducationExamQuestionMpping

            #region EducationExamQuestionMpping
            Mapper.CreateMap<NEPService.ExamQuestionService.EducationEvaluation, HCRGUniversityMgtApp.Domain.Models.EvaluationModel.EducationEvaluation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EvaluationModel.EducationEvaluation, NEPService.ExamQuestionService.EducationEvaluation>();
            #endregion EducationExamQuestionMpping

            #region HomeContent
            Mapper.CreateMap<NEPService.NewsService.HomeContent, HCRGUniversityMgtApp.Domain.Models.HomeContentModel.HomeContent>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.HomeContentModel.HomeContent, NEPService.NewsService.HomeContent>();
            #endregion HomeContent

            #region HomeContent
            Mapper.CreateMap<NEPService.NewsService.TrainingAndSeminar, HCRGUniversityMgtApp.Domain.Models.TrainingAndSeminar.TrainingAndSeminar>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.TrainingAndSeminar.TrainingAndSeminar, NEPService.NewsService.TrainingAndSeminar>();

            Mapper.CreateMap<NEPService.NewsService.TrainingAndSeminar, HCRGUniversityMgtApp.Domain.ViewModels.TrainingAndSeminarViewModel.TrainingSeminarViewModel>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.ViewModels.TrainingAndSeminarViewModel.TrainingSeminarViewModel, NEPService.NewsService.TrainingAndSeminar>();
            #endregion HomeContent

            #region IndustryResearchContent
            Mapper.CreateMap<NEPService.NewsService.IndustryResearch, HCRGUniversityMgtApp.Domain.Models.IndustryResearchModel.IndustryResearch>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.IndustryResearchModel.IndustryResearch, NEPService.NewsService.IndustryResearch>();
            #endregion IndustryResearchContent

            #region PrivacyPolicy
            Mapper.CreateMap<NEPService.NewsService.PrivacyPolicy, HCRGUniversityMgtApp.Domain.Models.Privacy.PrivacyPolicy>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Privacy.PrivacyPolicy, NEPService.NewsService.PrivacyPolicy>();
            #endregion PrivacyPolicy

            #region TermsCondition
            Mapper.CreateMap<NEPService.NewsService.TermsCondition, HCRGUniversityMgtApp.Domain.Models.TermsCondition.TermsCondition>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.TermsCondition.TermsCondition, NEPService.NewsService.TermsCondition>();
            #endregion TermsCondition

            #region Return Policy
            Mapper.CreateMap<NEPService.NewsService.ReturnPolicy, HCRGUniversityMgtApp.Domain.Models.ReturnPolicy.ReturnPolicy>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ReturnPolicy.ReturnPolicy, NEPService.NewsService.ReturnPolicy>();
            #endregion Return Policy

            #region Delivery Term
            Mapper.CreateMap<NEPService.NewsService.DeliveryTerm, HCRGUniversityMgtApp.Domain.Models.DeliveryTerm.DeliveryTerm>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.DeliveryTerm.DeliveryTerm, NEPService.NewsService.DeliveryTerm>();
            #endregion Delivery Term

            #region AccreditorMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.Accreditor, HCRGUniversityMgtApp.Domain.Models.AccreditorModel.Accreditor>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.AccreditorModel.Accreditor, HCRGUniversityMgtApp.NEPService.EducationService.Accreditor>();
            #endregion AccreditorMapping

            #region AccreditorPagedMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.PagedAccreditor, HCRGUniversityMgtApp.Domain.Models.AccreditorModel.PagedAccreditor>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.AccreditorModel.PagedAccreditor, HCRGUniversityMgtApp.NEPService.EducationService.PagedAccreditor>();
            #endregion AccreditorPagedMapping

            #region EducationCredentialMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.EducationService.EducationCredential, HCRGUniversityMgtApp.Domain.Models.EducationCredential.EducationCredential>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EducationCredential.EducationCredential, HCRGUniversityMgtApp.NEPService.EducationService.EducationCredential>();
            #endregion AccreditorMapping

            #region EventMapping
            Mapper.CreateMap<NEPService.NewsService.Event, HCRGUniversityMgtApp.Domain.Models.EventModel.Event>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EventModel.Event, NEPService.NewsService.Event>();
            #endregion EventMapping

            #region PagedEventMapping
            Mapper.CreateMap<NEPService.NewsService.PagedEvents, HCRGUniversityMgtApp.Domain.Models.EventModel.PagedEvents>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EventModel.PagedEvents, NEPService.NewsService.PagedEvents>();
            #endregion PagedEventMapping

            #region PagedLogSessionDetailMapping
            Mapper.CreateMap<NEPService.LogSessionService.LogSessionDetail, HCRGUniversityMgtApp.Domain.Models.LogSessionModel.LogSessionDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.LogSessionModel.LogSessionDetail, NEPService.LogSessionService.LogSessionDetail>();
            Mapper.CreateMap<NEPService.LogSessionService.PagedLogSessionDetail, HCRGUniversityMgtApp.Domain.Models.LogSessionModel.PagedLogSessionDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.LogSessionModel.PagedLogSessionDetail, NEPService.LogSessionService.PagedLogSessionDetail>();
            #endregion PagedLogSessionDetailMapping

            #region Accreditation
            Mapper.CreateMap<NEPService.NewsService.Accreditation, HCRGUniversityMgtApp.Domain.Models.AccreditationModel.Accreditation>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.AccreditationModel.Accreditation, NEPService.NewsService.Accreditation>();
            #endregion Accreditation

            #region Certification
            Mapper.CreateMap<NEPService.NewsService.Certification, HCRGUniversityMgtApp.Domain.Models.Certification.Certification>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Certification.Certification, NEPService.NewsService.Certification>();
            #endregion Certification

            #region Product
            Mapper.CreateMap<NEPService.ProductService.Product, HCRGUniversityMgtApp.Domain.Models.ProductModel.Product>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.Product, NEPService.ProductService.Product>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.PagedProduct, HCRGUniversityMgtApp.Domain.Models.ProductModel.PagedProduct>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.PagedProduct, HCRGUniversityMgtApp.NEPService.ProductService.PagedProduct>();

            Mapper.CreateMap<NEPService.ProductService.ProductPurchase, HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductPurchase>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductPurchase, NEPService.ProductService.ProductPurchase>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.PagedProductPurchasesRecord, HCRGUniversityMgtApp.Domain.Models.Product.PagedProductPurchasesRecord>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Product.PagedProductPurchasesRecord, HCRGUniversityMgtApp.NEPService.ProductService.PagedProductPurchasesRecord>();

            Mapper.CreateMap<NEPService.ProductService.ProductPurchase, HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductPurchase>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductPurchase, NEPService.ProductService.ProductPurchase>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.PagedProductPurchase, HCRGUniversityMgtApp.Domain.Models.ProductModel.PagedProductPurchase>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.PagedProductPurchase, HCRGUniversityMgtApp.NEPService.ProductService.PagedProductPurchase>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.ProductShippingAddressDetailByID, HCRGUniversityMgtApp.Domain.Models.Product.ProductShippingAddressDetailByID>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Product.ProductShippingAddressDetailByID, HCRGUniversityMgtApp.NEPService.ProductService.ProductShippingAddressDetailByID>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.ProductShippingDetail, HCRGUniversityMgtApp.Domain.Models.Product.ProductShippingDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Product.ProductShippingDetail, HCRGUniversityMgtApp.NEPService.ProductService.ProductShippingDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ProductService.PagedProductShippingDetail, HCRGUniversityMgtApp.Domain.Models.Product.PagedProductShippingDetail>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Product.PagedProductShippingDetail, HCRGUniversityMgtApp.NEPService.ProductService.PagedProductShippingDetail>();

            #endregion Product

            #region ProductQuantity
            Mapper.CreateMap<NEPService.ProductService.ProductQuantity, HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductQuantity>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.ProductModel.ProductQuantity, NEPService.ProductService.ProductQuantity>();
            #endregion Product

            #region NewsLetter
            Mapper.CreateMap<NEPService.NewsService.NewsLetter, HCRGUniversityMgtApp.Domain.Models.NewsLetterModel.NewsLetter>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.NewsLetterModel.NewsLetter, NEPService.NewsService.NewsLetter>();
            #endregion NewsLetter

            #region Common

            Mapper.CreateMap<NEPService.CommonService.State, HCRGUniversityMgtApp.Domain.Models.Common.State>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Common.State, NEPService.CommonService.State>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CommonService.PagedSuggestCourse, HCRGUniversityMgtApp.Domain.Models.SuggestCourse.PagedSuggestCourse>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.SuggestCourse.PagedSuggestCourse, HCRGUniversityMgtApp.NEPService.CommonService.PagedSuggestCourse>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CommonService.SuggestCourse, HCRGUniversityMgtApp.Domain.Models.SuggestCourse.SuggestCourse>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.SuggestCourse.SuggestCourse, HCRGUniversityMgtApp.NEPService.CommonService.SuggestCourse>();          

            Mapper.CreateMap<NEPService.CommonService.Industry, HCRGUniversityMgtApp.Domain.Models.Common.Industry>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Common.Industry, NEPService.CommonService.Industry>();

            #endregion

            #region Client
            Mapper.CreateMap<NEPService.ClientService.Client, HCRGUniversityMgtApp.Domain.Models.Client.Client>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Client.Client, NEPService.ClientService.Client>();

            Mapper.CreateMap<NEPService.ClientService.Organization, HCRGUniversityMgtApp.Domain.Models.Client.Organization>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Client.Organization, NEPService.ClientService.Organization>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.ClientService.OrganizationContact, HCRGUniversityMgtApp.Domain.Models.OrganizationContact.OrganizationContact>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.OrganizationContact.OrganizationContact, HCRGUniversityMgtApp.NEPService.ClientService.OrganizationContact>();

            #endregion

            #region Client Paging
            Mapper.CreateMap<NEPService.ClientService.PagedClient, HCRGUniversityMgtApp.Domain.Models.Client.PagedClient>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Client.PagedClient, NEPService.ClientService.PagedClient>();
            #endregion

            #region Enterprise


            Mapper.CreateMap<NEPService.CommonService.Enterprise, HCRGUniversityMgtApp.Domain.Models.Enterprise.Enterprise>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Enterprise.Enterprise, NEPService.CommonService.Enterprise>();


            Mapper.CreateMap<NEPService.CommonService.PagedEnterpriseDetail, HCRGUniversityMgtApp.Domain.Models.Enterprise.PagedEnterprise>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Enterprise.PagedEnterprise, NEPService.CommonService.PagedEnterpriseDetail>();


            #endregion

            #region EnterprisePackageRegister
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.UserService.PagedEnterprisePackageRegister, HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister.PagedEnterprisePackageRegister>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister.PagedEnterprisePackageRegister, HCRGUniversityMgtApp.NEPService.UserService.PagedEnterprisePackageRegister>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.UserService.EnterprisePackageRegister, HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister.EnterprisePackageRegister>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister.EnterprisePackageRegister, HCRGUniversityMgtApp.NEPService.UserService.EnterprisePackageRegister>();
            #endregion  EnterprisePackageRegister

            #region CertificationTitleOptionMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption, HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.CertificationTitleOption>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.CertificationTitleOption, HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList, HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.CourseNameDropDownList>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.CourseNameDropDownList, HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CourseNameDropDownList>();
            #endregion CertificationTitleOptionMapping

            #region PagedCertificationTitleOptionMapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption, HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.PagedCertificationTitleOption>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption.PagedCertificationTitleOption, HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.PagedCertificationTitleOption>();
            #endregion PagedCertificationTitleOptionMapping

            #region Plan Mapping
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.Plan, HCRGUniversityMgtApp.Domain.Models.PlanModel.Plan>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PlanModel.Plan, HCRGUniversityMgtApp.NEPService.PlanService.Plan>();

            //Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.PagedPlan, HCRGUniversityMgtApp.Domain.Models.PlanModel.PagedPlan>();
            //Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PlanModel.PagedPlan, HCRGUniversityMgtApp.NEPService.PlanService.PagedPlan>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.PagedPlanGrid, HCRGUniversityMgtApp.Domain.Models.Plan.PagedPlanGrid>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Plan.PagedPlanGrid, HCRGUniversityMgtApp.NEPService.PlanService.PagedPlanGrid>();
            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.PlanGrid, HCRGUniversityMgtApp.Domain.Models.Plan.PlanGrid>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Plan.PlanGrid, HCRGUniversityMgtApp.NEPService.PlanService.PlanGrid>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.CoursePlan, HCRGUniversityMgtApp.Domain.Models.PlanModel.CoursePlan>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PlanModel.CoursePlan, HCRGUniversityMgtApp.NEPService.PlanService.CoursePlan>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.UserPlan, HCRGUniversityMgtApp.Domain.Models.PlanModel.UserPlan>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PlanModel.UserPlan, HCRGUniversityMgtApp.NEPService.PlanService.UserPlan>();

            Mapper.CreateMap<HCRGUniversityMgtApp.NEPService.PlanService.PlanPackages, HCRGUniversityMgtApp.Domain.Models.PlanModel.PlanPackages>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.PlanModel.PlanPackages, HCRGUniversityMgtApp.NEPService.PlanService.PlanPackages>();
            #endregion Plan Mapping

            #region UserMenuGroup

            Mapper.CreateMap<NEPService.UserService.UserMenuGroup, HCRGUniversityMgtApp.Domain.Models.Group.UserMenuGroup>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Group.UserMenuGroup, NEPService.UserService.UserMenuGroup>();

            Mapper.CreateMap<NEPService.UserService.UserMenuPermission, HCRGUniversityMgtApp.Domain.Models.Group.UserMenuPermission>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Group.UserMenuPermission, NEPService.UserService.UserMenuPermission>();

            Mapper.CreateMap<NEPService.UserService.PagedUserMenuGroup, HCRGUniversityMgtApp.Domain.Models.Group.PagedUserMenuGroup>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.Group.PagedUserMenuGroup, NEPService.UserService.PagedUserMenuGroup>();

            Mapper.CreateMap<NEPService.UserService.UserMenuGroupAndPermission, HCRGUniversityMgtApp.Domain.Models.UserModel.UserMenuGroupAndPermission>();
            Mapper.CreateMap<HCRGUniversityMgtApp.Domain.Models.UserModel.UserMenuGroupAndPermission, NEPService.UserService.UserMenuGroupAndPermission>();
            #endregion
        }
    }
}