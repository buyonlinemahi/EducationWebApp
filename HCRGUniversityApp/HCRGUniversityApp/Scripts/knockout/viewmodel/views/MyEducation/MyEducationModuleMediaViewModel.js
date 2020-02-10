function MyEducationModuleMediaGridViewModel() {
    var self = this;
    var browser;

    self.MyEducationModuleDetail = ko.observableArray([]);
    self.test = ko.observable();
    //self.EducationModuleFileDetail = ko.observableArray([]);
    self.ModuleContent = ko.observable();
    self.CountArray = ko.observableArray();
    self.ExpireDaysLeft = ko.observable();
    self.ImagePath = ko.observable();
    self.UserID = ko.observable();
    self.IsRevisit = ko.observable();
    self.SessionTimeout = ko.observable();
    self.FirstModule = ko.observable(0);
    self.LastModule = ko.observable(0);
    self.CurrentMEMIndex = ko.observable();
    self.CourseFile = ko.observable();
    var Isloaded = true;
    this.Init = function (model) {        
        showloderDiv();
        if (model != null) {
            /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
            self.IsRevisit(model.IsRevisit);

            if (self.IsRevisit()) {
                browser = getBrowser();
                if (browser != null || browser != "") {
                    self.UserID(model.UserID);
                    self.SessionTimeout(model.SessionTimeout);
                    $.post("/LogSession/LogModuleOrExam", {
                        browser: browser,
                        newurl: 'RevisitModule', MEID: model.MyEducationModuleDetail.MEID, UserId: self.UserID()
                    }, function (_model) {
                        var model = $.parseJSON(_model);
                        if (model != "success") {
                            $(window).unbind('beforeunload');
                            var path = '/User/Unauthorise';
                            window.location = path;
                        }
                    });
                }
                setInterval(function () {                    
                    if (self.IsRevisit() && $('#Timer').text() == "00:00:00" || $('#hndMCompleted').val() == 'true') {
                        CheckSession()
                    }
                }, self.SessionTimeout());
            }
            /*****************************End*********************************/

            ko.mapping.fromJS(model.MyEducationModuleDetailResults, {}, self.MyEducationModuleDetail);
            //ko.mapping.fromJS(model.MyEducationModuleDetailResults.EducationModuleFileDetail, {}, self.EducationModuleFileDetail);
            ko.mapping.fromJS(model, {}, self);
            ko.mapping.fromJS(model.MyEducationModuleDetail, {}, self);
            self.CourseFile(model.MyEducationModuleDetail.CourseFile);            
            self.ModuleContent(model.MyEducationModuleDetail.FilePath);
            self.ExpireDaysLeft(self.MyEducationModuleDetail()[0].ExpireDaysLeft());
            self.ImagePath(model.MyEducationModuleDetail.FilePath + model.MyEducationModuleDetail.ModuleFile);
            self.CountArray.removeAll();
            for (var i = 1; i <= model.MyEducationModuleDetail.CountDirectoryFiles; i++)
                self.CountArray.push({ CountValue: i, ModuleFilePPT: model.MyEducationModuleDetail.ModuleFile, ImagePath: self.ImagePath });
            $("#DivModulecontent").html(self.ModuleContent());
            if(!self.MyEducationModuleDetail().IsTimerRequired())
            {
                $("#Timer").hide();
            }
            for (var i = 0; i < self.MyEducationModuleDetail().length;i++)
            {
                if (self.MyEducationModuleDetail()[i].MEMID() == self.MyEducationModuleDetail().MEMID()) {
                    self.CurrentMEMIndex(i);
                }
            }
                if (self.MyEducationModuleDetail()[0].MEMID() == self.MyEducationModuleDetail().MEMID()) {
                    self.FirstModule(1);
                }
                else {
                    $("#btnPrevious").removeClass("hide");
                    self.FirstModule(0);                    
                }
                if (self.MyEducationModuleDetail()[self.MyEducationModuleDetail().length - 1].MEMID() == self.MyEducationModuleDetail().MEMID()) {
                    $("#btnFinished").removeClass("hide");
                    self.LastModule(1);                    
                }
                else {                    
                    $("#btnNext").removeClass("hide");
                    self.LastModule(0);
                }
                hideloderDiv();
        }
    };

    self.finishedClick = function () {
        showloderDiv();
        $(window).unbind('beforeunload');
        $.post("/MyEducation/FinishMyCourse", { _educationID: $("#hndeid").val(), _mEID: $("#hdfMEID").val() }, function (_path) {
            $.ajax({
                type: 'POST',
                url: '/LogSession/DeleteSession',
                data: { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() },
                success: function () {
                    if (_path == '/MyEducation/') {
                        window.location = '/MyEducation/';
                    }
                    else {
                        window.location = _path.Item1 + _path.Item2 + "/" + _path.Item3;
                    }
                },
                async: false
            });
        });
    };

    self.nextClick = function () {
        showloderDiv();
        $(window).unbind('beforeunload');
        if (browser != null || browser != "") {
            $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                if ($('#Timer').text() != "00:00:00") {
                    $.ajax({
                        url: "/MyEducation/UpdateTimer",
                        cache: false,
                        type: "POST", async: false, dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ meMID: $('#hndmemid').val(), updatedTime: $('#Timer').text() }),
                        success: function (data) {
                        },
                        error: function (data) {
                            alert("Error  - " + data);
                        }
                    });
                }
                $.post("/CertificationProgram/EncryptQueryString", { plainText: self.MyEducationModuleDetail()[self.CurrentMEMIndex() + 1].MEMID() }, function (_Encrypttext) {
                    var EncryptMEMID = $.parseJSON(_Encrypttext);
                    var path = '/MyEducation/MyEducationModuleMedia/' + EncryptMEMID;
                    window.location = path;
                });
            });
        }
    }

    self.previousClick = function () {
        showloderDiv();
        $(window).unbind('beforeunload');
        if (browser != null || browser != "") {
            $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                if ($('#Timer').text() != "00:00:00") {
                    $.ajax({
                        url: "/MyEducation/UpdateTimer",
                        cache: false,
                        type: "POST", async: false, dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ meMID: $('#hndmemid').val(), updatedTime: $('#Timer').text() }),
                        success: function (data) {
                        },
                        error: function (data) {
                            alert("Error  - " + data);
                        }
                    });
                }
                $.post("/CertificationProgram/EncryptQueryString", { plainText: self.MyEducationModuleDetail()[self.CurrentMEMIndex() - 1].MEMID() }, function (_Encrypttext) {
                    var EncryptMEMID = $.parseJSON(_Encrypttext);
                    var path = '/MyEducation/MyEducationModuleMedia/' + EncryptMEMID;
                    window.location = path;
                });
            });
        }
    };

    self.ShowModuleContent = function (myeducationModule) {
        showloderDiv();
        $(window).unbind('beforeunload');
        if (browser != null || browser != "") {
            $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {

                self.EducationModuleDescription(myeducationModule.EducationModuleDescription());
                self.ModuleFile(myeducationModule.ModuleFile());
                if ($('#Timer').text() != "00:00:00") {
                    $.ajax({
                        url: "/MyEducation/UpdateTimer",
                        cache: false,
                        type: "POST", async: false, dataType: 'json',
                        contentType: 'application/json',
                        data: ko.toJSON({ meMID: $('#hndmemid').val(), updatedTime: $('#Timer').text() }),
                        success: function (data) {
                        },
                        error: function (data) {
                            alert("Error  - " + data);
                        }
                    });
                }
                $.post("/CertificationProgram/EncryptQueryString", { plainText: myeducationModule.MEMID() }, function (_Encrypttext) {
                    var EncryptMEMID = $.parseJSON(_Encrypttext);
                    var path = '/MyEducation/MyEducationModuleMedia/' + EncryptMEMID;
                    window.location = path;
                });
            });
        }
    }

    $(document).ready(function () {
        showloderDiv();
        if ($('#Timer').length == '0')
        {
            $("#btnNext").removeClass("disable_btn");
            $("#btnFinished").removeClass("disable_btn");
        }
        else if ($('#Timer').text() != '00 : 00 : 00') {
            setTimeout(function () { updateTimer(); }, 5000);
            $("#btnNext").addClass("disable_btn");
            $("#btnFinished").addClass("disable_btn");
        }
        else {
            $("#btnNext").removeClass("disable_btn");
            $("#btnFinished").removeClass("disable_btn");
        }           
        $("#" + $('#hndmemid').val()).addClass("highlight-div");                
        $("#collapseOne" + $('#hndmemid').val()).removeClass("collapse");
         hideloderDiv();
    });

    function GetMyEducationModulesDetailsByMEID(_mEMID) {
        //showloderDiv();
        $.ajax({
            url: "/MyEducation/GetMyEducationModulesDetailsByMEID",
            cache: false,
            type: "post", datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ MEMID: _mEMID }),
            success: function (data) {
                var _data = $.parseJSON(data);
                ko.mapping.fromJS(_data, {}, self.MyEducationModuleDetail);
                $("#" + $('#hndmemid').val()).addClass("highlight-div");
                $("#collapseOne" + $('#hndmemid').val()).removeClass("collapse");
               // hideloderDiv();
            },
            error: function (data) {
                alert("error  - " + data);
            }
        });        
    }


    function updateTimer() {       
        $.ajax({
            url: "/MyEducation/UpdateTimer",
            cache: false,
            type: "post", datatype: 'json',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON({ meMID: $('#hndmemid').val(), updatedTime: $('#Timer').text() }),
            success: function (data) {
                $('#hndMCompleted').val(data);
                if (data == 'true') {                    
                    GetMyEducationModulesDetailsByMEID($('#hndmemid').val());                    
                }
            },
            error: function (data) {
                alert("error  - " + data);
            }
        });
        if ($('#Timer').text() != "00:00:00" || $('#hndMCompleted').val() == 'false') {            
            setTimeout(function () { updateTimer(); }, 5000);
            $("#btnNext").addClass("disable_btn");
            $("#btnFinished").addClass("disable_btn");            
        }
        else {
            $("#btnNext").removeClass("disable_btn");            
            $("#btnFinished").removeClass("disable_btn");            
             if (self.MEMPositiontoStart() == model.MyEducationModuleDetail.EducationModulePosition) {
            self.MEMPositiontoStart(self.MEMPositiontoStart() + 1);
            }
        }        
    }

    window.onbeforeunload = function () {
        if ($('#hndMCompleted').val() == 'false') {
            if (navigator.userAgent.indexOf("Chrome") != -1) {
                return "You are about to leave this page without completing the module time requirement. When you continue the module, you will resume where you left off";
            }
            else {
                alert("You are about to leave this page without completing the module time requirement. When you continue the module, you will resume where you left off");
            }

        }
    };


    //$('.download_icon').click(function (e) {
    //    $(window).unbind('beforeunload');
    //    return true;
    //});

    $('#backbutton').click(function (e) {
        var path = "";       
        e.preventDefault();
        if (self.IsRevisit()) {
            $(window).unbind('beforeunload');
            if (browser != null || browser != "") {
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                    var path = '/MyEducation/';
                    window.location = path;
                });
            }
        }
        else {
            $.post("/CertificationProgram/EncryptQueryString", { plainText: $("#hndmeid").val() }, function (_Encrypttext) {
                var path = '/MyEducation/';
                window.location = path;
            });
           
        }
    });
    /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
    if (browser != null || browser != "") {
        function CheckSession() {
            $.post("/User/CheckSession", function (data) {
                if (data != "true") {
                    $(window).unbind('beforeunload');
                    $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                        window.location = "/Home/";
                    });
                }
            });
        }

        bajb_backdetect.OnBack = function () {
            if (browser == "safari" && self.IsRevisit()) {
                $(window).unbind('beforeunload');
            }
            else if (self.IsRevisit() && browser == "mozilla") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                });
                alert("You are  leaving this page");
            }
        }
        $(window).load(function () {
            document.getElementById("mytext").focus();
        });
        $(".pageunload").click(function (event) {

            var href = $(this).attr('href');
            if (self.IsRevisit()) {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                    window.location = href;
                });
                event.preventDefault();
            }
            else
                window.location = href;
        });
        $(".logout").click(function (event) {
            if (self.IsRevisit()) {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                    $.get("/User/LogOff", function (_Encrypttext) {
                        window.location = "/";
                    });
                });
                event.preventDefault();
            }
            else {
                $.get("/User/LogOff", function (_Encrypttext) {
                    window.location = "/";
                });
            }
        });

        $(function () {
            
            if (self.IsRevisit()) {                
                $(window).on('beforeunload', function () {
                    //showloderDiv();
                    $.ajax({
                        type: 'POST',
                        url: '/LogSession/DeleteSession',
                        data: { UserID: self.UserID(), browser: browser, MEID: $("#hndmeid").val() },
                        success: function () { },
                        async: false
                    });
               
                    setTimeout(function () {
                        //for insert again for stay here
                        setTimeout(function () {
                            $.post("/LogSession/InsertSession", { UserID: self.UserID(), browser: browser, pageurl: 'RevisitModule', MEID: $("#hndmeid").val() }, function (_Encrypttext) {
                                
                            });
                        }, 1000);
                    }, 1);
                    //hideloderDiv();
                    return 'are you sure';
                    e.preventDefault();
                });
            }
        });

        function hideloderDiv() {
            $("#loaderDiv").removeClass('loaderbg');
            $("#loading").hide();
        }

        function showloderDiv() {
            $("#loaderDiv").addClass('loaderbg');
            $("#loading").show();
        }
    }
    /*****************************End*********************************/
}

function ShowhideRightToDown(id) {

    $("span[id*='right#']").show();
    $("span[id*='down#']").hide();
    var myarray = id.split('#')
    document.getElementById("down#" + myarray[1]).style.display = 'block';
    document.getElementById("right#" + myarray[1]).style.display = 'none';

}

function ShowhideDownToRight(id) {

    $("span[id*='right#']").show();
    $("span[id*='down#']").hide();
    var myarray = id.split('#')
    document.getElementById("down#" + myarray[1]).style.display = 'none';
    document.getElementById("right#" + myarray[1]).style.display = 'block';

}