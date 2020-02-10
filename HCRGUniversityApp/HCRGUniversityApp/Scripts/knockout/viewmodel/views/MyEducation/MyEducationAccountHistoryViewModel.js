function MyEducationAccountHistoryGridViewModel() {
   
    var self = this;
    self.TotalItemCount = ko.observable(0);   
    self.MyEducationAccountHistoryResults = ko.observableArray([]);
    self.MyEducationAccountHistoryPrintResults = ko.observableArray([]);
    var mappingOptions = {
        'PurchaseDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
    var mappingPrintOptions = {
        'PurchaseDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.myEducationAccountHistory, mappingOptions, self.MyEducationAccountHistoryResults);
            self.TotalItemCount(model.TotalItemCount);
            self.Pager().CurrentPage(1);
            self.Pager().CurrentPage();
            printAllAccountHistory();
           
        }
    };
    function printAllAccountHistory() {
        $.ajax({
            url: "/MyEducation/MyEducationAccountHistoryPrint",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ Totalcount: self.TotalItemCount() }),
            success: function (data) {
                if (data != null) {
                    self.MyEducationAccountHistoryPrintResults.removeAll();
                    ko.mapping.fromJS(data.myEducationAccountHistory, mappingPrintOptions, self.MyEducationAccountHistoryPrintResults);
                    self.TotalItemCount(data.TotalItemCount);

                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    }
    //end

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.ajax({
            url: "/MyEducation/MyEducationAccountHistory",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: self.Skip() }),
            success: function (data) {
                if (data != null) {
                    self.MyEducationAccountHistoryResults.removeAll();
                    ko.mapping.fromJS(data.myEducationAccountHistory, {}, self.MyEducationAccountHistoryResults);
                    self.TotalItemCount(data.TotalItemCount);                   
                    
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    };
    var pagingSettings = {
        pageSize: 25,
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
}