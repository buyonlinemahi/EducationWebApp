function EvaluationViewModel(model) {
    var self = this;
    self.EvaluationQuestionDetailResults = ko.observableArray([]);
    self.EvaluationAnswers = ko.observableArray([]);
    self.EvaluationID = ko.observable();
    self.CourseName = ko.observable();
    ko.mapping.fromJS(model.EvaluationQuestionDetailResults, {}, self.EvaluationQuestionDetailResults);
    ko.mapping.fromJS(model.EvaluationAnswers, {}, self.EvaluationAnswers);
    if (model.EvaluationQuestionDetailResults.length > 0) {
        self.CourseName(model.EvaluationQuestionDetailResults[0].CourseName);
        self.EvaluationID(model.EvaluationQuestionDetailResults[0].EvaluationID);       
    }
    self.Isreturn = ko.observable(true);
    self.AddEvaluationInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEvaluationResult").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        validateradiobutton();
        if (self.Isreturn())
            return true;
        else
            return false;

        return true;
    }
    self.AddEvaluationDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        alertify.confirm(data, function (e) {
            if (e) {
                window.location = '/MyEducation/';
            }
        });
    }

    function validateradiobutton() {
        var names = {};
        $(':radio').each(function () {
            names[$(this).attr('name')] = true;
        });
        var count = 0;
        $.each(names, function () {
            count++;
        });
        if ($(':radio:checked').length === count) {
            self.Isreturn(true);
        }
        else {
            self.Isreturn(false);
            alertify.confirm("Please Answer all Questions");
        }
    }


}


$(document).ready(function () {
    
    $('.table tbody tr td').click(function (event) {
        if (event.target.type !== 'radio') {
            $(':radio', this).trigger('click');
        }
    });

});
