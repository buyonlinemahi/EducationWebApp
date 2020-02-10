function NewsDetailViewModel() {
    var self = this;
    self.SectionID = ko.observable(0);
    self.NewsID = ko.observable();
    self.EncryptedNewsID = ko.observable();
    self.NewsSectionID = ko.observable();
    self.NewsTitle = ko.observable();
    self.NewsDescription = ko.observable();
    self.NewsType = ko.observable();
    self.NewsEditorPick = ko.observable();
    self.NewsDate = ko.observable();
    self.NewsAuthor = ko.observable();
    self.NewsSectionTitle = ko.observable();
    self.PhotoVideoTitle = ko.observable();
    self.PhotoVideoId = ko.observable();
    self.PhotoFIlterMenu = ko.observable('All');
    self.NewsVideoResults = ko.observableArray([]);
    self.NewsSectionResults = ko.observableArray([]);
    self.NewsResults = ko.observableArray([]);
    self.TotalItemCount = ko.observable();
 
    self.tabsModel = [
                 { TabTitle: "Latest Headlines", TabUrl: "All" },
                 { TabTitle: "Editor's Picks", TabUrl: "EditorPick" },
                 { TabTitle: "NewsLetter", TabUrl: "NewsLetter" }               

    ];

    self.tabs = ko.observableArray([
      new x(self.tabsModel[0]),
       new x(self.tabsModel[1]),
       new x(self.tabsModel[2])
      
    ]);
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.NewsResults, mappingOptions, self.NewsResults);
            ko.mapping.fromJS(model.NewsSectionResults, {}, self.NewsSectionResults);
            self.SectionID(model.SectionID);
            self.TotalItemCount(model.TotalCount);
            hideloderDiv();
        }
    };

    function ManagetabsViewModel(text) {
        var self = this;
        self.text = ko.observable(text);
    }

    function x(features) {

        var self = this;
        self.feature = ko.observable(features);
    }

    var mappingOptions = {
        'NewsDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD");
            }
        }
    }

    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }

    function showloderDiv() {
        $("#loaderDiv").addClass('loaderbg');
        $("#loading").show();
    }

    this.FilterNews = function (Filtertype) {      
        showloderDiv();
        
        self.PhotoFIlterMenu(Filtertype.feature().TabUrl);
        self.Skip(0);
        if (Filtertype.feature().TabUrl != 'NewsLetter') {
            $.ajax({
                url: "/News/GetFilterNews",
                cache: false,
                data: ko.toJSON({ filtertype: Filtertype.feature().TabUrl, sectionid: self.SectionID(), skip: self.Skip() }),
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    //self.NewsResults.removeAll();
                    ko.mapping.fromJS(data.NewsResults, mappingOptions, self.NewsResults);
                    self.TotalItemCount(data.TotalCount);
                    self.Pager().CurrentPage(1);
                    hideloderDiv();
                },
                error: function (data) {
                    hideloderDiv();
                    alert("Error when Getting Video News - " + data);
                }
            });
        }
        else {
            self.TotalItemCount(0);
            hideloderDiv();
        }


    }

    this.FilterSectionNews = function (model) {
        if (self.PhotoFIlterMenu() == "NewsLetter") {
         
            showloderDiv();
            $('#AllButton').click();
            self.SectionID(model.NewsSectionID());
            self.NewsResults.removeAll();
            self.Skip(0);
            $.ajax({
                url: "/News/GetFilterNews",
                cache: false,
                data: ko.toJSON({ filtertype: "All", sectionid: self.SectionID(), skip: self.Skip() }),
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                success: function (data) {
                    ko.mapping.fromJS(data.NewsResults, mappingOptions, self.NewsResults);
                    self.TotalItemCount(data.TotalCount);
                    self.Pager().CurrentPage(1);
                    hideloderDiv();
                },
                error: function (data) {
                    hideloderDiv();
                    alert("Error when Getting Video News - " + data);
                }
            });
        }
        else
            {
        showloderDiv();
        self.SectionID(model.NewsSectionID());
        self.NewsResults.removeAll();
        self.Skip(0);
        $.ajax({
            url: "/News/GetFilterNews",
            cache: false,
            data: ko.toJSON({ filtertype: self.PhotoFIlterMenu(), sectionid: self.SectionID(), skip: self.Skip() }),
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                ko.mapping.fromJS(data.NewsResults, mappingOptions, self.NewsResults);
                self.TotalItemCount(data.TotalCount);
                self.Pager().CurrentPage(1);
                hideloderDiv();
            },
            error: function (data) {
                hideloderDiv();
                alert("Error when Getting Video News - " + data);
            }
        });
        }

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
        showloderDiv();
        $.ajax({
            url: "/News/GetFilterNews",
            cache: false,
            data: ko.toJSON({ filtertype: self.PhotoFIlterMenu(), sectionid: self.SectionID(),skip: self.Skip() }),
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                ko.mapping.fromJS(data.NewsResults, mappingOptions, self.NewsResults);
                self.TotalItemCount(data.TotalCount);
                hideloderDiv();
            },
            error: function (data) {
                hideloderDiv();
                alert("Error when Getting Video News - " + data);
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
        self.GetRecordsWithSkipTake(skip, take);
    });

}

ko.bindingHandlers.trimLengthText = {};
ko.bindingHandlers.trimText = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel) {
        var trimmedText = ko.computed(function () {
            var untrimmedText = ko.utils.unwrapObservable(valueAccessor());
            var defaultMaxLength = 20;
            var minLength = 5;
            var maxLength = ko.utils.unwrapObservable(allBindingsAccessor().trimTextLength) || defaultMaxLength;
            if (maxLength < minLength) maxLength = minLength;
            var text = untrimmedText.length > maxLength ? untrimmedText.substring(0, maxLength - 1) + '...' : untrimmedText;
            return text;
        });
        ko.applyBindingsToNode(element, {
            text: trimmedText
        }, viewModel);

        return {
            controlsDescendantBindings: true
        };
    }
};






