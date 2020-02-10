function UserSubscriptionResultViewModel() {


    var self = this;

    self.EncryptedUserSubscriptionID = ko.observable();
    self.AllAccessPassPricing = ko.observable();
    self.AllAccessParametersCoursePriceStart = ko.observable();
    self.AllAccessParametersCoursePriceEnd = ko.observable();
    self.AllAccessIncludeProgram = ko.observable(false);
    self.AllAccessTermsOfService = ko.observable();
    self.EnterprisePricing = ko.observable();
    self.EnterpriseTermsOfService = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
        }
    };
    self.UserSubscriptionDetailSuccess = function (model) {
        if (model != null) {
            var data = $.parseJSON(model);
            if (data.Mode == "Add") {
                alertify.alert("Added Successfully", function (e) {
                    if (e) {
                        redirect();
                    }
                });               
             }
            else {                
                alertify.alert("Updated Successfully", function (e) {
                    if (e) {
                        redirect();
                    }
                });
              
              }

            //ko.mapping.fromJS(data, {}, self);
            //this.Init(data);
        }
       resetcontrol();
    };

    function redirect() {
        window.location = '/UserSubscription/AllSubcriptions';
    }

   

    self.UserSubscriptionInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmUserSubscription").jqBootstrapValidation('hasErrors')) {
            return false;
        }

        $("#spanAllAccessParametersCoursePriceStart").html("");
        $("#spanAllAccessParametersCoursePriceEnd").html("");

        var _aapStart = $('#AllAccessParametersCoursePriceStart').val();
        if ((_aapStart != "") && (_aapStart != "0")) {
            var _aapEnd = $('#AllAccessParametersCoursePriceEnd').val();
            if ((_aapEnd != "") && (_aapEnd != "0")) {
                if (parseInt(_aapStart) > parseInt(_aapEnd)) {
                    $('#AllAccessParametersCoursePriceStart').val('');
                    $('#AllAccessParametersCoursePriceStart').focus();
                    //alertify.alert("Enter All Access Parameters Course Price Start is less then Price End amount");
                    $("#spanAllAccessParametersCoursePriceStart").html("Enter All Access Parameters Course Price Start is less then Price End amount.");
                    return false;
                }
            }
        }

        var _aapEnd = $('#AllAccessParametersCoursePriceEnd').val();
        if ((_aapEnd != "") && (_aapEnd != "0")) {
            var _aapStart = $('#AllAccessParametersCoursePriceStart').val();
            if ((_aapStart != "") && (_aapStart != "0")) {
                if (parseInt(_aapEnd) < parseInt(_aapStart)) {
                    $('#AllAccessParametersCoursePriceEnd').val('');
                    $('#AllAccessParametersCoursePriceEnd').focus();
                    //alertify.alert("Enter All Access Parameters Course Price End is less then Price Start amount");
                    $("#spanAllAccessParametersCoursePriceEnd").html("Enter All Access Parameters Course Price End is more then Price Start amount.");
                    return false;
                }
            }
        }


        $("#hdAllAccessTermsOfService").val($("<div>").text($("#Editor1").val()).html());//encoding
        $("#hdEnterpriseTermsOfService").val($("<div>").text($("#Editor2").val()).html());//encoding
        return true;
    };
    $("#AllAccessTermsOfServiceModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });

    $("#EnterpriseTermsOfServiceEditorModalPopUp").click(function () {
        $("#EditorModal2").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal2").addClass("in");
    });

    $(".btnClose").click(function () {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

    $(".btnClose2").click(function () {
        $('#EditorModal2').modal('hide');
        $("#EditorModal2").css("display", "none");
        $("#EditorModal2").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });
    
}

$(document).ready(function () {
    $("#AllAccessParametersCoursePriceStart").change(function () {
        $("#spanAllAccessParametersCoursePriceStart").html("");
        var _aapStart = $('#AllAccessParametersCoursePriceStart').val();
        if ((_aapStart != "") && (_aapStart != "0")) {
            var _aapEnd = $('#AllAccessParametersCoursePriceEnd').val();
            if ((_aapEnd != "") && (_aapEnd != "0")) {
                if (parseInt(_aapStart) > parseInt(_aapEnd)) {
                    $('#AllAccessParametersCoursePriceStart').val('');
                    $('#AllAccessParametersCoursePriceStart').focus();
                    //alertify.alert("Enter All Access Parameters Course Price Start is less then Price End amount");
                    $("#spnaAllAccessParametersCoursePriceStart").html("Enter All Access Parameters Course Price Start is less then Price End amount.");
                    return false;
                }
            }
        }
    });

    $("#AllAccessParametersCoursePriceEnd").change(function () {
        $("#spanAllAccessParametersCoursePriceEnd").html("");
        var _aapEnd = $('#AllAccessParametersCoursePriceEnd').val();
        if ((_aapEnd != "") && (_aapEnd != "0")) {
            var _aapStart = $('#AllAccessParametersCoursePriceStart').val();
            if ((_aapStart != "") && (_aapStart != "0")) {
                if (parseInt(_aapEnd) < parseInt(_aapStart)) {
                    $('#AllAccessParametersCoursePriceEnd').val('');
                    $('#AllAccessParametersCoursePriceEnd').focus();
                    //alertify.alert("Enter All Access Parameters Course Price End is less then Price Start amount");
                    $("#spanAllAccessParametersCoursePriceEnd").html("Enter All Access Parameters Course Price End is less then Price Start amount.");
                    return false;
                }
            }
        }
    });

    function resetcontrol() {
        self.AllAccessPassPricing("");
        self.AllAccessParametersCoursePriceStart("");
        self.AllAccessParametersCoursePriceEnd("");
        self.AllAccessIncludeProgram("");
        self.AllAccessTermsOfService("");
        self.EnterprisePricing("");
        self.EnterpriseTermsOfService("");
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText("");
        var editor1 = document.getElementById("Editor2").editor;
        editor1.SetText("");
    }
});

function submitform() {
    $("#hdAllAccessTermsOfService").val($("<div>").text($("#Editor1").val()).html());//encoding
    $("#hdEnterpriseTermsOfService").val($("<div>").text($("#Editor2").val()).html());//encoding
}