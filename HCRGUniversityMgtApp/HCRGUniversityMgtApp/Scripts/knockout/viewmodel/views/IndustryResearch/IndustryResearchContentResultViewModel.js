function IndustryResearchContentGridViewModel() {
    var self = this;
 

    self.IndustryResearchID = ko.observable();
    self.IndustryResearchContent = ko.observable();
    self.DescriptionShort = ko.observable();


    self.IndustryResearchData = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false)
    self.IsHCRGAdmin = ko.observable();

    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();
    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    this.Init = function (model) {
        if (model != null) {
            if (model.IndustryResearchResults.length != 0) {
                $("#divIndustryResearchData").hide();
            }
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $.each(model.IndustryResearchResults, function (index, value) {
                    self.IndustryResearchData.push(new IndustryResearch(value));
                });

                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
        getAllOrganization(0);
    };

    self.SearchIndustryResearch = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/HomeContent/GetIndustryResearchByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (data) {
            self.IndustryResearchData.removeAll();
            $.each(data.IndustryResearchResults, function (index, value) {
                self.IndustryResearchData.push(new IndustryResearch(value));
            });

        });
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
    self.IndustryResearchContentDetailSuccess = function (model) {
        if (model != null) {
            $("#divIndustryResearchData").hide();
            var nreIndustryResearch = $.parseJSON(model);
            var viewModel = self.IndustryResearchData;
            if (!nreIndustryResearch.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].IndustryResearchID() == nreIndustryResearch.IndustryResearchID) {
                        self.IndustryResearchData.splice(i, 1);
                        self.IndustryResearchData.splice(i, 0, new InsertIndustryResearchLineItem(nreIndustryResearch.IndustryResearchID, nreIndustryResearch.IndustryResearchContent, nreIndustryResearch.DescriptionShort, nreIndustryResearch.OrganizationName, nreIndustryResearch.OrganizationID));
                        alertify.success("IndustryResearch Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.IndustryResearchData.splice(i, 0, new InsertIndustryResearchLineItem(nreIndustryResearch.IndustryResearchID, nreIndustryResearch.IndustryResearchContent, nreIndustryResearch.DescriptionShort, nreIndustryResearch.OrganizationName, nreIndustryResearch.OrganizationID));
                $("#main").scrollTop(0);
                alertify.success("IndustryResearch Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };

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

    self.IndustryResearchContentInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[3].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#frmIndustryResearchContent").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding
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

    self.EditIndustryResearchLineItems = function (config) {
        self.IndustryResearchID(config.IndustryResearchID());
        self.IndustryResearchContent(config.IndustryResearchContent());
        self.DescriptionShort(config.DescriptionShort());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("Editor").editor;
        editor1.SetText(($("<div>").html(config.IndustryResearchContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divIndustryResearchData").show();

    }
    function IndustryResearch(value) {
        var self = this;
        self.IndustryResearchID = ko.observable(value.IndustryResearchID);
        self.IndustryResearchContent = ko.observable(value.IndustryResearchContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };


    function InsertIndustryResearchLineItem(_IndustryResearchID, _IndustryResearchContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.IndustryResearchID = ko.observable(_IndustryResearchID);
        self.IndustryResearchContent = ko.observable(_IndustryResearchContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.IndustryResearchID("");
        self.IndustryResearchContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("Editor").editor;
        editor1.SetText("");
    }

}
function submitform() {
    $("#hd").val($("<div>").text($("#Editor").val()).html());//encoding
}

