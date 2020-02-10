function SearchGroupViewModel() {
    var self = this;
    self.EncryptedUserMenuGroupID = ko.observable();
    self.UserMenuGroupDetails = ko.observableArray();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.PagedRecords = ko.observable(0);

    $('#loaderDiv').hide();

    self.addNewGroup = function () {
        window.location = '/Group/AddGroup';
    }
    self.updateGroup = function (model) {
        window.location = '/Group/AddGroup/' + model.EncryptedUserMenuGroupID();
    }

    var mappingOptions = {
        'GroupEndDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    this.Init = function (model) {
        if (model.UserMenuGroupDetails != null) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.UserMenuGroupDetails, mappingOptions, self.UserMenuGroupDetails);
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            $('#loaderDiv').hide();
        }
    };
    self.SearchGroupFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchGroup").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.SearchGroupDetailSuccess = function (model) {
        $("#loaderDiv").show();
        ko.mapping.fromJS(model.GroupDetails, {}, self.GroupSearchResults);
        self.TotolCount(model.TotalCount);
        self.CurrentPage(self.maxId());
        self.maxId(self.maxId() + model.PagedRecords);
        $('#loaderDiv').hide();
    }
    self.scrolled = function (data, event) {
        if (self.TotolCount() > self.maxId()) {
            var elem = event.target;
            if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                getAllGroup();
            }
        }
        else {
            self.pendingRequest(true);
        }
    }
    function getAllGroup() {
        $("#loaderDiv").show();
        $.post("/Group/GetAllPagedGroup", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
            var data = $.parseJSON(data);
            $.each(data.GroupDetails, function (index, value) {
                self.GroupSearchResults.push(new Group(value));
            });
            self.TotolCount(data.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + data.PagedRecords);
            $('#loaderDiv').hide();
        });
    }
    function Group(value) {
        var self = this;
        self.UserMenuGroupID = ko.observable(value.UserMenuGroupID);
        self.UserMenuGroupName = ko.observable(value.UserMenuGroupName);
    }
    self.SearchGroup = function () {

        $.ajax({
            url: "/Group/Index",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchText: self.searchText() }),
            success: function (data) {
                self.TotolCount(0);
                self.CurrentPage(0);
                self.maxId(0);
                self.GroupSearchResults.removeAll();
                $.each(data.GroupDetails, function (index, value) {
                    self.GroupSearchResults.push(new Group(value));
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