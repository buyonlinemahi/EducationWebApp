function NewsSectionViewModel() {
    var self = this;
    self.NewsSectionResults = ko.observableArray([]);
    this.Init = function (model) {
        if (model != null) {
            $.each(model.NewsSectionResults, function (index, value) {
                self.NewsSectionResults.push(new NewsSection(value));
            });
        }
    };
    function NewsSection(value) {
        var self = this;
        self.NewsSectionID = ko.observable(value.NewsSectionID);
        self.NewsSectionTitle = ko.observable(value.NewsSectionTitle);
        self.NewsSectionType = ko.observable(value.NewsSectionType);
    };
}