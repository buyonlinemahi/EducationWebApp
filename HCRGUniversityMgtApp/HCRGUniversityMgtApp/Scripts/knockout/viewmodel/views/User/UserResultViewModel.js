function UserGridViewModel() {
    var self = this;
    self.OrganizationID = ko.observable();
    self.ClientTypeName = ko.observable();
    self.ClientTypeID = ko.observable();
    self.UID = ko.observable();
    self.LastLoginDate = ko.observable();
    self.userResults = ko.observableArray([]);
    self.UserData = ko.observableArray([]);
    self.searchText = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.IsManagement = ko.observable();
    self.IsCoursePreview = ko.observable();
    self.UserMenuGroupID = ko.observable();

    self.selectedGroup = ko.observable(0);
    self.Groups = ko.observableArray();
    self.Groups = ko.observableArray([self.Groups(0)]);

    self.Menus = ko.observableArray();
    self.Menus = ko.observableArray([self.Menus(0)]);    
    self.MenuID = ko.observable();
    self.MenuName = ko.observable();
    self.selectedMenu = ko.observableArray();
    //---------update user menu model
    self.UpdateSelectedGroup = ko.observable();
    self.UpdateSelectedMenu = ko.observableArray();
    //-------------------------------
    self.ClientResults = ko.observableArray();
    self.Clients = ko.observableArray();

    self.UserMenuGroupAndPremission = ko.observableArray();

   



    //**************Client Model ****************************//
    self.ClientID = ko.observable();
    self.EncryptedClientID = ko.observable();
    self.OrganizationID = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.EmailID = ko.observable();
    self.RegisteredPaypalEmailID = ko.observable();
    self.Address = ko.observable();
    self.Address2 = ko.observable();
    self.City = ko.observable();
    self.StateID = ko.observable();
    self.Zip = ko.observable();
    self.Phone = ko.observable();
    self.Password = ko.observable();
    self.IsActive = ko.observable();
    self.IsApproved = ko.observable();
    self.IsEmailSent = ko.observable();
    self.ClientTypeID = ko.observable();

    self.ClientResults = ko.observableArray();
    self.Clients = ko.observableArray();

    self.selectedClient = ko.observable(0);
    self.Clientdropdownlists = ko.observableArray();
    self.Clientdropdownlists = ko.observableArray([self.Clientdropdownlists(0)]);

    self.selectedState = ko.observable(0);
    self.StatesClient = ko.observableArray();
    self.StatesClient = ko.observableArray([self.StatesClient(0)]);

    self.selectedOrganization = ko.observable(0);
    self.Organizations = ko.observableArray();
    self.Organizations = ko.observableArray([self.Organizations(0)]);
    //=============== Search page Drop Downs=======================//
    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.SearchSelectedClientType = ko.observable(0);
    self.AllClientTypes = ko.observableArray();
    self.AllClientTypes = ko.observableArray([self.AllClientTypes(0)]);
    //============================================================//


    self.selectedClientType = ko.observable(0);
    self.ClientTypes = ko.observableArray();
    self.ClientTypes = ko.observableArray([self.ClientTypes(0)]);
    self.IsHCRGAdmin = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            //ko.mapping.fromJS(model, {}, self.userResults);
            $('#loaderDiv').hide();
        }

        $.getJSON("/Common/getAllMenu", function (data) {
            self.Menus(data.slice());
        });
        self.IsHCRGAdmin(model.IsHCRGAdmin);
        getAllUserMenuGroupAndPremissionAccordingToOrganizationID();
        getAllUserMenuGroupByOrganizationID(0);
        getAllOrganization(0);
        getAllClientTypeInSearch();
        $("#DrpOrgID").val("");
        $("#hdfTypeID").val("");
        $("#hdfOrgID").val("");     
    };

    $(document).ready(function () {
        $("#btnAddNew").click(function () {
            ResetControl();
            ResetControlClient();
            $('#AddUserModalPopUp').modal('show');
            BindStatesDropDown(0);
            BindClientTypesDropDown(0);
            getAllOrganization(0);
            $("#btnResetStudentPwd").hide();
            $("#btnResetClientPwd").hide();
            $("#DrpOrganizationID").prop("disabled", false);
            $("#DrpClientTypeID").prop("disabled", false);
        });

    });
    $("#btnSearch").click(function () {
        $('#loaderDiv').show();
        SearchUserAndClient();

    });

    function getAllOrganization(val) {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (model) {
                self.Organizations.removeAll();
                self.AllOrganizations.removeAll();
                self.Organizations(model.slice());
                self.AllOrganizations(model.slice());
                if (val != 0) {
                    $("#DrpOrganizationID").val(val);
                    $("#hdfOrgID").val(val);
                    $("#DrpOrgID").val(val);
                }

            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }
     
    function getAllClientTypeInSearch() {
        $.ajax({
            url: "/Common/getAllClientTypeInSearch",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (model) {
                self.AllClientTypes(model.slice());
                self.AllClientTypes.push(new AddAllType());
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }


    function AddAllType() {
        var self = this;
        self.ClientTypeName = ko.observable("All");
        self.ClientTypeID = ko.observable(0);
    }
    $("#searchtext").keypress(function (e) {//To Search when enter is pressed
        var key = e.which;
        if (key == 13)  // the enter key code
        {
            $('#loaderDiv').show();
            SearchUserAndClient();
        }
    });
    //**************Client Model ****************************//


    //to select all checkboxes
    $('.selectall').click(function () {
        self.selectedMenu([]);
        if ($(this).is(':checked')) {
            for (i = 0; i < self.Menus().length; i++) {
                self.selectedMenu.push(parseInt(self.Menus()[i].MenuID));
            }
        }
        else {
            self.selectedMenu.push(1);
        }
    });

    //check if all are selected or not on ever check box click
    self.MenuChange = function (checkedValue) {
        if ($("#updateGroupMenus :input[name='MenuOptions']").length == $("input[name='MenuOptions']").length) {           
            $('.selectall').prop('checked', true);
        }
        else {
            self.CheckboxClickedOrChange(checkedValue);
            $('.selectall').prop('checked', false);
        }
        return true;
    };

    self.UserMenuGroupChanged = function (val)
    {
        getUserPermissionByGroupID(val.selectedGroup());
    }
    function getUserPermissionByGroupID(val) {
        $.getJSON("/User/GetUserPermissionByGroupId", { userMenuGroupID: val },
           function (menuVal) {
               self.selectedMenu([]);
               arr = menuVal.MenuIDs.split(',');
               for (i = 0; i < arr.length; i++) {
                   self.selectedMenu.push(parseInt(arr[i]));
               }
           });
    }
   
   
    function SearchUserAndClient() {
         if (model.IsHCRGAdmin == true) {
             if ($("#hdfOrgID").val() == "") {
                 alertify.alert("Please Select Atleast One Organization.");
                 $('#loaderDiv').hide();
                 return false;
             }
         }
         else
         {
             if ($("#hdfTypeID").val() == "") {
                 alertify.alert("Please Select Atleast One User Type.");
                 $('#loaderDiv').hide();
                 return false;
             }
         }
        var _orgID = $("#hdfOrgID").val() == undefined ? 0 : $("#hdfOrgID").val();
        var _clienttypeid = $("#hdfTypeID").val() == "" ? 0 : $("#hdfTypeID").val();
      
        $.ajax({
            url: "/User/SearchUserAndClientNameCriteria/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',            
            data: ko.toJSON( { OrganizationID: _orgID, ClientTypeID: _clienttypeid, SelectionTypeName: $("#DrpTypeID option:selected").text(), SearchText: $("#searchtext").val() }),
            success: function (model) {
                $('#userresult').show();
                self.userResults.removeAll();
                ko.mapping.fromJS(model, {}, self.userResults);
                $('#loaderDiv').hide();

            },
            error: function (data) {
                $("#DrpOrgID").focus();
                alertify.alert("Please select atleast one organization");
                return false;
                $('#loaderDiv').hide();         
               
            }
        });
    }
    
    function BindStatesDropDown(val) 
    {
        $.getJSON("/Common/getAllState", function (data) {
            self.StatesClient(data.slice());
            if (val != 0) {
                $("#StateID").val(val);
            }
        });
    }

    function BindClientTypesDropDown(val)
    {
        $.ajax({
            url: "/Common/getAllClientTypeWithoutHcrgAdmin",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (model) {
                self.ClientTypes(model.slice());
                if (val != 0) {
                    $("#DrpClientTypeID").val(val);
                }
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
       
    }
       
    self.UserStatus = ko.observableArray([{ StatusName: "Active", Status: 'true' }, { StatusName: "Deactive", Status: 'false' }]);

   
    ///////////////////////////////-----------------------For Checking User Groups and Permissions on checkbox change---------------------------------/////////////////////////////////
    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    self.CheckboxClickedOrChange = function (checkedValue) {
        var _userMenuGpID =0;
        var menuIDsAndPermissions = $("#hdnMenuIDs").val().split(',');
        var index = menuIDsAndPermissions.indexOf(checkedValue.toString());
        if (index > -1) {
            menuIDsAndPermissions.splice(index, 1);
        }
        else {
            menuIDsAndPermissions.splice(2, 0, checkedValue);
        }
        menuIDsAndPermissions = menuIDsAndPermissions.sort();

        $.each(self.UserMenuGroupAndPremission(), function (index, value) {
            if (value.MenuIDs == menuIDsAndPermissions) {
                _userMenuGpID = value.UserMenuGroupID;               
            }           
        });
        if (_userMenuGpID != 0) {
            $("#UserMenuGroupID").val(_userMenuGpID);
            $("#UserMenuGroupID").attr("required");
        }
        else {
            $("#UserMenuGroupID").val(0);
            $("#UserMenuGroupID").removeAttr("required");
        }
    }
    function getAllUserMenuGroupAndPremissionAccordingToOrganizationID() {
        $.ajax({
            url: "/User/GetAllUserMenuGroupAndPremissionAccordingToOrganizationID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                if (data != null) {
                    self.UserMenuGroupAndPremission(data);
                }
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }
    function getAllUserMenuGroupByOrganizationID(val) {
        $.getJSON("/User/GetAllUserMenuGroupByOrganizationID", function (data) {                 
            self.Groups(data.slice());
            if (val != 0) {                
                $("#UserMenuGroupID").val(val);
            }
        });
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllUser();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
   
  
    self.BindUserForUpdatefunction = function (itemToUpdate) {
        $('#loaderDiv').show();
        $('#AddUserModalPopUp').modal('show');
        if ((itemToUpdate.ClientTypeName() == "Student")) {
            $('#AddUserModel').show();
            $('#ClientModel').hide();
            $("#DrpOrganizationID").prop("disabled", true);
            $("#DrpClientTypeID").prop("disabled", true);
            $("#btnResetStudentPwd").removeAttr("style");
            $("#btnResetClientPwd").hide();
            $("#userIsActive").show();
            $("#userManagement").show();
            $("#coursePreview").show();
            $.ajax({
                url: "/User/UserDetailByID",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ userID: itemToUpdate.ID() }),
                cache: false,
                success: function (_model) {
                    BindUserModelFromGrid(_model);
                    $('#loaderDiv').hide();
                }
            })
           
        }
        else {
            $.ajax({
                url: "/Client/ClientDetailByID",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ ClientID: itemToUpdate.ID() }),
                cache: false,
                success: function (_model) {
                    BindClientModelFromGrid(_model);
                    $('#loaderDiv').hide();
                }
            })
           
            $("#btnResetClientPwd").removeAttr("style");
            $("#btnResetStudentPwd").hide();
            $("#DrpOrganizationID").prop("disabled", true);
            $("#DrpClientTypeID").prop("disabled", true);
            $('#AddUserModel').hide();
            $('#ClientModel').show();
            $("#userIsActive").hide();
            $("#userManagement").hide();
            $("#coursePreview").hide();
            
        }
    }
    self.UserInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmUpdateUser").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };

    self.AddUserInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($('#FirstNameAdd').val().trim() == "") {
            alertify.alert("Please delete spaces from First Name textbox.");
            return false;
        }
        if ($('#LastNameAdd').val().trim() == "") {
            alertify.alert("Please delete spaces from Last Name textbox.");
            return false;
        }
        if ($('#EmailIDAdd').val().trim() == "") {
            alertify.alert("Please delete spaces from EmailID textbox.");
            return false;
        }
        if ($('#CompanyAdd').val().trim() == "") {
            alertify.alert("Please delete spaces from Company textbox.");
            return false;
        }
        if ($('#PhoneAdd').val().trim() == "") {
            alertify.alert("Please delete spaces from Phone textbox.");
            return false;
        }
        if ($("#frmAddUser").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
        $("#loaderDiv").show();
    };
    self.AddUserDetailSuccess = function (responseText) {
        var data = $.parseJSON(responseText);
        if (data.Message == "AddUser") {
            alertify.success("User Information Added Successfully");
            $("#AddUserPoPupClose").click();
            $("#hdnMenuIDs").val("");
             SearchUserAndClient();
        }
        else if (data.Message == "UpdateUser")
        {
           //SearchUserAndClient();
            $("#AddUserPoPupClose").click();
            alertify.success("User Information Updated Successfully");
        }
        else {
            alertify.alert("User Email Allready Exists.");
            $('#FirstNameAdd').val(data.FirstName);
            $('#LastNameAdd').val(data.LastName);
            $('#EmailIDAdd').val(data.EmailID);
            $('#CompanyAdd').val(data.Company);
            $('#PhoneAdd').val(data.Phone);
            $('#ProfessionalTitleAdd').val(data.ProfessionalTitle);
            $('#EmailIDAdd').focus();
            $("#hdnMenuIDs").val("");
        }
        $('#loaderDiv').hide();
    };


    self.ResetClientPassword = function () {
        $.ajax({
            url: "/Client/ResetClientPassword",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ ClientId: $("#hiddenClientID").val() }),
            cache: false,
            success: function (_model) {
                if (_model == "Password Reset Sucessfully.")
                {
                    alertify.alert(_model);
                }
                else
                {
                    alertify.alert("Error occoured while sending email");
                }
            }
        })
    };

    self.ResetUserPassword = function () {
       
        $.ajax({
            url: "/User/ResetUserPassword",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ userId: $("#hdUID").val() }),
            cache: false,
            success: function (_model) {
                if (_model == "Password Reset Sucessfully.") {
                    alertify.alert(_model);
                   
                }
                else {
                    alertify.alert("Error occoured while sending email");
                }
            }
        })
    };

   

    $("#IsActiveClient").change(function () {
        var _status = $("#IsActiveHidden").val();
        if (_status == "true") {
            $("#IsActiveClient").prop("checked", false);
            $("#IsActiveHidden").val(false);
        }
        else {
            $("#IsActiveClient").prop("checked", true);
            $("#IsActiveHidden").val(true);
        }
    });

    $("#IsApproved").change(function () {
        var _status = $("#IsApprovedHidden").val();
        if (_status == "true") {
            $("#IsApproved").prop("checked", false);
            $("#IsApprovedHidden").val(false);
        }
        else {
            $("#IsApproved").prop("checked", true);
            $("#IsApprovedHidden").val(true);
        }
    });


    function ValidateEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };
    self.SaveClientDetails = function () {
          

        if ($("#OrganizationID").val() == 0) {
            $("OrganizationID").prop("required", "true");
            alert("Select Organization!");

        }
        else if ($("#txtFirstName").val().trim() == "") {
            $("txtFirstName").prop("required", "true");
            alert("First Name cannot be empty!");
        }
        else if ($("#txtLastName").val().trim() == "") {
            $("txtLastName").prop("required", "true");
            alert("Last Name  cannot be empty!");
        }
        else if ($("#txtEmail").val().trim() == "") {
            $("txtEmail").prop("required", "true");
            alert("Email cannot be empty!");
        }
        else if (!ValidateEmail($("#txtEmail").val())) {
            alert("Invalid email address.");
        }
        else if ($("#txtAddress").val().trim() == "") {
            $("txtAddress").prop("required", "true");
            alert("Address cannot be empty!");
        }
        else if ($("#txtCity").val().trim() == "") {
            $("txtCity").prop("required", "true");
            alert("City cannot be empty!");
        }
        else if ($("#StateID").val() == 0) {
            $("StateID").prop("required", "true");
            alert("Select State!");
        }
        else if ($("#txtZip").val().trim() == "") {
            $("txtZip").prop("required", "true");
            alert(" Zip cannot be empty!");
        }
        else if ($("#txtPhone").val().trim() == "") {
            $("txtPhone").prop("required", "true");
            alert("Phone cannot be empty!");
        }
        else if ($("#ClientTypeID").val() == 0) {
            $("ClientTypeID").prop("required", "true");
            alert("Select Client Type!");
        }
        else {
            $('#loaderDiv').show();
            var _saveClient = {
                OrganizationID: $("#hdfOrganizationID").val(),
                ClientID: $("#hiddenClientID").val(),
                FirstName: $("#txtFirstName").val(),
                LastName: $("#txtLastName").val(),
                EmailID: $("#txtEmail").val(),
                Address: $("#txtAddress").val(),
                Address2: $("#txtAddress2").val(),
                City: $("#txtCity").val(),
                StateID: $("#StateID").val(),
                ClientTypeID: $("#hdfClientTypeID").val(),
                Zip: $("#txtZip").val(),
                Phone: $("#txtPhone").val(),
                IsApproved: $("#IsApprovedHidden").val(),
                IsActive: $("#IsActiveHidden").val(),
                IsEmailSent : $('#hiddenIsEmailSent').val()
            }

            $.ajax({
                url: "/Client/SaveClient",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ _client: _saveClient }),
                cache: false,
                success: function (_model) {
                    if (_model == "The email you are trying to use already exists. We cannot create this account. If you think this is a mistake, please contact us at support@allgatelearning.com") {
                        alertify.alert(_model);
                        $('#loaderDiv').hide();
                    }
                    else if (_model == "Error occoured while sending email") {
                        alertify.alert(_model);
                        $('#loaderDiv').hide();
                    }
                    else {                       
                        
                        alertify.alert(_model);
                        $('#loaderDiv').hide();
                        SearchUserAndClient();
                        $("#AddUserPoPupClose").click();
                    }
                }
            });
        }

    }
    function ResetControl() {
        $('#FirstNameAdd').val("");
        $('#LastNameAdd').val("");
        $('#EmailIDAdd').val("");
        $('#CompanyAdd').val("");
        $('#PhoneAdd').val("");
        $('#ProfessionalTitleAdd').val("");
        $("#IsActive").prop("checked", false);
        $("#chkbtnManagement").prop("checked", false);
        $("#IsCoursePreview").prop("checked", false);
        $('#hdUID').val(0);

    }
    function ResetControlClient() {
        self.Organizations.removeAll();
        self.Clientdropdownlists.removeAll();
        self.ClientTypes.removeAll();

        $('#DrpOrganizationID').prop('selectedIndex', 0);
        $('#DrpClientID').prop('selectedIndex', 0);
        $('#DrpClientTypeID').prop('selectedIndex', 0);        
        $('#DrpOrganizationID').prop('selectedIndex', 0);
        $("#DrpOrgID").prop('selectedIndex', 0);
        $("#hdfOrgID").val(0);        
        self.FirstName("");
        self.LastName("");
        self.EmailID("");
        self.RegisteredPaypalEmailID("");
        self.Address("");
        self.Address2('');
        self.City("");
        $('#StateID').prop('selectedIndex', 0);
        self.Zip("");
        self.Phone("");
        $("#ClientModal").modal('hide');
        $("#IsActiveClient").prop("checked", false);
        $("#IsApproved").prop("checked", false);
        self.ClientID("");
        self.selectedClient("");
    }

    self.updateClient = function (model) {
        window.location = '/Client/ClientEdit?ClientID=' + model.EncryptedClientID();

    }
    $("#AddUserPoPupClose").click(function () {
        $("#hdfOrganizationID").val(0);
        $("#hdfClientID").val(0);
        $("#hdfClientTypeID").val(0);

        $('#AddUserModel').hide();
        $('#ClientModel').hide();
        $('#loaderDiv').hide();
        ResetControlClient();
        ResetControl();
    });
    self.OrganizationIDChanged = function (item) {
        if (item.selectedOrganization()) {
            $("#hdOrganizationID").val(item.selectedOrganization());
            $("#hdfOrganizationID").val(item.selectedOrganization());
            BindClientsAccordingToOrganization(item.selectedOrganization(),0);
        }
       
    }

   

    function BindClientsAccordingToOrganization(_OrganizationID, _ClientID) {
        $.ajax({
            url: "/Client/GetClientsByOrganizationID",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ OrganizationID: _OrganizationID }),
            cache: false,
            success: function (_model) {
                self.Clientdropdownlists.removeAll();
                if (_model != null) {
                    $("#DrpClientID").prop("disabled", false);
                    self.Clientdropdownlists(_model.slice());
                    if(_ClientID!=0)
                    $("#DrpClientID").val(_ClientID);
                }
                
            }
        });
    }
    self.SearchOrgChangedValue = function (item) {
        $("#hdfOrgID").val(item.SearchSelectedOrganization());
    }
    self.SearchUserTypeIDChanged = function(item)
    {
        $("#hdfTypeID").val(item.SearchSelectedClientType());
    }
    self.UserTypeIDChanged = function (item) {
        if (item.selectedClientType()) {
            if (item.selectedClientType() == 4) {
                $('#hdClientTypeID').val(item.selectedClientType());
                $('#hdfClientTypeID').val(item.selectedClientType());
                $('#AddUserModel').show();
                $('#ClientModel').hide();
                
            }
            else {
                $('#hdClientTypeID').val(item.selectedClientType());
                $('#hdfClientTypeID').val(item.selectedClientType());
                $('#AddUserModel').hide();
                $('#ClientModel').show();
            }
        }
        
    }

    self.ClientsIDChanged = function (item) {
        if (item.selectedClient()) {            
            $('#hdClientID').val(item.selectedClient());
            $('#hdfClientID').val(item.selectedClient());
            }
        }

    function BindClientModelFromGrid(data) {

        BindStatesDropDown(data.StateID);
        getAllOrganization(data.OrganizationID);
        BindClientTypesDropDown(data.ClientTypeID);
        BindClientsAccordingToOrganization(data.OrganizationID,0);

        $('#hdfOrganizationID').val(data.OrganizationID);
        $('#hdfClientTypeID').val(data.ClientTypeID);

        $('#hiddenClientID').val(data.ClientID);
        $('#txtFirstName').val(data.FirstName);
        $('#txtLastName').val(data.LastName);
        $('#txtEmail').val(data.EmailID);
        $('#txtAddress').val(data.Address);
        $('#txtAddress2').val(data.Address2);
        $('#txtCity').val(data.City);
        $('#txtZip').val(data.Zip);
        $('#txtPhone').val(data.Phone);
        $('#hiddenIsEmailSent').val(data.IsEmailSent);
        if (data.IsApproved == true) {
            $("#IsApproved").prop("checked", true);
            $("#IsApprovedHidden").val(true);
        }
        else {
            $("#IsApproved").prop("checked", false);
            $("#IsApprovedHidden").val(false);
        }


        if (data.IsActive == true) {
            $("#IsActiveClient").prop("checked", true);
            $("#IsActiveHidden").val(true);

        }
        else {
            $("#IsActiveClient").prop("checked", false);
            $("#IsActiveHidden").val(false);
        }
    }

    function BindUserModelFromGrid(data) {
        getAllOrganization(data.OrganizationID);
        BindClientTypesDropDown(4);
        BindClientsAccordingToOrganization(data.OrganizationID,data.ClientID);        

        
        getAllUserMenuGroupByOrganizationID(data.UserMenuGroupID);
        getUserPermissionByGroupID(data.UserMenuGroupID);


        $('#hdOrganizationID').val(data.OrganizationID);
        $('#hdClientID').val(data.ClientID);

        $('#FirstNameAdd').val(data.FirstName);
        $('#LastNameAdd').val(data.LastName);
        $('#EmailIDAdd').val(data.EmailID);
        $('#CompanyAdd').val(data.Company);
        $('#PhoneAdd').val(data.Phone);
        $('#ProfessionalTitleAdd').val(data.ProfessionalTitle);  
        $('#hdUID').val(data.UID);

        if (data.IsActive == true) {
            $('#IsActive').val("true");
        }
        else {
            $('#IsActive').val("false");
        }


        if (data.IsCoursePreview == true) {
            $("#chkbtnCoursePreview").prop("checked", true);
        }
        else {
            $("#chkbtnCoursePreview").prop("checked", false);
        }


        if (data.IsManagement == true) {
            $("#chkbtnManagement").prop("checked", true);
        }
        else {
            $("#chkbtnManagement").prop("checked", false);
        }
        
      
    }

};