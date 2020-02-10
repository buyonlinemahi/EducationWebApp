function EventGridViewModel() {
    var self = this;
    self.EventID = ko.observable();
    self.EventName = ko.observable();
    self.EventDate = ko.observable();
    self.EventDescription = ko.observable();
    self.NewsID = ko.observable();
    self.EducationID = ko.observable();
    self.CourseName = ko.observable();
    self.NewsTitle = ko.observable();
    self.EventResults = ko.observableArray([]);
    self.EducationResults = ko.observableArray([]);
    self.NewsResults = ko.observableArray([]);
    self.SearchText = ko.observable("");
    self.TitleResult = ko.observable();
    self.OldTitleResult = ko.observable();
    self.OldselectedItemId = ko.observable();
    self.TotalItemCount = ko.observable(0);
    self.iTotalItemCount = ko.observable(0);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();


    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    self.IsHCRGAdmin = ko.observable();


    self.LinkTypes = ko.observableArray([{ LinkTypeName: "Article", LinkTypeValue: 'Article Name' }, { LinkTypeName: "Course", LinkTypeValue: 'Course Name' }]);
    this.selectedItemId = ko.observable(3);
    this.selectedItem = function () {
        var self = this;
        return ko.utils.arrayFirst(this.LinkTypes(), function (item) {
            if (self.OldselectedItemId() != item.LinkTypeValue)
                self.TitleResult('');
            else
                self.TitleResult(self.OldTitleResult());
            return self.selectedItemId() == item.LinkTypeValue;
        });
    }.bind(this);

    var mappingOptions = {
        'EventDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    this.Init = function (model) {
        if (model != null) {
            if (model._events.length != 0) {
                $("#divEvents").hide();
                if (model.IsHCRGAdmin == false) {
                    $("#divEvents").show();
                }
            }
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            self.EventResults.removeAll();
            $.each(model._events, function (index, value) {
                self.EventResults.push(new EventGridDetails(value));
            });
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        }
        getAllOrganization();
    };
    
    function EventGridDetails(val) {
        var self = this;
        self.EventID = ko.observable(val.EventID);
        self.EventName = ko.observable(val.EventName);
        self.EventDate = ko.observable(moment(val.EventDate).format("MM/DD/YYYY"));
        self.EventDescription = ko.observable(val.EventDescription);
        self.NewsID = ko.observable(val.NewsID);
        self.EducationID = ko.observable(val.EducationID);
        self.CourseName = ko.observable(val.CourseName);
        self.NewsTitle = ko.observable(val.NewsTitle);
        self.OrganizationName = ko.observable(val.OrganizationName);
        self.OrganizationID = ko.observable(val.OrganizationID);
    }

    self.SearchCourseOrNews = function () {
        self.iTotalItemCount(0);
        self.iSkip(0);
        SearchCourseOrNewsBind();
        self.iPager().iCurrentPage(1);
    }
    $('#SearchText').change(function() {
        $("#SearchText").removeClass("validation-error");
    });
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

    self.SearchEvent = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/Event/GetAllEventsByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {
            self.EventResults.removeAll();
            $.each(data._events, function (index, value) {
                self.EventResults.push(new EventGridDetails(value));
            });
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        });
    };


    function InsertNewsLineItem(_NewsID, _NewsTitle, _Link) {
        var self = this;
        self.NewsID = ko.observable(_NewsID);
        self.NewsTitle = ko.observable(_NewsTitle);
        self.buttonText = ko.observable(_Link);

    }
    function InsertEducationLineItem(_EducationID, _CourseName, _Link) {
        var self = this;
        self.EducationID = ko.observable(_EducationID);
        self.CourseName = ko.observable(_CourseName);
        self.buttonText = ko.observable(_Link);

    }
    self.EventInfoFormBeforeSubmit = function (arr, $form, options) {
        if (self.NewsID() == null && self.EducationID() == null) {
            alertify.alert("Please select  Artical or Course");
            return false;
        }
        if ($("#frmAddEvent").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddEventDetailSuccess = function (model) {
        if (model != null) {
            $("#divEvents").hide();
            var newEvent = $.parseJSON(model);
            var viewModel = self.EventResults;
            
            if (!newEvent.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].EventID() == newEvent.EventID) {
                        if (newEvent.NewsID != null) {
                            newEvent.NewsTitle = self.TitleResult();
                        }
                        else {
                            newEvent.CourseName = self.TitleResult();
                        }
                        self.EventResults.splice(i, 1);
                        self.EventResults.splice(i, 0, new InsertEventLineItem(newEvent.EventID, newEvent.EventName, newEvent.EventDate, newEvent.EventDescription, newEvent.NewsID, newEvent.EducationID, newEvent.CourseName, newEvent.NewsTitle, newEvent.OrganizationName, newEvent.OrganizationID));
                        alertify.success("Event Updated Successfully");
                        break;
                    }
                }
            }
            else {
                RebindEvent();
                alertify.success("Event Insert Successfully");
            }
            resetEventcontrol();
        }
    };
    function RebindEvent() {
        $.post("/Event/", {
            skip: 0
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.EventResults.removeAll();
            for (var i = 0; i < model._events.length; i++) {
                self.EventResults.push(new InsertEventLineItem(model._events[i].EventID, model._events[i].EventName, model._events[i].EventDate, model._events[i].EventDescription, model._events[i].NewsID, model._events[i].EducationID, model._events[i].CourseName, model._events[i].NewsTitle, model._events[i].OrganizationName, model._events[i].OrganizationID));
            }
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        });
    }
    function InsertEventLineItem(_EventID, _EventName, _EventDate, _EventDescription, _NewsID, _EducationID, _CourseName, _NewsTitle, _organizationName, _OrganizationID) {
        var self = this;
        self.EventID = ko.observable(_EventID);
        self.EventName = ko.observable(_EventName);
        self.EventDate = ko.observable(moment(_EventDate).format("MM/DD/YYYY"));
        self.EventDescription = ko.observable(_EventDescription);
        self.NewsID = ko.observable(_NewsID);
        self.EducationID = ko.observable(_EducationID);
        self.CourseName = ko.observable(_CourseName);
        self.NewsTitle = ko.observable(_NewsTitle);
        self.OrganizationName = ko.observable(_organizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }
    function resetEventcontrol() {
        self.EventID("");
        self.EventName("");
        self.EventDate("");
        self.EventDescription("");
        self.NewsID("");
        self.EducationID("");
        self.TitleResult("");
    }

    self.LinkCourseName = function (model) {
        $('#sectionClose').click();
        self.NewsID(null);
        self.EducationID(model.EducationID());
        self.TitleResult(model.CourseName());
    }


    this.DeleteEvent = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: "/Event/DeleteEvent",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ deleteEvent: itemToDelete }),
                    success: function (data) {
                        self.EventResults.remove(function (item) { return item.EventID == itemToDelete.EventID })
                        self.EventResults.removeAll();
                        RebindEvent();
                        alertify.success(data);
                    },
                    error: function (data) {
                        alert("Error while deleting a item - " + data);
                    }
                });
            }
        });
    };

    self.LinkNewsTitle = function (model) {
        $('#sectionClose').click();
        self.NewsID(model.NewsID());
        self.EducationID(null);
        self.TitleResult(model.NewsTitle());

    };
  
    self.ResetGrid = function () {
        self.NewsResults.removeAll();
        self.EducationResults.removeAll();
        self.SearchText("");
        self.iTotalItemCount(0);
    }
    self.editEvent = function (config) {
        self.EventID(config.EventID());
        self.EventName(config.EventName());
        self.EventDate(config.EventDate());
        self.EventDescription(config.EventDescription());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        self.NewsID(config.NewsID());
        self.EducationID(config.EducationID());
        if (self.NewsID() != null) {
            self.TitleResult(config.NewsTitle());
            self.OldTitleResult(config.NewsTitle());
            self.selectedItemId("Article Name");
            self.OldselectedItemId("Article Name");
        }

        else {
            self.OldTitleResult(config.CourseName());
            self.TitleResult(config.CourseName());
            self.OldselectedItemId("Course Name");
            self.selectedItemId("Course Name");
        }
        
        $(window).scrollTop(0);
        $("#divEvents").show();
    }
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.post("/Event/", {
             skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.EventResults.removeAll();
            for (var i = 0; i < model._events.length; i++) {
                self.EventResults.push(new InsertEventLineItem(model._events[i].EventID, model._events[i].EventName, model._events[i].EventDate, model._events[i].EventDescription, model._events[i].NewsID, model._events[i].EducationID, model._events[i].CourseName, model._events[i].NewsTitle, model._events[i].OrganizationName, model._events[i].OrganizationID));
            }
            self.TotalItemCount(model.TotalCount);
        });
    };
    var pagingSettings = {
        pageSize: 10,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });

    //========= paging code for popup grid only ===========//
    var ipagingSettings = {
        ipageSize: 10,
        ipageSlide: 2
    };
    self.iSkip = ko.observable(0);
    self.iTake = ko.observable(ipagingSettings.ipageSize);
    self.iPager = ko.ipager(self.iTotalItemCount);
    self.iPager().iPageSize(ipagingSettings.ipageSize);
    self.iPager().iPageSlide(ipagingSettings.ipageSlide);
    self.iPager().iCurrentPage(1);
    self.iPager().iCurrentPage.subscribe(function () {
        var skip = ipagingSettings.ipageSize * (self.iPager().iCurrentPage() - 1);
        var take = ipagingSettings.ipageSize;
        self.iGetRecordsWithSkipTake(skip, take);
    });
    self.iGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.iSkip(0);
            self.iTake(ipagingSettings.ipageSize);
        }
        else {
            self.iSkip(skip);
            self.iTake(take);
        }

        SearchCourseOrNewsBind();

    };
    // ============= paging code for invoice ends =============== //
    function SearchCourseOrNewsBind() {
        var LinkType = $('#drpLinkType :selected').text();

        if (self.SearchText() != "") {
            self.EducationResults.removeAll();
            self.NewsResults.removeAll();
            if (LinkType == 'Course') {
                $.post("/Education/GetEducationReadyToDisplayByEducationName", { educationName: self.SearchText(), skip: self.iSkip() }, function (data) {
                    var data = $.parseJSON(data);
                    ko.mapping.fromJS(data._educations, {}, self.EducationResults);
                    self.iTotalItemCount(data.TotalCount);
                });
            }
            else {
                $.post("/Event/GetNewsByNewsTitle", { newsTitle: self.SearchText(), skip: self.iSkip() }, function (data) {
                    var data = $.parseJSON(data);
                    ko.mapping.fromJS(data.NewsRecords, {}, self.NewsResults);
                    self.iTotalItemCount(data.TotalCount);
                });
            }

        }
        else {
            $("#SearchText").addClass("validation-error");
            return false;
        }
    }
}
