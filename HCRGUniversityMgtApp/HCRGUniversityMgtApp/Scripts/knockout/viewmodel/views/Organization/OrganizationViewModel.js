function OrganizationViewModel(model) {
    var self = this;

    self.OrganizationID = ko.observable();
    self.EncryptedOrganizationID = ko.observable();
    self.OrganizationName = ko.observable();
    self.WebsiteName = ko.observable();
    self.ThemeID = ko.observable();
    self.RegisteredPaypalEmailID = ko.observable();
    self.LogoImage = ko.observable();
    self.LogoPath = ko.observable();
    self.OrganizationImageFile = ko.observable();
    self.FaviconImageFile = ko.observable();
    self.FaviconImage = ko.observable();
    self.Message = ko.observable();
    self.OrganizationResults = ko.observableArray([]);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
                                                
    
    if (model != null) {
        ko.mapping.fromJS(model, {}, self);
        self.IsHCRGAdmin(model.IsHCRGAdmin);
    }

    self.Add = function () {
        window.location = '/Organization/Detail?_OrganizationID=';
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
//***************************Edit Organization***********************************///
    self.edit = function (model) {
        window.location = '/Organization/Detail?_OrganizationID=' + model.EncryptedOrganizationID();       
    }
//***************************************************************************************************//

    self.addNewOrg = function () {
        window.location = '/Organization/OrganizationContact';
    }
   
}
