function TermsConditionGridViewModel() {
    var self = this;
   
    self.TermsConditionID = ko.observable();
    self.TermsConditionContent = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);


    self.TermConditionsData = ko.observableArray([]);
    this.Init = function (model) {
         if (model != null) {
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                $.each(model.TermConditionsResults, function (index, value) {
                    self.TermConditionsData.push(new TermConditon(value));
                });

                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
    };

    self.TermsConditionDetailSuccess = function (model) {
        if (model != null) {
            var newTermCondtion = $.parseJSON(model);
            var viewModel = self.TermConditionsData;
            if (!newTermCondtion.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].TermsConditionID() == newTermCondtion.TermsConditionID) {
                        self.TermConditionsData.splice(i, 1);
                        self.TermConditionsData.splice(i, 0, new InsertTermConditionLineItem(newTermCondtion.TermsConditionID, newTermCondtion.TermsConditionContent, newTermCondtion.DescriptionShort));
                        alertify.success("Term Conditon Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.TermConditionsData.splice(i, 0, new InsertTermConditionLineItem(newTermCondtion.TermsConditionID, newTermCondtion.TermsConditionContent, newTermCondtion.DescriptionShort));
                $("#main").scrollTop(0);
                alertify.success("Term Condition Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };

    self.TermsConditionInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[1].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#frmTermsCondition").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorPolicy").val()).html());//encoding
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

    self.EditTermConditionLineItems = function (config) {
        self.TermsConditionID(config.TermsConditionID());
        self.TermsConditionContent(config.TermsConditionContent());
        var editor1 = document.getElementById("EditorPolicy").editor;
        editor1.SetText(($("<div>").html(config.TermsConditionContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
    }

    function TermConditon(value) {
        var self = this;
        self.TermsConditionID = ko.observable(value.TermsConditionID);
        self.TermsConditionContent = ko.observable(value.TermsConditionContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
    };


    function InsertTermConditionLineItem(_TermsConditionID, _TermsConditionContent, _DescriptionShort) {
        var self = this;
        self.TermsConditionID = ko.observable(_TermsConditionID);
        self.TermsConditionContent = ko.observable(_TermsConditionContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
   }

    function resetcontrol() {
        self.TermsConditionID("");
        self.TermsConditionContent("");
        var editor1 = document.getElementById("EditorPolicy").editor;
        editor1.SetText("");
    }
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorPolicy").val()).html());//encoding
}