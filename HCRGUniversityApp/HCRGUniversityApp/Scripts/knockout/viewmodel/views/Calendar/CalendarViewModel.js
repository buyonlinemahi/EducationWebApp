function CalendarViewModel(model) {
    var self = this;
    self.eventList = ko.observableArray([]);
    self.eventListPaaging = ko.observableArray([]);
    self.TotalItemCount = ko.observable();
    self.GetEventList = function () {
        bindList();
    }

    var mappingOptions = {
        'start': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD");
            }
        }
    }

    function bindList(skip) {
        $.ajax({
            url: "/Calendar/GetEventData/",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',   
            data: ko.toJSON({ start: $('#hidStartTime').val(), end: $('#hidEndTime').val() }),
            success: function (data) {
                if (data != null) {
                    self.eventList.removeAll();
                    self.eventListPaaging.removeAll();
                     self.eventList(data[0]);
                     self.TotalItemCount(data[1]);
                     self.Pager().CurrentPage(1);
                     if (data[1] > 0) {
                         bindListPaged();
                         document.getElementById("EmptyGrid").style.display = "none";
                     }
                     else {
                         document.getElementById("EmptyGrid").style.display = "block";
                     }
                }
            },
        });
    }

    ko.mapping.fromJS(model, mappingOptions, self);
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        // bindList();
        bindListPaged();


    };

    function bindListPaged() {
        var plainJs = ko.toJS(self.eventList());
        $.ajax({
            url: "/Calendar/GetEventDataPagng/?skip=" + self.Skip(),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(plainJs),
            success: function (data) {
                if (data != null) {                    
                    ko.mapping.fromJS(data, mappingOptions, self.eventListPaaging);
                }
            },
        });
    }

    var pagingSettings = {
        pageSize: 5,
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