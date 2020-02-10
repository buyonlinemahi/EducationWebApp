function HomeContentnGridViewModel() {
    var self = this;

    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.HomeContentID = ko.observable();
    self.msg = ko.observable();
    self.HomePageContent = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.OrganizationID = ko.observable();

    self.HomeContentData = ko.observableArray([]);

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
     this.Init = function (model) {
         if (model != null) {
             if (model.HomeContentResults.length != 0) {
                 $("#divAddContentData").hide();
             }
            ko.mapping.fromJS(model, {}, self);
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            $.each(model.HomeContentResults, function (index, value) {
                self.HomeContentData.push(new HomeContent(value));
              
            });

            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
        }
        getAllOrganization();
     };
    
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

//==============get data for Grid binding===========================//
     function getAllHomeData() {      
         $.post("/HomeContent/GridData", function (data) {
             self.HomeContentData.removeAll();
             ko.mapping.fromJS(data.HomeContentResults, {}, self.HomeContentData);
         });
     }
    //===================================================================//

     self.HomeContentDetailSuccess = function (model) {
         var newHomeContent = $.parseJSON(model);
         if (newHomeContent.msg != 'You are not authorized to add Home Content') {
             if (newHomeContent != null) {
                 $("#divAddContentData").hide();
                var viewModel = self.HomeContentData;
                if (!newHomeContent.flag) {
                    for (var i = 0; i <= viewModel().length - 1; i++) {
                        if (viewModel()[i].HomeContentID() == newHomeContent.HomeContentID) {
                            self.HomeContentData.splice(i, 1);
                            self.HomeContentData.splice(i, 0, new InsertHomeContentLineItem(newHomeContent.HomeContentID, newHomeContent.HomePageContent, newHomeContent.DescriptionShort, newHomeContent.OrganizationName,newHomeContent.OrganizationID));
                            alertify.success("Home Content  Updated Successfully");
                     
                            break;
                        }
                    }
                }
                else {
                    self.HomeContentData.splice(i, 0, new InsertHomeContentLineItem(newHomeContent.HomeContentID, newHomeContent.HomePageContent, newHomeContent.DescriptionShort, newHomeContent.OrganizationName,newHomeContent.OrganizationID));
                    $("#main").scrollTop(0);
                    alertify.success("Home Content Insert Successfully");
                    $("#divAddContentData").hide();
                    self.TotolCount(self.TotolCount() + 1);
                    self.maxId(self.maxId() + 1);
                }
                resetcontrol();
            }
        }
        else {
            alertify.success("You Are not authorized to Add Content");
         }
         getAllHomeData();
    };

    self.HomeContentInfoFormBeforeSubmit = function (arr, $form, options) {
       var string = arr[1].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#EditorContent").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorContent").val()).html());//encoding
        return true;
    };

    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });

    $(".btnClose").click(function () {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    ko.mapping.fromJS(model, {}, self);
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }

    self.EditHomeContentLineItems = function (config) {
        self.HomeContentID(config.HomeContentID());
        self.HomePageContent(config.HomePageContent());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("EditorContent").editor;
        editor1.SetText(($("<div>").html(config.HomePageContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divAddContentData").show();
    }

    function HomeContent(value) {
        var self = this;
        self.HomeContentID = ko.observable(value.HomeContentID);
        self.HomePageContent = ko.observable(value.HomePageContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };


    function InsertHomeContentLineItem(_HomeContentID, _HomePageContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.HomeContentID = ko.observable(_HomeContentID);
        self.HomePageContent = ko.observable(_HomePageContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.HomeContentID("");
        self.HomePageContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("EditorContent").editor;
        editor1.SetText("");
    }

    self.SearchHomeContent = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/HomeContent/GetHomeContentByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
            self.HomeContentData.removeAll();
            ko.mapping.fromJS(data, {}, self.HomeContentData);
        });
    };
  
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorContent").val()).html());//encoding
}

