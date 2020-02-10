function AccreditorViewModel() {
    var self = this;
    self.AccreditorID = ko.observable();
    self.AccreditorName = ko.observable();
    self.IsActive = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.AccreditorResults = ko.observableArray([]);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    self.IsHCRGAdmin = ko.observable();
    var IsHcrgAdmin;


    $('#loaderDiv').hide();
    this.Init = function (model) {
        if (model != null) {

            if (model.AccreditorRecords.length != 0) {
                $("#divAccreditor").hide();
                if (model.IsHCRGAdmin == false) {
                    $("#divAccreditor").show();
                }
            }
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                ko.mapping.fromJS(model.AccreditorRecords, {}, self.AccreditorResults);
                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            IsHcrgAdmin = model.IsHCRGAdmin;
        };
        getAllOrganization(0);
    }

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

    self.SearchAccreditor = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/Accreditor/GetAllAccreditorsByOrganizationID/", { orgID: self.SearchSelectedOrganization() }, function (data) {
            self.AccreditorResults.removeAll();
            ko.mapping.fromJS(data.AccreditorRecords, {}, self.AccreditorResults);

        });
    };


    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllAccreditor();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllAccreditor() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/Accreditor/GetAllPagedAccreditor",  function (data) {
                var data = $.parseJSON(data);
                for (var i = 0; i < data.AccreditorRecords.length; i++) {
                    self.AccreditorResults.push(new InsertAccreditorLineItem(data.AccreditorRecords[i].AccreditorID, data.AccreditorRecords[i].AccreditorName, data.AccreditorRecords[i].IsActive, data.AccreditorRecords[i].OrganizationName));
                }
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
                setgrdDesignondelete();
            });
        }
       
    }
    function InsertAccreditorLineItem(_AccreditorID, _AccreditorName,_IsActive,_OrganizationName, _OrganizationID) {
        var self = this;


        
        self.AccreditorID = ko.observable(_AccreditorID);
        self.AccreditorName = ko.observable(_AccreditorName);
        self.IsActive = ko.observable(_IsActive);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);

    }
    self.AccreditorInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddAccreditor").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddAccreditorDetailSuccess = function (model) {
        if (model != null) {           
            var newaccreditor = $.parseJSON(model);
            var viewModel = self.AccreditorResults;
            if (!newaccreditor.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].AccreditorID() == newaccreditor.AccreditorID) {
                        self.AccreditorResults.splice(i, 1);
                        self.AccreditorResults.splice(i, 0, new InsertAccreditorLineItem(newaccreditor.AccreditorID, newaccreditor.AccreditorName, newaccreditor.IsActive, newaccreditor.OrganizationName, newaccreditor.OrganizationID));
                        alertify.success("Updated Successfully");
                        IsHcrgAdmin
                        if (IsHcrgAdmin == true)
                        {
                            $("#divAccreditor").hide();
                        }                       
                        break;
                    }
                }
            }
            else {
                self.AccreditorResults.splice(0, 0, new InsertAccreditorLineItem(newaccreditor.AccreditorID, newaccreditor.AccreditorName, newaccreditor.IsActive, newaccreditor.OrganizationName, newaccreditor.OrganizationID));
                $("#main").scrollTop(0);
                alertify.success("Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
            setgrdDesign();
        }
    };
    function resetcontrol() {
        self.AccreditorID("");
        self.AccreditorName("");

    }

    self.ViewAccreditorLineItems = function (config) {
        self.AccreditorID(config.AccreditorID());
        self.AccreditorName(config.AccreditorName());
        self.IsActive(config.IsActive());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        $(window).scrollTop(0);
        $("#divAccreditor").show();
    }

    this.DeleteAccreditorLineItems = function (itemToDelete) {
        alertify.confirm("Are you sure you want to change the status?", function (e) {
            if (e) {
                $.ajax({
                    url: '/Accreditor/DeleteAccreditor',
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ accreditor: itemToDelete }),
                    success: function (data) { 
                      
                        var viewModel = self.AccreditorResults;
                     
                            for (var i = 0; i <= viewModel().length - 1; i++) {
                                if (viewModel()[i].AccreditorID() == data.AccreditorID) {
                                    self.AccreditorResults.splice(i, 1);
                                    self.AccreditorResults.splice(i, 0, new InsertAccreditorLineItem(data.AccreditorID, data.AccreditorName, data.IsActive,data.OrganizationName));
                                    alertify.success("Status Updated Successfully.");
                                    break;
                                }
                            }
                        
                        setgrdDesignondelete();
                    },
                    error: function (data) {
                        alert('Error while deleting a item - ' + data);
                    }
                });
            }
        });
    };


}
if ($('#main').hasScrollBar()) {
    setgrdDesign();
}
function setgrdDesign() {
    if ($('#main').hasScrollBar()) {

        $('#main tr td').each(function (index) {
            var className = this.className.match(/paddingleft\d+/);
            if (className != null) {
                $(this).removeClass(className[0]);
            }
            $(this).addClass("paddingleft45");
        });
    }
}
//function setgrdDesignondelete() {
//    if ($('#main').hasScrollBar()) {
//        $('#main tr td').each(function (index) {
//            var className = this.className.match(/paddingleft\d+/);
//            if (className != null) {
//                $(this).removeClass(className[0]);
//            }
//            $(this).addClass("paddingleft45");
//        });
//    }
//    else if (!$('#main').hasScrollBar()) {
//        $('#main tr td').each(function (index) {
//            var className = this.className.match(/paddingleft\d+/);
//            if (className != null) {
//                $(this).removeClass(className[0]);
//            }
//            $(this).addClass("paddingleft35");
//        });
//    }
//}
