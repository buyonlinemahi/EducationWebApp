function RegisterViewModel() {
    var self = this;
    self.FirstName = ko.observable("sdd");
    self.LastName = ko.observable();
    self.Password = ko.observable();
    self.EmailID = ko.observable();
    self.Company = ko.observable();
    self.Phone = ko.observable();
    self.ProfessionalTitle = ko.observable();
    self.IsActive = ko.observable();
    self.DisableAddButton = ko.observable(false);
    this.Init = function (model) {
        hideloderDiv();
        if (model != null) {
            if (model.User != null) {
                if (model.User.EmailID != null && model.User.Password != null) {
                    $("#exampleInputEmail31").val(model.User.EmailID);
                    $("#exampleInputPassword14").val(model.User.Password);
                    if (model.InvalidMsg == "Invalid User Name or Password") {
                        document.getElementById("Remember").checked = false;
                        $("#exampleInputEmail31").val("");
                        $("#exampleInputPassword14").val("");
                    }
                    else {
                        document.getElementById("Remember").checked = true;
                    }
                }
                
            }
        }
    }
    self.AddUserDetailSuccess = function (responseText) {
        hideloderDiv();
        alertify.alert($.parseJSON(responseText));
       
       };
   
    self.UserInfoFormBeforeSubmit = function (arr, $form, options) {                
        if ($("#frmAddUser").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        showloderDiv();
        return true;
    };    
    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }
    function showloderDiv() {
        $("#loaderDiv").addClass('loaderbg');
        $("#loading").show();
    }
    
   
    $("#emailIDExists").bind("focusout", function () {
        var valueData = $("#emailIDExists").val();
        if (valueData != "") {
            $("#imgLoader").show();
            $.getJSON("/User/UserExists", { value: valueData }, function (data) {
                if (data == "Yes") {
                    $("#messageblock").show();
                    $("#BtnAdding").attr('disabled', 'disabled');
                }
                else {
                    $("#messageblock").hide();
                    $("#BtnAdding").removeAttr('disabled');
                }
                $("#imgLoader").hide();               
            });
        };
    });
};
function LoginViewModel() {
    
    self.UserLoginInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmLoginUser").jqBootstrapValidation('hasErrors')) {
            return false;
        }
       
        return true;
    };
    $("#forgetpassword").click(function () {
        $("#Message").html("")
        $("#frmReset")[0].reset();
        $("#frmReset :input").jqBootstrapValidation();
    });

};
function ResetViewModel() {

    self.ResetPasswordSuccess = function (responseText) {
        if ($.parseJSON(responseText) == "Yes") {
            hideloderDiv();
            alertify.alert("Password has been sent to your email address.", function () {
                $("#btnClose").click();
            });
        }
        else if ($.parseJSON(responseText) == "No") {
            hideloderDiv();
            alertify.alert("Email Id does not exist");
        }
        else {
            hideloderDiv();
            alertify.alert(responseText);
        }
    };

    self.ResetPasswordFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmReset").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };

    $("#btnClose").click(function () {
        $("#Message").html("")
        $("#frmReset")[0].reset();
        $("#frmReset :input").jqBootstrapValidation("destroy");      
    });

    $("#resetpass").click(function () {
        if ($("#inputResetEmail").val() != "") {
            showloderDiv();
            $("#Message").html("")
            $("#frmReset :input").jqBootstrapValidation("destroy");
        }
    });

    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }

    function showloderDiv() {
        $("#loaderDiv").addClass('loaderbg');
        $("#loading").show();
    }
};



