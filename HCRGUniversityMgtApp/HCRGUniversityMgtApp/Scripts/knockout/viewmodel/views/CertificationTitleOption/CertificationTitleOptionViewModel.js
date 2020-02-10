function CertificationTitleOptionViewModel() {
    var self = this;
    self.CertificationTitleOptionID = ko.observable(0);
    self.CertificationTitle = ko.observable();
    self.CertificationContent = ko.observable();
    self.EducationId = ko.observable();
    self.CourseName = ko.observable();
    self.flag = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.PagedRecords = ko.observable(0);
    self.CertificationTitleOptionResults = ko.observableArray([]);
    self.Educations = ko.observableArray([]);
    self.EducationsForBackUp = ko.observableArray([]);

    $('#loaderDiv').hide();

    $(document).ready(function () {    
        $.post("/CertificationTitleOption/GetAllCourseDropDownList", function (_data) {
            var model = $.parseJSON(_data);
            if (model.length > 0) {
                $.each(model, function (index, value) {
                    self.Educations.push(new Education(value));
                    ko.mapping.fromJS(model, {}, self.EducationsForBackUp);
                });
                $("#createOptionID").prop("disabled", false);
            }
            else {
                $("#createOptionID").prop("disabled", true);
            }
        });
    });

   
    function Education(value) {
        var self = this;
        self.EducationID = ko.observable(value.EducationID);
        self.isDone = ko.observable(false);
        self.isDoneChecked = ko.observable(true);
        self.CourseName = ko.observable(value.CourseName);
    }

    function EducationOnDelete(value) {
        var self = this;
        self.EducationID = ko.observable(value.EducationID());
        self.isDone = ko.observable(false);
        self.isDoneChecked = ko.observable(true);
        self.CourseName = ko.observable(value.CourseName());
    }

    function EducationInEdit(_eduID, _courseName) {
        var self = this;
        self.EducationID = ko.observable(_eduID);
        self.isDone = ko.observable(true);
        self.isDoneChecked = ko.observable(true);
        self.CourseName = ko.observable(_courseName);
        $("#hdEducationID").val(_eduID);
        $("#hdCourseName").val(_courseName);
    };

    this.Init = function (model) {
        if (model != null) {
            $("#loaderDiv").show();
            ko.mapping.fromJS(model.CertificationTitleOptionRecords, {}, self.CertificationTitleOptionResults);
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            $('#loaderDiv').hide();
        };
    };
    self.scrolled = function (data, event) {
        if (self.TotolCount() > self.maxId()) {
            var elem = event.target;
            if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                getAllCertificationTitle();
            }
        }

    };
    function getAllCertificationTitle() {
        $("#loaderDiv").show();
        $.post("/CertificationTitleOption/GetAllPagedCertificationTitleOption", { skip: self.maxId() }, function (data) {
            var model = $.parseJSON(data);
            $.each(model.CertificationTitleOptionRecords, function (index, value) {
                self.CertificationTitleOptionResults.push(new InsertCertificationTitleLineItem(value));
            });
            self.TotolCount(data.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + data.PagedRecords);
            $('#loaderDiv').hide();
            setgrdDesignondelete();
        });
    };
    function getAllCertificationTitleForInsertAndUpdate() {
        $("#loaderDiv").show();
        $.post("/CertificationTitleOption/GetAllPagedCertificationTitleOption", { skip: 0 }, function (data) {
            var model = $.parseJSON(data);
            self.CertificationTitleOptionResults.removeAll();
            ko.mapping.fromJS(model.CertificationTitleOptionRecords, {}, self.CertificationTitleOptionResults);
            self.TotolCount(model.TotalCount);
            self.CurrentPage(self.maxId());
            self.maxId(self.maxId() + model.PagedRecords);
            $('#loaderDiv').hide();
            setgrdDesignondelete();
        });
    };

    function InsertCertificationTitleLineItem(value) {
        var self = this;
        self.CertificationTitleOptionID = ko.observable(value.CertificationTitleOptionID);
        self.CertificationTitle = ko.observable(value.CertificationTitle);
        self.CertificationContent = ko.observable(value.CertificationContent);
        self.EducationId = ko.observable(value.EducationId);
        self.CourseName = ko.observable(value.CourseName);

    };
    self.CertificationTitleInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddCertificationTitle").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    };
   
    self.AddCertificationSuccess = function (data) {       
        if (data != null) {
            var newCertification = $.parseJSON(data);
            if (newCertification.flag == "NotSaved") {
                alertify.alert("Please select one course");
            }
            else {
                BindCheckBoxList();
                getAllCertificationTitleForInsertAndUpdate();
                if (newCertification.flag == "Update") {
                    alertify.success("Updated Successfully");
                }
                else {
                    alertify.success("Insert Successfully");
                }
                resetcontrol();
                setgrdDesign();
            }            
            
        }
    };
    function resetcontrol() {
        $("#hd").val('');
        $("#Editor1").val('');
        var $iFrameContents = $('iframe').contents().find('div.designborder');
        $iFrameContents.html('');
        $("#hdCertificationTitleOptionID").val("");
        $("#hdCertificationTitle").val("");
        $("#hdEducationID").val(0);
        $("#hdCourseName").val("");
        self.CertificationTitleOptionID("");
        self.CertificationTitle("");
        self.CertificationContent("");
        self.EducationId(0);
        self.CourseName("");        
    };

    self.ViewCertificationTitleLineItems = function (config) {        
        editor1.SetText(($("<div>").html(config.CertificationContent()).text()));
        self.CertificationTitleOptionID(config.CertificationTitleOptionID());
        self.CertificationTitle(config.CertificationTitle());
        self.CertificationContent(config.CertificationContent());       
        self.Educations.removeAll();
        AddFromBackupOFRadioList();
        self.Educations.push(new EducationInEdit(config.EducationId(), config.CourseName()));      
        $("#createOptionID").prop("disabled", false);
        $(window).scrollTop(0);
    };

    function AddFromBackupOFRadioList() {
           var model = self.EducationsForBackUp();
           $.each(model, function (index, value) {
               self.Educations.push(new EducationOnDelete(value));
            });        
    };
    function BindCheckBoxList() {
        $.post("/CertificationTitleOption/GetAllCourseDropDownList", function (_data) {
            var model = $.parseJSON(_data);
            if (model.length > 0) {
                self.Educations.removeAll();
                self.EducationsForBackUp.removeAll();
                $.each(model, function (index, value) {
                    self.Educations.push(new Education(value));
                    ko.mapping.fromJS(model, {}, self.EducationsForBackUp);
                });
                $("#createOptionID").prop("disabled", false);
            }
            else {
                self.Educations.removeAll();
                $("#createOptionID").prop("disabled", true);
            }
        });
    };

    this.DeleteCertificationTitleLineItems = function (itemToDelete) {
        alertify.confirm("Are you sure you want to delete this record?", function (e) {
            if (e) {
                $.ajax({
                    url: '/CertificationTitleOption/DeleteCertificationTitleOption',
                    cache: false,
                    type: 'POST',
                    dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ _certificationTitleOption: itemToDelete }),
                    success: function (data) {
                        self.CertificationTitleOptionResults.remove(itemToDelete);
                        BindCheckBoxList();
                        alertify.success("Deleted Successfully");
                        setgrdDesignondelete();
                    },
                    error: function (data) {
                        alert('Error while deleting a item - ' + data);
                    }
                });
            }
        });
    };

    self.PutEducations = function (data) {
        $("#hdEducationID").val(data.EducationID());
        $("#hdCourseName").val(data.CourseName());
        $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
        return true;
    };

    self.CheckSubmitForm = function (obj, event) {
        //This is for checking change or not......mahi
        submitFormBefore(event);
    }

    function submitFormBefore(event) {
        if (event.originalEvent) { //user changed
            $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
        }
        else {// program changed
        }
    };

    $(".btnClose").click(function () {
        clearcontent();
    });

    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });
    function clearcontent() {
        if ($("#hdCertificationTitleOptionID").val() == "") {
            alertify.confirm("Are you sure want to clear this content?", function (e) {
                if (e) {
                    $("#hd").val('');
                    $("#Editor1").val('');
                    var $iFrameContents = $('iframe').contents().find('div.designborder');
                    $iFrameContents.html('');
                    $('.EditorModal').modal('hide');
                    $(".EditorModal").css("display", "none");
                    $(".EditorModal").removeClass("in");
                    $(".modal-backdrop").remove();
                    $("#inner-content").removeClass("modal-open");
                }
            });
        }
        else {
            $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
            if ($("#hd").val().length > 60000) {
                alert("Limit Exceed.")
            }
            else {
                $('#EditorModal').modal('hide');
                $("#EditorModal").css("display", "none");
                $("#EditorModal").removeClass("in");
                $(".modal-backdrop").remove();
                $("#inner-content").removeClass("modal-open");
            }
        }
    };
    $("#btnSaveandContinue").click(function () {
        $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
        if ($("#hd").val().length > 60000) {
            alert('Limit Exceed')
        }
        else {
            $('#EditorModal').modal('hide');
            $("#EditorModal").css("display", "none");
            $("#EditorModal").removeClass("in");
            $(".modal-backdrop").remove();
            $("#inner-content").removeClass("modal-open");
        }
    });

}

if ($('#main').hasScrollBar()) {
    setgrdDesign();
};
function setgrdDesign() {
    if ($('#main').hasScrollBar()) {

        $('#main tr td').each(function (index) {
            var className = this.className.match(/paddingleft\d+/);
            if (className != null) {
                $(this).removeClass(className[0]);
            }
            $(this).addClass("paddingleft45");
        });
    }
};
function setgrdDesignondelete() {
    if ($('#main').hasScrollBar()) {
        $('#main tr td').each(function (index) {
            var className = this.className.match(/paddingleft\d+/);
            if (className != null) {
                $(this).removeClass(className[0]);
            }
            $(this).addClass("paddingleft45");
        });
    }
    else if (!$('#main').hasScrollBar()) {
        $('#main tr td').each(function (index) {
            var className = this.className.match(/paddingleft\d+/);
            if (className != null) {
                $(this).removeClass(className[0]);
            }
            $(this).addClass("paddingleft35");
        });
    }
}
