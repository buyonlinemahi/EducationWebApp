function OrganizationContactUsViewModel() {
    var self = this;
    self.ClientID = ko.observable();
    self.ContactID = ko.observable();
    self.Phone = ko.observable();
    self.Fax = ko.observable();
    self.EmailID = ko.observable();
    self.Address = ko.observable();
    self.Address2 = ko.observable();
    self.City = ko.observable();
    self.StateID = ko.observable();
    self.Zip = ko.observable();
    self.OperationHour = ko.observable();
    self.StartTime = ko.observable();
    self.EndTime = ko.observable();
    self.selectedState = ko.observable(0);
    self.OrganizationName = ko.observable();
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.OrganizationName = ko.observable();
    self.StateName = ko.observable();

    this.Init = function (model) {
        if (model != null) {           
            ko.mapping.fromJS(model, {}, self);
         };      
    }
}




