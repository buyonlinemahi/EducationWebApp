function ProductpurchaseViewModel()
{
    var self = this;
    self.ProductID = ko.observable();
    self.Date = ko.observable();
    self.ProductPurchaseResults = ko.observable();
    self.ProductPurchaseResultList = ko.observable();
    self.ProductShippingDetailsList = ko.observable();
    self.TotalCount = ko.observable(0);
    self.TotalItemCount = ko.observable(0);
    self.UserID == ko.observable();
    self.UserFirstname = ko.observable();
    self.UserLastname = ko.observable();
    self.ProductName = ko.observable();
    self.ProductShoppingID = ko.observable();
    self.SAFirstName = ko.observable();
    self.SALastName = ko.observable();
    self.SAAddress = ko.observable();
    self.SAAddress2 = ko.observable();
    self.SACity = ko.observable();
    self.StateName = ko.observable();
    self.SAPostalCode = ko.observable();
    self.CreatedBy = ko.observable();
    self.TotalItemCount = ko.observableArray([]);
    self.ProductShippedOn = ko.observable();
    self.CreatedOn = ko.observable();
    self.ShippingPaymentID = ko.observable();
    self.CreatedBy = ko.observable();
    self.ppTotalItemCount = ko.observable(0);

    var mappingOptions = {
        'Date': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }    

    self.getProductPurchase = function (skip) {     
        $.post("/Product/ProductPurchase", {
            skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);            
            ko.mapping.fromJS(model.ProductPurchasesRecords, mappingOptions, self.ProductPurchaseResults);
            self.TotalCount(model.TotalCount);
        });
    };
    self.getProductPurchase(0);
  
    /*Get by productShopping ID*/
    self.GetProductPurchaseDetailByID = function (dataResult) {
        var _shippingpaymentID = dataResult.ShippingPaymentID();     
        $.post("/Product/GetProductPurchaseDetailByID", {
            shippingpaymentID: _shippingpaymentID
        },
        function (_data) {
            var model = $.parseJSON(_data);      
            ko.mapping.fromJS(model.ProductPurchaseDetails, mappingOptions, self.ProductPurchaseResultList);
            self.ppTotalItemCount(model.TotalCount);
            // self.ppTotalItemCount.push(model.ProductName);            
        });

        $.post("/Product/GetProductShippingAddressDetailByID", {
            shippingpaymentID: _shippingpaymentID
        },
        function (_dataModel) {
            var model = $.parseJSON(_dataModel);      
            self.SAFirstName(model.SAFirstName);
            self.SALastName(model.SALastName);
            self.SAAddress(model.SAAddress);
            self.SAAddress2(model.SAAddress2);
            self.SACity(model.SACity);
            self.StateName(model.StateName);
            self.SAPostalCode(model.SAPostalCode);
            self.ShippingPaymentID(model.ShippingPaymentID);
            self.ProductShoppingID(model.ProductShoppingID);
        });
        $.post("/Product/GetProductShippingDetail", {
            Skip: null, shippingpaymentID: _shippingpaymentID
        },
        function (_dataModelList) {
            var modelList = _dataModelList;       
            if (modelList != "") {              
                self.ShippingPaymentID(modelList.ShippingPaymentID);
                self.ProductShippedOn(modelList.ProductShippedOn);
                var dt = moment(modelList[0].ProductShippedOn).format('MM/DD/YYYY');              
                $('#ProductShippedOn').val(dt);
                $("#ProductShippedOn").prop("readonly", true);                
                $("#ProductShippedOn").datepicker("destroy");
            }
            else {
                $('#ProductShippedOn').val("");
                $("#ProductShippedOn").prop("readonly", false);
              var dates=  $("#ProductShippedOn").datepicker({
                    minDate: "0",
                    maxDate: "+2Y",
                    defaultDate: "+1w",
                    dateFormat: 'mm/dd/yy',
                    numberOfMonths: 1,
                    onSelect: function (date) {
                        for (var i = 0; i < dates.length; ++i) {
                            if (dates[i].id < this.id)
                                $(dates[i]).datepicker('option', 'minDate', date);
                            else if (dates[i].id > this.id)
                                $(dates[i]).datepicker('option', 'maxDate', date);
                        }                      
                    }
                
                });              
            }
        });
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
        self.getProductPurchase(skip);        
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

    /*to save record*/

    self.AddDetailSuccess = function (model) {        
        alertify.success("Successfully Added");        
        $('#myModalProductPurchase').modal('hide');
    };

    /****get skip take for new****/
    //========= paging code for pp grid only ===========//

    self.ppGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.ppSkip(0);
            self.ppTake(pppagingSetting.pppageSize);
        }
        else {
            self.ppSkip(skip);
            self.ppTake(take);
        }
       
        $.post("/Product/GetProductPurchaseDetailByID", {
            shippingpaymentID: self.ShippingPaymentID(),
            skip: self.ppSkip()
        }, function (_data) {
            var model = $.parseJSON(_data);              
            ko.mapping.fromJS(model.ProductPurchaseDetails, mappingOptions, self.ProductPurchaseResultList);
            self.ppTotalItemCount(model.TotalCount);           
        });        
    }

    var pppagingSetting = {
        pppageSize: 5,
        pppageSlide: 2
    };
    self.ppSkip = ko.observable(0);
    self.ppTake = ko.observable(pppagingSetting.pppageSize);
    self.ppPager = ko.pppager(self.ppTotalItemCount);

    self.ppPager().ppPageSize(pppagingSetting.pppageSize);
    self.ppPager().ppPageSlide(pppagingSetting.pppageSlide);
    self.ppPager().ppCurrentPage(1);

    self.ppPager().ppCurrentPage.subscribe(function () {
        var skip = pppagingSetting.pppageSize * (self.ppPager().ppCurrentPage() - 1);
        var take = pppagingSetting.pppageSlide;
        self.ppGetRecordsWithSkipTake(skip, take);
    });



    // ============= paging code for pp ends =============== //

}
//========== pager js for   pp  grid only ========//
(function (ko) {
    var ppnumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function ppPager(totalItemCount) {
        var self = this;
        self.ppCurrentPage = ppnumericObservable(1);

        self.ppTotalItemCount = ko.computed(totalItemCount);

        self.ppPageSize = ppnumericObservable(10);
        self.ppPageSlide = ppnumericObservable(2);

        self.ppLastPage = ko.computed(function () {
            return Math.floor((self.ppTotalItemCount() - 1) / self.ppPageSize()) + 1;
        });

        self.ppHasNextPage = ko.computed(function () {
            return self.ppCurrentPage() < self.ppLastPage();
        });

        self.ppHasPrevPage = ko.computed(function () {
            return self.ppCurrentPage() > 1;
        });

        self.ppFirstItemIndex = ko.computed(function () {
            return self.ppPageSize() * (self.ppCurrentPage() - 1) + 1;
        });

        self.ppLastItemIndex = ko.computed(function () {
            return Math.min(self.ppFirstItemIndex() + self.ppPageSize() - 1, self.ppTotalItemCount());
        });

        self.ppPages = ko.computed(function () {
            var pppageCount = self.ppLastPage();
            var pppageFrom = Math.max(1, self.ppCurrentPage() - self.ppPageSlide());
            var pppageTo = Math.min(pppageCount, self.ppCurrentPage() + self.ppPageSlide());
            pppageFrom = Math.max(1, Math.min(pppageTo - 2 * self.ppPageSlide(), pppageFrom));
            pppageTo = Math.min(pppageCount, Math.max(pppageFrom + 2 * self.ppPageSlide(), pppageTo));

            var result = [];
            for (var i = pppageFrom; i <= pppageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.pppager = function (totalItemCount) {
        var pppager = new ppPager(totalItemCount);
        return ko.observable(pppager);
    };
}(ko));
//============= pager js for pp ends  ============//
