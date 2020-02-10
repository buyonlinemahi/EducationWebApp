function CertificationnGridViewModel() {
    var self = this;
    self.CertificationID = ko.observable();
    self.CertificationContent = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);

    self.OrganizationID = ko.observable();
    self.OrganizationName = ko.observable();
    self.CertificationData = ko.observableArray([]);

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    self.IsHCRGAdmin = ko.observable();
    this.Init = function (model) {
        if (model != null) {
            if (model.CertificationResults.length != 0) {
                $("#divCertificationData").hide();
            }
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $.each(model.CertificationResults, function (index, value) {
                    self.CertificationData.push(new Certification(value));
                });

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

    self.SearchCertification = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/Certification/GetAllCertificationsByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (result) {
            self.CertificationData.removeAll();
            $.each(result.CertificationResults, function (index, value) {
                self.CertificationData.push(new Certification(value));
            });


        });
    };

    function Certification(value) {
        var self = this;
        self.CertificationID = ko.observable(value.CertificationID);
        self.CertificationContent = ko.observable(value.CertificationContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };

    self.EditCertificationLineItems = function (config) {
        self.CertificationID(config.CertificationID());
        self.CertificationContent(config.CertificationContent());
        self.DescriptionShort(config.DescriptionShort());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("EditorCertification").editor;
        editor1.SetText(($("<div>").html(config.CertificationContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divCertificationData").show();
    }

    self.CertificationDetailSuccess = function (model) {
        if (model != null) {
            $("#divCertificationData").hide();
            var nreCertification = $.parseJSON(model);
            var viewModel = self.CertificationData;
            if (!nreCertification.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].CertificationID() == nreCertification.CertificationID) {
                        self.CertificationData.splice(i, 1);
                        self.CertificationData.splice(i, 0, new InsertCertificationLineItem(nreCertification.CertificationID, nreCertification.CertificationContent, nreCertification.DescriptionShort, nreCertification.OrganizationName, nreCertification.OrganizationID));
                        alertify.success("Certification Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.CertificationData.splice(i, 0, new InsertCertificationLineItem(nreCertification.CertificationID, nreCertification.CertificationContent, nreCertification.DescriptionShort, nreCertification.OrganizationName, nreCertification.OrganizationID));
                $("#main").scrollTop(0);
                alertify.success("Certification Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };


    function InsertCertificationLineItem(_CertificationID, _CertificationContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.CertificationID = ko.observable(_CertificationID);
        self.CertificationContent = ko.observable(_CertificationContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.CertificationID("");
        self.CertificationContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("EditorCertification").editor;
        editor1.SetText("");
    }

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

    self.CertificationInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmCertification").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorCertification").val()).html());//encoding
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

}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorCertification").val()).html());//encoding
}

