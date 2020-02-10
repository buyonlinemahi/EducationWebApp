function SuggestCoursesViewModel() {
    var self = this;
    self.SuggestCourseID = ko.observable();
    self.SCMyOccupation = ko.observable();
    self.SCStateID = ko.observable();
    self.SCName = ko.observable();
    self.SCPhone = ko.observable();
    self.SCEmail = ko.observable();
    self.SCPossibleTitle = ko.observable();
    self.SCBriefAgendaOutline = ko.observable();
    self.SCAudience = ko.observable();
    self.SCSingleDaySeminarCourse = ko.observable();
    self.SCOndemandLiveWebinarCourse = ko.observable();
    self.SCInterestedInstructor = ko.observable();
    self.SCCreatedDate = ko.observable();
    self.SCStateName = ko.observable();
    self.IsHCRGAdmin = ko.observable();
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SuggestCoursesResults = ko.observableArray();
    self.TotalItemCount = ko.observable(0);


    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);

    self.IsHCRGAdmin = ko.observable();

    var mappingOptions = {
        'SCCreatedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }


    this.Init = function (model) {
        if (model.SuggestCoursesDetails != null) {
            self.IsHCRGAdmin(model.IsHCRGAdmin);
            ko.mapping.fromJS(model.SuggestCoursesDetails, mappingOptions, self.SuggestCoursesResults);
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
        }
        getAllOrganization(0);
    };

    self.SearchSuggestCourses = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/Common/getSuggestCourseByOrganizationID/", { orgID: self.SearchSelectedOrganization() }, function (data) {
            ko.mapping.fromJS(data, mappingOptions, self.SuggestCoursesResults);
        });        
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
    self.SuggestCourseDetailByID = function (dataResult) {
        var _SuggestCourseID = dataResult.SuggestCourseID();
        $.post("/Common/GetSuggestCourseByID", {
            ID: _SuggestCourseID
        }, function (_data) {
            var model = _data;

            self.SuggestCourseID(model.SuggestCourseID);
            self.SCMyOccupation(model.SCMyOccupation);
            self.SCStateID(model.SCStateID);
            self.SCName(model.SCName);
            self.SCPhone(model.SCPhone);
            self.SCEmail(model.SCEmail);
            self.SCPossibleTitle(model.SCPossibleTitle);
            self.SCBriefAgendaOutline(model.SCBriefAgendaOutline);
            self.SCAudience(model.SCAudience);
            self.SCStateName(model.SCStateName);
            if (model.SCSingleDaySeminarCourse == true) {
                $('#chkSCSingleDaySeminarCourse').prop('checked', true);
            }
            else {
                $('#chkSCSingleDaySeminarCourse').prop('checked', false);
            }


            if (model.SCOndemandLiveWebinarCourse == true) {
                $('#chkSCOndemandLiveWebinarCourse').prop('checked', true); 
            }
            else {
                $('#chkSCOndemandLiveWebinarCourse').prop('checked', false);
            }


            if (model.SCInterestedInstructor == true) {
                $('#chkSCInterestedInstructor').prop('checked', true); 
            }
            else {
                $('#chkSCInterestedInstructor').prop('checked', false); 
            }

           
            self.SCCreatedDate(moment(model.CreatedDate).format("MM/DD/YYYY"));
        });
    };

    ko.bindingHandlers.formatNumberText = {
        update: function (element, valueAccessor) {
            var phone = ko.utils.unwrapObservable(valueAccessor());
            var formatPhone = function () {
                return phone.replace(/(\d{3})(\d{3})(\d{4})/, "($1)-$2-$3");
            }
            ko.bindingHandlers.text.update(element, formatPhone);
        }
    };

    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.post("/Common/getAllSuggestCourses", {
            skip: skip
        }, function (_data) {
            var model = $.parseJSON(_data);
            self.SuggestCoursesResults.removeAll();
            ko.mapping.fromJS(model.SuggestCoursesDetails, mappingOptions, self.SuggestCoursesResults);
            self.TotalItemCount(model.TotalCount);
        });
    };

    var pagingSettings = {
        pageSize: 15,
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

};