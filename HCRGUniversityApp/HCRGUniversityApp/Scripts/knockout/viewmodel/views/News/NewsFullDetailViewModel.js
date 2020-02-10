function NewsFullDetailViewModel() {
    var self = this;
    self.NewsID = ko.observable();
    self.NewsSectionID = ko.observable();
    self.EncryptedNewsSectionID = ko.observable();
    self.NewsTitle = ko.observable();
    self.NewsDescription = ko.observable();
    self.NewsType = ko.observable();
    self.NewsEditorPick = ko.observable();
    self.NewsDate = ko.observable();
    self.NewsSectionTitle = ko.observable();
    self.PhotoVideoTitle = ko.observable();
    self.PhotoVideoId = ko.observable();
    self.NewsResults = ko.observableArray([]);
    self.NewsSectionResults = ko.observableArray([]);
    self.NewsDetailLength = ko.observable();
    var mappingOptions = {
        'NewsDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD");
            }
        }
    }

    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.NewsResults, mappingOptions, self.NewsResults);
            ko.mapping.fromJS(model.NewsSectionResults, {}, self.NewsSectionResults);
            self.NewsDetailLength(self.NewsResults().length);
        }
    };

    this.FilterSectionNews = function (model) {
        window.location = '/News?SectionId=' + model.EncryptedNewsSectionID();

    }


}