function SearchOrgContactViewModel() {
    var self = this;
    self.EncryptedOrganizationContactID = ko.observable();
    self.IsHCRGAdmin = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.Init = function (model) {
        self.IsHCRGAdmin(model.IsHCRGAdmin);
        ko.mapping.fromJS(model, {}, self);
        getAllOrganization();
        if (model != null) {
            if (model.OrganizationContactResults.length != 0) {
                $("#divContact").hide();               
            }

        }
    };
    

    self.SearchOrgContact = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/Organization/GetOrgContactByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
            self.OrganizationContactResults.removeAll();
            ko.mapping.fromJS(data, {}, self.OrganizationContactResults);
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
      

    self.AddNewContactUs = function () {
        window.location = '/Organization/OrganizationContact';

    }

    self.updateContact = function (model) {
        window.location = '/Organization/EditOrganizationContact?_ID=' + model.EncryptedOrganizationContactID();
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
}