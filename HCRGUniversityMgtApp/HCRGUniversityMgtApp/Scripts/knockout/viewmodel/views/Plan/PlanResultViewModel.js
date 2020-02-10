function PlanGridViewModel() {
    var self = this;
    self.PlanID = ko.observable();
    self.PlanName = ko.observable();
    self.ClientID = ko.observable();
    self.EncryptedPlanID = ko.observable();
    self.PlanResults = ko.observableArray([]);
    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    self.IsHCRGAdmin = ko.observable();
    self.TotalCount = ko.observable(0);
    self.TotalItemCount = ko.observable(0);


    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.PlanRecords, {}, self.PlanResults);
            self.TotalCount(model.TotalCount);
            self.IsHCRGAdmin(model.IsHCRGAdmin);

            if (model.PlanRecords.length != 0) {
                $("#divPlan").hide();

                if (model.IsHCRGAdmin == false) {
                    $("#divPlan").show();
                }
            }
        }
        getAllOrganization();
        $("#hdSearchBtn").val(0);
    };
    function getAllOrganization() {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (model) {
                self.AllOrganizations.removeAll();
                self.AllOrganizations(model.slice());    
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }

    self.GetPlanAccordingToOrganization = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $("#hdSearchBtn").val(1);
        BindDropDownByOrganization(0);
    }
    function BindDropDownByOrganization(val) {        
        $.ajax({
            url: "/Plan/GetPlanAccordingToOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ OrganizationID: $("#DrpOrgID").val(),skip : val }),
            success: function (model) {
                if (model != null) {
                    self.PlanResults.removeAll();
                    ko.mapping.fromJS(model.PlanRecords, {}, self.PlanResults);
                    self.TotalCount(model.TotalCount);
                }
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }
    self.EditByPlanID = function (data) {        
        $("#hdEncryptedPlanID").val(data.EncryptedPlanID());
        $("#hdplanName").val(data.PlanName());
        $("#hdClientID").val(data.ClientID());
        $("#divPlan").show();
    }

    self.AddPlanDetailSuccess = function (model) {
        var model = $.parseJSON(model);
        if (model == "Add Sucessfully.") {
            $("#divPlan").hide();
            alertify.success("Successfully Added");
            $("#hdEncryptedPlanID").val("");
            $("#hdplanName").val("");
            getAllPlan(0);
        }
        else {
            $("#hdEncryptedPlanID").val("");
            $("#hdplanName").val("");
            alertify.success("Successfully Updated");
            getAllPlan(self.Skip());
        }
      
    };

    self.PlanInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmPlan").jqBootstrapValidation('hasErrors')) {
            return false;
        }        
        return true;
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
        if ($("#hdSearchBtn").val() == 1) {            
            BindDropDownByOrganization(skip);
        }       
        else {            
            getAllPlan(skip);
        }
        
    };

    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });

    function getAllPlan(skipN) {       
        $.post("/Plan/GetAllPlanByClientID", { skip: skipN, orgID: $("#DrpOrgID").val() }, function (data) {
                var data = $.parseJSON(data);
                self.PlanResults.removeAll();
                ko.mapping.fromJS(data.PlanRecords, {}, self.PlanResults);
                self.TotalCount(data.TotalCount);
            });
        
    }

    self.GetPlainDetailByID = function (model) {
        window.location = '/Plan/AddPackage/' + model.EncryptedPlanID();        
    }
  
};


