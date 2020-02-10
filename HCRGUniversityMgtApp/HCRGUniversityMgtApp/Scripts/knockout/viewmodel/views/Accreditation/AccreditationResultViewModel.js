function AccreditationnGridViewModel() {
    var self = this;

    self.AccreditationID = ko.observable();
    self.AccreditationContent = ko.observable();
    self.DescriptionShort = ko.observable();


    self.AccreditationData = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false)

    self.IsHCRGAdmin = ko.observable();

    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
     this.Init = function (model) {
         if (model != null) {
             if (model.AccreditationResults.length != 0) {
                 $("#divAccreditationData").hide();
             }
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $.each(model.AccreditationResults, function (index, value) {
                    self.AccreditationData.push(new Accreditation(value));
                });

                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
        getAllOrganization(0);
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
     self.SearchAccreditation = function () {
         if ($("#DrpOrgID").val() == '') {
             alertify.alert("Please select atleast one Organization");
             return false;
         }
         $.post("/Accreditation/GetAccreditationByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (data) {
             self.AccreditationData.removeAll();
             $.each(data.AccreditationResults, function (index, value) {
                 self.AccreditationData.push(new Accreditation(value));
             });

         });
     };
    self.AccreditationDetailSuccess = function (model) {
        if (model != null) {
            $("#divAccreditationData").hide();
            var nreAccreditation = $.parseJSON(model);
            var viewModel = self.AccreditationData;
            if (!nreAccreditation.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].AccreditationID() == nreAccreditation.AccreditationID) {
                        self.AccreditationData.splice(i, 1);
                        self.AccreditationData.splice(i, 0, new InsertAccreditationLineItem(nreAccreditation.AccreditationID, nreAccreditation.AccreditationContent, nreAccreditation.DescriptionShort, nreAccreditation.OrganizationName, nreAccreditation.OrganizationID));
                        alertify.success("Accreditation Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.AccreditationData.splice(i, 0, new InsertAccreditationLineItem(nreAccreditation.AccreditationID, nreAccreditation.AccreditationContent, nreAccreditation.DescriptionShort, nreAccreditation.OrganizationName, nreAccreditation.OrganizationID));
                $("#main").scrollTop(0);
                alertify.success("Accreditation Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };

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
    self.AccreditationInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[3].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#frmAccreditation").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorAccreditation").val()).html());//encoding
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
    self.EditAccreditationLineItems = function (config) {
        self.AccreditationID(config.AccreditationID());
        self.AccreditationContent(config.AccreditationContent());
        self.DescriptionShort(config.DescriptionShort());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("EditorAccreditation").editor;
        editor1.SetText(($("<div>").html(config.AccreditationContent()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divAccreditationData").show();

    }
    function Accreditation(value) {
        var self = this;
        self.AccreditationID = ko.observable(value.AccreditationID);
        self.AccreditationContent = ko.observable(value.AccreditationContent);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };


    function InsertAccreditationLineItem(_AccreditationID, _AccreditationContent, _DescriptionShort, _OrganizationName, _OrganizationID) {
        var self = this;
        self.AccreditationID = ko.observable(_AccreditationID);
        self.AccreditationContent = ko.observable(_AccreditationContent);
        self.DescriptionShort = ko.observable(_DescriptionShort);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }

    function resetcontrol() {
        self.AccreditationID("");
        self.AccreditationContent("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("EditorAccreditation").editor;
        editor1.SetText("");
    }
  
}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorAccreditation").val()).html());//encoding
}

