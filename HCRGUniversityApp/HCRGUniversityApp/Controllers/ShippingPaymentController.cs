using AutoMapper;
using HCRGUniversityApp.Domain.Models.ShippingPayment;
using HCRGUniversityApp.Domain.ViewModels.ShippingPaymentViewModel;
using HCRGUniversityApp.NEPService.CommonService;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Domain.Models.EducationShoppingCartModel;
using HCRGUniversityApp.Domain.Models.DiscountCouponModel;
using HCRGUniversityApp.NEPService.ShippingPaymentService;
using HCRGUniversityApp.Infrastructure.ApplicationFilters;
using Braintree;
using System.Net;
using System.Configuration;
using System.Text;
using System.IO;


namespace HCRGUniversityApp.Controllers
{
    [AuthorizedUserCheck]
    public class ShippingPaymentController : BaseController
    {
        private readonly IShippingPaymentService _shippingPaymentService;
        private readonly ICommonService _commonService;
        private readonly NEPService.ShoppingEducationService.IShoppingEducationService _shoppingeducationService;
        private readonly NEPService.DiscountCouponService.IDiscountCouponService _discountCouponService;
        private readonly NEPService.UserService.IUserService _userService;
        private readonly IStorage _storageService;
        private readonly IEncryption _iEncryptionService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public ShippingPaymentController(IShippingPaymentService shippingPaymentService, ICommonService commonService, NEPService.ShoppingEducationService.IShoppingEducationService shoppingeducationService, NEPService.DiscountCouponService.IDiscountCouponService discountCouponService, NEPService.UserService.IUserService userService, IStorage storageService, NEPService.ClientService.IClientService clientService, IEncryption iEncryptionService)
        {
            _shippingPaymentService = shippingPaymentService;
            _commonService = commonService;
            _shoppingeducationService = shoppingeducationService;
            _discountCouponService = discountCouponService;
            _userService = userService;
            _storageService = storageService;
            _clientService = clientService;
            _iEncryptionService = iEncryptionService;
        }

        // GET: ShippingPayment


        public ActionResult Index()
        {
            ShippingPaymentViewModel _shippingPaymentViewModel = new ShippingPaymentViewModel();
            _shippingPaymentViewModel = CallShippingPaymentViewModel();
            if (Session["Countdown"] == null)
            {
                DateTime dt = DateTime.Now;
                Session["Countdown"] = dt.ToString("dd-MM-yyyy h:mm:ss tt");
                Session["StartDate"] = dt.AddMinutes(5).ToString("dd-MM-yyyy h:mm:ss tt");
            }
            ViewBag.Countdown = Session["Countdown"];
            ViewBag.StartDate = Session["StartDate"];
            return View(_shippingPaymentViewModel);

        }

        [ValidateAntiForgeryToken]
        private ShippingPaymentViewModel CallShippingPaymentViewModel()
        {
            ShippingPaymentViewModel _shippingPaymentViewModel = new ShippingPaymentViewModel();
            _shippingPaymentViewModel.ShippingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>(_shippingPaymentService.getShippingAddressByUserID(HCRGUser.UID));
            HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getPendingShippingPaymentByUserID(HCRGUser.UID));
            if (_shippingPayment == null)
            {
                HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment _shippingPaymentModel = new HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment();
                if (_shippingPaymentViewModel.ShippingAddressResults != null)
                    _shippingPaymentModel.ShippingAddressID = _shippingPaymentViewModel.ShippingAddressResults.ShippingAddressID;
                else
                    _shippingPaymentModel.ShippingAddressID = 0;

                _shippingPaymentModel.UserID = HCRGUser.UID;
                _shippingPaymentModel.CreatedBy = HCRGUser.UID;
                _shippingPaymentModel.CreatedOn = System.DateTime.Now;
                _shippingPaymentModel.IsPaymentRecevied = false;
                _shippingPaymentModel.ShippingPaymentID = _shippingPaymentService.addShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPaymentModel));
                _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getPendingShippingPaymentByUserID(HCRGUser.UID));
            }
            _shippingPaymentViewModel.ShippingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>(_shippingPaymentService.getShippingAddressByUserID(HCRGUser.UID));
            _shippingPaymentViewModel.BillingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress>(_shippingPaymentService.getBillingAddressByUserID(HCRGUser.UID));
            _shippingPaymentViewModel.EducationShoppingCartTempResults = Mapper.Map<IEnumerable<EducationShoppingCart>>(_shoppingeducationService.GetEducationShoppingCartByUserID(HCRGUser.UID));
            _shippingPaymentViewModel.ShippingTabDisable = 1;
            foreach (EducationShoppingCart viewmodel in _shippingPaymentViewModel.EducationShoppingCartTempResults)
            {
                viewmodel.Price = (viewmodel.Price - viewmodel.DiscountAmount.Value);
                // Update the shipping Payment ID
                if ((viewmodel.ShippingPaymentID == null) && (viewmodel.TempID != 0))
                {
                    _shippingPaymentService.UpdateEducationShoppingCartTempByShippingPaymentID(viewmodel.TempID, _shippingPayment.ShippingPaymentID, viewmodel.PType);
                    viewmodel.ShippingPaymentID = _shippingPayment.ShippingPaymentID;
                }
                if (viewmodel.PType.ToLower().Contains("hard") && _shippingPaymentViewModel.ShippingTabDisable == 1)
                    _shippingPaymentViewModel.ShippingTabDisable = 0;
            }


            if (_shippingPaymentViewModel.ShippingAddressResults != null)
            {
                _shippingPaymentViewModel.SAStateName = _commonService.getAllState().Where(rk => rk.StateID == _shippingPaymentViewModel.ShippingAddressResults.SAStateID).SingleOrDefault().StateName;
                if ((_shippingPaymentViewModel.ShippingAddressResults.IsSaveShippingAddress == false) || (_shippingPaymentViewModel.ShippingAddressResults.IsSaveShippingAddress == null))
                    _shippingPaymentViewModel.BillingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress>(_shippingPaymentService.getBillingAddressByUserID(HCRGUser.UID));
                else
                    _shippingPaymentViewModel.BillingAddressResults = null;
            }
            if (_shippingPayment.ShippingPaymentID > 0)
                _shippingPaymentViewModel.ShippingPaymentResults = _shippingPayment;
            else
                _shippingPaymentViewModel.ShippingPaymentResults = null;

            _shippingPaymentViewModel.TotalItemCountBA = _shippingPaymentService.getAllBillingAddressByUserID(HCRGUser.UID, 0, GlobalConst.Records.Take).TotalCount;
            _shippingPaymentViewModel.ShippingAddressRecordResults = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>>(_shippingPaymentService.getAllShippingAddressByUserID(HCRGUser.UID, 0, GlobalConst.Records.Take).ShippingAddresses);
            _shippingPaymentViewModel.TotalItemCountSA = _shippingPaymentService.getAllShippingAddressByUserID(HCRGUser.UID, 0, GlobalConst.Records.Take).TotalCount;
            _shippingPaymentViewModel.StateResults = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.State>>(_commonService.getAllState());
            _shippingPaymentViewModel.ShippingMethodRecordResults = Mapper.Map<IEnumerable<HCRGUniversityApp.Domain.Models.Common.ShippingMethod>>(_commonService.getAllShippingMethod());
            if (_shippingPayment != null)
            {
                if (_shippingPayment.ShippingMethodID > 0)
                    _shippingPaymentViewModel.ShippingMethodResults = _shippingPaymentViewModel.ShippingMethodRecordResults.Where(rk => rk.ShippingMethodID == _shippingPayment.ShippingMethodID).SingleOrDefault();
            }
            return _shippingPaymentViewModel;
        }

        [HttpPost]
        public JsonResult SaveShiipingAddress(HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress _shippingAddress)
        {
            string[] _Ids = new string[3];
            if (_shippingAddress.IsSaveShippingAddress.Value)
            {

                _shippingAddress.SAUserID = HCRGUser.UID;
                if (_shippingAddress.ShippingAddressID == 0)
                {
                    _Ids[0] = "Add";
                    _shippingAddress.ShippingAddressID = _shippingPaymentService.addShippingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>(_shippingAddress));
                    _Ids[1] = _shippingAddress.ShippingAddressID.ToString();
                    _Ids[2] = _shippingAddress.ShippingPaymentID.ToString();

                    Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
                    _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_shippingAddress.ShippingPaymentID));
                    _shippingPayment.ShippingAddressID = _shippingAddress.ShippingAddressID;
                    _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));
                }
                else
                {
                    _shippingPaymentService.updateShippingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>(_shippingAddress));
                    _Ids[0] = "Update";
                    _Ids[1] = _shippingAddress.ShippingAddressID.ToString();
                    _Ids[2] = _shippingAddress.ShippingPaymentID.ToString();

                    Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
                    _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_shippingAddress.ShippingPaymentID));
                    _shippingPayment.ShippingAddressID = _shippingAddress.ShippingAddressID;
                    _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));
                }
                return Json(_Ids, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Ids[1] = "NoChnage";
                _Ids[1] = _shippingAddress.ShippingAddressID.ToString();
                _Ids[2] = _shippingAddress.ShippingPaymentID.ToString();
                return Json(_Ids, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult SaveShiipingMethod(int _shippingPaymentID, int _shippingMethodID)
        {
            string[] _Ids = new string[3];
            _Ids[0] = "Add";
            Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
            _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_shippingPaymentID));
            _shippingPayment.ShippingMethodID = _shippingMethodID;
            _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));
            _Ids[1] = _shippingMethodID.ToString();
            _Ids[2] = _shippingPaymentID.ToString();
            return Json(_Ids, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveBillingAddress(HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress _billingAddress)
        {
            string[] _Ids = new string[3];
            if (_billingAddress.IsSaveBillingAddress.Value)
            {
                _billingAddress.BAUserID = HCRGUser.UID;
                if (_billingAddress.BillingAddressID == 0)
                {
                    if (!_billingAddress.IsSABillingAddressSame.Value)
                    {
                        _billingAddress.BillingAddressID = _shippingPaymentService.addBillingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.BillingAddress>(_billingAddress));
                        _Ids[0] = "Add";
                        _Ids[1] = _billingAddress.BillingAddressID.ToString();
                        Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
                        _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_billingAddress.ShippingPaymentID));
                        _shippingPayment.BillingAddressID = _billingAddress.BillingAddressID;
                        _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));

                        if (_shippingPayment.ShippingAddressID > 0)
                        {
                            Domain.Models.ShippingPayment.ShippingAddress _shippingAddress = new Domain.Models.ShippingPayment.ShippingAddress();
                            _shippingAddress = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>(_shippingPaymentService.getShippingAddressByID(_shippingPayment.ShippingAddressID));
                            _shippingAddress.SABillingAddressSame = false; // as user enter new address 
                            _shippingAddress.ShippingAddressID = _shippingPaymentService.updateShippingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>(_shippingAddress));
                        }
                        _Ids[2] = _shippingPayment.ShippingPaymentID.ToString();
                    }
                    else
                    {
                        _Ids[0] = "SABillingAddressSame";
                        _Ids[1] = "0";
                        _Ids[2] = _billingAddress.ShippingPaymentID.ToString();
                    }
                }
                else
                {

                    if (!_billingAddress.IsSABillingAddressSame.Value)
                    {
                        _shippingPaymentService.updateBillingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.BillingAddress>(_billingAddress));

                        _Ids[0] = "Update";
                        _Ids[1] = _billingAddress.BillingAddressID.ToString();

                        Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
                        _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_billingAddress.ShippingPaymentID));
                        _shippingPayment.BillingAddressID = _billingAddress.BillingAddressID;
                        _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));

                        if (_shippingPayment.ShippingAddressID > 0)
                        {
                            Domain.Models.ShippingPayment.ShippingAddress _shippingAddress = new Domain.Models.ShippingPayment.ShippingAddress();
                            _shippingAddress = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>(_shippingPaymentService.getShippingAddressByID(_shippingPayment.ShippingAddressID));
                            _shippingAddress.SABillingAddressSame = false; // as user enter new address 
                            _shippingAddress.ShippingAddressID = _shippingPaymentService.updateShippingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>(_shippingAddress));
                        }

                        _Ids[2] = _billingAddress.ShippingPaymentID.ToString();
                    }
                    else
                    {
                        Domain.Models.ShippingPayment.ShippingPayment _shippingPayment = new Domain.Models.ShippingPayment.ShippingPayment();
                        _shippingPayment = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.getShippingPaymentByID(_billingAddress.ShippingPaymentID));
                        _shippingPayment.BillingAddressID = _billingAddress.BillingAddressID;
                        _shippingPayment.ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(_shippingPayment));

                        if (_shippingPayment.ShippingAddressID > 0)
                        {
                            Domain.Models.ShippingPayment.ShippingAddress _shippingAddress = new Domain.Models.ShippingPayment.ShippingAddress();
                            _shippingAddress = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingAddress>(_shippingPaymentService.getShippingAddressByID(_shippingPayment.ShippingAddressID));
                            _shippingAddress.SABillingAddressSame = true; // as user enter new address 
                            _shippingAddress.ShippingAddressID = _shippingPaymentService.updateShippingAddress(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingAddress>(_shippingAddress));
                        }

                        _Ids[0] = "SABillingAddressSame";
                        _Ids[1] = _billingAddress.BillingAddressID.ToString();
                        _Ids[2] = _billingAddress.ShippingPaymentID.ToString();

                    }

                }
                return Json(_Ids, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
            }
            else
            {
                _Ids[1] = "NoChnage";
                _Ids[1] = _billingAddress.BillingAddressID.ToString();
                _Ids[2] = _billingAddress.ShippingPaymentID.ToString();
                return Json(_Ids, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ResetShippingOrderQuentityStock()
        {
            _shoppingeducationService.ResetShippingOrderQuentityStock(HCRGUser.UID);
            return Json(GlobalConst.Message.ResetShippingOrderQuentityStock, GlobalConst.Message.text_html);
        }

        public ActionResult Pay()
        {
            ShippingPaymentViewModel _shippingPaymentViewModel = new ShippingPaymentViewModel();
            PaypalDetail _paypalmodel = new PaypalDetail();
            _shippingPaymentViewModel.BillingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress>(_shippingPaymentService.getBillingAddressByUserID(HCRGUser.UID));
            string RegisteredEmail = _clientService.GetOrganizationByID(HCRGUser.OrganizationID).RegisteredPaypalEmailID;
            string statename = _commonService.getStateByID(_shippingPaymentViewModel.BillingAddressResults.BAStateID);
            string coursenames = _shoppingeducationService.GetShoppingCartCoursesByUserID(HCRGUser.UID);
            _paypalmodel.ProductName = coursenames;
            _paypalmodel.email = HCRGUser.EmailID;
            _paypalmodel.business = RegisteredEmail;
            _paypalmodel.Amount = Convert.ToDecimal(TempData["TotalAmount"]);
            _paypalmodel.CurrencyCode = _iEncryptionService.EncryptString(System.Configuration.ConfigurationManager.AppSettings["CurrencyCode"]);
            _paypalmodel.CurrencySymbol = _iEncryptionService.EncryptString(System.Configuration.ConfigurationManager.AppSettings["CurrencySymbol"]);
            _paypalmodel.notify_url = System.Configuration.ConfigurationManager.AppSettings["ReSetUrl"] + "/ShippingPayment/IPN";
            _paypalmodel.cancel_url = System.Configuration.ConfigurationManager.AppSettings["ReSetUrl"] + "" + "/ShoppingCart/Index";
            _paypalmodel.return_url = System.Configuration.ConfigurationManager.AppSettings["ReSetUrl"] + "" + "/ShippingPayment/IPN";
            _paypalmodel.first_name = _shippingPaymentViewModel.BillingAddressResults.BAFirstName;
            _paypalmodel.last_name = _shippingPaymentViewModel.BillingAddressResults.BALastName;
            _paypalmodel.address1 = _shippingPaymentViewModel.BillingAddressResults.BAAddress;
            _paypalmodel.city = _shippingPaymentViewModel.BillingAddressResults.BACity;
            _paypalmodel.state = statename;
            _paypalmodel.zip = _shippingPaymentViewModel.BillingAddressResults.BAPostalCode;
            _paypalmodel.UserID = HCRGUser.UID.ToString();
            _paypalmodel.cmd = "_xclick";
            _paypalmodel.URL = PaypalUrl();
            return View(_paypalmodel);

        }


        public static string PaypalUrl()
        {
            if (ConfigurationManager.AppSettings["UseSandBox"].ToString().Equals("true"))
                return "https://www.sandbox.paypal.com/cgi-bin/webscr";
            else
                return "https://www.paypal.com/cgi-bin/webscr";
        }

        public ActionResult IPN()
        {

            ShippingPaymentViewModel _shippingPaymentViewModel = new ShippingPaymentViewModel();
            _shippingPaymentViewModel.BillingAddressResults = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.BillingAddress>(_shippingPaymentService.getBillingAddressByUserID(HCRGUser.UID));
            var shipping = Mapper.Map<HCRGUniversityApp.Domain.Models.ShippingPayment.ShippingPayment>(_shippingPaymentService.GetShippingPaymentByUserID(HCRGUser.UID));
            Domain.Models.ShippingPayment.ShippingPayment shippingDetails = new Domain.Models.ShippingPayment.ShippingPayment();
            shippingDetails.UserID = _shippingPaymentViewModel.BillingAddressResults.BAUserID;
            shippingDetails.BillingAddressID = _shippingPaymentViewModel.BillingAddressResults.BillingAddressID;
            shippingDetails.ShippingAddressID = 0;
            shippingDetails.ShippingMethodID = 0;
            shippingDetails.ShippingPaymentID = shipping.ShippingPaymentID;
            shippingDetails.TransactionNumber = Request.QueryString["tx"];
            shippingDetails.TransactionDatetime = System.DateTime.Now;
            shippingDetails.PaymentStatus = Request.QueryString["st"];

            if (shippingDetails.PaymentStatus == "Completed")
            {
                shippingDetails.IsPaymentRecevied = true;
                shippingDetails.CreatedOn = System.DateTime.Now;
                shippingDetails.CreatedBy = HCRGUser.UID;
                if (shipping.ShippingPaymentID != null)
                {
                    int ShippingPaymentID = _shippingPaymentService.updateShippingPayment(Mapper.Map<HCRGUniversityApp.NEPService.ShippingPaymentService.ShippingPayment>(shippingDetails));
                    _shippingPaymentService.addEducationShoppingRecordByShippingPaymentID(shippingDetails.ShippingPaymentID);
                }
                Domain.Models.UserModel.User _user = new Domain.Models.UserModel.User();
                _user = Mapper.Map<Domain.Models.UserModel.User>(_userService.GetUserByID(HCRGUser.UID));

                if ((_user.IsAllAccessPricing == true) && (_user.UserAllAccessPassID == null))
                {
                    Domain.Models.UserModel.UserAllAccessPass _userAllAccessPass = new Domain.Models.UserModel.UserAllAccessPass();
                    _userAllAccessPass.AAPCouponID = null;
                    _userAllAccessPass.IsAllAccessPricingAmountRecevied = true;
                    _userAllAccessPass.AllAccessStartDate = DateTime.Now;
                    _userAllAccessPass.AllAccessEndDate = DateTime.Now.AddYears(1).AddDays(-1);
                    _userAllAccessPass.UserID = HCRGUser.UID;
                    _userAllAccessPass.ShippingPaymentID = shippingDetails.ShippingPaymentID;

                    _userService.addUserAllAccessPass(Mapper.Map<NEPService.UserService.UserAllAccessPass>(_userAllAccessPass));
                }
            }
            else
            {
                shippingDetails.IsPaymentRecevied = false;

            }
            return View(shippingDetails);
        }
    }
}