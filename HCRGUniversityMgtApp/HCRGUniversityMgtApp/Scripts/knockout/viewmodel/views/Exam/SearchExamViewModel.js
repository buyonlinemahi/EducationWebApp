function SearchExamViewModel() {
    var self = this;
    self.EncryptedExamID = ko.observable();
    self.ExamSearchResults = ko.observableArray();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);


    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    $('#loaderDiv').hide();

    self.addNewExam = function () {
        window.location = '/Exam/Add';
    }
    self.updateExam = function (model) {
        window.location = '/Exam/EditExam?ExamID=' + model.EncryptedExamID();
    }
    this.Init = function (model) {
        if (model.Exams != null) {
            if (!self.pendingRequest()) {
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $("#loaderDiv").show();
                ko.mapping.fromJS(model.Exams, {}, self.ExamSearchResults);
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

    
    self.SearchExamByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/Exam/GetAllExamByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.ExamSearchResults.removeAll();
                ko.mapping.fromJS(data.Exams, {}, self.ExamSearchResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };
    self.ExamFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchExam").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.SearchExamDetailSuccess = function (model) {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.Exams, {}, self.ExamSearchResults);
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
                    getAllExam();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllExam() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/Exam/GetAllPagedExam", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
                var data = $.parseJSON(data);
                $.each(data.Exams, function (index, value) {
                    self.ExamSearchResults.push(new Exam(value));
                });
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }

    }
    function Exam(value) {
        var self = this;
        self.ExamID = ko.observable(value.ExamID);
        self.ExamName = ko.observable(value.ExamName);
        self.ExamStatus = ko.observable(value.ExamStatus);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.EncryptedExamID = ko.observable(value.EncryptedExamID);
    }
    self.SearchExam = function () {
        if ($("#TxtSearch").val() == '') {
            alertify.alert("Please Enter Text ");
            return false;
        }
        self.pendingRequest(false);
        $.ajax({
            url: "/Exam/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchText: self.searchText() }),
            success: function (data) {
                self.TotolCount(0);
                self.CurrentPage(0);
                self.maxId(0);
                self.ExamSearchResults.removeAll();
                $.each(data.Exams, function (index, value) {
                    self.ExamSearchResults.push(new Exam(value));
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