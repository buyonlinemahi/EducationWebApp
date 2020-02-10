function MyEducationModuleGridViewModel() {
    var self = this;
    var browser;
    self.MyEducationModuleDetail = ko.observableArray([]);
    self.CourseName = ko.observable();
    self.ExpireDaysLeft = ko.observable();
    self.MEID = ko.observable();
    self.ModuleContent = ko.observable('test');
    self.percentage = ko.observable();
    self.UserID = ko.observable();
    self.IsRevisit = ko.observable();
    self.SessionTimeout = ko.observable();
    this.Init = function (model) {
        
        if (model != null) {
            /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
           
            self.IsRevisit(model.IsRevisit);
            if (self.IsRevisit()) {
                browser = getBrowser();
                if (browser != null || browser != "") {
                    self.UserID(model.UserID);
                    self.SessionTimeout(model.SessionTimeout);
                    setInterval(function () {
                        if (self.IsRevisit()) {
                            CheckSession();
                        }
                    }, self.SessionTimeout());
                   
                    $.post("/LogSession/LogModuleOrExam", { browser: browser, newurl: 'RevisitModule', MEID: model.MyEducationModuleDetailResults[0].MEID, UserId: self.UserID() }, function (_model) {
                        var model = $.parseJSON(_model);
                        if (model != "success") {
                            var path = '/User/Unauthorise';
                            window.location = path;
                        }
                    });
                }
            }
            /*****************************End*********************************/
            ko.mapping.fromJS(model.MyEducationModuleDetailResults, {}, self.MyEducationModuleDetail)
            self.CourseName(self.MyEducationModuleDetail()[0].CourseName());
            self.MEID(self.MyEducationModuleDetail()[0].MEID());
            self.ExpireDaysLeft(self.MyEducationModuleDetail()[0].ExpireDaysLeft());
            self.ModuleContent(model.MyEducationModuleDetail.EducationModuleDescription);
            self.percentage(model.MyEducationModuleDetail.percent);
            hideloderDiv();
        }
    };
    self.ShowModuleContent = function (myeducationModule) {
       
        self.ModuleContent(myeducationModule.EducationModuleDescription())
    }
    self.ShowModuleMedia = function (myeducationModule) {
        
        var MEMID = myeducationModule.MEMID();       
        
        if (self.IsRevisit()) {
            $(window).unbind('beforeunload');
            /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
            if (browser != null || browser != "") {
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                    $.post("/CertificationProgram/EncryptQueryString", { plainText: MEMID }, function (_Encrypttext) {
                        var EncryptMEMID = $.parseJSON(_Encrypttext);
                        
                        var path = '/MyEducation/MyEducationModuleMedia/' + EncryptMEMID;
                        window.location = path;
                    });
                });
            }
            /*****************************End*********************************/
        }
        else {     

            $.post("/CertificationProgram/EncryptQueryString", { plainText: MEMID }, function (_Encrypttext) {
                var EncryptMEMID = $.parseJSON(_Encrypttext);
                var path = '/MyEducation/MyEducationModuleMedia/' + EncryptMEMID;
                window.location = path;
            });
        }
    }
    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }
    /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
    if (browser != null || browser != "") {
       
        function CheckSession() {
            $.post("/User/CheckSession", function (data) {
                if (data != "true") {
                    $(window).unbind('beforeunload');
                    $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                        window.location = "/Home/";
                    });
                }
            });
        }
        bajb_backdetect.OnBack = function () {
            if (self.IsRevisit() && browser == "safari") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                });
            }
            else if (self.IsRevisit() && browser == "mozilla") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                });
                alert("You are  leaving this page");
            }
        }

        $(".pageunload").click(function (event) {
            var href = $(this).attr('href');
            if (self.IsRevisit()) {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
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
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
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
                $(window).bind('beforeunload', function () {
                    $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                    });
                    setTimeout(function () {
                        //for insert again for stay here
                        setTimeout(function () {
                            $.post("/LogSession/InsertSession", { UserID: self.UserID(), browser: browser, pageurl: 'RevisitModule', MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                            });
                        }, 1000);
                    }, 1);
                    return "Do you really want to close?";
                });
            }
        });
        $(window).load(function () {
            document.getElementById("mytext").focus();
        });

        /*****************************End*********************************/
    }

    $('#backbutton').click(function (e) {
        if (self.IsRevisit()) {
            e.preventDefault();
            $(window).unbind('beforeunload');
            if (browser != null || browser != "") {
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: $("#hdfMEID").val() }, function (_Encrypttext) {
                        var path = '/MyEducation/';
                        window.location = path;
                });
            }
        }
    });
}
