
function SuggestCourseDetailViewModel() {


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

    self.SCSingleDaySeminarCourse = ko.observable(false);
    self.SCOndemandLiveWebinarCourse = ko.observable(false);
    self.SCInterestedInstructor = ko.observable(false);
    

    self.SCselectedItem = ko.observable(0);
    self.SCStates = ko.observable();
    self.SCStates = ko.observableArray();
    self.StateResults = ko.observableArray([]);

    this.Init = function (model) {
        if (model != null) {

            ko.mapping.fromJS(model.SuggestCourseResults, {}, self);
            self.StateResults(model.StateResults.slice());
            self.SCStates = ko.observableArray(self.StateResults());
        }
    }

    self.AddSuggestCourseInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEnterprisePackageRegister").jqBootstrapValidation('hasErrors')) {
            return false;
        } 
        self.SCStateID($("#SCStateID option:selected").val());
        if ($('#chkSCSingleDaySeminarCourse').is(':checked') || $('#chkSCOndemandLiveWebinarCourse').is(':checked')) {
            // do nothing
        }
        else {
            alertify.alert("Please select aleast one checkbox.");
            return false;
        }
        return true;
    }

    self.AddSuggestCourseDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        alertify.alert(data);
    }

    $(document).ready(function () {
        $('.phonemask').mask('(999)-999-9999');
    });
};