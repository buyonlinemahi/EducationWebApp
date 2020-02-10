function TrainingAndSeminarResultViewModel() {
    var self = this;
    self.TrainingAndSeminarID = ko.observable();
    self.TrainingAndSeminarDesc = ko.observable();
    self.DescriptionShort = ko.observable();


    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();
    self.TrainingAndSeminarData = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false)

    self.IsHCRGAdmin = ko.observable();
    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    this.Init = function (model) {
        if (model != null) {
            if (model.TrainingAndSeminarResults.length != 0) {
                $("#divAddTrainingAndSeminar").hide();
            }
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                $.each(model.TrainingAndSeminarResults, function (index, value) {
                    self.TrainingAndSeminarData.push(new TrainingAndSeminar(value));
                });

                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
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

        self.SearchTrainingAndSeminar = function () {
            if ($("#DrpOrgID").val() == '') {
                alertify.alert("Please select atleast one Organization");
                return false;
            }
            $.post("/TrainingAndSeminar/GetTrainingAndSeminarByOrganizationID/", { organizationID: self.SearchSelectedOrganization() }, function (data) {
                self.TrainingAndSeminarData.removeAll();
                $.each(data.TrainingAndSeminarResults, function (index, value) {
                    self.TrainingAndSeminarData.push(new TrainingAndSeminar(value));
                });

            });
        };

        self.TrainingAndSeminarDetailSuccess = function (model) {
            if (model != null) {
                $("#divAddTrainingAndSeminar").hide();
               var nreTraniningAndSeminar = $.parseJSON(model);
                var viewModel = self.TrainingAndSeminarData;
                if (!nreTraniningAndSeminar.flag) {
                    for (var i = 0; i <= viewModel().length - 1; i++) {
                        if (viewModel()[i].TrainingAndSeminarID() == nreTraniningAndSeminar.TrainingAndSeminarID) {
                            self.TrainingAndSeminarData.splice(i, 1);
                            self.TrainingAndSeminarData.splice(i, 0, new InsertTrainingAndSeminarLineItem(nreTraniningAndSeminar.TrainingAndSeminarID, nreTraniningAndSeminar.TrainingAndSeminarDesc, nreTraniningAndSeminar.DescriptionShort, nreTraniningAndSeminar.OrganizationName, nreTraniningAndSeminar.OrganizationID));
                            alertify.success("Training And Seminar Updated Successfully");
                            break;
                        }
                    }
                }
                else {
                    self.TrainingAndSeminarData.splice(i, 0, new InsertTrainingAndSeminarLineItem(nreTraniningAndSeminar.TrainingAndSeminarID, nreTraniningAndSeminar.TrainingAndSeminarDesc, nreTraniningAndSeminar.DescriptionShort, nreTraniningAndSeminar.OrganizationName, nreTraniningAndSeminar.OrganizationID));
                    $("#main").scrollTop(0);
                    alertify.success("Training And Seminar Insert Successfully");
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

        self.TrainingAndSeminarInfoFormBeforeSubmit = function (arr, $form, options) {
            var string = arr[3].value;
            var Rplacedstring = string.replace('&amp;', "");
            Rplacedstring = Rplacedstring.replace('nbsp;', "");
            if (Rplacedstring == " " || Rplacedstring == "") {
                alertify.alert("Content cannot be empty!");
                return false;
            }
            if ($("#frmTrainingAndSeminar").jqBootstrapValidation('hasErrors')) {
                return false;
            }
            $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding
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

        self.EditTrainingAndSeminarLineItems = function (config) {
            self.TrainingAndSeminarID(config.TrainingAndSeminarID());
            self.TrainingAndSeminarDesc(config.TrainingAndSeminarDesc());
            self.DescriptionShort(config.DescriptionShort());
            self.OrganizationName(config.OrganizationName());
            self.OrganizationID(config.OrganizationID());
            var editor1 = document.getElementById("Editor").editor;
            editor1.SetText(($("<div>").html(config.TrainingAndSeminarDesc()).text()));
            $(window).scrollTop(0);
            $("#EditorModalPopUp").click();
            $("#divAddTrainingAndSeminar").show();

        }
        function TrainingAndSeminar(value) {
            var self = this;
            self.TrainingAndSeminarID = ko.observable(value.TrainingAndSeminarID);
            self.TrainingAndSeminarDesc = ko.observable(value.TrainingAndSeminarDesc);
            self.DescriptionShort = ko.observable(value.DescriptionShort);
            self.OrganizationName = ko.observable(value.OrganizationName);
            self.OrganizationID = ko.observable(value.OrganizationID);
        };


        function InsertTrainingAndSeminarLineItem(_TrainingAndSeminarID, _TrainingAndSeminarDesc, _DescriptionShort, _OrganizationName, _OrganizationID) {
            var self = this;
            self.TrainingAndSeminarID = ko.observable(_TrainingAndSeminarID);
            self.TrainingAndSeminarDesc = ko.observable(_TrainingAndSeminarDesc);
            self.DescriptionShort = ko.observable(_DescriptionShort);
            self.OrganizationName = ko.observable(_OrganizationName);
            self.OrganizationID = ko.observable(_OrganizationID);
        }

        function resetcontrol() {
            self.TrainingAndSeminarID("");
            self.TrainingAndSeminarDesc("");
            self.OrganizationName("");
            self.OrganizationID("");
            var editor1 = document.getElementById("Editor").editor;
            editor1.SetText("");
        }

    }
}
function submitform() {
    $("#hd").val($("<div>").text($("#Editor").val()).html());//encoding
}

