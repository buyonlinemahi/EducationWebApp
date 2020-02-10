var filetype;
function EducationModuleViewModel() {
    var self = this;
    self.EducationModuleName = ko.observable();
    self.EducationModuleTime = ko.observable();
    self.EducationModuleDate = ko.observable();
    self.EducationModuleID = ko.observable();
    self.EducationModulePDFName = ko.observable();
    self.EducationModulePPTName = ko.observable();
    self.EducationModuleVideoName = ko.observable();
    self.EducationModulePDF = ko.observable();
    self.EducationModulePPT = ko.observable();
    self.EducationModuleVideo = ko.observable();
    self.EducationModuleTypeFile = ko.observable();
    self.EducationModuleDescription = ko.observable();
    self.EducationID = ko.observable();
    self.EducationModuleResults = ko.observableArray([]);
    self.SubModuleTitles = ko.observableArray([]);
    self.UploadFileType = ko.observable();

    self.ParentModule = ko.observable();
   
    self.SubModuleTitles = ko.observableArray([]);
    self.ModulePositions = ko.observableArray([]);
    self.MainModulePositions = ko.observableArray([]);
    self.ParentModule = ko.observable();
    self.positioncount = ko.observable();
    self.Modulepositioncount = ko.observable();
   
    self.MainModulePositions.push(new EducationModulePosition(1));
    self.ModuleselectedItem = ko.observable();
    self.selectedItem = ko.observable();
    self.ParentFor = ko.observable('Parent');
    self.isEnabled = ko.observable(false);
    self.selectedSubItem = ko.observable();
    self.CountDirectoryFiles = ko.observable(0);
    self.CountArray = ko.observableArray();
    //////////
    self.EducationModuleFileID = ko.observable();
    self.ModuleFilePDF = ko.observable();
    self.ModuleFilePPT = ko.observable();
    self.ModuleFileVideo = ko.observable();
    self.FileTypeID = ko.observable();
    self.radioSelectedOptionValue = ko.observable();
    self.EducationModuleTime1 = ko.observable();
    self.DocumentName = ko.observable();
    self.EducationModuleFileID = ko.observable();
    self.DocumentUploadedDate = ko.observable();
    self.DocumentArr = ko.observableArray();
    self.UserID = ko.observable();
    self.UserName = ko.observable();
    /////////////
    $("#rbChild").attr('disabled', false);
    $("#rbParent").attr('disabled', false);
    //Scrolling//
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    $('#loaderDiv1').hide();
    
    ///////////////////progress bar/////////////// /
    


    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllEducationModule();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }
    var mappingOptions = {
        'EducationModuleDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }      
    }
    var mappingOptionsUpload = {
        'DocumentUploadedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MM/DD/YYYY");
            }
        }
    }

    self.AddModulePop = function () {
        if ($("#hfTimerReq").val() == "false") {
            $("#EducationModuleTime").prop("disabled", true);
        }
        else {
            $("#EducationModuleTime").prop("disabled", false);           
        }
        
    }

    $("#save").click(function () {

        if ($("#EducationModuleName").val() != "" || $("#EducationModuleTime").val() != "")
        {
            $('#btnPerview').prop("disabled", false);
        }
    });

   
       

   
    function getAllEducationModule() {
        if (!self.pendingRequest()) {
            $("#loaderDiv1").show();
            $.post("/EducationModule/ShowModule", { skip: self.maxId(), Educationid: self.EducationID() }, function (data) {
                var data = $.parseJSON(data);
                for (var i = 0; i < data.list_educationModule.length; i++) {
                    self.EducationModuleResults.push(new InsertEducationModuleLineItem(data.list_educationModule[i].EducationID, data.list_educationModule[i].EducationModuleName, data.list_educationModule[i].EducationModuleDate, data.list_educationModule[i].EducationModuleTime, data.list_educationModule[i].EducationModuleID, data.list_educationModule[i].EducationModuleDescription, data.list_educationModule[i].EducationModuleTime, data.list_educationModule[i].EducationModuleVideoName, data.list_educationModule[i].EducationModulePDFName, data.list_educationModule[i].EducationModulePPTName, data.list_educationModule[i].EducationModuleTypeFile, data.list_educationModule[i].EducationModulePosition, data.list_educationModule[i].EducationModuleShortDesc));
                }
                if (data.pagedEducationModule.TotalCount > 1) {
                    self.MainModulePositions.removeAll();
                    for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
                        self.MainModulePositions.push(new EducationModulePosition(i));
                    }
                }
                else
                    self.MainModulePositions.push(new EducationModulePosition(1));
                self.Modulepositioncount(data.pagedEducationModule.TotalCount + 1);
                self.TotolCount(data.pagedEducationModule.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.pagedEducationModule.PagedRecords);
                $('#loaderDiv1').hide();
            });
        }
    }
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.EducationModules, mappingOptions, self.EducationModuleResults);
            self.EducationID($('#hdEducationID').val());
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            self.PagedRecords(model.PagedRecords);
            $('#loaderDiv').hide();
            if (self.EducationModuleResults() != null) {
                if (self.EducationModuleResults().length>0) 
                {
                    $("#ModuleLinkCompleted").removeClass('previous visited');
                    //   $("#ModuleLinkCompleted").addClass('checkout-bar green-bar');
                    $("#btnPerview").prop("disabled", false);
                }
              
            }
            else {
                $("#ModuleLinkCompleted").addClass('previous visited');
                $("#btnPerview").prop("disabled", true);
              //  $("#ModuleLinkCompleted").removeClass('checkout-bar green-bar');
            }
            if (model.TotalCount > 1) {
                self.MainModulePositions.removeAll();
                for (var i = model.TotalCount + 1; i >= 1; i--) {
                    self.MainModulePositions.push(new EducationModulePosition(i));
                }
            }
            else
                self.MainModulePositions.push(new EducationModulePosition(1));
            self.Modulepositioncount(model.TotalCount + 1);
        }
        else {
            self.Modulepositioncount(1);
            self.MainModulePositions.push(new EducationModulePosition(1));
        }       
    };
    self.AddEducationModuleDetailSuccess = function (model) {
        if (model != null) {
        
            var newEducationModule = $.parseJSON(model);
            $("#hfEducationID").val(newEducationModule.EducationID);
            $("#hdMultipleUploadEducationModuleID").val(newEducationModule.EducationModuleID);
            $("#hfEducatioReadyToDisplay").val(newEducationModule.ReadyToDisplay);
            var viewModel = self.EducationModuleResults;
            if (!newEducationModule.flag) {
                ProgressBarUpdating(20);
                self.EducationModuleResults.removeAll();
                ProgressBarUpdating(40);
                $.post("/EducationModule/ShowModule", { Educationid: newEducationModule.EducationID, skip: 0, take: self.maxId() }, function (_data) {
                    var data = $.parseJSON(_data);
                    
                    if (data.list_educationModule != null) {
                        ko.mapping.fromJS(data.list_educationModule, mappingOptions, self.EducationModuleResults);
                        if (data.list_educationModule.length > 1) {
                            self.MainModulePositions.removeAll();
                            for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
                                self.MainModulePositions.push(new EducationModulePosition(i));
                            }
                        }
                        else
                            self.MainModulePositions.push(new EducationModulePosition(1));
                    }
                    self.Modulepositioncount(data.pagedEducationModule.EducationModules.length + 1);
                    // resetModulecontrol();
                    ProgressBarUpdating(60);
                    ProgressBarUpdating(100);
                    $('html, body').scrollTop($(document).height());
                    alertify.success("Education Module Update Successfully");
                    self.TotolCount(data.pagedEducationModule.TotalCount);
                    self.PagedRecords(data.pagedEducationModule.PagedRecords);
                    self.pendingRequest(false);
                    $('#save').prop('disabled', false);

                   // ProgressBarUpdating(100);
                });
            }
            else {
               
                self.EducationModuleResults.removeAll();
                ProgressBarUpdating(40);
                $.post("/EducationModule/ShowModule", { Educationid: newEducationModule.EducationID, skip: 0 }, function (_data) {
                    var data = $.parseJSON(_data);
                    ProgressBarUpdating(60);
                    if (data.list_educationModule != null) {
                        ko.mapping.fromJS(data.list_educationModule, mappingOptions, self.EducationModuleResults);
                        if (data.list_educationModule.length > 1) {
                            self.MainModulePositions.removeAll();
                            for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
                                self.MainModulePositions.push(new EducationModulePosition(i));
                            }
                        }
                        else
                            self.MainModulePositions.push(new EducationModulePosition(1));
                    }
                    self.Modulepositioncount(data.pagedEducationModule.TotalCount + 1);
                    // resetModulecontrol();
                    
                    $('#divscrollnew').scrollTop($(document).height());
                    alertify.success("Education Module Insert Successfully");
                    self.maxId(0);
                    self.TotolCount(data.pagedEducationModule.TotalCount);
                    self.CurrentPage(self.maxId());
                    self.maxId(self.maxId() + data.pagedEducationModule.PagedRecords);
                    self.PagedRecords(data.pagedEducationModule.PagedRecords);
                    self.pendingRequest(false);
                    $("#panelUploadMultipleModules").show();
                    ProgressBarUpdating(100);
                    $('#save').prop('disabled', false);

                });
                
            }
           // $("#btnmoduleclosepopup").click();
           
                $("#ModuleLinkCompleted").removeClass('previous visited');
              //  $("#ModuleLinkCompleted").addClass('checkout-bar green-bar');
                
                $("#myProgress").css("display", "none");
                $("#loading").css("display", "none");
                $("#loaderDivmain").css("display", "none");
        }
      //  ProgressBarUpdating(100);
        $("#btnCloseDivMainEducationModule").click();
    };
    $('#addmodule').click(function () {
        resetModulecontrol();
        if (self.DocumentArr() != null)
            self.DocumentArr.removeAll();
        $("#panelUploadMultipleModulesGrid").hide();
        $("#panelUploadMultipleModules").hide();
    });
    self.EditModule = function (config) {
        if ($("#hfTimerReq").val() == "false") {
            $("#EducationModuleTime").prop("disabled", true);
            //$("#EducationModuleTime").val(null);
        }
        else {
            $("#EducationModuleTime").prop("disabled", false);
        }
        var t = config.EducationModuleTypeFile();
        if (t == "2") {
            $("#EducationModuleTime").prop("disabled", true);
        }
        if (self.DocumentArr() != null)
            self.DocumentArr.removeAll();
        $("#panelUploadMultipleModulesGrid").hide();
        $("#panelUploadMultipleModules").hide();
        ClearValidation();
        if (config.EducationModuleTime() != null) {
            var i = config.EducationModuleTime().lastIndexOf(":");
            if (i > 0)
                self.EducationModuleTime(config.EducationModuleTime().slice(0, i));
            else
                self.EducationModuleTime();
        }
      
        self.EducationModuleName(config.EducationModuleName());
        self.EducationID(config.EducationID());
       
        self.EducationModuleVideoName(config.EducationModuleVideoName());
        self.EducationModuleID(config.EducationModuleID());
        var editor1 = document.getElementById("Editor4").editor;
        editor1.SetText(($("<div>").html(config.EducationModuleDescription()).text()));
     
        self.EducationModuleDescription(config.EducationModuleDescription());
        $(window).scrollTop(0);
        $('#panelUpload').css('display', 'none');
        $('#panelUploadPDF').css('display', 'none');
        $('#panelUploadPPT').css('display', 'none');
        $('#panelUploadVideo').css('display', 'none');
        self.radioSelectedOptionValue("Upload Content");
        if (self.EducationModuleVideoName() != null) {
            self.EducationModuleTime1(config.EducationModuleTime());
            $("#panelModuleTime").css("display", "none");
        }
        else {
            $("#panelModuleTime").css("display", "block");
        }
        self.MainModulePositions.removeAll();
        for (var i = self.TotolCount() ; i >= 1; i--) {
            self.MainModulePositions.push(new EducationModulePosition(i));
        }
        self.ModuleselectedItem(config.EducationModulePosition());
        $("#hdMultipleUploadEducationModuleID").val(config.EducationModuleID());

        $("#panelUploadMultipleModules").show();
        $.post("/EducationModule/GetEducationModuleFileByEducationModuleFileID", {
            EMID: config.EducationModuleID()
        }, function (model) {
            if (self.DocumentArr() != null)
                self.DocumentArr.removeAll();
            if (model.EducationModuleFileResults != null) {
                ko.mapping.fromJS(model.EducationModuleFileResults, mappingOptionsUpload, self.DocumentArr);
                $("#panelUploadMultipleModulesGrid").show();
            }

        });
    }
    self.ForBindingAfterAdding = function (model) {
        if (model != null) {
            self.EducationModuleResults.removeAll();
            ko.mapping.fromJS(model.EducationModules, mappingOptions, self.EducationModuleResults);
        }
    };
    function resetModulecontrol() {
        self.EducationModuleName("");
        self.EducationModuleDate("");
        self.EducationModuleTime("");
        self.EducationModuleID("");
        self.EducationModuleDescription("");
        self.EducationModuleTime1("");
        ClearValidation();
        $("#UploadPDF").val("");
        $("#UploadPPT").val("");
        $("#UploadVideo").val("");
        var control = $("#UploadPDF"),
         control1 = $("#UploadPPT");
        control.replaceWith(control.val('').clone(true));
        control1.replaceWith(control1.val('').clone(true));
        self.MainModulePositions.removeAll();
        for (var i = self.Modulepositioncount() ; i >= 1; i--) {
            self.MainModulePositions.push(new EducationModulePosition(i));
        }
        self.ModuleselectedItem(self.Modulepositioncount());
    }
    function InsertEducationModuleLineItem(_EducationID, _EducationModuleName, _EducationModuleDate, _EducationModuleTime, _EducationModuleID, _EducationModuleDescription, _EducationModuleTime, _EducationModuleVideoName, _EducationModulePDFName, _EducationModulePPTName, _EducationModuleTypeFile, _EducationModulePosition, _EducationModuleShortDesc) {
        var self = this;
        self.EducationID = ko.observable(_EducationID);
        self.EducationModuleName = ko.observable(_EducationModuleName);
        self.EducationModuleDate = ko.observable(_EducationModuleDate);
        self.EducationModuleTime = ko.observable(_EducationModuleTime);
        self.EducationModuleID = ko.observable(_EducationModuleID);
        self.EducationModuleDescription = ko.observable(_EducationModuleDescription);
        self.EducationModuleVideoName = ko.observable(_EducationModuleVideoName);
        self.EducationModulePDFName = ko.observable(_EducationModulePDFName);
        self.EducationModulePPTName = ko.observable(_EducationModulePPTName);
        self.EducationModuleTypeFile = ko.observable(_EducationModuleTypeFile);
        self.EducationModulePosition = ko.observable(_EducationModulePosition);
        self.EducationModuleShortDesc = ko.observable(_EducationModuleShortDesc);
    }
   
    $('#rbChild').change(function (model) {
        self.isEnabled(true);
    });
 
    //------- Multiple Uploads against Module......Mahi
    $("#CloseUploadMultipleModuleBTN").click(function () {
        resetModulecontrol();
        if (self.DocumentArr() != null)
            self.DocumentArr.removeAll();       
        $("#panelUploadMultipleModulesGrid").hide();
        $("#panelUploadMultipleModules").hide();
        $("#btnmoduleclosepopup").click();
    });
   
    self.AddNewDocumentFileSuccess = function (hdEducationModuleID) {
        if (hdEducationModuleID > 0) {
            $.post("/EducationModule/GetEducationModuleFileByEducationModuleFileID", {
                EMID: hdEducationModuleID
            }, function (model) {
                if (self.DocumentArr() != null)
                    self.DocumentArr.removeAll();

                ko.mapping.fromJS(model.EducationModuleFileResults, mappingOptionsUpload, self.DocumentArr);
                $("#panelUploadMultipleModulesGrid").show();
                $("#hdMultipleUploadEducationModuleID").val(hdEducationModuleID);
                alertify.alert("Upload Successfully");
            });           

            var control = $('#UploadModulesFiles');
            control.replaceWith(control.val('').clone(true));

            control.on({
                change: function () { console.log("Changed") },
                focus: function () { console.log("Focus") }
            });
        }
        else {
            alert("Error Occur");
        }
        
    }

    self.UploadModuleInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#_UploadModuleFileBeginForm").jqBootstrapValidation('hasErrors')) {
            if ($("#UploadModulesFiles").val() == "") {
                return false;
            }
            else
                return true;
        }
        return true;
    }

    self.removeDocumentFile = function (item) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                if (item.EducationModuleFileID() != 0) {
                    $("#loaderDivmain").css("display", "block");
                    $.post("/EducationModule/DeleteEducationModuleFileByEducationModuleFileID", {
                        EducationModuleFileID: item.EducationModuleFileID(), FileTypeID: item.FileTypeID()
                    }, function (data) {
                        if (data != 0) {
                            $.post("/EducationModule/GetEducationModuleFileByEducationModuleFileID", {
                                EMID: $("#hdMultipleUploadEducationModuleID").val()
                            }, function (model) {
                                $("#loaderDivmain").css("display", "none");
                                self.DocumentArr.removeAll();
                                if (model.EducationModuleFileResults != null) {
                                    ko.mapping.fromJS(model.EducationModuleFileResults, mappingOptionsUpload, self.DocumentArr);
                                }
                                else {
                                    $("#panelUploadMultipleModulesGrid").hide();
                                }
                            });                           
                            alertify.alert("Deleted Successfully");
                        }
                        else {
                            $("#loaderDivmain").css("display", "none");
                            alert("Error Occur")
                        }
                    });
                    $("#loaderDivmain").css("display", "none");
                }
            }
        });
    }

    //------------------END--------------------------//

    self.EducationModuleInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($('#panelUpload').is(':visible')) {
            if ($("#rbUploadPDF").prop("checked")) {
                if ($("#UploadPDF").val() == "") {
                    
                    $('#UploadPPT').removeClass('required_bdr');
                    $('#UploadVideo').removeClass('required_bdr');
                    
                    return false;
                }
            }
            else if ($("#rbUploadPPT").prop("checked")) {
                 
                if ($("#UploadPPT").val() == "") {
                    $('#UploadPPT').addClass('required_bdr');
                    $('#UploadVideo').removeClass('required_bdr');
                    
                    return false;
                }               
            }
            else if ($("#rbUploadVideo").prop("checked")) {
                
                if ($("#UploadVideo").val() == "") {
                    $('#UploadVideo').addClass('required_bdr');
                    $('#UploadPPT').removeClass('required_bdr');
                    
                    return false;
                }
            }
        }
        //if ($("#hfTimerReq").val() == "true") {
            //if (!document.getElementById('rbUploadPPT').checked) {
            //    if ($("#EducationModuleTime").val() == "") {
            //        $('#EducationModuleTime').addClass('required_bdr');
            //        return false;
            //    }
            //}
        //}

        $('#EducationModuleTime').removeClass('required_bdr');
        if ($("#frmAddEducationModule").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#myProgress").css("display", "block");
        $("#loading").css("display", "block");        
        $("#loaderDivmain").css("display", "block");
        $('#save').prop('disabled', true);
        return true;
    }
    this.deleteModule = function (itemToDelete) {

        alertify.confirm("Are you sure you want to change to delete?", function (e) {
            if (e) {
                $.ajax({
                    url: "/EducationModule/DeleteModuleInfo",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ educationModule: itemToDelete }),
                    success: function (model) {
                        self.EducationModuleResults.removeAll();
                        $.post("/EducationModule/ShowModule", { Educationid: itemToDelete.EducationID, skip: 0, take: self.maxId() }, function (_data) {
                            var data = $.parseJSON(_data);
                            if (data.list_educationModule != null) {
                                ko.mapping.fromJS(data.list_educationModule, mappingOptions, self.EducationModuleResults);
                                if (self.EducationModuleResults().length > 0) {
                                    $("#ModuleLinkCompleted").removeClass('previous visited');
                                    ///  $("#ModuleLinkCompleted").addClass('checkout-bar green-bar');
                                }
                                else {
                                    $("#ModuleLinkCompleted").addClass('previous visited');
                                    $("#btnPerview").prop("disabled", true);
                                    //   $("#ModuleLinkCompleted").removeClass('checkout-bar green-bar');
                                }
                                if (data.list_educationModule.length > 1) {
                                    self.MainModulePositions.removeAll();
                                    for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
                                        self.MainModulePositions.push(new EducationModulePosition(i));
                                    }
                                }
                                else
                                    self.MainModulePositions.push(new EducationModulePosition(1));
                            }
                            self.Modulepositioncount(data.pagedEducationModule.TotalCount + 1);
                            resetModulecontrol();
                            alertify.success(model);
                            self.TotolCount(data.pagedEducationModule.TotalCount);
                            self.PagedRecords(data.pagedEducationModule.PagedRecords);
                            self.pendingRequest(false);
                        });
                    },
                    error: function (model) {
                        alert("Error while deleting a item - " + model);
                    }
                });
            }
        });

  
   
        }
  
    function EducationModulePosition(_EducationModulePosition) {
        var self = this;
        self.EducationModulePosition = ko.observable(_EducationModulePosition);
    }

    self.popModule = function (model) {
        $.ajaxSetup({ cache: false });
        $("#diviframe").hide();
        $("#divVideo").hide();
        $("#iframePPTPDF").attr("src", "");
        $('#panelUploadVideo1').css('display', 'none');
        $('#panelUploadPDF1').css('display', 'none');
        $('#panelUploadPPT1').css('display', 'none');
        self.EducationID(model.EducationID());
        $.ajax({
            url: "/EducationModule/GetEducationModuleFile",
            data: ko.toJSON({ educationmoduleid: model.EducationModuleID() }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                self.CountDirectoryFiles(data.CountDirectoryFiles);
                self.CountArray.removeAll();
                for (var i = 1; i <= data.CountDirectoryFiles; i++)
                    self.CountArray.push({ CountValue: i, ModuleFilePPT: data.ModuleFile });
                self.FileTypeID(data.FileTypeID);
                self.EducationModuleID(data.EducationModuleID);
                self.EducationModuleFileID(data.EducationModuleFileID);
                if (data.FileTypeID == "1") {
                    $("#diviframe").hide();
                    $("#iframePPTPDF").attr("href", "");

                    $("#divVideo").hide();
                    var video = document.getElementById('v1');
                    video.setAttribute('src', "");

                    var editor1 = document.getElementById("Editor3").editor;
                    editor1.SetText(data.ModuleFile);
                    self.ModuleFilePDF(data.ModuleFile);
                    self.radioSelectedOptionValue("Upload Content");
                    filetype = "Upload Content";
                    $("#rbUploadPDF1").prop("checked", true);
                    $('#panelUploadPDF1').css('display', 'block');
                    $('#panelUploadPPT1').css('display', 'none');
                    $('#panelUploadVideo1').css('display', 'none');
                }
                else if (data.FileTypeID == "2") {
                    
                    self.ModuleFilePPT(data.ModuleFile);
                    $("#diviframe").show();
                    $("#iframePPTPDF").attr("href", data.ModuleFile);
                    $('iframePPTPDF').html(data.ModuleFile)
                    $("#divVideo").hide();
                    var video = document.getElementById('v1');
                    video.setAttribute('src', "");
                    
                    self.radioSelectedOptionValue("Upload PPT");
                    filetype = "Upload PPT";
                    $("#rbUploadPPT1").prop("checked", true);
                    $('#panelUploadPDF1').css('display', 'none');
                    $('#panelUploadPPT1').css('display', 'block');
                    $('#panelUploadVideo1').css('display', 'none');
                }
                else if (data.FileTypeID == "3") {
                   
                    $("#diviframe").hide();
                    $("#iframePPTPDF").attr("href", "");
                    $("#divVideo").show();

                    self.ModuleFileVideo(data.ModuleFile);
                    self.radioSelectedOptionValue("Upload video");
                    filetype = "Upload video";
                    $("#rbUploadVideo1").prop("checked", true);
                    $('#panelUploadPDF1').css('display', 'none');
                    $('#panelUploadPPT1').css('display', 'none');
                    $('#panelUploadVideo1').css('display', 'block');
                    var video = document.getElementById('v1');
                    video.setAttribute('src', data.ModuleFile);
                }
            }
        });
        $("#loaderDiv").css("display", "none");
    }
    self.AddEducationModuleFileDetailSuccess = function (model) {

        if (model != null) {

            var _model = $.parseJSON(model);
            //if (_model.FileTypeID == 1) {
            //    $("#diviframe").hide();
            //    $("#iframePPTPDF").attr("href", "");
            //    $("#divVideo").hide();
            //    var video = document.getElementById('v1');
            //    video.setAttribute('src', '');
            //}
            //else if (_model.FileTypeID == 2) {
            //    $("#diviframe").show();
            //    $("#iframePPTPDF").attr("href", _model.UploadFilePath + _model.ModuleFile);
            //    $("#divVideo").hide();
            //    var editor1 = document.getElementById("Editor3").editor;
            //    editor1.SetText("");
            //    var video = document.getElementById('v1');
            //    video.setAttribute('src', '');
            //}
            //else {
            //    $("#diviframe").hide();
            //    $("#iframePPTPDF").attr("href", "");
            //    $("#divVideo").show();
            //    var editor1 = document.getElementById("Editor3").editor;
            //    editor1.SetText("");
            //    var video = document.getElementById('v1');
            //    video.setAttribute('src', _model.UploadFilePath);
            //}

            

            ProgressBar(20);
            self.EducationModuleResults.removeAll();
            ProgressBar(40);
            $.post("/EducationModule/ShowModule", { Educationid: self.EducationID(), skip: 0, take: self.maxId() }, function (_data) {
                var data = $.parseJSON(_data);
                ProgressBar(60);
                if (data.list_educationModule != null) {
                    ko.mapping.fromJS(data.list_educationModule, mappingOptions, self.EducationModuleResults);
                    if (data.list_educationModule.length > 1) {
                        self.MainModulePositions.removeAll();
                        for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
                            self.MainModulePositions.push(new EducationModulePosition(i));
                        }
                    }
                    else
                        self.MainModulePositions.push(new EducationModulePosition(1));
                }

                self.Modulepositioncount(data.list_educationModule.length + 1);
                ProgressBar(80);
                $("#hndUploadEditText").val("");
                $("#UploadPPT1").val("");
                $("#UploadVideo1").val("");
                control1 = $("#UploadPPT1");

                control1.replaceWith(control1.val('').clone(true));

                // $("#btnclosepopup").click();
                ProgressBar(100);
                alertify.success("Education ModuleFile Update Successfully");
                self.TotolCount(data.pagedEducationModule.TotalCount);
                self.PagedRecords(data.pagedEducationModule.PagedRecords);
                self.pendingRequest(false);

            });


            $("#myProgress1").css("display", "none");
            $("#loading1").css("display", "none");
            $("#btnCloseDivMainEducationModule").click();
        }

    }
    self.EducationModuleFileInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#rbUploadPDF1").prop("checked")) {
            if ($("#UploadPDF1").val() == "") {
                //  $('#UploadPDF').addClass('required_bdr');
                $('#UploadPPT1').removeClass('required_bdr');
                $('#UploadVideo1').removeClass('required_bdr');
                return false;
            }
        }
        else if ($("#rbUploadPPT1").prop("checked")) {
            if ($("#UploadPPT1").val() == "") {
                $('#UploadPPT1').addClass('required_bdr');
                $('#UploadVideo1').removeClass('required_bdr');
                return false;
            }
        }
        else if ($("#rbUploadVideo1").prop("checked")) {
              if ($("#UploadVideo1").val() == "") {
                $('#UploadVideo1').addClass('required_bdr');
                $('#UploadPPT1').removeClass('required_bdr');
                return false;
            }
        }
        if ($("#frmAddEducationModuleFile").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#loaderDiv").css("display", "block");
        $("#myProgress1").css("display", "block");
        $("#loading1").css("display", "block");
        return true;
    }
    $("#addmodule").click(function () {
        self.radioSelectedOptionValue("Upload Content");
        $('#panelUpload').css('display', 'block');
        $('#panelUploadPDF').css('display', 'block');
        $('#panelUploadPPT').css('display', 'none');
        $('#panelUploadVideo').css('display', 'none');
        $("#rbUploadPDF").prop("checked", true);
        $("#UploadVideo").replaceWith($("#UploadVideo").clone(true));
        $("#UploadPPT").replaceWith($("#UploadPPT").clone(true));
        $("#EducationModuleTime").val('');
        $("#panelModuleTime").css("display", "block");
    });
}
///////////////////////////////
//$('#UploadPDF').change(function () {
//    var fileExtension = ['pdf'];
//    $("#UploadPPT").val("");
//    $("#UploadPPT").replaceWith($("#UploadPPT").clone(true));
//    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
//        alert("Only PDF formats are allowed.");
//        $("#UploadPDF").val("");
//        $("#UploadPDF").replaceWith($("#UploadPDF").clone(true));
//    }
//})
$('#UploadPPT').change(function () {
    var fileUpload = $("#UploadPPT").val();
    var ext = fileUpload.split('.').pop().toLowerCase();
    if (ext == 'pdf') {
        var formData = new FormData();
        var fileInput = document.getElementById('UploadPPT');
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
                    $("#UploadPPT").val('');
                    alert("Invalid or Corrupt file");
                    return false;
                }
            },
            error: function () {
                alert("Some error occured.");
                $("#UploadPPT").val('');
            }
        });
    }
    else {
        $("#UploadPPT").val('');
        alert("Upload pdf file only.");
        return false;
    }
});

$('#UploadModulesFiles').change(function () {    
    if ($("#UploadModulesFiles").val() != "") {
        var fileUpload = $("#UploadModulesFiles").val();
        var ext = fileUpload.split('.').pop().toLowerCase();
        if (ext == 'pdf') {
            var formData = new FormData();
            var fileInput = document.getElementById('UploadModulesFiles');
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
                        $("#UploadModulesFiles").val('');
                        alert("Invalid or Corrupt file");                        
                        return false;
                    }
                },
                error: function () {
                    $("#UploadModulesFiles").val('');
                }
            });
        }
        else {
            $("#UploadModulesFiles").val('');
            alert("Upload pdf file only.");
            return false;
        }
    }
})


$('#UploadVideo').change(function () {
    $("#UploadPDF").val("");
    $("#UploadPDF").replaceWith($("#UploadPDF").clone(true));
    $("#UploadPPT").val("");
    $("#UploadPPT").replaceWith($("#UploadPPT").clone(true));
    var fileExtension = ['flv', 'mp4', 'mkv', 'webm', 'flv', 'wmv', 'avi'];
    if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
        alert("Only Video formats are allowed.");
        $("#UploadVideo").val("");
        $("#UploadVideo").replaceWith($("#UploadVideo").clone(true));
    }
})
$('#EducationModuleTime').click(function () {
    if ($('#EducationModuleTime').val() == "" || $('#EducationModuleTime').val() == "__:__") {
        $(".ui-slider-handle").css("left", "0");
        $('.ui_tpicker_time').html('00:00');
    }
});

function ShowPanel() {
    if (document.getElementById('rbUploadPDF').checked) {
        $("#panelUploadPDF").css("display", "block");
        $("#panelUploadPPT").css("display", "none");
        $("#panelUploadVideo").css("display", "none");
        $("#panelModuleTime").css("display", "block");
        $("#UploadPDF").prop('required', true);
        $("#EducationModuleTime").prop('required', true);
        $("#EducationModuleTime").val('');
        $("#UploadPPT").removeAttr('Required', 'Required');
        $("#UploadVideo").removeAttr('Required', 'Required');
        var attr = $("#UploadVideo").attr('Required');
        if (typeof attr !== typeof undefined && attr !== false) {
            $("#UploadVideo").removeAttr('Required', 'Required');
        }
        var attr1 = $("#UploadPPT").attr('Required');
        if (typeof attr1 !== typeof undefined && attr1 !== false) {
            $("#UploadPPT").removeAttr('Required', 'Required');
        }
        $("#UploadPPT").replaceWith($("#UploadPPT").clone(true));
        $("#UploadVideo").replaceWith($("#UploadVideo").clone(true));
        $("#EducationModuleTime").prop("disabled", false);
    }
    else if (document.getElementById('rbUploadPPT').checked) {
        $("#panelUploadPDF").css("display", "none");
        $("#panelUploadPPT").css("display", "block");
        $("#panelUploadVideo").css("display", "none");
        $("#panelModuleTime").css("display", "block");
        $("#UploadPPT").prop('required', true);
        $("#EducationModuleTime").prop('required', true);
        $("#EducationModuleTime").val('');
        $("#EducationModuleTime").prop("disabled", true);
        var attr = $("#UploadVideo").attr('Required');
        if (typeof attr !== typeof undefined && attr !== false) {
            $("#UploadVideo").removeAttr('Required', 'Required');
        }
        var attr1 = $("#UploadPDF").attr('Required');
        if (typeof attr1 !== typeof undefined && attr1 !== false) {
            $("#UploadPDF").removeAttr('Required', 'Required');
        }
        $("#UploadPDF").replaceWith($("#UploadPDF").clone(true));
        $("#UploadVideo").replaceWith($("#UploadVideo").clone(true));
    }
    else if (document.getElementById('rbUploadVideo').checked) {
        $("#panelUploadPDF").css("display", "none");
        $("#panelUploadPPT").css("display", "none");
        $("#panelUploadVideo").css("display", "block");
        $("#panelModuleTime").css("display", "none");
        $("#UploadVideo").prop('required', true);
        $("#UploadPPT").removeAttr('Required', 'Required');
        $("#UploadPDF").removeAttr('Required', 'Required');
        var attr = $("#UploadPPT").attr('Required');
        if (typeof attr !== typeof undefined && attr !== false) {
            $("#UploadPPT").removeAttr('Required', 'Required');
        }
        var attr1 = $("#UploadPDF").attr('Required');
        if (typeof attr1 !== typeof undefined && attr1 !== false) {
            $("#UploadPDF").removeAttr('Required', 'Required');
        }
        var attr2 = $("#EducationModuleTime").attr('Required');
        if (typeof attr1 !== typeof undefined && attr1 !== false) {
            $("#EducationModuleTime").removeAttr('Required', 'Required');
        }
        $("#UploadPDF").replaceWith($("#UploadPDF").clone(true));
        $("#UploadPPT").replaceWith($("#UploadPPT").clone(true));
        $("#EducationModuleTime").prop("disabled", false);
    }
}
$(function () {
    $('#UploadPPT1').change(function () {
        $("#hndUploadEditText").val("");
        var fileExtension = ['pdf', 'PDF'];
        //alert(($(this).val().split('.').pop().toLowerCase(), fileExtension));
        if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            alert("Only PDF formats are allowed.");
            $("#UploadPPT1").val("");
            $("#UploadPPT1").replaceWith($("#UploadPPT1").clone(true));
        }
    })
})
$(function () {
    $("#EducationModuleTime").timepicker();
});
$(function () {
    $('#UploadVideo1').change(function () {
        $("#hndUploadEditText").val("");
        $("#UploadPPT1").val("");
        $("#UploadPPT1").replaceWith($("#UploadPPT1").clone(true));
        var fileExtension = ['flv', 'mp4', 'mkv', 'webm', 'flv', 'wmv', 'avi'];
        if ($.inArray($(this).val().split('.').pop().toLowerCase(), fileExtension) == -1) {
            alert("Only Video formats are allowed.");
            $("#UploadVideo1").val("");
            $("#UploadVideo1").replaceWith($("#UploadVideo1").clone(true));
        }
    })
})
function ShowPanel1() {
    if (document.getElementById('rbUploadPDF1').checked) {
        if (filetype != "Upload Content") {
            $("#hndUploadEditText").val();
            var editor1 = document.getElementById("Editor3").editor;
            editor1.SetText("");
        }
        $("#panelUploadPDF1").css("display", "block");
        $("#panelUploadPPT1").css("display", "none");
        $("#panelUploadVideo1").css("display", "none");
        $("#UploadPPT1").replaceWith($("#UploadPPT1").clone(true));
        $("#UploadVideo1").replaceWith($("#UploadVideo1").clone(true));
        $("#hndFiletypeid").val("1");
    }
    else if (document.getElementById('rbUploadPPT1').checked) {
        $("#panelUploadPDF1").css("display", "none");
        $("#panelUploadPPT1").css("display", "block");
        $("#panelUploadVideo1").css("display", "none");
        $("#UploadVideo1").replaceWith($("#UploadVideo1").clone(true));
        $("#hndFiletypeid").val("2");
        $("#hndUploadEditText").val();
    }
    else if (document.getElementById('rbUploadVideo1').checked) {
        if (filetype != "Upload video") {
            var video = document.getElementById('v1');
            video.setAttribute('src', "");
        }
        $("#panelUploadPDF1").css("display", "none");
        $("#panelUploadPPT1").css("display", "none");
        $("#panelUploadVideo1").css("display", "block");
        $("#UploadPPT1").replaceWith($("#UploadPPT1").clone(true));
        $("#hndFiletypeid").val("3");
        $("#hndUploadEditText").val();
    }
}
$('#UploadPDF').click(function () {
    $('#UploadPDF').removeClass('required_bdr');
});
$('#UploadPPT').click(function () {
    $('#UploadPPT').removeClass('required_bdr');
});
$('#UploadVideo').click(function () {
    $('#UploadVideo').removeClass('required_bdr');
});
$('#UploadVideo').bind('change', function () {
    setFileInfo(this);
    document.getElementById('getvideotime').click();
});
$('#UploadVideo1').bind('change', function () {
    setFileInfo(this);
    document.getElementById('getvideotime1').click();
});
function convertMS(duration) {
  
    var sec_num = parseInt(duration, 10); // don't forget the second param
    var hours = Math.floor(sec_num / 3600);
    var minutes = Math.floor((sec_num - (hours * 3600)) / 60);
    var seconds = sec_num - (hours * 3600) - (minutes * 60);
    if (hours < 10) { hours = "0" + hours; }
    if (minutes < 10) { minutes = "0" + minutes; }
    if (seconds < 10) { seconds = "0" + seconds; }
    var time = hours + ':' + minutes + ':' + seconds;
    if ($('#chkIsTimerRequired').is(':checked') == true) {
        $(".classEducationModuleTime").val(time);
    }
    else {
      
        $(".classEducationModuleTime").val(null);
    }
  
}
function ClearValidation() {
    $('#UploadPDF').removeClass('required_bdr');
    $('#UploadPPT').removeClass('required_bdr');
    $('#UploadVideo').removeClass('required_bdr');
    $('.form-group').removeClass('has-error has-feedback');
    $('.form-group').find('.help-block').hide();
    $('.form-group').find('i.form-control-feedback').hide();
    $('input').addClass('remove_bdr');
    $('textarea').addClass('remove_bdr');
    $(".ui-slider-handle").css("left", "0");
    $('.ui_tpicker_time').html('00:00');
    $("#myProgress").css("display", "none");
    $("#loading").css("display", "none");
    $("#loaderDivmain").css("display", "none");

}
function form_submit() {
    ProgressBarUpdating(20);
    $("#hndUploadText").val($("<div>").text($("#Editor2").val()).html());//encoding
    $("#hndEducationModuleDescription").val($("<div>").text($("#Editor4").val()).html());//encoding
    $('input').removeClass('remove_bdr');
    $('textarea').removeClass('remove_bdr');
    $('#frmAddEducationModule').submit();
    //ProgressBarUpdating(80);
    var editor1 = document.getElementById("Editor2").editor;
    editor1.SetText("");
    var editor2 = document.getElementById("Editor4").editor;
    editor2.SetText("");
   // ProgressBarUpdating(100);
};

function form_submit1() {
   
 ProgressBarUpdating(20);
    if ($("#hndFiletypeid").val() != "1") {
        $("#hndUploadEditText").val("");
        var editor1 = document.getElementById("Editor3").editor;
        editor1.SetText("");
    }
    else {
        $("#hndUploadEditText").val($("<div>").text($("#Editor3").val()).html());//encoding
    }
    $("#SliderDiv").html('');
    $("#frmAddEducationModuleFile").submit();
    //document.getElementById("v1").pause();
    };

$("#btnSaveandContinueModule").click(function () {
    $('#EditorModal1').modal('hide')
});

$("#btnSaveandContinueModule").click(function () {
    $('#EditorModalDesc').modal('hide')
});

$("#btnClosemodule").click(function () {
    clearcontent();
});

$("#btnClosemoduledesc").click(function () {
    if ($("#hdEducationModuleID").val() == "") {
        alertify.confirm("Are you sure want to clear this content?", function (e) {
            if (e) {
                $("#EditorModalDesc").val("");
                var editor1 = document.getElementById("Editor4").editor;
                editor1.SetText('');
                $('#EditorModalDesc').modal('hide')
            }
        });
    }
    else {
        $('#EditorModalDesc').modal('hide')
    }
});

function ProgressBarUpdating(percentageValue) {
    var id = 0;
    var bar = $('.progress-bar');
    var percent = $('.progress-bar');
    var status = $('#status');
    var percentValue = percentageValue + '%';
    bar.width(percentValue);
    percent.html(percentValue);
 
}
function ProgressBar(percentageValue) {
    var id = 0;
    var bar = $('.progress-bar');
    var percent = $('.progress-bar');
    var status = $('#status');
    var percentValue = percentageValue + '%';
    bar.width(percentValue);
    percent.html(percentValue);

}

function clearcontent() {
    if ($("#hdEducationModuleID").val() == "") {
        alertify.confirm("Are you sure want to clear this content?", function (e) {
            if (e) {
                $("#EditorModal1").val("");
                var editor1 = document.getElementById("Editor2").editor;
                editor1.SetText('');
                $('#EditorModal1').modal('hide')
            }
        });
    }
    else {
        $('#EditorModal1').modal('hide');
    }
}
$("#btnClose12").click(function () {
    clearcontent();
});
$("#btnCloseDesc").click(function () {
    
    if ($("#hdEducationModuleID").val() == "") {
        alertify.confirm("Are you sure want to clear this content?", function (e) {
            if (e) {
                $("#EditorModalDesc").val("");
                var editor1 = document.getElementById("Editor4").editor;
                editor1.SetText('');
                $('#EditorModalDesc').modal('hide')
            }
        });
    }
    else {
        $('#EditorModalDesc').modal('hide')
    }

});
$("#btnCloseedit").click(function () {
    $('#EditorModal2').modal('hide')
});
$("#btnSaveandContinueeditModule").click(function () {
    $("#hndUploadEditText").val($("<div>").text($("#Editor3").val()).html());//encoding
    $('#EditorModal2').modal('hide');

});

$("#btnSaveandContinueeditModuledesc").click(function () {
    $("#hndEducationModuleDescription").val($("<div>").text($("#Editor4").val()).html());//encoding  
    $('#EditorModalDesc').modal('hide')
});


$("#btnCloseeditmodule").click(function () {
    $("#EditorModal2").css("display", "none");
    $("#EditorModal2").removeClass("in");
    $(".modal-backdrop").remove();
    $("#inner-content").removeClass("modal-open");
    $('#EditorModal2').modal('hide');
});