function ProductGridViewModel() {
    var self = this;
    self.ProductResults = ko.observableArray([]);
    self.TotalItemCount = ko.observable(0);

   
    self.ReturnPolicy = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            self.ProductResults.removeAll();
            ko.mapping.fromJS(model.Products, {}, self.ProductResults);
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        }
    };

    this.LearnMore = function (model) {
        $.post("/CertificationProgram/EncryptQueryString", { plainText: model.ProductID() }, function (_Encrypttext) {
            var EncryptProductID = $.parseJSON(_Encrypttext);
            var path = '/Store/ProductDetail/' + EncryptProductID;
            window.location = path;
        });
    };

    function getProducts() {
        $.ajaxSetup({ cache: false });
        $.post("/Store/getProducts", {
            skip: self.Skip()
        }, function (_data) {
            var data = $.parseJSON(_data);
            self.ProductResults.removeAll();
            ko.mapping.fromJS(data.Products, {}, self.ProductResults);
            self.TotalItemCount(data.TotalCount);
        });;
    }
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        getProducts();
       
      
    };
    var pagingSettings = {
        pageSize: 20,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });

    self.DeliveryTermConditions = function () {
        $.post("/Store/getDeliveryTermConditions", {
        }, function (_data) {
            $("#divDeliveryTermContents").animate({ scrollTop: 0 }, "slow");
            var data = $.parseJSON(_data);
            $("#divDeliveryTermContents").html(data.DeliveryTermContents);
        });;
    };

    self.ReturnPolicy = function () {

        $.post("/Store/getRetrunPolicys", {
        }, function (_data) {
            $("#divReturnPolicyContent").animate({ scrollTop: 0 }, "slow");
            var data = $.parseJSON(_data);
            $("#divReturnPolicyContent").html(data.ReturnPolicyContent);
        });;

    }; 


};