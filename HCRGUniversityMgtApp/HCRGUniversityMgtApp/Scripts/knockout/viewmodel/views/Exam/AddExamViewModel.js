function AddExamViewModel() {
    var self = this;
    self.ExamQuestionID = ko.observable();
    self.ExamQues = ko.observable();
    self.ExamOptionA = ko.observable();
    self.ExamName = ko.observable();
    self.ExamName1 = ko.observable();
    self.ExamOptionB = ko.observable();
    self.ExamOptionD = ko.observable();
    self.ExamOptionC = ko.observable();
    self.ExamAnswer = ko.observable();
    self.ExamID = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.ExamAnswerTrueFalse = ko.observable();
    self.PagedRecords = ko.observable(0);
    self.ExamAnswerTypes = ko.observableArray([{ AnswerTypeName: "Multiple Choice", AnswerTypeValue: 'Multiple Choice' }, { AnswerTypeName: "True or False", AnswerTypeValue: 'True or False' }]);
    self.selectedItemId = ko.observable(3);
    $('#loaderDiv').hide();
    self.Exam = ko.observableArray([]);
    self.ExamQuestions = ko.observableArray([]);
    self.IsenableExamName = ko.observable(true);
    self.Init = function (model) {
        if (model != null) {
            if (model.pagedExamQuestion.ExamQuestions != null) {
                if (!self.pendingRequest()) {
                    $("#loaderDiv").show();
                    ko.mapping.fromJS(model.pagedExamQuestion.ExamQuestions, {}, self.ExamQuestions);
                    self.TotolCount(model.pagedExamQuestion.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + model.pagedExamQuestion.PagedRecords);
                    $('#loaderDiv').hide();
                }
            }
            if (model.Exam != null) {
                self.ExamName(model.Exam.ExamName);
                self.ExamName1(model.Exam.ExamName);
                self.ExamID(model.Exam.ExamID);
                self.IsenableExamName(false);
            }
            else {
                self.ExamID(0);
                self.ExamName("");
                self.IsenableExamName(true);
            }
        }
    };
    self.ExamFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddExam").jqBootstrapValidation('hasErrors')) {
            if (($("#examanswerID :input[type='radio']:checked")).length == 0) {
                $('#examanswerID :input:radio').removeClass('remove_bdr');
                $('#examanswerID :input:radio').addClass('required_bdr');
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
    self.AddExamDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        if (data != null) {
            self.ExamName(data.Exam.ExamName);
            if (data.Exam.ExamName != null)
                self.ExamName1(data.Exam.ExamName);
            self.ExamID(data.Exam.ExamID);
            if (!data.ExamQuestion.flag) {
                for (var i = 0; i <= self.ExamQuestions().length - 1; i++) {
                    if (self.ExamQuestions()[i].ExamQuestionID() == data.ExamQuestion.ExamQuestionID) {
                        self.ExamQuestions.splice(i, 1);
                        self.ExamQuestions.splice(i, 0, new InsertExamQuestionItem(data.ExamQuestion.ExamQuestionID, data.ExamQuestion.ExamID, data.ExamQuestion.ExamQues, data.ExamQuestion.ExamOptionA, data.ExamQuestion.ExamOptionB, data.ExamQuestion.ExamOptionC, data.ExamQuestion.ExamOptionD, data.ExamQuestion.ExamAnswer, data.ExamQuestion.ExamAnswerType, data.ExamQuestion.ExamAnswerTrueFalse));
                        alertify.success("Exam Question Updated Successfully");
                        self.ExamName(self.ExamName1());
                        break;
                    }
                }
            }
            else {
                self.ExamQuestions.splice(0, 0, new InsertExamQuestionItem(data.ExamQuestion.ExamQuestionID, data.ExamQuestion.ExamID, data.ExamQuestion.ExamQues, data.ExamQuestion.ExamOptionA, data.ExamQuestion.ExamOptionB, data.ExamQuestion.ExamOptionC, data.ExamQuestion.ExamOptionD, data.ExamQuestion.ExamAnswer, data.ExamQuestion.ExamAnswerType, data.ExamQuestion.ExamAnswerTrueFalse));
                $("#main").scrollTop(0);
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
                alertify.success("Exam Question Insert Successfully");
                self.IsenableExamName(false);
                self.ExamName(self.ExamName1());
                setgrdDesignAfterInsert();
            }
            resetcontrol();
        }
    }
    function InsertExamQuestionItem(_ExamQuestionID, _ExamID, _ExamQues, _ExamOptionA, _ExamOptionB, _ExamOptionC, _ExamOptionD, _ExamAnswer, _ExamAnswerType, _ExamAnswerTrueFalse) {
        var self = this;
        self.ExamQuestionID = ko.observable(_ExamQuestionID);
        self.ExamID = ko.observable(_ExamID);
        self.ExamQues = ko.observable(_ExamQues);
        self.ExamOptionA = ko.observable(_ExamOptionA);
        self.ExamOptionB = ko.observable(_ExamOptionB);
        self.ExamOptionC = ko.observable(_ExamOptionC);
        self.ExamOptionD = ko.observable(_ExamOptionD);
        self.ExamAnswer = ko.observable(_ExamAnswer);
        self.ExamAnswerType = ko.observable(_ExamAnswerType);
        self.ExamAnswerTrueFalse = ko.observable(_ExamAnswerTrueFalse);
    }
    function resetcontrol() {
        self.ExamQuestionID("");
        self.ExamQues("");
        self.ExamOptionA("");
        self.ExamOptionB("");
        self.ExamOptionD("");
        self.ExamOptionC("");
        self.ExamAnswer('');
        self.ExamAnswerTrueFalse("");
        self.selectedItemId("");
        ClearValidation();
        $("#divMultipleChoice").css("display", "none");
        $("#divTrueorFalse").css("display", "none");
    }
    self.editExamQuestionLineItems = function (config) {
        ClearValidation();
        if (config.ExamAnswerType() == "Multiple Choice") {
            $("#divMultipleChoice").css("display", "block");
            $("#divTrueorFalse").css("display", "none");
            divMultipleChoiceRequired();
            self.selectedItemId(config.ExamAnswerType());
        }
        else if (config.ExamAnswerType() == "True or False") {
            $("#divMultipleChoice").css("display", "none");
            $("#divTrueorFalse").css("display", "block");
            divTrueorFalseRequired();
            self.selectedItemId(config.ExamAnswerType());
            if (config.ExamAnswerTrueFalse() == null)
                self.ExamAnswerTrueFalse("");
            else {
                if (config.ExamAnswerTrueFalse() == true)
                    self.ExamAnswerTrueFalse('true');
                else
                    self.ExamAnswerTrueFalse('false');
            }
        }
        self.ExamQuestionID(config.ExamQuestionID());
        self.ExamQues(config.ExamQues());
        self.ExamOptionA(config.ExamOptionA());
        self.ExamOptionB(config.ExamOptionB());
        self.ExamOptionD(config.ExamOptionD());
        self.ExamOptionC(config.ExamOptionC());
        self.ExamAnswer(config.ExamAnswer());
        self.ExamID(config.ExamID());
        self.IsenableExamName(false);
        $(window).scrollTop(0);
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllExamQuestion();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllExamQuestion() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.ajax({
                url: '/Exam/GetAllPagedExamQuestionByExamID',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ ExamID: self.ExamID(), skip: self.maxId() }),
                success: function (model) {
                    for (var i = 0; i <= model.pagedExamQuestion.ExamQuestions.length - 1; i++) {
                        self.ExamQuestions.push(new InsertExamQuestionItem(model.pagedExamQuestion.ExamQuestions[i].ExamQuestionID, model.pagedExamQuestion.ExamQuestions[i].ExamID, model.pagedExamQuestion.ExamQuestions[i].ExamQues, model.pagedExamQuestion.ExamQuestions[i].ExamOptionA, model.pagedExamQuestion.ExamQuestions[i].ExamOptionB, model.pagedExamQuestion.ExamQuestions[i].ExamOptionC, model.pagedExamQuestion.ExamQuestions[i].ExamOptionD, model.pagedExamQuestion.ExamQuestions[i].ExamAnswer, model.pagedExamQuestion.ExamQuestions[i].ExamAnswerTrueFalse));
                    }
                    self.TotolCount(model.pagedExamQuestion.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + model.pagedExamQuestion.PagedRecords);
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    alert('Error while deleting a item - ' + data);
                }
            });
        }
    }
    function ClearValidation() {
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $('input').addClass('remove_bdr');
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
    $("#drpExamAnswerType").change(function () {
        var _ExamAnswerType = $('#drpExamAnswerType :selected').val();
        if (_ExamAnswerType == "Multiple Choice") {
            $("#divMultipleChoice").css("display", "block");
            $("#divTrueorFalse").css("display", "none");
            divMultipleChoiceRequired();
        }
        else if (_ExamAnswerType == "True or False") {
            $("#divMultipleChoice").css("display", "none");
            $("#divTrueorFalse").css("display", "block");
            divTrueorFalseRequired();
        }
    });
}
