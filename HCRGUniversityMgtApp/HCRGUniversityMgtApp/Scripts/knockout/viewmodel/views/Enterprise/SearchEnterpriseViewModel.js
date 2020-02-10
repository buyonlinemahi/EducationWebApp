function SearchEnterpriseViewModel() {
    var self = this;
    self.EnterpriseSearchResults = ko.observableArray();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.PagedRecords = ko.observable(0);

    $('#loaderDiv').hide();

    self.addNewEnterprise = function () {
        window.location = '/Enterprise/Add';
    }
    self.updateEnterprise = function (model) {
        window.location = '/Enterprise/EditEnterprise?EnterpriseID=' + model.EnterpriseID();
    }

    var mappingOptions = {
        'EnterpriseEndDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    this.Init = function (model) {
        if (model.EnterpriseDetails != null) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.EnterpriseDetails, mappingOptions, self.EnterpriseSearchResults);
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            $('#loaderDiv').hide();
        }
    };
    self.SearchEnterpriseFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchEnterprise").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.SearchEnterpriseDetailSuccess = function (model) {
        $("#loaderDiv").show();
        ko.mapping.fromJS(model.EnterpriseDetails, {}, self.EnterpriseSearchResults);
        self.TotolCount(model.TotalCount);
        self.CurrentPage(self.maxId());
        self.maxId(self.maxId() + model.PagedRecords);
        $('#loaderDiv').hide();
    }
    self.scrolled = function (data, event) {
        if (self.TotolCount() > self.maxId()) {
            var elem = event.target;
            if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                getAllEnterprise();
            }
        }
        else {
            self.pendingRequest(true);
        }
    }
    function getAllEnterprise() {
        $("#loaderDiv").show();
        $.post("/Enterprise/GetAllPagedEnterprise", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
            var data = $.parseJSON(data);
            $.each(data.EnterpriseDetails, function (index, value) {
                self.EnterpriseSearchResults.push(new Enterprise(value));
            });
            self.TotolCount(data.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + data.PagedRecords);
            $('#loaderDiv').hide();
        });
    }
    function Enterprise(value) {
        var self = this;
        self.EnterpriseID = ko.observable(value.EnterpriseID);
        self.EnterpriseClientName = ko.observable(value.EnterpriseClientName);
        self.EnterpriseEndDate = ko.observable(moment(value.EnterpriseEndDate).format("MM/DD/YYYY"));
        self.EnterpriseNumberUser = ko.observable(value.EnterpriseNumberUser);
    }
    self.SearchEnterprise = function () {

        $.ajax({
            url: "/Enterprise/Index",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchText: self.searchText() }),
            success: function (data) {
                self.TotolCount(0);
                self.CurrentPage(0);
                self.maxId(0);
                self.EnterpriseSearchResults.removeAll();
                $.each(data.EnterpriseDetails, function (index, value) {
                    self.EnterpriseSearchResults.push(new Enterprise(value));
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