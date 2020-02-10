using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Group;
using HCRGUniversityMgtApp.Domain.ViewModels.GroupViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class GroupController : BaseController
    {
        private readonly NEPService.UserService.IUserService _userService;
        public readonly IEncryption _encryptionService;

        public GroupController(NEPService.UserService.IUserService userService, IEncryption encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
        }
        public ActionResult Index()
        {
            return RedirectToAction(GlobalConst.Actions.HomeContentController.Index, GlobalConst.Controllers.Home, new { area = "" });

            //PagedUserMenuGroup pagedGroupModel = new PagedUserMenuGroup();
            //pagedGroupModel = Mapper.Map<PagedUserMenuGroup>(_userService.GetUserMenuGroup(HCRGCLIENT.OrganizationID));
            //foreach (var objResults in pagedGroupModel.UserMenuGroupDetails)
            //    objResults.EncryptedUserMenuGroupID = EncryptString(objResults.UserMenuGroupID.ToString());
            //return View(pagedGroupModel);
        }


        [HttpGet]
        public ActionResult AddGroup(string id)
        {
            return RedirectToAction(GlobalConst.Actions.HomeContentController.Index, GlobalConst.Controllers.Home, new { area = "" });
            
            //if (id != null)
            //{
            //    int _Id = Convert.ToInt32(DecryptString(id.ToString()));
            //    GroupViewModel model = new GroupViewModel();
            //    if (id != null)
            //    {
            //        model.UserMenuGroup = Mapper.Map<UserMenuGroup>(_userService.GetUserMenuGroupByID(_Id));
            //        model.UserMenuPermission = Mapper.Map<UserMenuPermission>(_userService.GetUserMenuPermissionByUserMenuGroupID(_Id));
            //    }
            //    return View(model);
            //}
            //else
            //{
            //    return View();
            //}
        }

        [HttpPost]
        public JsonResult CheckUserGroupName(string userGroupName)
        {
            var userMenuGroupByName = _userService.GetUserMenuGroupByGroupName(userGroupName, HCRGCLIENT.OrganizationID);            
            if (userMenuGroupByName != null)
                return Json("User name already exists");
            return Json(true);
        }

        [HttpPost]
        public JsonResult CheckUserGroupMenu(string menuIds)
        {            
            var userMenuGroupByIDs = _userService.GetUserMenuGroupByMenuIDs(menuIds, HCRGCLIENT.OrganizationID);
            if (userMenuGroupByIDs != null)
                return Json("User Permission set already exists for group name " + userMenuGroupByIDs.UserMenuGroupName);
            return Json(true);
        }

        [HttpPost]
        public ActionResult AddGroup(GroupViewModel model)
        {
            if (model.UserMenuGroup.UserMenuGroupID == 0)
            {
                model.UserMenuGroup.OrganizationID = HCRGCLIENT.OrganizationID;
                int userGroupID = _userService.AddUserMenuGroup(Mapper.Map<HCRGUniversityMgtApp.NEPService.UserService.UserMenuGroup>(model.UserMenuGroup));
                HCRGUniversityMgtApp.NEPService.UserService.UserMenuPermission _userMenuPermission = new NEPService.UserService.UserMenuPermission();
                _userMenuPermission.MenuIDs = model.UserMenuPermission.MenuIDs;
                _userMenuPermission.UserMenuGroupID = userGroupID;
                _userService.AddUserMenuPermission(_userMenuPermission);
            }
            else
            {
                HCRGUniversityMgtApp.NEPService.UserService.UserMenuPermission _userMenuPermission = new NEPService.UserService.UserMenuPermission();
                _userMenuPermission.UserMenuPermissionID = model.UserMenuPermission.UserMenuPermissionID;
                _userMenuPermission.MenuIDs = model.UserMenuPermission.MenuIDs;
                _userMenuPermission.UserMenuGroupID = model.UserMenuGroup.UserMenuGroupID;
                _userService.UpdateUserMenuPermission(_userMenuPermission);
            }
            return RedirectToAction("/Index");
        }
    }
}