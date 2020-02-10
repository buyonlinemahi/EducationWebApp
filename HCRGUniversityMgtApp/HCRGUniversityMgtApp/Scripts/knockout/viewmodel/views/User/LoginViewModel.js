//function LoginViewModel() {
    $(document).ready(function () {
        $("#btnClose").click(function () {
            $("#Message").html("")
        });
        $("#loaderDiv").hide();
        $("#resetpass").click(function () {

            if ($('#txtUserEmail').val() == "") {

                alertify.alert("Please Enter User Registered Email.", function (e) {
                    $("#txtUserEmail").focus();
                });
            }
            else {
                $("#loaderDiv").show();
                showloderDiv()
                $.ajax({
                    url: '/Login/ResetPassword',
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ Email: $('#txtUserEmail').val() }),
                    success: function (data) {
                        if (data == "Yes") {
                            alertify.alert("User password has been Sent. Please check your registered email.", function (e) {
                                $("#PasswordModal").modal('hide');
                                $("#txtUserEmail").val("");
                            });
                        }
                        else if (data == "InvalidRegisteredEmail")
                            alertify.alert("Please Enter User Registered Email Only.", function (e) {
                                $("#txtUserEmail").focus();
                            });
                    },
                    error: function (data) {
                        alert('Error while deleting a item - ' + data);
                    }
                });
                hideloderDiv();
            }
        });

    });

    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }
    function showloderDiv() {
        $("#loading").show();
        $("#loaderDiv").addClass('loaderbg');
    }
//}