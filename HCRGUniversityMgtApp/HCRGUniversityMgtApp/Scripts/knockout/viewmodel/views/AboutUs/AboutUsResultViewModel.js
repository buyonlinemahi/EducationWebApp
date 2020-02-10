function AboutUsGridViewModel() {
    var self = this;
    self.AboutUsID = ko.observable();
    self.Description = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();    
    self.DescriptionShort = ko.observable();
    self.AboutusResults = ko.observableArray([]);
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);

    self.IsHCRGAdmin = ko.observable();
    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    $('#loaderDiv').hide();
    this.Init = function (model) {
        if (model != null) {
            if (model.AboutUsRecords.length != 0)
            {
                $("#divAddContent").hide();
            }
            
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();                                
                $.each(model.AboutUsRecords, function (index, value) {
                    self.AboutusResults.push(new Aboutus(value));
                });                
                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                self.IsHCRGAdmin(model.IsHCRGAdmin);
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

    self.SearchAboutUs = function ()
    {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/AboutUs/GetAboutUsByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
            self.AboutusResults.removeAll();
            $.each(data, function (index, value) {                
                self.AboutusResults.push(new Aboutus(value));
            });
        });
    }

    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllAboutUs();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllAboutUs() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.ajax({
                url: '/AboutUs/GetAllPagedAboutus',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ skip: self.maxId() }),
                success: function (data) {
                    $.each(data.AboutUsRecords, function (index, value) {
                        self.AboutusResults.push(new Aboutus(value));
                    });
                    self.TotolCount(data.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + data.PagedRecords);
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    alert('Error while deleting a item - ' + data);
                }
            });
        }
    }
    self.AddAboutusDetailSuccess = function (model) {
        if (model != null) {
            $("#divAddContent").hide();
            var newaboutus = $.parseJSON(model);
            var viewModel = self.AboutusResults;
            if (!newaboutus.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].AboutUsID() == newaboutus.AboutUsID) {
                        self.AboutusResults.splice(i, 1);
                        self.AboutusResults.splice(i, 0, new InsertAboutusLineItem(newaboutus.AboutUsID, newaboutus.Description, newaboutus.DescriptionShort, newaboutus.OrganizationName, newaboutus.OrganizationID));
                        alertify.success("AboutUs Topic Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.AboutusResults.splice(0, 0, new InsertAboutusLineItem(newaboutus.AboutUsID, newaboutus.Description, newaboutus.DescriptionShort, newaboutus.OrganizationName, newaboutus.OrganizationID));
                // $('html, body').scrollTop($(document).height());
                $("#main").scrollTop(0);
                alertify.success("AboutUs Topic Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };
    self.AboutUsInfoFormBeforeSubmit = function (arr, $form, options) {
        var string = arr[1].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#frmAddAboutus").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#frmAddAboutus").val()).html());//encoding
        return true;
      
    }
    this.DeleteAboutUsLineItems = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: '/AboutUs/DeleteAboutUsInfo',
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ aboutus: itemToDelete }),
                    success: function (data) {
                        self.AboutusResults.remove(function (item) { return item.AboutUsID == itemToDelete.AboutUsID })
                        alertify.success(data);
                        self.TotolCount(self.TotolCount() - 1)
                        if (self.CurrentPage() < self.TotolCount() && self.TotolCount() > self.AboutusResults().length) {
                            $.post("/AboutUs/GetAllPagedAboutus", { skip: self.maxId()-1, take: 1 }, function (_data) {
                                var data = $.parseJSON(_data);
                                $("#loaderDiv").show();
                                var data = $.parseJSON(_data);
                                for (var i = 0; i < data.AboutUsRecords.length; i++) {
                                    self.AboutusResults.push(new InsertAboutusLineItem(data.AboutUsRecords[i].AboutUsID, data.AboutUsRecords[i].Description, data.AboutUsRecords[i].DescriptionShort));
                                }
                                self.TotolCount(data.TotalCount);
                                $('#loaderDiv').hide();
                            });
                        }
                    },
                    error: function (data) {
                        alert('Error while deleting a item - ' + data);
                    }
                });
            }
        });
    };
    function Aboutus(value) {
        var self = this;
        self.AboutUsID = ko.observable(value.AboutUsID);
        self.Description = ko.observable(value.Description);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
        self.OrganizationName = ko.observable(value.OrganizationName);
        self.OrganizationID = ko.observable(value.OrganizationID);
    };

    self.ViewAboutUsLineItems = function (config) {
        self.AboutUsID(config.AboutUsID());
        self.Description(config.Description());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText(($("<div>").html(config.Description()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
        $("#divAddContent").show();
    }
 

    function resetcontrol() {
        self.AboutUsID("");
        self.Description("");
        self.OrganizationName("");
        self.OrganizationID("");
        var editor1 = document.getElementById("Editor1").editor;
        editor1.SetText("");
    }


    $("#btnClose").click(function () {
        clearcontent();
    });
    $("#btnClose1").click(function () {
        clearcontent();
    });

    $("#btnSaveandContinue").click(function () {
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

    function clearcontent() {
        if ($("#hdAboutUsID").val() == "") {
            alertify.confirm("Are you sure want to clear this content?", function (e) {
                if (e) {
                    $("#hd").val("");
                    var editor1 = document.getElementById("Editor1").editor;
                    editor1.SetText("");
                    $("#EditorModal").css("display", "none");
                    $("#EditorModal").removeClass("in");
                    $(".modal-backdrop").remove();
                    $("#inner-content").removeClass("modal-open");
                }
            });
        }
        else {
            $("#EditorModal").css("display", "none");
            $("#EditorModal").removeClass("in");
            $(".modal-backdrop").remove();
            $("#inner-content").removeClass("modal-open");
        }
    }
    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });
};
function InsertAboutusLineItem(_AboutUsID,_Description,_DescriptionShort, _OrganizationName, _OrganizationID) {
    var self = this;
    self.AboutUsID = ko.observable(_AboutUsID);
    self.Description = ko.observable(_Description);
    self.DescriptionShort = ko.observable(_DescriptionShort);
    self.OrganizationName = ko.observable(_OrganizationName);
    self.OrganizationID = ko.observable(_OrganizationID);
}
function submitform() {
    $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding
}

