function CarouselSetupViewModel(model) {
    var self = this;
    self.CarouselID = ko.observable(0);
    self.CarouselTitle = ko.observable();
    self.CarouselDescription = ko.observable();
    self.CarouselBgColor = ko.observable();
    self.NewsID = ko.observable();
    self.hdNewsID = ko.observable();
    self.NewsTitle = ko.observable();
    self.NewsDescription = ko.observable();
    self.NewsSectionTitle = ko.observable();
    self.CarouselDataResult = ko.observableArray([]);
    self.NewsDataResult = ko.observableArray([]);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.IsHCRGAdmin = ko.observable();

    $(document).ready(function () {
        getAllOrganization(0);
        $("#NewsTitle").hide();
    })
   
    if (model != null)
        if (model.carouselSetupResult.length != 0) {
            $("#divCarousel").hide();
            if (model.IsHCRGAdmin == false) {
                $("#divCarousel").show();
            }
        }
    {
        self.IsHCRGAdmin(model.IsHCRGAdmin);
        ko.mapping.fromJS(model.carouselSetupResult, {}, self.CarouselDataResult);
        
    }

    function getAllOrganization() {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                self.AllOrganizations.removeAll();
                self.AllOrganizations(data.slice());
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }

    self.SearchCarouselSetUp = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/CarouselSetUp/GetCarouselSetUpByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (data) {
            self.CarouselDataResult.removeAll();
            ko.mapping.fromJS(data.carouselSetupResult, {}, self.CarouselDataResult);

        });
    };
  
    self.AddCarousel = function (model) {        
        if (model != null) {
            $('#NewsTitle').hide();
            $("#divCarousel").hide();
            var viewModel = self.CarouselDataResult();
            if (model.NewsDescription == "Update") {
                for (var i = 0; i <= viewModel.length - 1; i++) {
                    if (viewModel[i].CarouselID() == model.CarouselID) {
                        self.CarouselDataResult.splice(i, 1);
                        self.CarouselDataResult.splice(i, 0, new InsertCarouselDataLineItem(model.CarouselID, model.CarouselTitle, model.CarouselDescription, model.CarouselBgColor, model.NewsID, model.NewsTitle,model.OrganizationID,model.OrganizationName));                       
                        alertify.success("Carousel Updated Successfully");
                    }
                }
            }
            else if (model.NewsDescription == "Add") {
                self.CarouselDataResult.push(new InsertCarouselDataLineItem(model.CarouselID, model.CarouselTitle, model.CarouselDescription, model.CarouselBgColor, model.NewsID, model.NewsTitle,model.OrganizationID,model.OrganizationName));
                alertify.success("Carousel Added Successfully");
            }
            resetControl();
        }
    }

    self.CarouselInfoFormBeforeSubmit = function (arr, $form, options) {
        var viewModel = self.CarouselDataResult();
        if (self.CarouselID() == 0) {
            if (viewModel.length >= 5) {
                alertify.alert("You can enter only five records");
                return false;
            }
            //for (var i = 0; i <= viewModel.length - 1; i++) {
            //    if (self.CarouselBgColor() == viewModel[i].CarouselBgColor()) {
            //        alertify.alert("Selected background color already exists");
            //        return false;
            //    }
            //}
        }
        else {
            //for (var i = 0; i <= viewModel.length - 1; i++) {
            //    if (self.CarouselID() != viewModel[i].CarouselID() && self.CarouselBgColor() == viewModel[i].CarouselBgColor()) {
            //        alertify.alert("Selected background color already exists");
            //        return false;
            //    }
            //}
        }
        if ($("#frmAddCarousel").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };

    self.SelectNewsCarousel = function (modelPartial) {
        self.NewsID(modelPartial.NewsID());
        self.NewsTitle(modelPartial.NewsTitle());
        $('#NewsTitle').show();
        $('#sectionClose').click();
    };

    self.OpenNewsArticle = function () {
        var searchText = $("#SearchNewsTitle").val();
        if (searchText == "") {
            alert("News title required")
        }
        else {
            $.ajax({
                url: "/CarouselSetUp/SearchNewsSection",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ NewsTitle: $("#SearchNewsTitle").val() }),
                success: function (data) {
                    if (data.length >0) {
                        ko.mapping.fromJS(data, {}, self.NewsDataResult);
                    }
                    else {
                        alertify.alert("No data found");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
    };

    self.EditCarouselData = function (model) {     
        self.CarouselID(model.CarouselID());
        self.CarouselTitle(model.CarouselTitle());
        self.CarouselDescription(model.CarouselDescription());
        self.CarouselBgColor(model.CarouselBgColor());
        self.NewsID(model.NewsID());
        self.NewsTitle(model.NewsTitle());
        self.OrganizationID(model.OrganizationID());
        self.OrganizationName(model.OrganizationName());
        $('#NewsTitle').show();
        $("#divCarousel").show();
    };


    $('#sectionClose').click(function () {
        self.NewsDataResult.removeAll();
        $('#SearchNewsTitle').val("");
    });

    function resetControl() {
        self.CarouselID(0);
        self.CarouselTitle("");
        self.CarouselDescription("");
        self.CarouselBgColor("");
        self.NewsID(0);
        self.NewsTitle("");
        self.NewsDescription("");
        self.OrganizationID("");
        self.OrganizationName("");
        $("#NewsTitle").hide();
    };
};

function InsertCarouselDataLineItem(_carouselID, _carouselTitle, _carouselDescription, _carouselBgColor, _carouselNewsID, _carouselNewsTitle, _organizationID, _organizationName) {
    var self = this;
    self.CarouselID = ko.observable(_carouselID);
    self.CarouselTitle = ko.observable(_carouselTitle);
    self.CarouselDescription = ko.observable(_carouselDescription);
    self.CarouselBgColor = ko.observable(_carouselBgColor);
    self.NewsID = ko.observable(_carouselNewsID);
    self.NewsTitle = ko.observable(_carouselNewsTitle);
    self.OrganizationID = ko.observable(_organizationID);
    self.OrganizationName = ko.observable(_organizationName);
};