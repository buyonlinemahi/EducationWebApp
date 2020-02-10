function ProfessionGridViewModel() {
    var self = this;
    self.ProfessionID = ko.observable();
    self.ProfessionTitle = ko.observable();
    self.IsActive = ko.observable();
    self.ProfessionResults = ko.observableArray([]);
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);

    self.ClientID = ko.observable();
    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    var IsHcrgAdmin;

    $('#loaderDiv').hide();
    this.Init = function (model) {
        if (model != null) {
            if (!self.pendingRequest()) {
                if (model.Professions.length != 0) {
                    $("#divAddprofession").hide();

                    if (model.IsHCRGAdmin == false) {
                        $("#divAddprofession").show();
                    }
                }
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $("#loaderDiv").show();
                $.each(model.Professions, function (index, value) {
                    self.ProfessionResults.push(new Profession(value));
                });
                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
                IsHcrgAdmin = model.IsHCRGAdmin;
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

    

    self.SearchProfessionByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/Profession/GetAllProfessionByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.ProfessionResults.removeAll();
                ko.mapping.fromJS(data.Professions, {}, self.ProfessionResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };


    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllProfession();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllProfession() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.ajax({
                url: '/Profession/GetAllPagedProfession',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: self.maxId() }),
                success: function (data) {
                    $.each(data.Professions, function (index, value) {
                        self.ProfessionResults.push(new Profession(value));
                    });
                    
                    self.TotolCount(data.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + data.PagedRecords);
                    self.PagedRecords(data.PagedRecords);
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    alert('Error while deleting a item - ' + data);
                }
            });
        }
    }
    self.AddProfessionDetailSuccess = function (model) {
        if (model != null) {            
            var newProfession = $.parseJSON(model);
            var viewModel = self.ProfessionResults;
            if (!newProfession.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].ProfessionID() == newProfession.ProfessionID) {
                        self.ProfessionResults.splice(i, 1);
                        self.ProfessionResults.splice(i, 0, new InsertProfessionLineItem(newProfession.ProfessionID, newProfession.ProfessionTitle, newProfession.IsActive, newProfession.OrganizationName,newProfession.ClientID));
                        alertify.success("Profession Updated Successfully");
                        if (IsHcrgAdmin == true) {
                            $("#divAddprofession").hide();
                        }
                    }
                }
            }
            else {
                self.ProfessionResults.splice(0, 0, new InsertProfessionLineItem(newProfession.ProfessionID, newProfession.ProfessionTitle, newProfession.IsActive,newProfession.OrganizationName,newProfession.ClientID));
                $("#main").scrollTop(0);
                alertify.success("Profession Inserted Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };
    function resetcontrol() {
        self.ProfessionID("");
        self.ProfessionTitle("");
        self.IsActive("");
    }
    self.ProfessionInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddProfession").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };
    function Profession(value) {
        var self = this;
        self.ProfessionID = ko.observable(value.ProfessionID);
        self.ProfessionTitle = ko.observable(value.ProfessionTitle);
        self.IsActive = ko.observable(value.IsActive);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.ClientID = ko.observable(value.ClientID);
    };
    this.deleteProfession = function (model) {
        alertify.confirm("Are you sure want to make  this change.It will be reflected on Main site?", function (e) {
            if (e) {
                $.ajax({
                    url: "/Profession/DisableProfession",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ profession: model }),
                    success: function (data) {
                        if (data != null) {
                            var newProfession = data;
                            var viewModel = self.ProfessionResults;
                            if (!newProfession.flag) {
                                for (var i = 0; i <= viewModel().length - 1; i++) {
                                    if (viewModel()[i].ProfessionID() == newProfession.ProfessionID) {
                                        self.ProfessionResults.splice(i, 1);
                                        self.ProfessionResults.splice(i, 0, new InsertProfessionLineItem(newProfession.ProfessionID, newProfession.ProfessionTitle, newProfession.IsActive, newProfession.OrganizationName, newProfession.ClientID));
                                        alertify.success("Profession change Successfully");
                                    }
                                }
                            }
                            resetcontrol();
                        }
                    },
                    error: function (data) {
                        alert("Error while deleting a Profession - " + data);
                    }
                });
            }
        })
    };
    self.edit = function (Profession) {
        self.ProfessionTitle(Profession.ProfessionTitle());
        self.IsActive(Profession.IsActive());
        self.ProfessionID(Profession.ProfessionID());
        self.OrganizationName(Profession.OrganizationName());
        self.ClientID(Profession.ClientID());
        $("#divAddprofession").show();
    }
    function InsertProfessionLineItem(_ProfessionID, _ProfessionTitle, _IsActive, _OrganizationName, _ClientID) {
        var self = this;
        self.ProfessionID = ko.observable(_ProfessionID);
        self.ProfessionTitle = ko.observable(_ProfessionTitle);
        self.IsActive = ko.observable(_IsActive);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.ClientID = ko.observable(_ClientID);
    }
};
