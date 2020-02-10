function NewsLetterGridViewModel() {
    var self = this;
    self.NewsLetterID = ko.observable();
    self.NewsLetterContent = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.NewsLetterData = ko.observableArray([]);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    self.IsHCRGAdmin = ko.observable();

   this.Init = function (model) {
       if (model != null) {           
           if (model.NewsLetterResutls.length != 0)
           {
               $("#divUpdateContentButton").hide();
           }
           self.IsHCRGAdmin(model.IsHCRGAdmin);
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                $.each(model.NewsLetterResutls, function (index, value) {
                    self.NewsLetterData.push(new NewsLetter(value));
                });

                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
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

   self.SearchNewsLetter = function () {
       if ($("#DrpOrgID").val() == '') {
           alertify.alert("Please select atleast one Organization");
           return false;
       }
       $.post("/HomeContent/GetNewsLetterByClientID/", { orgID: self.SearchSelectedOrganization() }, function (data) {
           self.NewsLetterData.removeAll();
           $.each(data.NewsLetterResutls, function (index, value) {
               self.NewsLetterData.push(new NewsLetter(value));
           });
           self.TotolCount(data.TotalCount);
           self.CurrentPage(self.maxId());
           self.maxId(self.maxId() + data.PagedRecords);
       });
   };
    self.NewsLetterDetailSuccess = function (model) {
        if (model != null) {
            var nreLetterCondtion = $.parseJSON(model);
            var viewModel = self.NewsLetterData;
            if (!nreLetterCondtion.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].NewsLetterID() == nreLetterCondtion.NewsLetterID) {
                        self.NewsLetterData.splice(i, 1);
                        $("#divUpdateContentButton").hide();
                        self.NewsLetterData.splice(i, 0, new InsertNewsLetterLineItem(nreLetterCondtion.NewsLetterID, nreLetterCondtion.NewsLetterContent, nreLetterCondtion.DescriptionShort, nreLetterCondtion.OrganizationName, nreLetterCondtion.OrganizationID));
                        alertify.success("News Letter Updated Successfully");                                                
                        break;
                    }
                }
            }
            else {
                self.NewsLetterData.splice(i, 0, new InsertNewsLetterLineItem(nreLetterCondtion.NewsLetterID, nreLetterCondtion.NewsLetterContent, nreLetterCondtion.DescriptionShort, nreLetterCondtion.OrganizationName, nreLetterCondtion.OrganizationID));
                $("#main").scrollTop(0);
                $("#divUpdateContentButton").hide();
                alertify.success("NewsLetter Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };

    self.NewsLetterInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[3].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#EditorNewsLetter").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorNewsLetter").val()).html());//encoding
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
    self.EditNewsLetterLineItems = function (config) {
         self.NewsLetterID(config.NewsLetterID());
         self.NewsLetterContent(config.NewsLetterContent());
         self.OrganizationName(config.OrganizationName());
         self.OrganizationID(config.OrganizationID());
         var editor1 = document.getElementById("EditorNewsLetter").editor;
        editor1.SetText(($("<div>").html(config.NewsLetterContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divUpdateContentButton").show();
    }

    function NewsLetter(value) {
        var self = this;
        self.NewsLetterID = ko.observable(value.NewsLetterID);
        self.NewsLetterContent = ko.observable(value.NewsLetterContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };

    function InsertNewsLetterLineItem(_NewsLetterID, _NewsLetterContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.NewsLetterID = ko.observable(_NewsLetterID);
        self.NewsLetterContent = ko.observable(_NewsLetterContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.NewsLetterID("");
        self.NewsLetterContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("EditorNewsLetter").editor;
        editor1.SetText("");
    }
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorNewsLetter").val()).html());//encoding
}

