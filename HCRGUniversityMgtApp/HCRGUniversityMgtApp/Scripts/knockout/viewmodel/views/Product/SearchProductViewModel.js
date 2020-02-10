function SearchProductViewModel() {
    var self = this;
    self.ProductSearchResults = ko.observableArray();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv').hide();


    this.Init = function (model) {
        if (model.Products != null) {
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                ko.mapping.fromJS(model.Products, {}, self.ProductSearchResults);
                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
    };


    self.scrolled = function (data, event) {
       
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllSearchProduct();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllSearchProduct() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/Product/SearchProduct", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
                var data = $.parseJSON(data);
                for (var i = 0; i < data.Products.length; i++) {
                    self.ProductSearchResults.push(new InsertProductLineItem(data.Products[i].ProductName, data.Products[i].ProductPrice));
                }
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }
    }

    self.ProductSearchFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchProduct").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.addNewProduct = function () {
        window.location = '/Product/Add';
    }
    self.updateProduct = function (model) {
        window.location = '/Product/Add?ProductID=' + model.ProductID();
    }

   

    var mappingOptions = {
        CourseUploadDate: {
            create: function (options) {
                return moment(options.data).format("MM/DD/YYYY");
            }
        }
    };
    self.SearchProductSuccess = function (model) {
        var _model = $.parseJSON(model);
        self.pendingRequest(false);
        self.CurrentPage(0);
        self.TotolCount(0);
        self.maxId(0);
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(_model.Products, mappingOptions, self.ProductSearchResults);
            self.TotolCount(_model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + _model.PagedRecords);
            $('#loaderDiv').hide();
        }
    }
    function InsertProductLineItem(_ProductName, _ProductPrice) {
        var self = this;
        self.ProductName = ko.observable(_ProductName);
        self.ProductPrice = ko.observable(_ProductPrice);       
    }

}