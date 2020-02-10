function PrivacyPolicyGridViewModel() {
    var self = this;
    self.PrivacyPolicyID = ko.observable();
    self.PrivacyPolicyContent = ko.observable();
    self.DescriptionShort = ko.observable();
     self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.PrivacyPolicyData = ko.observableArray([]);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.IsHCRGAdmin = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            if (model.PrivacyPolicyResults.length != 0) {
                $("#divAddPrivacyData").hide();
            }
              if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $.each(model.PrivacyPolicyResults, function (index, value) {
                    self.PrivacyPolicyData.push(new PrivacyPolicy(value));
                });

                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
        getAllOrganization(0);
    };

    function getAllOrganization() {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                self.AllOrganizations.removeAll();
                self.AllOrganizations(data.slice());
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }


    self.SearchPrivacyPolicy = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/HomeContent/GetPrivacyPolicyByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (data) {
            self.PrivacyPolicyData.removeAll();
            $.each(data.PrivacyPolicyResults, function (index, value) {
                self.PrivacyPolicyData.push(new PrivacyPolicy(value));
            });

        });
    };

    self.PrivacyPolicyDetailSuccess = function (model) {
        if (model != null) {
            $("#divAddPrivacyData").hide();
            var nreReturnCondtion = $.parseJSON(model);
            var viewModel = self.PrivacyPolicyData;
            if (!nreReturnCondtion.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].PrivacyPolicyID() == nreReturnCondtion.PrivacyPolicyID) {
                        self.PrivacyPolicyData.splice(i, 1);
                        self.PrivacyPolicyData.splice(i, 0, new InsertReturnPolicyLineItem(nreReturnCondtion.PrivacyPolicyID, nreReturnCondtion.PrivacyPolicyContent, nreReturnCondtion.DescriptionShort, nreReturnCondtion.OrganizationName, nreReturnCondtion.OrganizationID));
                        alertify.success("Privacy Policy Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.PrivacyPolicyData.splice(i, 0, new InsertReturnPolicyLineItem(nreReturnCondtion.PrivacyPolicyID, nreReturnCondtion.PrivacyPolicyContent, nreReturnCondtion.DescriptionShort, nreReturnCondtion.OrganizationName, nreReturnCondtion.OrganizationID));
                $("#main").scrollTop(0);
                alertify.success("Privacy Policy Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };
    
    self.PrivacyPolicyInfoFormBeforeSubmit = function (arr, $form, options) {     
    
        var string = arr[3].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#EditorPrivacy").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorPrivacy").val()).html());//encoding
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

    self.EditPrivacyPolicyLineItems = function (config) {
        self.PrivacyPolicyID(config.PrivacyPolicyID());
        self.PrivacyPolicyContent(config.PrivacyPolicyContent());
        self.DescriptionShort(config.DescriptionShort());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("EditorPrivacy").editor;
        editor1.SetText(($("<div>").html(config.PrivacyPolicyContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divAddPrivacyData").show();
    }
    function PrivacyPolicy(value) {
        var self = this;
        self.PrivacyPolicyID = ko.observable(value.PrivacyPolicyID);
        self.PrivacyPolicyContent = ko.observable(value.PrivacyPolicyContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };


    function InsertReturnPolicyLineItem(_PrivacyPolicyID, _PrivacyPolicyContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.PrivacyPolicyID = ko.observable(_PrivacyPolicyID);
        self.PrivacyPolicyContent = ko.observable(_PrivacyPolicyContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.PrivacyPolicyID("");
        self.PrivacyPolicyContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("EditorPrivacy").editor;
        editor1.SetText("");
    }
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorPrivacy").val()).html());//encoding
}

