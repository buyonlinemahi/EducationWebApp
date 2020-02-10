function FAQDetailViewModel() {
    var self = this;
    self.FAQCategoryResults = ko.observableArray([]);
    self.FAQResults = ko.observableArray([]);
    self.FaqCatName = ko.observable();
    this.Init = function (model) {

        if (model != null) {
            ko.mapping.fromJS(model.FAQCategoryDetaildResults, {}, self.FAQCategoryResults)
            ko.mapping.fromJS(model.FAQDetailResults, {}, self.FAQResults)
            if(self.FAQCategoryResults().length >0)
           self.FaqCatName(self.FAQCategoryResults()[0].FAQCategoryTitle())
           
        }
    };
    self.ShowArrowImg = function (model) {

        var str = "#imgid" + model.FAQID();    
        var url = $(str).attr('src');
        var res = url.split("/");
        var imgid = res[res.length - 1];       
        $("img[id^='imgid']").attr('src', '/Content/img/r-arrow.png');
        if (imgid == 'r-arrow.png') {
            $(str).attr('src', '/Content/img/d-arrow.png');
        }
        else {
            $(str).attr('src', '/Content/img/r-arrow.png');
        }
    }

    self.ShowFAQ = function (model) {
        var catID = model.FAQCatID();
           $.ajax({
            url: "/Resource/FAQByCatID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ faqCatID: catID }),
            success: function (data) {
             if (data != null) {                
                    ko.mapping.fromJS(data, {}, self.FAQResults)
                    self.FaqCatName(model.FAQCategoryTitle())
                }
            },
            error: function (data) {
                alert("No Question Is added - " + data);
            }
        });
    }
}