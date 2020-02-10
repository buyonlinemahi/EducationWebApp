function ReturnPolicyViewModel() {
    var self = this;
    self.ReturnPolicyID = ko.observable();
    self.ReturnPolicyContent = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.ReturnPolicyData = ko.observableArray([]);

    this.Init = function (model) {
        if (model != null) {
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                $.each(model.ReturnPolicyResutls, function (index, value) {
                    self.ReturnPolicyData.push(new ReturnPolicy(value));
                });

                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
    };
    self.ReturnPolicyDetailSuccess = function (model) {        
        if (model != null) {
            var nreReturnCondtion = $.parseJSON(model);
            var viewModel = self.ReturnPolicyData;
            if (!nreReturnCondtion.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].ReturnPolicyID() == nreReturnCondtion.ReturnPolicyID) {
                        self.ReturnPolicyData.splice(i, 1);
                        self.ReturnPolicyData.splice(i, 0, new InsertReturnPolicyLineItem(nreReturnCondtion.ReturnPolicyID, nreReturnCondtion.ReturnPolicyContent, nreReturnCondtion.DescriptionShort));
                        alertify.success("Return Policy Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.ReturnPolicyData.splice(i, 0, new InsertReturnPolicyLineItem(nreReturnCondtion.ReturnPolicyID, nreReturnCondtion.ReturnPolicyContent, nreReturnCondtion.DescriptionShort));
                $("#main").scrollTop(0);
                alertify.success("Return PolicyInsert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };

    self.ReturnPolicyInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[1].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "")
        {
         alertify.alert("Content cannot be empty!");
         return false;
     }
        
    if ($("#frmReturnPolicy").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorReturnPolicy").val()).html());//encoding
        return true;
    };
    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });

    $(".btnClose").click(function () {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });


    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    ko.mapping.fromJS(model, {}, self);
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }

    self.EditReturnPolicyLineItems = function (config) {
        self.ReturnPolicyID(config.ReturnPolicyID());
        self.ReturnPolicyContent(config.ReturnPolicyContent());
        var editor1 = document.getElementById("EditorReturnPolicy").editor;
        editor1.SetText(($("<div>").html(config.ReturnPolicyContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
    }
    function ReturnPolicy(value) {
        var self = this;
        self.ReturnPolicyID = ko.observable(value.ReturnPolicyID);
        self.ReturnPolicyContent = ko.observable(value.ReturnPolicyContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
    };


    function InsertReturnPolicyLineItem(_ReturnPolicyID, _ReturnPolicyContent, _DescriptionShort) {
        var self = this;
        self.ReturnPolicyID = ko.observable(_ReturnPolicyID);
        self.ReturnPolicyContent = ko.observable(_ReturnPolicyContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
    }

    function resetcontrol() {
        self.ReturnPolicyID("");
        self.ReturnPolicyContent("");
        var editor1 = document.getElementById("EditorReturnPolicy").editor;
        editor1.SetText("");
    }
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorReturnPolicy").val()).html());//encoding
}