function PreviewEducationViewModel() {
    var self = this;
    self.CourseName = ko.observable();
    self.EncryptedEDUModuleID = ko.observable();
    self.EducationModulesData = ko.observableArray();
    self.EducationModuleFileResults = ko.observableArray();
    self.ModuleFile = ko.observable();
    self.ModuleUploadFilePath = ko.observable();
    self.FileTypeID = ko.observable();
    self.EducationModuleVideo = ko.observable();
    self.UploadFilePath = ko.observable();
    self.CurrentMEMIndex = ko.observable(0);
    self.FirstModule = ko.observable(0);
    self.LastModule = ko.observable(0);
    self.ModuleTotalCount = ko.observable(0);

    self.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.EducationModules.EducationModules, {}, self.EducationModulesData);
            ko.mapping.fromJS(model.EducationModules.EducationModules[0].EducationModuleFileResults, {}, self.EducationModuleFileResults);
            self.CourseName(model.CourseName);
            $("#hndmeid").val(model.EducationModules.EducationModules[0].EducationModuleID);
            if (model.EducationModules.EducationModules[0].FileTypeID == 1) {
                DivModule();
                $("#divModuleContent").show();
                $("#divModuleFile").html(model.EducationModules.EducationModules[0].ModuleFile);
            }
            else if (model.EducationModules.EducationModules[0].FileTypeID == 2) {
                DivModule();
                $("#divModulePDF").show();
                self.UploadFilePath(model.EducationModules.EducationModules[0].ModuleFile);
            }
            else if (model.EducationModules.EducationModules[0].FileTypeID == 3) {
                DivModule();
                $("#divModuleVideo").show();
                self.EducationModuleVideo(model.EducationModules.EducationModules[0].ModuleFile);
            }
        }
        self.ModuleTotalCount(model.EducationModules.TotalCount);
        if (model.EducationModules.TotalCount > 1) {
            $("#btnNext").removeClass("hide");
            self.LastModule(0);
        }
    };

    self.PreviewModuleContent = function (index, EncryptedEDUModuleID)
    {
        //Menu click  
          $.ajaxSetup({ cache: false });               
          $.ajax({
            url: "/Education/GetEducationModuleFile",
            data: ko.toJSON({ educationmoduleid: EncryptedEDUModuleID() }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) 
            {
                $(".EducationModuleFile").addClass("collapse");
                $("#collapseOne" + data.educationModuleFile.EducationModuleID).removeClass("collapse");
                if (data.educationModuleFile.FileTypeID == 1)
                {
                    DivModule();
                    $("#divModuleContent").show();
                    //self.ModuleFile(data.educationModuleFile.ModuleFile);
                    $("#divModuleFile").html(data.educationModuleFile.ModuleFile);
                }
                else if (data.educationModuleFile.FileTypeID == 2)
                {
                    DivModule();
                    $("#divModulePDF").show();
                    self.UploadFilePath(data.educationModuleFile.UploadFilePath);                
                }
                else if (data.educationModuleFile.FileTypeID == 3)
                {
                    DivModule();
                    $("#divModuleVideo").show();
                    self.EducationModuleVideo(data.educationModuleFile.EducationModuleVideo);                 
                }
                if (self.ModuleTotalCount() > 1) {
                    self.CurrentMEMIndex(index());
                    self.btnDisplayTrigger();
                }
                
            }
        });
         
    }

    function DivModule() {
        $("#divModuleVideo").hide();
        $("#divModuleContent").hide();
        $("#divModulePDF").hide();
    }
    

    //**************** Getting First Module data By default *******************************//
    self.GetModuleData = function (id) {
       // button nevigation

        $.ajax({
            url: "/Education/GetEducationModuleFile",
            data: ko.toJSON({ educationmoduleid: model.EducationModules.EducationModules[id].EncryptedEDUModuleID }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                $(".EducationModuleFile").addClass("collapse");
                $("#collapseOne" + data.educationModuleFile.EducationModuleID).removeClass("collapse");
                if (data.educationModuleFile.FileTypeID == 1) {
                    DivModule();
                    $("#divModuleContent").show();
                    self.ModuleFile(data.educationModuleFile.ModuleFile);
                    $("#divModuleFile").html(data.educationModuleFile.ModuleFile);
                }
                else if (data.educationModuleFile.FileTypeID == 2) {
                    DivModule();
                    $("#divModulePDF").show();
                    self.UploadFilePath(data.educationModuleFile.UploadFilePath);
                }
                else if (data.educationModuleFile.FileTypeID == 3) {
                    DivModule();
                    $("#divModuleVideo").show();
                    self.EducationModuleVideo(data.educationModuleFile.EducationModuleVideo);
                }
            }            
        });
        if (self.ModuleTotalCount() > 1) {
            self.btnDisplayTrigger();
        }
    }
    //*********************************************************************************************************//
    self.nextClick = function () {
        $.ajaxSetup({ cache: false });
        self.GetModuleData(self.CurrentMEMIndex() + 1);
        self.CurrentMEMIndex(self.CurrentMEMIndex() + 1);
        self.btnDisplayTrigger();
    }
    self.previousClick = function () {
        $.ajaxSetup({ cache: false });
        self.GetModuleData(self.CurrentMEMIndex() - 1);
        self.CurrentMEMIndex(self.CurrentMEMIndex() - 1);
        self.btnDisplayTrigger();
    };

    self.finishedClick = function () {
        window.location = '/Education/SearchCourseCatalog';
    };

    self.btnDisplayTrigger = function ()
    {        
        if ((self.EducationModulesData().length - 1) == self.CurrentMEMIndex()) {
            self.LastModule(1);
            self.FirstModule(0);
            $("#btnPrevious").removeClass("hide");
            $("#btnFinished").removeClass("hide");
        }
        else {
            self.LastModule(0);
            $("#btnPrevious").removeClass("hide");
        }
        if (self.CurrentMEMIndex() == 0) {
            self.FirstModule(1);
            self.LastModule(0);
            $("#btnNext").removeClass("hide");
            $("#btnFinished").removeClass("hide");
        }
        else {
            self.FirstModule(0);
            $("#btnPrevious").removeClass("hide");
        }
    }
}
$(document).ready(function () {
    $("#collapseOne" + $('#hndmeid').val()).removeClass("collapse");
});