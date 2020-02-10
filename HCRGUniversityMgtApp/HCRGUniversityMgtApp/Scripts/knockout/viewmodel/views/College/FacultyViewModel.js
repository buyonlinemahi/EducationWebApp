function FacultyViewModel() {
    var self = this;
    self.FacultyID = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.Email = ko.observable();
    self.Company = ko.observable();
    self.Phone = ko.observable();
    self.ProfessionalTitle = ko.observable();
    self.AddressStreet = ko.observable();
    self.AddressFloor = ko.observable();
    self.City = ko.observable();
    self.State = ko.observable();
    self.Zip = ko.observable();
    self.AreaOfExpertise = ko.observable();
    self.Topics = ko.observable();
    self.Resume = ko.observable();
    self.ResumeLink = ko.observable();
    self.CreatedDate = ko.observable();
    self.FullName = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.FacultySearchResults = ko.observableArray();
    self.TotalItemCount = ko.observable(0);

  

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.IsHCRGAdmin = ko.observable();

    var mappingOptions = {
        'CreatedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
  
  
    this.Init = function (model) {
        if (model.FacultyDetails != null) {
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            ko.mapping.fromJS(model.FacultyDetails, mappingOptions, self.FacultySearchResults);
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);            
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


    self.SearchFaculty = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/College/GetAllFacultiesByOrganizationID/", { skip: 0, OrganizationID: self.SearchSelectedOrganization() }, function (data) {            
            self.FacultySearchResults.removeAll();
            ko.mapping.fromJS(data.FacultyDetails, mappingOptions, self.FacultySearchResults);
            self.TotalItemCount(data.TotalCount);
            self.Pager().CurrentPage(1);

        });
    };



    self.FacultyDetailByID = function (dataResult) {
        var _facultyID = dataResult.FacultyID();
        $.post("/College/GetFacultyByID", {
            ID: _facultyID
        }, function (_data) {
            var model = _data;
           
            self.FacultyID(model.FacultyID);
            self.FirstName(model.FirstName);
            self.LastName(model.LastName);
            self.Email(model.Email);
            self.Company(model.Company);
            self.Phone(model.Phone);
            self.ProfessionalTitle(model.ProfessionalTitle);
            self.AddressStreet(model.AddressStreet);
            self.AddressFloor(model.AddressFloor);
            self.City(model.City);
            self.State(model.State);
            self.Zip(model.Zip);
            self.AreaOfExpertise(model.AreaOfExpertise);
            self.Topics(model.Topics);          
            if (model.Resume != null) {
                self.Resume(model.Resume);
                self.ResumeLink(_data.ResumeLink);
            }
            else
                self.Resume("");
            self.CreatedDate(moment(model.CreatedDate).format("MM/DD/YYYY"));
            
            
        });
    };

    ko.bindingHandlers.formatNumberText = {
        update: function (element, valueAccessor) {
            var phone = ko.utils.unwrapObservable(valueAccessor());
            var formatPhone = function () {
                return phone.replace(/(\d{3})(\d{3})(\d{4})/, "($1)-$2-$3");
            }
            ko.bindingHandlers.text.update(element, formatPhone);
        }
    };


    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.post("/College/getAllFaculties", {
            skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.FacultySearchResults.removeAll();
            ko.mapping.fromJS(model.FacultyDetails, mappingOptions, self.FacultySearchResults);
            self.TotalItemCount(model.TotalCount);            
        });
    };

    var pagingSettings = {
        pageSize: 15,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });
   
};