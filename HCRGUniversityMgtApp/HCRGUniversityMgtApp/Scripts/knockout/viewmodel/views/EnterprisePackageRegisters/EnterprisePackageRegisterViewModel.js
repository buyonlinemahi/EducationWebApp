function EnterprisePackageRegisterViewModel() {
    var self = this;   
    self.EPRID = ko.observable();
    self.EPRName = ko.observable();
    self.EPRPhone = ko.observable();
    self.EPREmail = ko.observable();
    self.EPRCompany = ko.observable();
    self.EPRCreatedDate = ko.observable();


    self.EnterprisePackageRegisterResults = ko.observableArray();
    self.TotalItemCount = ko.observable(0);



    var mappingOptions = {
        'EPRCreatedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }


    this.Init = function (model) {
        if (model.EnterprisePackageRegisterDetails != null) {
            ko.mapping.fromJS(model.EnterprisePackageRegisterDetails, mappingOptions, self.EnterprisePackageRegisterResults);
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        }
    };


    self.EnterprisePackageRegisterDetailByID = function (dataResult) {
        var _EPRID = dataResult.EPRID();
        $.post("/User/GetEnterprisePackageRegisterByID", {
            ID: _EPRID
        }, function (_data) {
            var model = _data;
            self.EPRID(model.EPRID);
            self.EPRName(model.EPRName);
            self.EPRPhone(model.EPRPhone);
            self.EPREmail(model.EPREmail);
            self.EPRCompany(model.EPRCompany);
            self.EPRCreatedDate(moment(model.EPRCreatedDate).format("MM/DD/YYYY"));

        });
    };

    ko.bindingHandlers.formatNumberText = {
        update: function (element, valueAccessor) {
            var phone = ko.utils.unwrapObservable(valueAccessor());
            var formatPhone = function () {
                return phone.replace(/(\d{3})(\d{3})(\d{4})/, "($1)-$2-$3");
            }
            ko.bindingHandlers.text.update(element, formatPhone);
        }
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
        $.post("/User/getAllEnterprisePackageRegister", {
            skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.EnterprisePackageRegisterResults.removeAll();
            ko.mapping.fromJS(model.EnterprisePackageRegisterDetails, mappingOptions, self.EnterprisePackageRegisterResults);
            self.TotalItemCount(model.TotalCount);
        });
    };

    var pagingSettings = {
        pageSize: 15,
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

};