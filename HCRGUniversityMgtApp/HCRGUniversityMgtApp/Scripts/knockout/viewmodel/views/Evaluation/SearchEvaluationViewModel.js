function SearchEvaluationViewModel() {
    var self = this;
    self.EvaluationSearchResults = ko.observableArray();
    self.EncryptedEvaluationID = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv').hide();

    self.ClientID = ko.observable();
    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.addNewEvaluation = function () {
        window.location = '/Evaluation/Add';
    }
    self.updateEvaluation = function (model) {
        window.location = '/Evaluation/EditEvaluation?EvaluationID=' + model.EncryptedEvaluationID();
    }
    this.Init = function (model) {
        if (model.Evaluations != null) {
            if (!self.pendingRequest()) {
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $("#loaderDiv").show();
                ko.mapping.fromJS(model.Evaluations, {}, self.EvaluationSearchResults);
                self.TotolCount(model.TotalCount);
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

    self.SearchEvaluationByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/Evaluation/GetAllEvaluationByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.EvaluationSearchResults.removeAll();
                ko.mapping.fromJS(data.Evaluations, {}, self.EvaluationSearchResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };

    self.EvaluationFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchEvaluation").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.SearchEvaluationDetailSuccess = function (model) {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.Evaluations, {}, self.EvaluationSearchResults);
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            $('#loaderDiv').hide();
        }
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllEvaluation();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllEvaluation() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/Evaluation/GetAllPagedEvaluation", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
                var data = $.parseJSON(data);
                $.each(data.Evaluations, function (index, value) {
                    self.EvaluationSearchResults.push(new Evaluation(value));
                });
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }

    }
    function Evaluation(value) {
        var self = this;
        self.EvaluationID = ko.observable(value.EvaluationID);
        self.EvaluationName = ko.observable(value.EvaluationName);
        self.EvaluationStatus = ko.observable(value.EvaluationStatus);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.ClientID = ko.observable(value.ClientID);
        self.EncryptedEvaluationID = ko.observable(value.EncryptedEvaluationID);
    }
    self.SearchEvaluation = function () {  
       if ($("#TxtSearch").val() == '')
        {
          alertify.alert("Please Enter Text ");
            return false;
        }
        self.pendingRequest(false);
        $.ajax({
            url: "/Evaluation/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchText: self.searchText() }),
            success: function (data) {
                self.TotolCount(0);
                self.CurrentPage(0);
                self.maxId(0);
                self.EvaluationSearchResults.removeAll();
                $.each(data.Evaluations, function (index, value) {
                    self.EvaluationSearchResults.push(new Evaluation(value));
                });
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    };
}