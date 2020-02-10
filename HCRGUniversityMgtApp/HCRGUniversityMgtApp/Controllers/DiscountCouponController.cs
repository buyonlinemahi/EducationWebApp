using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.DiscountCouponForCoursesModel;
using HCRGUniversityMgtApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using HCRGUniversityMgtApp.Domain.ViewModels.DiscountCouponForCoursesViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class DiscountCouponController : BaseController
    {
        private readonly NEPService.DiscountCouponService.IDiscountCouponService _DiscountCouponService;
        //
        // GET: /DiscountCoupon/

        public DiscountCouponController(NEPService.DiscountCouponService.IDiscountCouponService discountCouponService)
        {
            _DiscountCouponService = discountCouponService;

        }

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Add(IEnumerable<Education> myList, string CouponType, string CouponDiscount, string CouponExpiryDate, string noofcoupon, string couponFor)
        {
            List<DiscountCoupon> list_discountCoupon = new List<DiscountCoupon>();
            DiscountCoupon discountCoupon = new DiscountCoupon();
            foreach (var item in myList)
            {

                for (int i = 0; i < Convert.ToInt32(noofcoupon); i++)
                {
                    discountCoupon = new DiscountCoupon();
                    discountCoupon.CouponDiscount = Convert.ToDecimal(CouponDiscount);
                    discountCoupon.CouponExpiryDate = Convert.ToDateTime(CouponExpiryDate);  //DateTime.ParseExact(CouponExpiryDate, format, en.DateTimeFormat);
                    discountCoupon.CouponIssueDate = DateTime.Now;
                    discountCoupon.CouponCode = MakeCoupon(10);
                    discountCoupon.CoupanValid = true;
                    discountCoupon.CouponType = CouponType;
                    discountCoupon.ClientID = HCRGCLIENT.ClientID;
                    if (couponFor == GlobalConst.CouponType.Course)
                    {
                        discountCoupon.CouponProduactID = 0;
                        discountCoupon.CouponEducationID = item.EducationID;
                        discountCoupon.CouponEducationName = item.CourseName;
                    }
                    else
                    {
                        discountCoupon.CouponProduactID = 1;
                        discountCoupon.CouponEducationID = 0;
                        discountCoupon.CouponEducationName = "";
                    }
                    discountCoupon.CouponID = _DiscountCouponService.AddDiscountCoupon(Mapper.Map<HCRGUniversityMgtApp.NEPService.DiscountCouponService.DiscountCoupon>(discountCoupon));
                    list_discountCoupon.Add(discountCoupon);
                }
            }
            return Json(list_discountCoupon, GlobalConst.Message.text_html);
        }

        private static string MakeCoupon(int pl)
        {
            string possibles = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
            char[] coupons = new char[pl];
            Random rd = new Random();

            for (int i = 0; i < pl; i++)
            {
                coupons[i] = possibles[rd.Next(0, possibles.Length)];
            }
            return new string(coupons);
        }

       
        public ActionResult GetAllDiscountCouponsForCourses()
          {
              var discountCouponForCourses = _DiscountCouponService.GetDiscountCouponForCourses(HCRGCLIENT.ClientID);
              DiscountCouponForCoursesViewModel discountCouponForCoursesModel = new DiscountCouponForCoursesViewModel();
              discountCouponForCoursesModel.DiscountCouponForCoursesResults = Mapper.Map<IEnumerable<DiscountCouponForCourses>>(discountCouponForCourses);
              return Json(discountCouponForCoursesModel.DiscountCouponForCoursesResults, GlobalConst.Message.text_html);
            
          }
       
        public ActionResult GetAllPagedDiscountCouponForCourse(int skip,int?take)
        {
            PagedDiscountCoupon pagedDiscountCoupon = new PagedDiscountCoupon();
            if(take==null)
                pagedDiscountCoupon = Mapper.Map<PagedDiscountCoupon>(_DiscountCouponService.GetAllPagedDiscountCouponForCourses(skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID));
            else
                pagedDiscountCoupon = Mapper.Map<PagedDiscountCoupon>(_DiscountCouponService.GetAllPagedDiscountCouponForCourses(skip, take.Value, HCRGCLIENT.ClientID));
            pagedDiscountCoupon.PagedRecords = GlobalConst.Records.LandingTake;
            return Json(pagedDiscountCoupon, GlobalConst.Message.text_html);

        }

        [HttpPost]
        public JsonResult DeleteDiscountCoupon(DiscountCoupon discountCoupon)
        {

            _DiscountCouponService.DeleteDiscountCoupon(discountCoupon.CouponID);
            return Json("Discount Coupon deleted Successfully");
        }

     

    }
}
