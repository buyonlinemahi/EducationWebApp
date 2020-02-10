function AddPreTestViewModel() {
    var self = this;
    self.PreTestQuestionID = ko.observable();
    self.PreTestQues = ko.observable();
    self.PreTestOptionA = ko.observable();
    self.PreTestName = ko.observable();
    self.PreTestName1 = ko.observable();
    self.PreTestOptionB = ko.observable();
    self.PreTestOptionD = ko.observable();
    self.PreTestOptionC = ko.observable();
    self.PreTestAnswer = ko.observable();
    self.PreTestID = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PreTestAnswerTrueFalse = ko.observable();
    self.PagedRecords = ko.observable(0);
    self.PreTestAnswerTypes = ko.observableArray([{ AnswerTypeName: "Multiple Choice", AnswerTypeValue: 'Multiple Choice' }, { AnswerTypeName: "True or False", AnswerTypeValue: 'True or False' }]);
    self.selectedItemId = ko.observable(3);
    $('#loaderDiv').hide();
    self.PreTest = ko.observableArray([]);
    self.PreTestQuestions = ko.observableArray([]);
    self.IsenablePreTestName = ko.observable(true);
    self.Init = function (model) {
        if (model != null) {
            if (model.pagedPreTestQuestion.PreTestQuestions != null) {
                if (!self.pendingRequest()) {
                    $("#loaderDiv").show();
                    ko.mapping.fromJS(model.pagedPreTestQuestion.PreTestQuestions, {}, self.PreTestQuestions);
                    self.TotolCount(model.pagedPreTestQuestion.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + model.pagedPreTestQuestion.PagedRecords);
                    $('#loaderDiv').hide();
                }
            }
            if (model.preTest != null) {
                self.PreTestName(model.preTest.PreTestName);
                self.PreTestName1(model.preTest.PreTestName);
                self.PreTestID(model.preTest.PreTestID);
                self.IsenablePreTestName(false);
            }
            else {
                self.PreTestID(0);
                self.PreTestName("");
                self.IsenablePreTestName(true);
            }
        }
    };
    self.PreTestFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddPreTest").jqBootstrapValidation('hasErrors')) {
            if (($("#pretestanswerID :input[type='radio']:checked")).length == 0) {
                $('#pretestanswerID :input:radio').removeClass('remove_bdr');
                $('#pretestanswerID :input:radio').addClass('required_bdr');
            }
            return false;
        }
        return true;
    }
    function divTrueorFalseRequired() {
        $("#divMultipleChoice :input,select,textarea").removeAttr("required");
        $("#divMultipleChoice :input,select,textarea").jqBootstrapValidation("destroy");
        $("#divTrueorFalse :input,select,textarea").attr("required", "required");
        $("#divTrueorFalse :input,select,textarea").jqBootstrapValidation();
    }
    function divMultipleChoiceRequired() {
        $("#divTrueorFalse :input,select,textarea").removeAttr("required");
        $("#divTrueorFalse :input,select,textarea").jqBootstrapValidation("destroy");
        $("#divMultipleChoice :input,select,textarea").attr("required", "required");
        $("#divMultipleChoice :input,select,textarea").jqBootstrapValidation();
    }
    self.AddPreTestDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        if (data != null) {
            self.PreTestName(data.preTest.PreTestName);
            if (data.preTest.PreTestName != null)
                self.PreTestName1(data.preTest.PreTestName);
            self.PreTestID(data.preTest.PreTestID);
            if (!data.preTestQuestion.flag) {
                for (var i = 0; i <= self.PreTestQuestions().length - 1; i++) {
                    if (self.PreTestQuestions()[i].PreTestQuestionID() == data.preTestQuestion.PreTestQuestionID) {
                        self.PreTestQuestions.splice(i, 1);
                        self.PreTestQuestions.splice(i, 0, new InsertPreTestQuestionItem(data.preTestQuestion.PreTestQuestionID, data.preTestQuestion.PreTestID, data.preTestQuestion.PreTestQues, data.preTestQuestion.PreTestOptionA, data.preTestQuestion.PreTestOptionB, data.preTestQuestion.PreTestOptionC, data.preTestQuestion.PreTestOptionD, data.preTestQuestion.PreTestAnswer, data.preTestQuestion.PreTestAnswerType, data.preTestQuestion.PreTestAnswerTrueFalse));
                        self.PreTestName(self.PreTestName1());
                        break;
                    }
                }
            }
            else {
                self.PreTestQuestions.splice(0, 0, new InsertPreTestQuestionItem(data.preTestQuestion.PreTestQuestionID, data.preTestQuestion.PreTestID, data.preTestQuestion.PreTestQues, data.preTestQuestion.PreTestOptionA, data.preTestQuestion.PreTestOptionB, data.preTestQuestion.PreTestOptionC, data.preTestQuestion.PreTestOptionD, data.preTestQuestion.PreTestAnswer, data.preTestQuestion.PreTestAnswerType, data.preTestQuestion.PreTestAnswerTrueFalse));
                $("#main").scrollTop(0);
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
                self.IsenablePreTestName(false);
                self.PreTestName(self.PreTestName1());
                setgrdDesignAfterInsert();
            }
            resetcontrol();
            window.location = '/PreTest/Index';
        }
    }
    function InsertPreTestQuestionItem(_PreTestQuestionID, _PreTestID, _PreTestQues, _PreTestOptionA, _PreTestOptionB, _PreTestOptionC, _PreTestOptionD, _PreTestAnswer, _PreTestAnswerType, _PreTestAnswerTrueFalse) {
        var self = this;
        self.PreTestQuestionID = ko.observable(_PreTestQuestionID);
        self.PreTestID = ko.observable(_PreTestID);
        self.PreTestQues = ko.observable(_PreTestQues);
        self.PreTestOptionA = ko.observable(_PreTestOptionA);
        self.PreTestOptionB = ko.observable(_PreTestOptionB);
        self.PreTestOptionC = ko.observable(_PreTestOptionC);
        self.PreTestOptionD = ko.observable(_PreTestOptionD);
        self.PreTestAnswer = ko.observable(_PreTestAnswer);
        self.PreTestAnswerType = ko.observable(_PreTestAnswerType);
        self.PreTestAnswerTrueFalse = ko.observable(_PreTestAnswerTrueFalse);
    }
    function resetcontrol() {
        self.PreTestQuestionID("");
        self.PreTestQues("");
        self.PreTestOptionA("");
        self.PreTestOptionB("");
        self.PreTestOptionD("");
        self.PreTestOptionC("");
        self.PreTestAnswer('');
        self.PreTestAnswerTrueFalse("");
        self.selectedItemId("");
        ClearValidation();
        $("#divMultipleChoice").css("display", "none");
        $("#divTrueorFalse").css("display", "none");
    }
    self.editPreTestQuestionLineItems = function (config) {
        ClearValidation();
        if (config.PreTestAnswerType() == "Multiple Choice") {
            $("#divMultipleChoice").css("display", "block");
            $("#divTrueorFalse").css("display", "none");
            divMultipleChoiceRequired();
            self.selectedItemId(config.PreTestAnswerType());
        }
        else if (config.PreTestAnswerType() == "True or False") {
            $("#divMultipleChoice").css("display", "none");
            $("#divTrueorFalse").css("display", "block");
            divTrueorFalseRequired();
            self.selectedItemId(config.PreTestAnswerType());
            if (config.PreTestAnswerTrueFalse() == null)
                self.PreTestAnswerTrueFalse("");
            else {
                if (config.PreTestAnswerTrueFalse() == true)
                    self.PreTestAnswerTrueFalse('true');
                else
                    self.PreTestAnswerTrueFalse('false');
            }
        }
        self.PreTestQuestionID(config.PreTestQuestionID());
        self.PreTestQues(config.PreTestQues());
        self.PreTestOptionA(config.PreTestOptionA());
        self.PreTestOptionB(config.PreTestOptionB());
        self.PreTestOptionD(config.PreTestOptionD());
        self.PreTestOptionC(config.PreTestOptionC());
        self.PreTestAnswer(config.PreTestAnswer());
        self.PreTestID(config.PreTestID());
        self.IsenablePreTestName(false);
        $(window).scrollTop(0);
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllPreTestQuestion();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllPreTestQuestion() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.ajax({
                url: '/PreTest/GetAllPagedPreTestQuestionByPreTestID',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ PreTestID: self.PreTestID(), skip: self.maxId() }),
                success: function (model) {
                    for (var i = 0; i <= model.pagedPreTestQuestion.PreTestQuestions.length - 1; i++) {
                        self.PreTestQuestions.push(new InsertPreTestQuestionItem(model.pagedPreTestQuestion.PreTestQuestions[i].PreTestQuestionID, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestID, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestQues, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestOptionA, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestOptionB, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestOptionC, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestOptionD, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestAnswer, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestAnswerType, model.pagedPreTestQuestion.PreTestQuestions[i].PreTestAnswerTrueFalse));
                    }
                    self.TotolCount(model.pagedPreTestQuestion.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + model.pagedPreTestQuestion.PagedRecords);
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    alert('Error while deleting a item - ' + data);
                }
            });
        }
    }
    function setgrdDesignAfterInsert() {
        //if (!$('#main').hasScrollBar()) {
        //    var className;
        //    $('#main > tr:first').each(function () {
        //        $(this).children('td').addClass(function (index) {
        //            $(this).removeClass("marginleft10");
        //            return 'paddingleft-none';
        //        })
        //    });

        //}
    }
    function ClearValidation() {
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $('input').addClass('remove_bdr');
        $('radio').addClass('remove_bdr');
    }
    $("#drpPreTestAnswerType").change(function () {
        var _PreTestAnswerType = $('#drpPreTestAnswerType :selected').val();
        if (_PreTestAnswerType == "Multiple Choice") {
            $("#divMultipleChoice").css("display", "block");
            $("#divTrueorFalse").css("display", "none");
            divMultipleChoiceRequired();
        }
        else if (_PreTestAnswerType == "True or False") {
            $("#divMultipleChoice").css("display", "none");
            $("#divTrueorFalse").css("display", "block");
            divTrueorFalseRequired();
        }
    });
}