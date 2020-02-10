 
function EducationGridViewModel(model) {
    

    $('.datepickerCourseEndDate').datepicker();
   
    var self = this;
    self.ClientID = ko.observable();
    self.extendIsCheck = ko.observable();
    self.EducationID = ko.observable();
    self.CourseCode = ko.observable();
    self.CourseName = ko.observable();
    self.CourseDescription = ko.observable();
    self.CourseCredential = ko.observable();
    self.CourseUploadDate = ko.observable();
    self.HardCopyPrice = ko.observable();
    self.CoursePrice = ko.observable();
    if (model.Education != null) {
        if (model.Education.CoursePrice == 0) {
            self.extendIsCheck("true");
        }
        else {
            self.CoursePrice = ko.observable().extend({ numeric: 2 });
            self.extendIsCheck("false");
        }
    }
    self.CourseStartDate = ko.observable();
    self.CourseEndDate = ko.observable();
    self.CourseLocation = ko.observable();
    self.CourseStartTime = ko.observable();
    self.CourseEndTime = ko.observable();
    if (model.Education == null) {
        self.CourseDate = ko.observable();
    }
    else {
        if (model.Education.CourseDate != null) {
            self.CourseDate = ko.observable(moment(model.Education.CourseDate).format("MM/DD/YYYY"));
            var _courseDate = (moment(model.Education.CourseDate).format("MM/DD/YYYY"));
            $("#CourseDate").val(_courseDate);
        }
        else {
            self.CourseDate = ko.observable();
        }
    }
    self.CoursePresenterName = ko.observable();
    self.CourseTime = ko.observable();
    self.selectedEducationFormat = ko.observable();
    self.selectedWays = ko.observable();
    self.OnlinePriceAID = ko.observable();
    self.CourseAllotedTime = ko.observable();
    self.ReadyToDisplay = ko.observable();
    self.AllowRevisit = ko.observable();
    self.IsTimerRequired = ko.observable();
    self.IsProgramRequired = ko.observable();
    self.StateID = ko.observable();
    self.IndustryID = ko.observableArray();

    self.selectedItem = ko.observable(0);
    self.States = ko.observable();
    self.States = ko.observableArray();
    self.StateResults = ko.observableArray([]);
    self.Industries = ko.observable();
    self.Industries = ko.observableArray();
    self.IndustryResults = ko.observableArray([]);
    self.IndustryselectedItem = ko.observable();
    $.getJSON("/Common/getAllState", function (data) {
        self.States(data.slice());
        if (model.Education != null) {
            self.selectedItem(self.StateID());
            $("#hdStateID").val(model.Education.StateID);
            $("#hdEncryptedEducationID").val(model.Education.EncryptedEducationID);
        }
    });
    $.getJSON("/Common/getAllIndustry", function (data) {
        self.Industries(data.slice());
        if (model.Education != null) {
            self.IndustryselectedItem(self.IndustryID());
            //$("#hdIndustryID").val(model.Education.IndustryID);
            
        }
    });
   
    if (model.Education != null) {
        self.selectedWays(model.Education.CouseAllotedDaysMonth);
        //$("#chkIsTimerRequired").prop("disabled", true);

       /* if (model.Education.IsProgramRequired == "True")
            $("#chkIsProgramRequired").prop("disabled", true);
        else
            $("#chkIsProgramRequired").prop("disabled", false);*/
    }
    /*else {
        $("#chkIsTimerRequired").prop("disabled", false);
    }*/
    //rsingh
    self.EPageID = ko.observable();
    self.PContent = ko.observable();
    self.PDate = ko.observable();
    self.Colleges = ko.observableArray();
    
    self.CollegeEducation = ko.observableArray();
    self.DeptNotAssociatedCollege = ko.observableArray();
    self.PreTests = ko.observableArray();
    self.Exams = ko.observableArray();
    self.Evaluations = ko.observableArray();
    self.CourseFile = ko.observable();
    self.selectedEvaluation = ko.observable();
    self.selectedPreTest = ko.observable();
    self.selectedExam = ko.observable();
    self.EducationAvailableID = ko.observable();
    self.CollegeEducationDeleted = ko.observableArray();
    self.EducationFormats = ko.observableArray();
    //Profession
    self.Professions = ko.observableArray();
    self.ProfessionEducation = ko.observableArray();
    self.ProfessionEducationDeleted = ko.observableArray();
    self.EducationModuleResults = ko.observableArray([]);

    self.CourseWays = ko.observableArray([{ CouseAllotedDaysMonth: "Days", CousreWayType: "Days" }, { CouseAllotedDaysMonth: "Months", CousreWayType: "Months" }]);
    var mappingOptions = {
        'PDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        },
        'CourseDate': {
            create: function (options) {
                if (options.data == "/Date(1482258600000)/")
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    var mappingOptions1 = {
        'EducationModuleDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }
 
    $.ajax({
        type: "GET",
        url: "/Education/GetEducationFormatsAll",
        contentType: "application/json; charset=utf-8",
        success: function (responseText) {
            var data = $.parseJSON(responseText);
            ko.mapping.fromJS(data, {}, self.EducationFormats);
            if (model.EducationFormatAvailable != null) {
                self.selectedEducationFormat(model.EducationFormatAvailable[0].EducationFormatID);
                //self.EducationAvailableID(model.EducationFormatAvailable[0].EducationAvailableID);
            }
            pnlHideNShow();
            $('#EducationFormat').change(function () {
                pnlHideNShow();
            });
        }
    })

    $("#CoursePrice").change(function () {
        //if (self.extendIsCheck() == "true") {
            var value = parseFloat($('#CoursePrice').val());
            var v = value.toFixed(2);
            $('#CoursePrice').val(v);
        //}
    });


    function pnlHideNShow() {
        if ($("#EducationFormat :selected").val() == "3") {
            $('#panelOnsite').slideDown(700);
           
            $('#CourseLocation').prop('required', true);

        }
        else {
           
            $('#CourseLocation').removeAttr('required');
            $('#panelOnsite').slideUp(700);
        }
    }

    ko.mapping.fromJS(model.Education, mappingOptions, self);   
    ko.mapping.fromJS(model.EducationPreTestQuestion, {}, self);
    ko.mapping.fromJS(model.EducationDetailPage, mappingOptions, self);
    ko.mapping.fromJS(model.CollegeEducation, {}, self.CollegeEducation);
    ko.mapping.fromJS(model.ProfessionEducation, {}, self.ProfessionEducation);
    ko.mapping.fromJS(model.EvaluationResults, {}, self.Evaluations);
    ko.mapping.fromJS(model.ExamResults, {}, self.Exams);
    ko.mapping.fromJS(model.PreTestResults, {}, self.PreTests);

    bindDate();

    function bindDate()
    {
        if (model.Education != null) {
            if (model.Education.CourseStartDate != null)
                self.CourseStartDate(moment(model.Education.CourseStartDate).format("MM/DD/YYYY"));
            if (model.Education.CourseEndDate != null)
                self.CourseEndDate(moment(model.Education.CourseEndDate).format("MM/DD/YYYY"));
        }
    }
    if (model.EducationEvaluation != null) {
        self.selectedEvaluation(model.EducationEvaluation.EvaluationID);
        $("#hdCourseEvaluationID").val(model.EducationEvaluation.CourseEvaluationID);
    }
    if (model.EducationPreTestQuestion != null) {
        self.selectedPreTest(model.EducationPreTestQuestion.PreTestID);
        $("#hdCoursePreTestID").val(model.EducationPreTestQuestion.CoursePreTestID);
    }
    if (model.EducationExamQuestion != null) {
        self.selectedExam(model.EducationExamQuestion.ExamID);
        $("#hdCourseExamID").val(model.EducationExamQuestion.CourseExamID);
    }
    $('#loaderDiv1').hide();


    self.EducationFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmEducation").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }

    self.SelectState = function () {
        if ($("#StateID").val() != "")
        {
            self.StateID($("#StateID").val());
        }
    };
    //function to show module section when education saved.....
    if (model.Education != null) {
        
        ViewModule(model.Education.EducationID, model.Education.IsTimerRequired);
        SHowStepStatus();

    }
    self.AddEducationSuccess = function (model) {
        var _model = $.parseJSON(model);
        self.EducationAvailableID(_model.EducationFormatAvailable[0].EducationAvailableID);
        alertify.success("Successfully Saved");
        $("#hdEncryptedEducationID").val(_model.Education.EncryptedEducationID);
        getEducationModuleByEducationID(_model.Education.EducationID)

        ko.mapping.fromJS(_model.Education, {}, self);

        if (_model.Education.CourseStartDate!= null)
            self.CourseStartDate(moment(_model.Education.CourseStartDate).format("MM/DD/YYYY"));

        self.selectedWays(_model.Education.CouseAllotedDaysMonth);

        if (_model.Education.CourseEndDate != null)
            self.CourseEndDate(moment(_model.Education.CourseEndDate).format("MM/DD/YYYY"));

        if (_model.Education.CourseDate != null)
            self.CourseDate(moment(_model.Education.CourseDate).format("MM/DD/YYYY"));

        ko.mapping.fromJS(_model.CollegeEducation, {}, self.CollegeEducation);
        ko.mapping.fromJS(_model.ProfessionEducation, {}, self.ProfessionEducation);
        self.EPageID(_model.EducationDetailPage.EPageID);
        self.CollegeEducationDeleted.removeAll();
        //function to show module section when education saved.....
        ViewModule(_model.Education.EducationID, _model.Education.IsTimerRequired);
        $("#hdCourseEvaluationID").val(_model.EducationEvaluation.CourseEvaluationID);
        $("#hdCoursePreTestID").val(_model.EducationPreTestQuestion.CoursePreTestID);
        $("#hdCourseExamID").val(_model.EducationExamQuestion.CourseExamID);
        $("#hdStateID").val(_model.EducationExamQuestion.StateID);
        SHowStepStatus();
    }

    function getEducationModuleByEducationID(EducationID) {
        $.post("/Education/getEducationModuleByEducationID", { educationID: EducationID }, function (data) {
            var data = $.parseJSON(data);            
            EducationModuleViewModel.ForBindingAfterAdding(data.EducationModules);
        });

    }
    function SHowStepStatus() {
        $("#EducationLinkCompleted").removeClass('previous visited');
      //  $("#EducationLinkCompleted").addClass('checkout-bar green-bar');
        if (self.CollegeEducation() != null) {
            if (self.CollegeEducation().length > 0) {
                $("#CollegeLinkCompleted").removeClass('previous visited');
                //$("#CollegeLinkCompleted").addClass('checkout-bar ');
               
            }
        }
        else {
            $("#CollegeLinkCompleted").addClass('previous visited');
            //  $("#CollegeLinkCompleted").removeClass('checkout-bar green-bar');
          
        }
  

        if (self.ProfessionEducation() != null) {
            if (self.ProfessionEducation().length > 0) {
                $("#ProfessionLinkCompleted").removeClass('previous visited');
               
             //   $("#ProfessionLinkCompleted").addClass('checkout-bar green-bar');
            }
        }
        else {
            $("#ProfessionLinkCompleted").addClass('previous visited');
            //  $("#ProfessionLinkCompleted").removeClass('checkout-bar green-bar');
           
        }       
    }
    self.getColleges = function () {
        $.getJSON("/College/getColleges", self.Colleges);
    }
  
    $('#College').change(function () {
        var _collegeID = $('#College :selected').val();
     
    });

    //add collage to education..hp
    self.addCollegeEducation = function () {
    if ($('#College :selected').text() == 'Select Any Subject Matter')
        {
            alertify.alert("Please select Subject Matter");
            return false;
        }
            //check for duplicate college should not add to education..hp
            self.filteredRecords = function () {
                return ko.utils.arrayFilter(self.CollegeEducation(), function (Colg) {
                    if (isNaN(Colg.CollegeID))
                        return Colg.CollegeID() == $('#College :selected').val();
                    else
                        return Colg.CollegeID == $('#College :selected').val();
                });
            }
            if (self.filteredRecords().length > 0) {
                alertify.alert("This College already added to education");
                return;
            }
            if (self.CollegeEducation() != null)
                self.CollegeEducation.push({ CollegeName: $('#College :selected').text(), CollegeCourseID: 0, CollegeID: $('#College :selected').val() });
            else {
                self.CollegeEducation1 = ko.observableArray([{ CollegeName: $('#College :selected').text(), CollegeCourseID: 0, CollegeID: $('#College :selected').val() }]);
                self.CollegeEducation(self.CollegeEducation1().slice(0));
            }
     
    }
    //to be change..hp
 
    self.DeleteCollegeEducation= function (model) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                self.CollegeEducation.remove(model);
                var CollegeCourseID;
                if (isNaN(model.CollegeCourseID))
                    CollegeCourseID = model.CollegeCourseID();
                else
                    CollegeCourseID = model.CollegeCourseID;
                if ((self.EducationID() != null) && (CollegeCourseID > 0)) {
                    model.Active = false;
                    self.CollegeEducationDeleted.push(model);
                }
            }
        });
    }
    $("#btnClose").click(function () {
        clearcontent();
    });
    $("#btnClose1").click(function () {
        clearcontent();
    });

    $("#ModuleSectionClosebutton").click(function () {
        clearcontent();
    });

    function clearcontent() {
        if ($("#hfEducationID").val() == "") {
            alertify.confirm("Are you sure want to clear this content?", function (e) {
                if (e) {
                    $("#hd").val("");
                    var $iFrameContents = $('iframe').contents().find('div.designborder');
                    $iFrameContents.html('');
                    $("#EditorModal").css("display", "none");
                    $("#EditorModal").removeClass("in");
                    $(".modal-backdrop").remove();
                    $("#inner-content").removeClass("modal-open");
                }
            });
        }
        else {
            $("#EditorModal").css("display", "none");
            $("#EditorModal").removeClass("in");
            $(".modal-backdrop").remove();
            $("#inner-content").removeClass("modal-open");
        }
    }
    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });
    $("#btnSaveandContinue").click(function () {
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

    //add Profession to education..
    self.getProfessions = function () {
    
         $.getJSON("/Profession/getProfessions", self.Professions);
    }

    $('#Profession').change(function () {
        var _ProfessionID = $('#Profession :selected').val();

    });

    self.addProfessionEducation = function () {
        if ($('#Profession :selected').text() == 'Select Any Profession')
            {
            alertify.alert("Please select Profession");
         return false;
            }
        //check for duplicate Profession should not add to education..hp
        self.filteredRecords = function () {
            return ko.utils.arrayFilter(self.ProfessionEducation(), function (Prof) {
                if (isNaN(Prof.ProfessionID))
                    return Prof.ProfessionID() == $('#Profession :selected').val();
                else
                    return Prof.ProfessionID == $('#Profession :selected').val();
            });
        }
        if (self.filteredRecords().length > 0) {
            alertify.alert("This Profession already added to education");
            return;
        }
        if (self.ProfessionEducation() != null)
            self.ProfessionEducation.push({ ProfessionTitle: $('#Profession :selected').text(), EducationProfessionID: 0, ProfessionID: $('#Profession :selected').val() });
        else {
            self.ProfessionEducation1 = ko.observableArray([{ ProfessionTitle: $('#Profession :selected').text(), EducationProfessionID: 0, ProfessionID: $('#Profession :selected').val() }]);
            self.ProfessionEducation(self.ProfessionEducation1().slice(0));
        }

    }
    //to be change..hp
    self.DeleteProfessionEducation = function (model) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                self.ProfessionEducation.remove(model);
                var EducationProfessionID;
                if (isNaN(model.EducationProfessionID))
                    EducationProfessionID = model.EducationProfessionID();
                else
                    EducationProfessionID = model.EducationProfessionID;
                if ((self.EducationID() != null) && (EducationProfessionID > 0)) {
                    model.Active = false;
                    self.ProfessionEducationDeleted.push(model);
                }
            }
        });
    }
}
function ViewModule(educationID, IsTimerRequired) {
    if (educationID > 0) {
        $("#hfEducationID").val(educationID);
        $("#hfTimerReq").val(IsTimerRequired);
        $("#hfCreEducationID").val(educationID);        
        $('#divMainEducationModule').css('display', 'block');
        $('#divMainEducationCredential').css('display', 'block');
    }
}
function submitform() {
    if ($("#hdCourseFile").val() != "") {
        document.getElementById("EducationFile").required = false;
        $('#dictest').removeClass('form-group has-error');
        $('#dictest').removeClass('has - error');
        $('#dictest').find('.help-block').hide();
        $('#dictest').find('i.form-control-feedback').hide();
    }
    $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding

   
}

$("#btnPerview").click(function () {

    window.location = '/education/PreviewEducation?educationID=' + $("#hdEncryptedEducationID").val();
});


function StopVideo() {
    var video = document.getElementById('v1');
    video.pause();
}
function hideimage() {
    $("#loaderDivmain").css("display", "none");
    var editor1 = document.getElementById("Editor2").editor;
    editor1.SetText('');
    var editor2 = document.getElementById("Editor4").editor;
    editor2.SetText('');

}

function checkFileType()
{    
    var fileUpload = $("#EducationFile").val();
    var ext = fileUpload.split('.').pop().toLowerCase();
    if (ext == 'pdf') {
        var formData = new FormData();
        var fileInput = document.getElementById('EducationFile');
        for (i = 0; i < fileInput.files.length; i++) {
            formData.append(fileInput.files[i].name, fileInput.files[i]);
        }

        $.ajax({
            type: "POST",
            url: "/Common/IsPdf",
            contentType: false,
            processData: false,
            data: formData,
            success: function (_res) {
                if (_res == 'True') {
                    return true;
                }
                else {
                    $("#EducationFile").val('');
                    alert("Invalid or Corrupt file");
                    return false;
                }
            },
            error: function () {
                alert("Some error occured.");
                $("#EducationFile").val('');
            }
        });
    }
    else {
        $("#EducationFile").val('');
        alert("Upload pdf file only.");
        return false;
    }
}