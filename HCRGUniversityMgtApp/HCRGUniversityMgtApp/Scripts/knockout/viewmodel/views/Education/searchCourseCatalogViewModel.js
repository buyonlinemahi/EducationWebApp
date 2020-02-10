function SearchCourseCatalogViewModel() {
    var self = this;
    self.EducatoinSearchResults = ko.observableArray();
    self.EncryptedEducationID = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.searchText = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);

    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv').hide();

    self.ClientName = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.IsHCRGAdmin = ko.observable();

    var mappingOptions = {
        'CourseUploadDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
    this.Init = function (model) {

        if (model.Educations != null) {
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                ko.mapping.fromJS(model.Educations, mappingOptions, self.EducatoinSearchResults);
                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
        getAllOrganization(0);
    };


    self.SearchCourseCatalogByOrgID = function () {
        if ($("#DrpOrgID").val() != "") {
            $.post("/Education/GetAllCourseCatalogByOrganizationID/", { OrgID: self.SearchSelectedOrganization(), searchText: $("#selectTestSearch").val() }, function (data) {
                self.EducatoinSearchResults.removeAll();
                ko.mapping.fromJS(data.Educations, {}, self.EducatoinSearchResults);
            });
        }
        else {
            alertify.alert("Please select organization");
        }
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

    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllSearchCourseCatalog();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllSearchCourseCatalog() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/education/SearchCourseCatalog", { skip: self.maxId(), searchText: self.searchText() }, function (data) {
                var data = $.parseJSON(data);
                for (var i = 0; i < data.Educations.length; i++) {
                    self.EducatoinSearchResults.push(new InsertEducatoinLineItem(data.Educations[i].CollegeName, data.Educations[i].CourseName, moment(data.Educations[i].CourseUploadDate).format("MM/DD/YYYY"),
                                     data.Educations[i].NumberOfModule, data.Educations[i].EducationID, data.Educations[i].IsPublished, data.Educations[i].IsCoursePreview, data.Educations[i].ReadyToDisplay, data.Educations[i].OrganizationName));
                }
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }
    }

    self.EducationSearchFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmSearchCourseCatalog").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#DrpOrgID").val(0);
        return true;
    }
    self.addNewEducation = function () {
        window.location = '/education/addEducation';
    }
    self.updateEducation = function (model) {
       
        window.location = '/education/editEducation?educationID=' + model.EncryptedEducationID();
       
    }

    self.coursePreviewEducation = function (model) {
        alertify.confirm("Are you sure you want to Preview this Course?", function (e) {
            if (e) {
                $.post("/education/CoursePreviewEducation", { educationID: model.EducationID() }, function (data) {
                    var data = $.parseJSON(data);
                    for (var i = 0; i <= self.EducatoinSearchResults().length - 1; i++) {
                        if (self.EducatoinSearchResults()[i].EducationID() == model.EducationID()) {
                            self.EducatoinSearchResults.splice(i, 1);
                            self.EducatoinSearchResults.splice(i, 0, new InsertEducatoinLineItem(model.CollegeName(), model.CourseName(), moment(model.CourseUploadDate).format("MM/DD/YYYY"),
                                model.NumberOfModule(), model.EducationID(), false, true, model.ReadyToDisplay(), model.OrganizationName(), model.ClientName()));

                            alertify.success(data);
                            window.location = '/education/PreviewEducation?educationID=' + model.EncryptedEducationID();
                            break;
                        }
                    }

                });
            }
        });
    }

    self.publishEducation = function (model) {
        alertify.confirm("Are you sure you want to Publish this Course? if yes, then you will not be able to Edit/Preview this course.", function (e) {
            if (e) {
                $.post("/education/PublishEducation", { educationID: model.EducationID() }, function (data) {
                    var data = $.parseJSON(data);
                    for (var i = 0; i <= self.EducatoinSearchResults().length - 1; i++) {
                        if (self.EducatoinSearchResults()[i].EducationID() == model.EducationID()) {
                            self.EducatoinSearchResults.splice(i, 1);
                            self.EducatoinSearchResults.splice(i, 0, new InsertEducatoinLineItem(model.CollegeName(), model.CourseName(), moment(model.CourseUploadDate).format("MM/DD/YYYY"),
                                            model.NumberOfModule(), model.EducationID(), true, true, model.ReadyToDisplay(), model.OrganizationName(), model.ClientName()));

                            alertify.success(data);
                            break;
                        }
                    }

                });
            }
        });


    }

    var mappingOptions = {
        CourseUploadDate: {
            create: function (options) {
                return moment(options.data).format("MM/DD/YYYY");
            }
        }
    };
    self.SearchCourseCatalogSuccess = function (model) {
        var _model = $.parseJSON(model);
        self.pendingRequest(false);
        self.CurrentPage(0);
        self.TotolCount(0);
        self.maxId(0);
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(_model.Educations, mappingOptions, self.EducatoinSearchResults);
            self.TotolCount(_model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + _model.PagedRecords);
            $('#loaderDiv').hide();
        }
    }
    function InsertEducatoinLineItem(_CollegeName, _CourseName, _CourseUploadDate, _NumberOfModule, _EducationID, _IsPublished, _IsCoursePreview, _ReadyToDisplay, _OrganizationName, _ClientName) {
        var self = this;
        self.CollegeName = ko.observable(_CollegeName);
        self.CourseName = ko.observable(_CourseName);
        self.CourseUploadDate = ko.observable(_CourseUploadDate);
        self.NumberOfModule = ko.observable(_NumberOfModule);
        self.EducationID = ko.observable(_EducationID);
        self.IsPublished = ko.observable(_IsPublished);
        self.ReadyToDisplay = ko.observable(_ReadyToDisplay);
        self.IsCoursePreview = ko.observable(_IsCoursePreview);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.ClientName = ko.observable(_ClientName);
    }

}