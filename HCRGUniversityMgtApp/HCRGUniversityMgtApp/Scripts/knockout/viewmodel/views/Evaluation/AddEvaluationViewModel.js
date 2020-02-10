function AddEvaluationViewModel() {
    var self = this;
    self.EvaluationQuestionID = ko.observable();
    self.EvaluationQues = ko.observable();
    self.IsStatus = ko.observable();
    self.EvaluationName = ko.observable();
    self.EvaluationName1 = ko.observable();
    self.EvaluationStatus = ko.observable();
    self.EvaluationID = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv').hide();
    self.Evaluation = ko.observableArray([]);
    self.EvaluationQuestions = ko.observableArray([]);
    self.IsenableEvaluationName = ko.observable(true);
    self.Init = function (model) {
        if (model != null) {
            if (model.pagedEvaluationQuestion.EvaluationQuestions != null) {
                if (!self.pendingRequest()) {
                    $("#loaderDiv").show();
                    ko.mapping.fromJS(model.pagedEvaluationQuestion.EvaluationQuestions, {}, self.EvaluationQuestions);
                    self.TotolCount(model.pagedEvaluationQuestion.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + model.pagedEvaluationQuestion.PagedRecords);
                    $('#loaderDiv').hide();
                }
            }
            if (model.Evaluation != null) {
                self.EvaluationName(model.Evaluation.EvaluationName);
                self.EvaluationName1(model.Evaluation.EvaluationName);
                self.EvaluationID(model.Evaluation.EvaluationID);
                self.EvaluationStatus(model.Evaluation.EvaluationStatus);
                self.IsenableEvaluationName(false);
                $("#addPredefinedQuestionsID").prop("disabled", true);
            }
            else {
                self.EvaluationID(0);
                self.EvaluationName("");
                self.IsenableEvaluationName(true);
                $("#addPredefinedQuestionsID").prop("disabled", false);
               
            }
        }

    };
    self.EvaluationFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEvaluation").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddEvaluationDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        if (data != null) {
            self.EvaluationName(data.Evaluation.EvaluationName);
           if (data.Evaluation.EvaluationName !=null)
             self.EvaluationName1(data.Evaluation.EvaluationName);
            self.EvaluationID(data.Evaluation.EvaluationID);
            if (!data.EvaluationQuestion.flag) {
                for (var i = 0; i <= self.EvaluationQuestions().length - 1; i++) {
                    if (self.EvaluationQuestions()[i].EvaluationQuestionID() == data.EvaluationQuestion.EvaluationQuestionID) {
                        self.EvaluationQuestions.splice(i, 1);
                        self.EvaluationQuestions.splice(i, 0, new InsertEvaluationQuestionItem(data.EvaluationQuestion.EvaluationQuestionID, data.EvaluationQuestion.EvaluationID, data.EvaluationQuestion.EvaluationQues, data.EvaluationQuestion.IsStatus));
                        self.EvaluationName(self.EvaluationName1());
                        break;
                    }
                }
            }
            else {
                self.EvaluationQuestions.splice(0, 0, new InsertEvaluationQuestionItem(data.EvaluationQuestion.EvaluationQuestionID, data.EvaluationQuestion.EvaluationID, data.EvaluationQuestion.EvaluationQues,data.EvaluationQuestion.IsStatus));
                $("#main").scrollTop(0);
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
                self.IsenableEvaluationName(false);
                self.EvaluationName(self.EvaluationName1());
            }
            resetcontrol();
            window.location = '/Evaluation/Index';
        }
    }
    function InsertEvaluationQuestionItem(_EvaluationQuestionID, _EvaluationID, _EvaluationQues,_EvalStatus,_EvaluationOptionA, _EvaluationOptionB, _EvaluationOptionC,_EvaluationOptionD,_EvaluationAnswer) {
        var self = this;
        self.EvaluationQuestionID = ko.observable(_EvaluationQuestionID);
        self.EvaluationID = ko.observable(_EvaluationID);
        self.EvaluationQues = ko.observable(_EvaluationQues);
        self.IsStatus = ko.observable(_EvalStatus);
    }
    function resetcontrol() {
        self.EvaluationQuestionID("");
        self.EvaluationQues("");
        ClearValidation();
    }
    self.editEvaluationQuestionLineItems = function (config) {
        ClearValidation();
        self.EvaluationName(self.EvaluationName1());
        self.EvaluationQuestionID(config.EvaluationQuestionID());
        self.EvaluationQues(config.EvaluationQues());
        self.EvaluationID(config.EvaluationID());
        self.IsStatus(config.IsStatus());
        self.IsenableEvaluationName(false);
        $(window).scrollTop(0);
    }
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            if (self.TotolCount() > self.maxId()) {
                var elem = event.target;
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllEvaluationQuestion();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    function getAllEvaluationQuestion() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.ajax({
                url: '/Evaluation/GetAllPagedEvaluationQuestionByEvaluationID',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ EvaluationID:self.EvaluationID(),skip: self.maxId() }),
                success: function (model) {
                
                    for (var i = 0; i <= model.pagedEvaluationQuestion.EvaluationQuestions.length - 1; i++) {
                        self.EvaluationQuestions.push(new InsertEvaluationQuestionItem(model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationQuestionID, model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationID, model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationQues));
                    }
                    $("#main").scrollTop(0);
                    self.TotolCount(self.TotolCount() + 1);
                    self.maxId(self.maxId() + 1);
                    alertify.success("Evaluation Question Insert Successfully");
                    self.IsenableEvaluationName(false);
                    self.EvaluationName(self.EvaluationName1());
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    alert('Error while deleting a item - ' + data);
                }
            });
        }
    }

    self.AddPredefinedQuestions = function () {       
        var _evalID = 0;
        if ($("#EvaluationID").val() != "") {
            _evalID = $("#EvaluationID").val();
        }
        if ($("#txtEvaluationName").val() != "") {
            $("#loaderDiv").show();
            $.ajax({
                url: '/Evaluation/AddPredefinedQuestions',
                cache: false,
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ EvaluationName: $("#txtEvaluationName").val(), EvaluationID: _evalID }),
                success: function (model) {
                    self.EvaluationQuestions.removeAll();
                    for (var i = 0; i <= model.pagedEvaluationQuestion.EvaluationQuestions.length - 1; i++) {
                        self.EvaluationQuestions.push(new InsertEvaluationQuestionItem(model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationQuestionID, model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationID, model.pagedEvaluationQuestion.EvaluationQuestions[i].EvaluationQues, model.pagedEvaluationQuestion.EvaluationQuestions[i].IsStatus));
                    }
                    $("#main").scrollTop(0);
                    self.TotolCount(self.TotolCount() + 1);
                    self.maxId(self.maxId() + 1);
                    alertify.success("Evaluation Question Insert Successfully");
                    self.IsenableEvaluationName(false);
                    self.EvaluationName($("#txtEvaluationName").val());
                    self.EvaluationName1($("#txtEvaluationName").val());
                    $("#EvaluationID").val(model.pagedEvaluationQuestion.EvaluationQuestions[0].EvaluationID)
                    $("#addPredefinedQuestionsID").prop("disabled", true);
                    $('#loaderDiv').hide();
                },
                error: function (data) {
                    $('#loaderDiv').hide();
                    alert('Error occured - ' + data);

                }
            });
        }
        else {
            alert("Please Enter Evaluation Name");
        }
    };

    this.DeleteQuestions = function (itemToDelete) {
        alertify.confirm("Are you sure you want to change the Status?", function (e) {
            if (e) {
                $.ajax({
                    url: "/Evaluation/UpdateEvalQuestions",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ _evaluationQuestions: itemToDelete }),
                    success: function (data) {
                        var viewModel = self.EvaluationQuestions;
                        for (var i = 0; i <= viewModel().length - 1; i++) {
                            if (viewModel()[i].EvaluationQuestionID() == data.EvaluationQuestionID) {
                                self.EvaluationQuestions.splice(i, 1);
                                self.EvaluationQuestions.splice(i, 0, new InsertEvaluationQuestionItem(data.EvaluationQuestionID, data.EvaluationID, data.EvaluationQues, data.IsStatus));
                                alertify.success("Evaluation Question Updated Successfully");
                                self.EvaluationName(self.EvaluationName1());
                                break;
                            }
                        }
                    },
                    error: function (data) {
                        alert("Error while deleting a Questions - " + data);
                    }
                });
            }
        });
    }


    function ClearValidation() {
        $('.form-group').removeClass('has-error has-feedback');
        $('.form-group').find('.help-block').hide();
        $('.form-group').find('i.form-control-feedback').hide();
        $('input').addClass('remove_bdr');
    }
}