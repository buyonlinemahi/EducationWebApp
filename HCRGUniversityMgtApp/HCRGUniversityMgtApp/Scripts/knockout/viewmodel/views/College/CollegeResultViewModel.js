function CollegeGridViewModel() {
    var self = this;
    self.CollegeID = ko.observable();
    self.CollegeName = ko.observable();

    self.ClientID = ko.observable();
    self.Abbreviation = ko.observable();
    self.StartNumber = ko.observable();

    self.IsActive = ko.observable();

    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.CollegeResults = ko.observableArray([]);
    var IsHcrgAdmin;   
    
    this.Init = function (model) {       
        if (model != null) {
            if (model.CollegeResults.length != 0) {
                $("#divAddSubjectMatter").hide();
                if (model.IsHCRGAdmin == false) {
                    $("#divAddSubjectMatter").show();
                }
            }
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            IsHcrgAdmin = model.IsHCRGAdmin;
            $.each(model.CollegeResults, function (index, value) {
                self.CollegeResults.push(new College(value));
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


    
    self.SearchCollegeByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/College/GetAllCollegeByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.CollegeResults.removeAll();
                ko.mapping.fromJS(data, {}, self.CollegeResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };

    self.AddCollegeDetailSuccess = function (model) {
        if (model != null) {          
            var newcollege = $.parseJSON(model);

            var viewModel = self.CollegeResults;
            if (!newcollege.flag) {

                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].CollegeID() == newcollege.CollegeID) {
                        self.CollegeResults.splice(i, 1);
                        self.CollegeResults.splice(i, 0, new InsertCollegeLineItem(newcollege.CollegeID, newcollege.CollegeName, newcollege.IsActive, newcollege.Abbreviation, newcollege.StartNumber,newcollege.OrganizationName,newcollege.ClientID));
                        alertify.success("Updated Successfully");
                        if (IsHcrgAdmin == true) {
                            $("#divAddSubjectMatter").hide();
                        }
                    }
                }
            }
            else {
                self.CollegeResults.push(new InsertCollegeLineItem(newcollege.CollegeID, newcollege.CollegeName, newcollege.IsActive, newcollege.Abbreviation, newcollege.StartNumber,newcollege.OrganizationName,newcollege.ClientID));
                alertify.success("Inserted Successfully");
            }
            resetcontrol();
        }


    };
    function resetcontrol() {
        self.CollegeID("");
        self.CollegeName("");
        self.Abbreviation("");
        self.StartNumber("");
    }

    function InsertCollegeLineItem(_CollegeID, _CollegeName, _IsActive, _Abbreviation, _StartNumber, _OrganizationName, _ClientID) {
        var self = this;
        self.CollegeID = ko.observable(_CollegeID);
        self.IsActive = ko.observable(_IsActive);
        self.CollegeName = ko.observable(_CollegeName);
        self.Abbreviation = ko.observable(_Abbreviation);
        self.StartNumber = ko.observable(_StartNumber);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.ClientID = ko.observable(_ClientID);
    }

    self.CollegeInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddCollege").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };


    function College(value) {
        var self = this;
        self.CollegeID = ko.observable(value.CollegeID);
        self.CollegeName = ko.observable(value.CollegeName);
        self.Abbreviation = ko.observable(value.Abbreviation);
        self.StartNumber = ko.observable(value.StartNumber);
        self.IsActive = ko.observable(value.IsActive);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.ClientID = ko.observable(value.ClientID);
    };

    this.deleteCollege = function (itemToDelete) {
        alertify.confirm("Are you sure you want to change the Status?", function (e) {
            if (e) {
                $.ajax({
                    url: "/College/DeleteCollege",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ college: itemToDelete }),
                    success: function (data) {
                        // var data = $.parseJSON(model);
                        var viewModel = self.CollegeResults;
                        for (var i = 0; i <= viewModel().length - 1; i++) {
                            if (viewModel()[i].CollegeID() == data.CollegeID) {
                                self.CollegeResults.splice(i, 1);
                                self.CollegeResults.splice(i, 0, new InsertCollegeLineItem(data.CollegeID, data.CollegeName, data.IsActive, data.Abbreviation, data.StartNumber, data.OrganizationName, data.ClientID));
                                alertify.success("Status Updated  Successfully");
                            }
                        }
                    },
                    error: function (data) {
                        alert("Error while deleting a College - " + data);
                    }
                });
            }
        })
    }

    //  Edit 
    self.edit = function (College) {
        self.CollegeName(College.CollegeName());
        self.Abbreviation(College.Abbreviation());
        self.StartNumber(College.StartNumber());
        self.CollegeID(College.CollegeID());
        self.IsActive(College.IsActive());
        self.OrganizationName(College.OrganizationName());
        self.ClientID(College.ClientID());
        $("#divAddSubjectMatter").show();
    }

};