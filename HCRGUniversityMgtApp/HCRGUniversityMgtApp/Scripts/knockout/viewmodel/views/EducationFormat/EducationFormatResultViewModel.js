function EducationFormatGridViewModel() {
    var self = this;
    self.EducationFormatID = ko.observable();
    self.EducationFormatType = ko.observable();
    self.EducationFormatResults = ko.observableArray([]);
    self.ClientID = ko.observable();
    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    var IsHcrgAdmin;

    this.Init = function (model) {
        if (model != null) {
            if (model.EducationFormatResults.length != 0) {
                $("#divAddEducationFormat").hide();
                if (model.IsHCRGAdmin == false) {
                    $("#divAddEducationFormat").show();
                }
            }
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            IsHcrgAdmin = model.IsHCRGAdmin;
            $.each(model.EducationFormatResults, function (index, value) {
                self.EducationFormatResults.push(new EducationFormat(value));
            });
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

    
    self.SearchEvaluationByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/EducationFormat/GetAllEducationFormatByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.EducationFormatResults.removeAll();
                ko.mapping.fromJS(data, {}, self.EducationFormatResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };

    self.AddEducationFormatDetailSuccess = function (model) {
        if (model != null) {         
            var neweducationformat = $.parseJSON(model);
            var viewModel = self.EducationFormatResults;
            if (!neweducationformat.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].EducationFormatID() == neweducationformat.EducationFormatID) {
                        self.EducationFormatResults.splice(i, 1);
                        self.EducationFormatResults.splice(i, 0, new InsertEducationFormatLineItem(neweducationformat.EducationFormatID, neweducationformat.EducationFormatType, neweducationformat.OrganizationName, neweducationformat.OrganizationID,neweducationformat.ClientID));
                        alertify.success("Education Format Information Updated Successfully");
                        if (IsHcrgAdmin == true) {
                            $("#divAddEducationFormat").hide();
                        }
                    }
                }
            }
            else {
                self.EducationFormatResults.push(new InsertEducationFormatLineItem(neweducationformat.EducationFormatID, neweducationformat.EducationFormatType,neweducationformat.OrganizationName,neweducationformat.OrganizationID,neweducationformat.ClientID));
                $('html, body').scrollTop($(document).height());
                alertify.success("Education Format Information Insert Successfully");
            }

            resetcontrol();
        }

    };
    self.EducationFormatInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmEducationFormat").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    
    function EducationFormat(value) {
        var self = this;
        self.EducationFormatID = ko.observable(value.EducationFormatID);
        self.EducationFormatType = ko.observable(value.EducationFormatType);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.ClientID = ko.observable(value.ClientID);;
    };
    self.ViewEducationFormatLineItems = function (config) {
        self.EducationFormatType(config.EducationFormatType());
        self.EducationFormatID(config.EducationFormatID());
        self.OrganizationName(config.OrganizationName());
        self.ClientID(config.ClientID());
        $("#divAddEducationFormat").show();
        $(window).scrollTop(0);
    }
    function resetcontrol() {
        self.EducationFormatID("");
        self.EducationFormatType("");
    }
};
function InsertEducationFormatLineItem(_EducationFormatID, _EducationFormatType, _OrganizationName,_ClientID) {
    var self = this;
    self.EducationFormatID = ko.observable(_EducationFormatID);
    self.EducationFormatType = ko.observable(_EducationFormatType);
    self.OrganizationName = ko.observable(_OrganizationName);
    self.ClientID = ko.observable(_ClientID);
}

