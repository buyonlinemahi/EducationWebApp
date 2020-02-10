function LogSessionViewModel() {
    var self = this;
    self.LogSessionDetails = ko.observableArray([]);
    self.searchText = ko.observable("");
    self.TotolCount = ko.observable(0);
    self.TotalItemCount = ko.observable(0);
    $('#loaderDiv').show();
    var mappingOptions = {
        'LogCreatedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.LogSessionDetails, mappingOptions, self.LogSessionDetails);
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
            $('#loaderDiv').hide();
        }
    };

    self.SearchUser = function () {
        $.ajax({
            url: "/LogSession/GetLogSessionByUserName",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ searchtext: self.searchText() }),
            success: function (data) {
                ko.mapping.fromJS(data.LogSessionDetails, mappingOptions, self.LogSessionDetails);
                self.TotalItemCount(data.TotalCount);
                self.Pager().CurrentPage(1);
               
                $('#loaderDiv').hide();
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    };
    this.DeleteSession = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $('#loaderDiv').show();
                $.ajax({
                    url: '/LogSession/DeleteSession',
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ LogSessionID: itemToDelete.LogSessionID() }),
                    success: function (data) {
                        RebindAndPaggging(self.Skip());
                        alertify.success(data);
                        if ((self.TotalItemCount() % self.Skip() == 1))
                            self.Pager().CurrentPage(self.Pager().CurrentPage() - 1);
                        $('#loaderDiv').hide();
                    },
                    error: function (data) {
                        alert('Error while deleting a item - ' + data);
                    }
                });
            }
        });
    };
    var pagingSettings = {
        pageSize: 10,
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
        $('#loaderDiv').show();
        self.GetRecordsWithSkipTake(skip, take);
    });
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        RebindAndPaggging(skip);
        $('#loaderDiv').hide();
    };
    function RebindAndPaggging(skip) {
        if (self.searchText() != "") {
            $.post("/LogSession/GetLogSessionByUserName", {
                skip: skip,
                searchtext: self.searchText()
            }, function (_data) {
                var model = $.parseJSON(_data);
                self.LogSessionDetails.removeAll();
                ko.mapping.fromJS(model.LogSessionDetails, mappingOptions, self.LogSessionDetails);
                self.TotalItemCount(model.TotalCount);
            });
           
            
        }
        else {
            $.post("/LogSession/GetLogSession", {
                skip: skip,
            }, function (_data) {
                var model = $.parseJSON(_data);
                self.LogSessionDetails.removeAll();
                ko.mapping.fromJS(model.LogSessionDetails, mappingOptions, self.LogSessionDetails);
                self.TotalItemCount(model.TotalCount);
            });
        }
    }
};