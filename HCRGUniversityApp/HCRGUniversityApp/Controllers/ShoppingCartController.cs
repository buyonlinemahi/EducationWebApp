using AutoMapper;
using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityApp.Domain.Models.EducationModel;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using HCRGUniversityApp.Domain.Models.EducationShoppingTempModel;
using HCRGUniversityApp.Domain.Models.Product;
using HCRGUniversityApp.Domain.Models.ProductModel;
using HCRGUniversityApp.Domain.Models.UserModel;
using HCRGUniversityApp.Domain.ViewModels.EducationShoppingCartTempViewModel;
using HCRGUniversityApp.Domain.ViewModels.EducationTypeAvailableViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class ShoppingCartController : BaseController
    {
        private readonly NEPService.ShoppingEducationService.IShoppingEducationService _shoppingeducationService;
        private readonly NEPService.DiscountCouponService.IDiscountCouponService _discountCouponService;
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.UserService.IUserService _userService;
        private readonly NEPService.ProductService.IProductService _productService;
        public readonly IEMail _mailService;

        public ShoppingCartController(NEPService.ShoppingEducationService.IShoppingEducationService shoppingeducationService, NEPService.EducationService.IEducationService educationService, NEPService.DiscountCouponService.IDiscountCouponService discountCouponService, NEPService.UserService.IUserService userService, NEPService.ProductService.IProductService productService, IEMail mailService)
        {
            _shoppingeducationService = shoppingeducationService;
            _discountCouponService = discountCouponService;
            _educationService = educationService;
            _userService = userService;
            _productService = productService;
            _mailService = mailService;
        }
        [AuthorizedUserCheck]
        public ActionResult Index()
        {
            decimal GrandTotal = 0; ;

            EducationShoppingCartTempViewModel educationshoppingModel = new EducationShoppingCartTempViewModel();
            if (Session["Countdown"] != null)
            {
                _shoppingeducationService.ResetShippingOrderQuentityStock(HCRGUser.UID);
                Session["Countdown"] = null;
                Session["StartDate"] = null;

            }
            educationshoppingModel.EducationShoppingCartTempResults = Mapper.Map<IEnumerable<EducationShoppingCart>>(_shoppingeducationService.GetEducationShoppingCartByUserID(HCRGUser.UID));
            List<DiscountCoupon> List_DiscountCoupon = new List<DiscountCoupon>();
            foreach (EducationShoppingCart viewmodel in educationshoppingModel.EducationShoppingCartTempResults)
            {
                if (viewmodel.CoupanID != null)
                {
                    DiscountCoupon discountCoupon = new DiscountCoupon();
                    discountCoupon = Mapper.Map<DiscountCoupon>(_discountCouponService.GetDiscountCouponByID(Convert.ToInt32(viewmodel.CoupanID)));
                    List_DiscountCoupon.Add(discountCoupon);
                }
                GrandTotal += viewmodel.Amount;
            }

            educationshoppingModel.DiscountCouponResults = List_DiscountCoupon.AsEnumerable();
            TempData["TotalAmount"] = GrandTotal;
            return View(educationshoppingModel);
        }
        [HttpPost]
        [AuthorizedUserCheck]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string EducationTypeID, string CredentialID)
        {
            bool checkCourseValdation = _shoppingeducationService.checkCoursePurchaseValidationByUserID(HCRGUser.UID, EducationID);
            if (!checkCourseValdation)
            {
                bool check = _shoppingeducationService.checkEducationinShoppingCart(HCRGUser.UID, EducationID, GlobalConst.CartType.Course);
                if (!check)
                {
                    EducationShoppingTemp educationShoppingTemp = new EducationShoppingTemp();
                    EducationTypesAvailableViewModel educationTypesAvailableViewModel = new EducationTypesAvailableViewModel();
                    educationTypesAvailableViewModel.EducationTypesAvailableResults = Mapper.Map<IEnumerable<EducationTypesAvailable>>(_educationService.GetEducationTypeByEducationID(EducationID));
                    HCRGUniversityApp.Domain.Models.EducationShoppingCartModel.EducationShoppingCart myList = new HCRGUniversityApp.Domain.Models.EducationShoppingCartModel.EducationShoppingCart();

                    var _res = _userService.getUserAllAccessPassByUserID(HCRGUser.UID);
                    if (_res != null)
                    {
                        if (_res.AllAccessEndDate.Value < DateTime.Now)
                        {
                            User _user1 = new User();
                            _user1 = Mapper.Map<User>(_userService.GetUserByID(HCRGUser.UID));
                            _user1.IsAllAccessPricing = null;
                            _user1.UserAllAccessPassID = null;
                            _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user1));
                        }
                    }


                    HCRGUniversityApp.Domain.Models.UserModel.User _user = new Domain.Models.UserModel.User();
                    _user = Mapper.Map<HCRGUniversityApp.Domain.Models.UserModel.User>(_userService.GetUserByID(HCRGUser.UID));


                    //if (_user.IsCoursePreview.Value != null)
                    if (_user.IsCoursePreview == true)
                    {
                        int cnt = 0;

                        foreach (HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable viewmodel in educationTypesAvailableViewModel.EducationTypesAvailableResults)
                        {
                            if (_educationService.GetEducationByID(EducationID).IsCoursePreview != null)
                            {
                                cnt = 1;
                                myList.EduorProID = EducationID;
                                myList.EducationTypeID = viewmodel.EducationTypeID;
                                myList.Price = viewmodel.Price;
                                myList.UserID = Convert.ToInt32(HCRGUser.UID);
                                myList.Quantity = 1;
                                myList.Date = DateTime.Now;
                                myList.ShippingPaymentID = null;
                                myList.UserAllAccessPassID = _user.UserAllAccessPassID;
                                myList.TaxPercentage = GlobalConst.ConstantChar.CourserTaxPercentage;
                                if (CredentialID != null)
                                    myList.CredentialID = int.Parse(CredentialID);
                            }
                            else
                                cnt = 0;
                        }

                        if (cnt == 1)
                        {
                            _shoppingeducationService.AddEducationShoppingOrderAllAccessPass(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingCart>(myList));
                            return Json(GlobalConst.Message.GoToMyEducation, GlobalConst.Message.text_html);
                        }
                    }



                    Domain.Models.UserModel.UserAllAccessPass _userAllAccessPass = new Domain.Models.UserModel.UserAllAccessPass();

                    if (_user.UserAllAccessPassID.HasValue)
                    {

                        _userAllAccessPass = Mapper.Map<HCRGUniversityApp.Domain.Models.UserModel.UserAllAccessPass>(_userService.getUserAllAccessPassByID(_user.UserAllAccessPassID.Value));

                        if (_userAllAccessPass.AllAccessEndDate.Value < System.DateTime.Now)
                        {
                            _user.IsAllAccessPricing = null;
                            _user.UserAllAccessPassID = null;
                            _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user));
                        }
                    }

                    // when courser price is zero ie coruse is free of cost.
                    foreach (HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable viewmodel in educationTypesAvailableViewModel.EducationTypesAvailableResults)
                    {
                        if (viewmodel.Price == 0)
                        {
                            myList.EduorProID = EducationID;
                            myList.EducationTypeID = viewmodel.EducationTypeID;
                            myList.Price = viewmodel.Price;
                            myList.UserID = Convert.ToInt32(HCRGUser.UID);
                            myList.Quantity = 1;
                            myList.Date = DateTime.Now;
                            myList.ShippingPaymentID = null;
                            myList.UserAllAccessPassID = null;
                            myList.TaxPercentage = GlobalConst.ConstantChar.CourserTaxPercentage;
                            if (CredentialID != null)
                                myList.CredentialID = int.Parse(CredentialID);
                            _shoppingeducationService.AddEducationShoppingOrderAllAccessPass(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingCart>(myList));
                            return Json(GlobalConst.Message.GoToMyEducation, GlobalConst.Message.text_html);
                        }
                    }


                    if (!_user.UserAllAccessPassID.HasValue)
                    {
                        foreach (HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable viewmodel in educationTypesAvailableViewModel.EducationTypesAvailableResults)
                        {
                            educationShoppingTemp.EducationID = EducationID;
                            educationShoppingTemp.EducationTypeID = viewmodel.EducationTypeID;
                            educationShoppingTemp.Amount = viewmodel.Price;
                            educationShoppingTemp.UserID = Convert.ToInt32(HCRGUser.UID);
                            educationShoppingTemp.Quantity = 1;
                            educationShoppingTemp.Date = DateTime.Now;
                            educationShoppingTemp.TaxPercentage = GlobalConst.ConstantChar.CourserTaxPercentage;
                            if (CredentialID != null)
                                educationShoppingTemp.CredentialID = int.Parse(CredentialID);

                        }
                        var EducationShoppingTempModelid = _shoppingeducationService.AddEducationToShoppingCart(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingTemp>(educationShoppingTemp));
                        return Json(educationShoppingTemp, GlobalConst.Message.text_html);
                    }
                    else
                    {

                        foreach (HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable viewmodel in educationTypesAvailableViewModel.EducationTypesAvailableResults)
                        {
                            myList.EduorProID = EducationID;
                            myList.EducationTypeID = viewmodel.EducationTypeID;
                            myList.Price = viewmodel.Price;
                            myList.UserID = Convert.ToInt32(HCRGUser.UID);
                            myList.Quantity = 1;
                            myList.Date = DateTime.Now;
                            myList.ShippingPaymentID = _userAllAccessPass.ShippingPaymentID.Value;
                            myList.UserAllAccessPassID = _user.UserAllAccessPassID;
                            myList.TaxPercentage = GlobalConst.ConstantChar.CourserTaxPercentage;
                            if (CredentialID != null)
                                myList.CredentialID = int.Parse(CredentialID);
                        }

                        var _rest = _userService.GetUserSubscriptionDetails();

                        if (myList.Price >= _rest.AllAccessParametersCoursePriceStart && myList.Price <= _rest.AllAccessParametersCoursePriceEnd)
                        {
                            _shoppingeducationService.AddEducationShoppingOrderAllAccessPass(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingCart>(myList));
                            return Json(GlobalConst.Message.GoToMyEducation, GlobalConst.Message.text_html);
                        }
                        else
                        {
                            foreach (HCRGUniversityApp.Domain.Models.EducationModel.EducationTypesAvailable viewmodel in educationTypesAvailableViewModel.EducationTypesAvailableResults)
                            {
                                educationShoppingTemp.EducationID = EducationID;
                                educationShoppingTemp.EducationTypeID = viewmodel.EducationTypeID;
                                educationShoppingTemp.Amount = viewmodel.Price;
                                educationShoppingTemp.UserID = Convert.ToInt32(HCRGUser.UID);
                                educationShoppingTemp.Quantity = 1;
                                educationShoppingTemp.Date = DateTime.Now;
                                educationShoppingTemp.UserAllAccessPassID = null;
                                myList.TaxPercentage = GlobalConst.ConstantChar.CourserTaxPercentage;
                                if (CredentialID != null)
                                    educationShoppingTemp.CredentialID = int.Parse(CredentialID);
                            }
                            var EducationShoppingTempModelid = _shoppingeducationService.AddEducationToShoppingCart(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingTemp>(educationShoppingTemp));
                            return Json(educationShoppingTemp, GlobalConst.Message.text_html);
                        }
                    }
                }
                else
                    return Json(GlobalConst.Message.AlreadyAddToCart, GlobalConst.Message.text_html);
            }
            else
                return Json(GlobalConst.Message.CourseAlreadyInProgress, GlobalConst.Message.text_html);
        }

        [HttpPost]
        [AuthorizedUserCheck]
        [ValidateAntiForgeryToken]
        public ActionResult AddProduct(Product product)
        {
            bool check = _shoppingeducationService.checkEducationinShoppingCart(HCRGUser.UID, product.ProductID, GlobalConst.CartType.Product);
            if (!check)
            {
                ProductShoppingTemp productShoppingTemp = new ProductShoppingTemp();
                productShoppingTemp.ProductID = product.ProductID;
                productShoppingTemp.Amount = product.ProductPrice;
                productShoppingTemp.UserID = Convert.ToInt32(HCRGUser.UID);
                productShoppingTemp.Quantity = 1;
                productShoppingTemp.Date = DateTime.Now;
                if (product.ProductType == "Hard Copy")
                    productShoppingTemp.TaxPercentage = GlobalConst.ConstantChar.ProductTaxPercentage;
                else
                    productShoppingTemp.TaxPercentage = GlobalConst.ConstantChar.ZeroTaxPercentage;

                var ProductShoppingTempModelid = _shoppingeducationService.AddProductShoppingCart(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.ProductShoppingTemp>(productShoppingTemp));
                if (ProductShoppingTempModelid > 0)
                    return Json(productShoppingTemp, GlobalConst.Message.text_html);
                else
                    return Json("Product is out of stock.", GlobalConst.Message.text_html);
            }
            else
                return Json(GlobalConst.Message.AlreadyAddToCart, GlobalConst.Message.text_html);

            //return Json("Add to card can not be done now.", GlobalConst.Message.text_html);
        }

        [ChildActionOnly]
        public PartialViewResult shoppingCartItemCount()
        {
            var ShoppingCartItemCount = 0;
            if (HttpContext.Session[GlobalConst.SessionKeys.USER] != null)
                ShoppingCartItemCount = _shoppingeducationService.GetEducationShoppingCartByUserID(HCRGUser.UID).Count();
            return PartialView(GlobalConst.partialViewPaths.shoppingCartItemCountPartial, ShoppingCartItemCount);
        }

        [HttpPost]
        public JsonResult ApplyCoupon(DiscountCoupon discountcoupon, string Couponcode)
        {
            discountcoupon = Mapper.Map<DiscountCoupon>(_discountCouponService.GetDiscountCouponByCouponCode(Couponcode));
            if (discountcoupon.CoupanValid && discountcoupon.CouponExpiryDate >= DateTime.Now)
                return Json(discountcoupon);
            else
                return Json(GlobalConst.Message.CoupanNotValid);
        }


        [HttpPost]
        public JsonResult DeleteCartItem(int id, string cartType, int _productID)
        {
            if (cartType == GlobalConst.CartType.Product)
            {
                var _result = _shoppingeducationService.GetProductShoppingTempByID(id);
                if (_result != null)
                {
                    if ((_result.ShippingPaymentID == null) && (_result.IsProcessed == null))
                        _shoppingeducationService.DeleteProductShoppingCart(id);
                    else
                    {
                        if ((_result.ShippingPaymentID != null) && (_result.IsProcessed == null))
                        {
                            Product _product = Mapper.Map<Product>(_productService.GetProductByID(_productID));
                            int deletedQty = _result.Quantity;
                            _product.ProductCurrentBalance = _product.ProductCurrentBalance + deletedQty;
                            _productService.UpdateProduct(Mapper.Map<HCRGUniversityApp.NEPService.ProductService.Product>(_product));

                            // Product Quantity ------
                            ProductQuantity _productQuantity = new ProductQuantity();
                            _productQuantity.ProdQuantity = deletedQty;
                            _productQuantity.ProdQuantityDate = DateTime.Now;
                            _productQuantity.ProductID = _productID;
                            _productQuantity.UserID = HCRGUser.UID;
                            _productQuantity.Mode = "Add-Deleted";
                            _productService.addProductQuantity(Mapper.Map<HCRGUniversityApp.NEPService.ProductService.ProductQuantity>(_productQuantity));
                            _shoppingeducationService.DeleteProductShoppingCart(id);
                        }
                        else
                            return Json(GlobalConst.Message.CannotDeleted);
                    }
                }
                else
                    return Json(GlobalConst.Message.AlReadyDeletedSuccessfully);
            }
            else if (cartType == GlobalConst.CartType.AllAccessPass)
            {
                User _user = new User();
                _user = Mapper.Map<User>(_userService.GetUserByID(HCRGUser.UID));

                if (_user.IsAllAccessPricing.HasValue)
                {
                    _user.IsAllAccessPricing = null;
                    _userService.UpdateUser(Mapper.Map<HCRGUniversityApp.NEPService.UserService.User>(_user));
                }
            }
            else
            {
                var _res = _shoppingeducationService.GetEducationShoppingTempByID(id);

                if (_res.ShippingPaymentID == null)
                    _shoppingeducationService.DeleteEducationFromShoppingCart(id); // online course deleted 
                else
                    return Json(GlobalConst.Message.CannotDeleted);
            }

            return Json(GlobalConst.Message.DeletedSuccessfully);
        }

        [HttpPost]
        public JsonResult UpdateEducationToShoppingCart(int CouponID, int EducationTypeID, int UserID, string CourseName, DateTime Date, int EducationShoppingTempID, string EducationType, decimal Price, int Quantity, decimal Amount, int EducationID, int? CredentialID)
        {
            EducationShoppingTemp educationShoppingTemp = new EducationShoppingTemp();
            educationShoppingTemp.Amount = Amount;
            educationShoppingTemp.CoupanID = CouponID;
            educationShoppingTemp.CourseName = CourseName;
            educationShoppingTemp.Date = Date;
            educationShoppingTemp.EducationID = EducationID;
            educationShoppingTemp.EducationShoppingTempID = EducationShoppingTempID;
            educationShoppingTemp.Quantity = Quantity;
            educationShoppingTemp.EducationTypeID = EducationTypeID;
            educationShoppingTemp.UserID = UserID;
            educationShoppingTemp.CredentialID = CredentialID;
            _shoppingeducationService.UpdateEducationToShoppingCart(Mapper.Map<HCRGUniversityApp.NEPService.ShoppingEducationService.EducationShoppingTemp>(educationShoppingTemp));
            return Json(GlobalConst.Message.DeletedSuccessfully);
        }

        public ActionResult Checkout(IEnumerable<EducationShoppingCart> myList)
        {
            if (myList != null)
                return Json(GlobalConst.Message.AddedSuccessfully);
            else
                return Json("No Data");
        }

        public ActionResult CheckProductQuantityStock(IEnumerable<EducationShoppingCart> myList)
        {

            int ProductOutOfStock = 0;
            if (myList != null)
            {
                // If their was any item , who is out of stock..
                int Chk = 0;
                int _coosCnt = 1;
                foreach (var item in myList)
                {
                    if (item.TempID > 0)
                    {
                        Product _productByIDvar = Mapper.Map<Product>(_productService.GetProductByID(item.EduorProID));
                        if (_productByIDvar != null)
                        {
                            if (_productByIDvar.ProductType != "Download")
                            {
                                if (item.CartType.ToLower() != "course")
                                {
                                    var _res = Mapper.Map<ProductShoppingTemp>(_shoppingeducationService.GetProductShoppingTempByID(item.TempID));
                                    if (!_res.ShippingPaymentID.HasValue)
                                    {
                                        if (Chk == 0)
                                            _coosCnt = _shoppingeducationService.CheckAnyProductsIsOutOfStock(HCRGUser.UID);
                                        if (_coosCnt == 0)
                                        {
                                            Chk = 1;
                                            Product _product = Mapper.Map<Product>(_productService.GetProductByID(item.EduorProID));
                                            if (_product.ProductCurrentBalance.Value <= 0)
                                            {
                                                ProductOutOfStock = 1; // out of stock
                                                _shoppingeducationService.DeleteProductShoppingCart(item.TempID);
                                            }
                                            else
                                            {
                                                Product _product1 = Mapper.Map<Product>(_productService.GetProductByID(_product.ProductID));
                                                _product1.ProductCurrentBalance = _product1.ProductCurrentBalance.Value - item.Quantity;
                                                _productService.UpdateProduct(Mapper.Map<HCRGUniversityApp.NEPService.ProductService.Product>(_product1));
                                            }

                                        }
                                        else
                                            return Json(GlobalConst.Message.ProductOutOfStock);
                                    }
                                    else
                                        return Json(GlobalConst.Message.ItemAlreadyInProcess);

                                }
                            }
                        }
                    }
                }
                if (ProductOutOfStock > 0)
                    return Json(GlobalConst.Message.ProductOutOfStock);
                else
                    return Json(GlobalConst.Message.ProductInStock);
            }
            else
            {
                return Json("No Data");
            }
        }
        [HttpPost]
        public JsonResult SendRequestBackorderEmail(string PName)
        {
            var msg = "";
            msg = @" <p>Hello,</p> <p>" + HCRGUser.FirstName + " " + HCRGUser.LastName + " has requested " + PName + " to be back ordered. Please restock this item for user to purchase.</p><p>Thank you,</p>";
            _mailService.SendEmail("Item Restock Request", msg, System.Configuration.ConfigurationSettings.AppSettings["FromAddress"].ToString().Trim());
            return Json("Yes", GlobalConst.Message.EmailSentSuccessfully);
        }
    }
}