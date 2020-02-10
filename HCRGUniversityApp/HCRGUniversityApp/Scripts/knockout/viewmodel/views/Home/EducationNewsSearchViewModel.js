function SearchGridViewModel() {
    var self = this;
    self.EducationNews = ko.observableArray([]);
    self.TotalItemCount = ko.observable();

    $("#btnSearch").click(function () {
        self.Pager().CurrentPage(1);
        self.Skip(0);
        self.Take(pagingSettings.pageSize);
         $.ajax({
            type: "POST",
            url: "/Home/EducationNewsSearch",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON({ search: $("#searchText").val(), skip: 0 }),
            success: function (responseText) {
                var data = $.parseJSON(responseText);
                ko.mapping.fromJS(data.educationNewsSearch, {}, self.EducationNews);
                self.TotalItemCount(data.TotalCount);
            }
        })
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
         $.ajax({
            url: "/Home/EducationNewsSearch",
            cache: false,
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: ko.toJSON({ search: $("#searchText").val(), skip: $("#hidskipLayout").val() }),
            success: function (responseText) {
                var data = $.parseJSON(responseText);
                if (data != null) {
                    ko.mapping.fromJS(data.educationNewsSearch, {}, self.EducationNews);
                    self.TotalItemCount(data.TotalCount);
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    };
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




}