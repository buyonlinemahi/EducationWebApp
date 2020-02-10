function ChangePasswordViewModel() {
    self.ChangePasswordSuccess = function (responseText) {

        var msg = $.parseJSON(responseText)

        if (msg == "OTPNotMatched") {
            alertify.alert("Enterd OTP is incorrect.");
            return false;
        }
        else if (msg == "PasswordNotcorrect") {
            alertify.alert("Entered temporary password is incorrect.");
            return false;
        }
        else if (msg == "PasswordUpdated") {
            alertify.alert("Password Updated Successfully.", function () {
                window.location = '/Home/';
            });
        }
    };
    self.ChangePasswordFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmPassword").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };
};
$(document).ready(function () {
    //called when key is pressed in textbox
    $(".Numeric").keypress(function (e) {
        //if the letter is not digit then display error and don't type anything
        if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
            //display error message
            //$("#errmsg").html("Digits Only").show().fadeOut("slow");
            return false;
        }
    });
});