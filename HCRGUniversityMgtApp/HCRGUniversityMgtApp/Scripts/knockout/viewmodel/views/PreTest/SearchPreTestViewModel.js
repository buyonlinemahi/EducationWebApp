function SearchPreTestViewModel() {
    var self = this;
    self.EncryptedPreTestID = ko.observable();
    self.PreTestSearchResults = ko.observableArray();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv').hide();

    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.addNewPreTest = function () {
        window.location = '/PreTest/Add';
    }
    self.updatePreTest = function (model) {
        window.location = '/PreTest/EditPreTest?PreTestID=' + model.EncryptedPreTestID();
    }
    this.Init = function (model) {
        if (model.PreTests != null) {
            if (!self.pendingRequest()) {
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $("#loaderDiv").show();
                ko.mapping.fromJS(model.PreTests, {}, self.PreTestSearchResults);
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

    
    self.SearchPreTestByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/PreTest/GetAllPreTestByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
                self.PreTestSearchResults.removeAll();
                ko.mapping.fromJS(data.PreTests, {}, self.PreTestSearchResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
    };

    self.PreTestFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchPreTest").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.SearchPreTestDetailSuccess = function (model) {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.PreTests, {}, self.PreTestSearchResults);
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
                    getAllPreTest();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllPreTest() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/PreTest/GetAllPagedPreTest", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
                var data = $.parseJSON(data);
                $.each(data.PreTests, function (index, value) {
                    self.PreTestSearchResults.push(new PreTest(value));
                });
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }

    }
    function PreTest(value) {
        var self = this;
        self.PreTestID = ko.observable(value.PreTestID);
        self.PreTestName = ko.observable(value.PreTestName);
        self.PreTestStatus = ko.observable(value.PreTestStatus);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.ClientID = ko.observable(value.ClientID);
        self.EncryptedPreTestID = ko.observable(value.EncryptedPreTestID);
    }
    self.SearchPreTest = function () {
        if ($("#TxtSearch").val() == '') {
            alertify.alert("Please Enter Text ");
            return false;
        }
        self.pendingRequest(false);
        $.ajax({
            url: "/PreTest/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchText: self.searchText() }),
            success: function (data) {
                self.TotolCount(0);
                self.CurrentPage(0);
                self.maxId(0);
                self.PreTestSearchResults.removeAll();
                $.each(data.PreTests, function (index, value) {
                    self.PreTestSearchResults.push(new PreTest(value));
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