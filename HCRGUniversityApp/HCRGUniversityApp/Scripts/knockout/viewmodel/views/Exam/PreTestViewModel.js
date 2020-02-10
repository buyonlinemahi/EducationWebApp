function PreTestViewModel(model) {
    var self = this;
    self.PreTestQuestionDetailResults = ko.observableArray([]);
    if (model.PreTestAttemptByUser < 1)
        ko.mapping.fromJS(model.PreTestQuestionDetailResults, {}, self.PreTestQuestionDetailResults);
    else {
        alertify.confirm("User Already Attempt This Pre-Test.", function (e) {
            if (e) {
                window.location = '/MyEducation/';
            }
        });
    }
    self.Isreturn = ko.observable(true);
    self.AddPreTestInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddPreTestResult").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        validateradiobutton();
        if (self.Isreturn()) 
            return true;
        else
            return false;
        
        return true;
    }
    self.AddPreTestDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        if (data < 1) {
            alertify.confirm("PreTest Submitted Successfully.", function (e) {
                if (e) {
                    window.location = '/MyEducation/';
                }
            });
        }
        else {
            alertify.confirm("User Already Attempt This Pre-Test.", function (e) {
                if (e) {
                    window.location = '/MyEducation/';
                }
            });
        }
        
}
    self.edit = function (data) {

        alert();
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
