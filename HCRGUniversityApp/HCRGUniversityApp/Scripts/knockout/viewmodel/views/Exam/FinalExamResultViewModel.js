function FinalExamResultViewModel(model) {
    var self = this;
    self.meID = ko.observable(model.MEID);
    self.ExamQuestions = ko.observableArray([]);
    self.IsPass = ko.observable(false);
    self.IsEvalutionAvailable = ko.observable(model.IsEvalutionAvailable);
    self.TotalAttempt = ko.observable(model.totalAttemptByUser);  
    if (model.TotalPercentage >= 70) {
        self.IsPass(true);
    }
    else {
        self.IsPass(false);
    }
    ko.mapping.fromJS(model, {}, self);
    hideloderDiv();
    self.EvaluationExam = function (model) {
        var path;
        if(self.IsEvalutionAvailable())
          path = '/Exam/EvaluationExam/' + model.EncryptedEducationID() + '/' + model.EncryptedMEID();
        else
            path = '/MyEducation/';
        window.location = path;
    }

    self.ViewExamQuestion = function () {
        $.post("/Exam/GetExamQuestionWrongAnswered", { meID: self.meID() }, function (_data) {
            ko.mapping.fromJS(_data, {}, self.ExamQuestions);
        });
    }
    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }

}