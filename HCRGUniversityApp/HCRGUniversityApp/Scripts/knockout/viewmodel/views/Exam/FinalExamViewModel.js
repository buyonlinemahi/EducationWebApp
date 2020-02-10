function FinalExamViewModel(model) {
    var self = this;
    var browser;
    self.ExamQuestionDetailResults = ko.observableArray([]);
    self.EvaluationQuestionDetailResults = ko.observableArray([]);
    self.EvaluationAnswers = ko.observableArray([]);
    self.CourseName = ko.observable();
    self.MEID = ko.observable();
    self.Isreturn = ko.observable(true);
    self.ExamResultPercentage = ko.observable(0);
    self.IsPass = ko.observable(false);
    self.ShowResultDiv = ko.observable(false);
    self.UserID = ko.observable();
    self.SessionTimeout = ko.observable();
    browser = getBrowser();
    if (model.FinalExamAttemptByUser < 3) {
        /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
        self.MEID(model.MEID);
        self.UserID(model.UserID);
        self.SessionTimeout(model.SessionTimeout);
        if (browser != null || browser != "") {
            $.post("/LogSession/LogModuleOrExam", { browser: browser, newurl: 'FinalExam', MEID: self.MEID(), UserId: self.UserID() }, function (_model) {
                var model = $.parseJSON(_model);
                if (model != "success") {
                    var path = '/User/Unauthorise';
                    window.location = path;
                }
            });
        }
        setInterval(function () {
                CheckSession()
        }, self.SessionTimeout());
        /*****************************End*********************************/
        ko.mapping.fromJS(model.ExamQuestionDetailResults, {}, self.ExamQuestionDetailResults);
        ko.mapping.fromJS(model.EvaluationQuestionDetailResults, {}, self.EvaluationQuestionDetailResults);
        ko.mapping.fromJS(model.EvaluationAnswers, {}, self.EvaluationAnswers);
        if (model.EvaluationQuestionDetailResults.length > 0)
            self.CourseName(model.EvaluationQuestionDetailResults[0].CourseName);
    }
    else if (model.FinalExamAttemptByUser >= 3) {
        alertify.confirm("User Already Attempt this test 3 times", function (e) {
            if (e) {
                window.location = '/MyEducation/';
            }
        });
    }
    $("#btnEvaluationPop").hide()
    self.AddExamInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddExamResult").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        validateradiobutton();
        if (self.Isreturn())
            return true;
        else
            return false;
        return true;
    }
    self.AddExamDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        $('#btnEvaluationPop').modal('hide');
        $("#btnEvaluationPop").show();
        $("#examSubmit").attr("disabled", true);       
        if (data.FinalExamAttemptByUser < 3) {
            //self.ShowResultDiv(true);
            //alertify.success("Exam Submitted Successfully.");
            //self.ExamResultPercentage(data.TotalPercentage);
            //if (data.TotalPercentage >= 33) {
            //    self.IsPass(true);
            //}
            //else {
            //    self.IsPass(false);
            //}
            alertify.confirm("Exam Submitted Successfully.", function (e) {
                if (e) {
                    $(window).unbind('beforeunload');
                    /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
                    if (browser != null || browser != "") {
                        $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                            window.location = '/Exam/ExamResult/' + data.encryptedTotalPercentage + '/' + $("#Hdneid").val() + '/' + $("#Hdnmeid").val() + '/' + data.FinalExamAttemptByUser;
                        });
                    }
                    /*****************************End*********************************/
                }
            });
        }
        else {
            alertify.confirm("User Already Attempt this test 3 times", function (e) {
                if (e) {
                    window.location = '/MyEducation/';
                }
            });
        }
    }
    function validateradiobutton() {
        var names = {};
        $('#DivFinalExam :radio').each(function () {
            names[$(this).attr('name')] = true;
        });
        var count = 0;
        $.each(names, function () {
            count++;
        });
        if ($('#DivFinalExam :radio:checked').length === count) {
            self.Isreturn(true);
        }
        else {
            self.Isreturn(false);
            alertify.confirm("Please Answer all Questions");
        }
    }
    function validateradiobuttonEvaluation() {
        var names = {};
        $('#DivEvaluation :radio').each(function () {
            names[$(this).attr('name')] = true;
        });
        var count = 0;
        $.each(names, function () {
            count++;
        });
        if ($('#DivEvaluation :radio:checked').length === count) {
            self.Isreturn(true);
        }
        else {
            self.Isreturn(false);
            alertify.confirm("Please Answer all Questions");
        }
    }
    self.AddEvaluationDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        $('#EvaluationPop').modal('hide');
        $("#btnEvaluationPop").hide();
        $("#examSubmit").attr("disabled", false);
        alertify.success(data);
    }
    self.AddEvaluationInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEvaluationResult").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        validateradiobuttonEvaluation();
        if (self.Isreturn())
            return true;
        else
            return false;
        return true;
    }
    self.EvaluationExam = function () {
        var path = '/Exam/EvaluationExam?educationID=' + $("#Hdneid").val() + '&meID=' + $("#Hdnmeid").val();
        window.location = path;
    }

  
    /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
    if (browser != null || browser != "") {
        function CheckSession() {
            $.post("/User/CheckSession", function (data) {
                if (data != true) {
                    $(window).unbind('beforeunload');
                    $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                        window.location = "/Home/";
                    });
                }
            });
        }
    
        bajb_backdetect.OnBack = function () {
            if (browser == "safari") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {

                });
            }
            else if (browser == "mozilla") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                });
                alert("You are  leaving this page");
            }
        }
        $(function () {
            $(window).bind('beforeunload', function () {
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                });
                setTimeout(function () {
                    //for insert again for stay here
                    setTimeout(function () {
                        $.post("/LogSession/InsertSession", { UserID: self.UserID(), browser: browser, pageurl: 'FinalExam', MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                        });
                    }, 1000);
                }, 1);
                return 'are you sure';
            });
        });
        $(".pageunload").click(function (event) {
            var href = $(this).attr('href');
            $(window).unbind('beforeunload');
            $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                window.location = href;
            });
            event.preventDefault();
        });
        $(".logout").click(function (event) {
            $(window).unbind('beforeunload');
            $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                $.get("/User/LogOff", function (_Encrypttext) {
                    window.location = "/";
                });
            });
            event.preventDefault();
        });
     
    }
    /*****************************End*********************************/
}