using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister;
using HCRGUniversityMgtApp.Domain.Models.Group;
using HCRGUniversityMgtApp.Domain.Models.UserModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class UserController : BaseController
    {
        private readonly NEPService.UserService.IUserService _userService;
        public readonly IEMail _mailService;
        public readonly IEncryption _encryptionService;
        public UserController(NEPService.UserService.IUserService userService, IEncryption encryptionService, IEMail mailService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
            _mailService = mailService;
        }

        public ActionResult Index()
        {
            //int _organizationID = 0;
            //if (HCRGCLIENT.ClientTypeID == 1)
            //    _organizationID = 0;
            //else
            //    _organizationID = HCRGCLIENT.OrganizationID;
            //IEnumerable<ClientAndUserbySearchCriterias> userModel = Mapper.Map<IEnumerable<ClientAndUserbySearchCriterias>>(_userService.GetClientAndUserbySearchCriterias(_organizationID, HCRGCLIENT.ClientTypeID, GlobalConst.ConstantChar.Blank_String, GlobalConst.CommonValues.Zero, GlobalConst.ConstantChar.Blank_String));          
            //foreach(ClientAndUserbySearchCriterias obj in userModel)
            //{
            //    if( obj.IsActive == true)
            //        obj.Status = "Active";
            //    else
            //        obj.Status = "InActive";
            //}
            ClientAndUserbySearchCriterias objdata = new ClientAndUserbySearchCriterias();
            if (HCRGCLIENT.ClientTypeID == 1)
                objdata.IsHCRGAdmin = true;
            else
                objdata.IsHCRGAdmin = false;
            return View(objdata);
        }
        [HttpPost]
        public ActionResult SearchUserAndClientNameCriteria(int OrganizationID, int ClientTypeID, string SelectionTypeName, string SearchText)
        {
            int _organizationID =0;int _clienttypeID =0;
            if (HCRGCLIENT.ClientTypeID == 1)
            {
                _organizationID = OrganizationID;
                _clienttypeID = ClientTypeID;
            }
            else
            {
                _organizationID = OrganizationID == 0 ? HCRGCLIENT.OrganizationID : OrganizationID;
                _clienttypeID = ClientTypeID == 0 ? HCRGCLIENT.ClientTypeID : ClientTypeID;
            }
            
            
            string _selectionTypeName = SelectionTypeName == "Select Client Type" ? GlobalConst.ConstantChar.Blank_String : SelectionTypeName;
            string _searchtext = SearchText == "" ? GlobalConst.ConstantChar.Blank_String : SearchText;

            IEnumerable<ClientAndUserbySearchCriterias> userModel = Mapper.Map<IEnumerable<ClientAndUserbySearchCriterias>>(_userService.GetClientAndUserbySearchCriterias(_organizationID, HCRGCLIENT.ClientTypeID, _selectionTypeName, ClientTypeID, _searchtext));
            foreach (ClientAndUserbySearchCriterias obj in userModel)
            {
                if (obj.IsActive == true)
                    obj.Status = "Active";
                else
                    obj.Status = "InActive";
            }
            return Json(userModel);
        }


        [HttpPost]
        public JsonResult GetAllUsersByPlanID(int _planID)
        {
            return Json(_userService.GetUsersByPlanID(_planID), GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult GetUsersByName(string searchtext)
        {
            PagedUser userModel = new PagedUser();
            userModel = Mapper.Map<PagedUser>(_userService.GetUsersByName(searchtext == null ? "" : searchtext, GlobalConst.Records.Skip, GlobalConst.Records.take500, HCRGCLIENT.ClientID));
            userModel.PagedRecords = GlobalConst.Records.LandingTake;
            foreach (User obj in userModel.Users)
                obj.ClientTypeName = "Student";
            return Json(userModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult GetAllPagedUser(int skip, string searchtext)
        {
            PagedUser userModel = new PagedUser();
            if (searchtext != null)
                userModel = Mapper.Map<PagedUser>(_userService.GetUsersByName(searchtext, skip, GlobalConst.Records.LandingTake, HCRGCLIENT.OrganizationID));
            else
                userModel = Mapper.Map<PagedUser>(_userService.GetAllPagedUser(skip, GlobalConst.Records.LandingTake, HCRGCLIENT.OrganizationID));
            userModel.PagedRecords = GlobalConst.Records.LandingTake;
            return Json(userModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult UpdateUserInfo(User user)
        {            
            var d = _userService.UpdateUser(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.User>(user));
            return Json(user, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult UpdateUser(User user)
        {
            user.OrganizationID = HCRGCLIENT.OrganizationID;
            user.ClearedBy = HCRGCLIENT.ClientID;            
            user.Password = _userService.GetUserByID(user.UID).Password;// temporary code to remove error due to null  password
            user.ClientID = HCRGCLIENT.ClientID;
            _userService.UpdateUser(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.User>(user));
            return Json(user, GlobalConst.Message.text_html);
        }


        [HttpPost]
        public JsonResult AddUser(User user)
        {
            if (user.UserMenuGroupID == null)
                user.UserMenuGroupID = 0;
            if (HCRGCLIENT.ClientTypeID > 1)
            {
                user.OrganizationID = HCRGCLIENT.OrganizationID;
                user.ClientID = HCRGCLIENT.ClientID;
            }
            if (user.UID > 0)
            {      
          
                user.ClearedBy = HCRGCLIENT.ClientID;
                user.IsVerified = true;
                user.VerifiedOn = System.DateTime.Now;
                user.Password = _userService.GetUserByID(user.UID).Password;// temporary code to remove error due to null  password
                
                _userService.UpdateUser(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.User>(user));
                user.Message = "UpdateUser";
                return Json(user, GlobalConst.Message.text_html);
            }
            else
            {
                var _usr = (_userService.GetUserByUserName(user.EmailID));
                
                    user.SpecialMenuIDs = null;

                

                if (_usr == null)
                {
                    string TempPassword = _encryptionService.GenratePassword(7);
                    user.Password = _encryptionService.HashPassword(TempPassword);
                   //user.OrganizationID = HCRGCLIENT.OrganizationID;
                    user.ClearedBy = HCRGCLIENT.ClientID;
                    user.ClearedOn = System.DateTime.Now;
                    user.IsActive = true;
                    user.IsVerified = true;
                    user.VerifiedOn = System.DateTime.Now;
                    user.ClientID = HCRGCLIENT.ClientID;

                    int _userID = _userService.AddUser(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.User>(user));

                    string subject;
                    string msg;
                    subject = "User Registration & New Password";
                    msg = @"   <p>Hi " + user.FirstName + " " + user.LastName + @",</p>
                            <p>Your password is this : " + TempPassword + @"</p>
                            <p>Thanks!</p>";
                    _mailService.SendRandomPasswordEmail(msg, user.EmailID, subject);

                    user.Message = "AddUser";
                }
                else
                    user.Message = "UserEmailExists";

                user.Password = null;
                user.EmailID = null;
                return Json(user, GlobalConst.Message.text_html);
            }
        }

        #region EnterprisePackageRegister
        public ActionResult getAllEnterprisePackageRegister(int? skip)
        {
            PagedEnterprisePackageRegister pagedEnterprisePackageRegisterModel = new PagedEnterprisePackageRegister();
            if (skip == null)
            {
                var EnterprisePackageRegisterList = _userService.GetAllPagedEnterprisePackageRegister(GlobalConst.Records.Skip, GlobalConst.Records.Take15, HCRGCLIENT.ClientID);
                pagedEnterprisePackageRegisterModel.EnterprisePackageRegisterDetails = Mapper.Map<IEnumerable<EnterprisePackageRegister>>(EnterprisePackageRegisterList.EnterprisePackageRegisterDetails);
                pagedEnterprisePackageRegisterModel.TotalCount = EnterprisePackageRegisterList.TotalCount;
                return View(pagedEnterprisePackageRegisterModel);
            }
            else
            {
                var EnterprisePackageRegisterList = _userService.GetAllPagedEnterprisePackageRegister(skip.Value, GlobalConst.Records.Take15, HCRGCLIENT.ClientID);
                pagedEnterprisePackageRegisterModel.EnterprisePackageRegisterDetails = Mapper.Map<IEnumerable<EnterprisePackageRegister>>(EnterprisePackageRegisterList.EnterprisePackageRegisterDetails);
                pagedEnterprisePackageRegisterModel.TotalCount = EnterprisePackageRegisterList.TotalCount;
                return Json(pagedEnterprisePackageRegisterModel, GlobalConst.Message.text_html);
            }
        }

        [HttpPost]
        public ActionResult GetEnterprisePackageRegisterByID(int ID)
        {
            var EnterprisePackageRegisterModel = Mapper.Map<EnterprisePackageRegister>(_userService.getEnterprisePackageRegisterByID(ID));
            return Json(EnterprisePackageRegisterModel);
        }
        #endregion 

        [HttpPost]
        public JsonResult GetNewUsers(int skip)
        {
            try
            {
                PagedUser _newUsers = new PagedUser();
                _newUsers = Mapper.Map<PagedUser>(_userService.getUserVerifiedDetails(skip, GlobalConst.Records.Take, HCRGCLIENT.OrganizationID));
                return Json(_newUsers, GlobalConst.Message.text_html);
            }
            catch 
            {
                return Json("");
            }
        }

        [HttpPost]
        public ActionResult ClearUser(int uid)
        {
            try {
                _userService.clearedUserDetail(uid,true);
                return Json("Record Cleared");
            }
            catch(Exception e)
            {
                return Json(e.Message);
             }            
        }
        [HttpGet]
        public  JsonResult GetAllUserMenuGroupByOrganizationID()
        {            
           return Json(Mapper.Map<IEnumerable<UserMenuGroup>>(_userService.GetAllUserMenuGroupByOrganizationID(Convert.ToInt32(HCRGCLIENT.OrganizationID))), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);            
        }
        [HttpGet]
        public JsonResult GetUserPermissionByGroupId(int userMenuGroupID)
        {
            return Json(Mapper.Map<UserMenuPermission>(_userService.GetUserMenuPermissionByUserMenuGroupID(userMenuGroupID)), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetUserMenuGroupByMenuIDs(string menuIDs)
        {
            var t = Mapper.Map<UserMenuGroup>(_userService.GetUserMenuGroupByMenuIDs(menuIDs, HCRGCLIENT.OrganizationID));
            return Json(Mapper.Map<UserMenuGroup>(_userService.GetUserMenuGroupByMenuIDs(menuIDs, HCRGCLIENT.OrganizationID)), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetAllUserMenuGroupAndPremissionAccordingToOrganizationID()
        {
            return Json(Mapper.Map<IEnumerable<UserMenuGroupAndPermission>>(_userService.GetAllUserMenuGroupAndPremissionAccordingToOrganizationID(HCRGCLIENT.OrganizationID)), GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult UserDetailByID(int userID)
        {
            User objUser = new User();
            objUser = Mapper.Map<User>(_userService.GetUserByID(userID));
            return Json(objUser);
        }

        [HttpPost]
        public ActionResult ResetUserPassword(int userId)
        {
            string subject;
            string msg;
            User user = new User();
            var User = Mapper.Map<User>(_userService.GetUserByID(userId));
            string TempPassword = _encryptionService.GenratePassword(7);
            user.Password = _encryptionService.HashPassword(TempPassword);

            subject = "User Registration & New Password";
            msg = @"   <p>Hi " + User.FirstName + " " + User.LastName + @",</p>
                            <p>Your password is this : " + TempPassword + @"</p>
                            <p>Thanks!</p>";
            _mailService.SendRandomPasswordEmail(msg, User.EmailID, subject);
            _userService.UpdateUserPassword(userId, user.Password);
            return Json(GlobalConst.Message.ResetPassword);
        }


    }
}