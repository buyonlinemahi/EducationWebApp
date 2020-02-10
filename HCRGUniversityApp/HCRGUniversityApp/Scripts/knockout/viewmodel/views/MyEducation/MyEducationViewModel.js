function MyEducationGridViewModel() {
    var self = this;
    var browser;
    self.TotalItemCount = ko.observable(0);
    self.iTotalItemCount = ko.observable(0);
    self.ProTotalItemCount = ko.observable(0);
    self.MyEducationDetailProgressResults = ko.observableArray([]);
    self.MyEducationDetailCompletedResults = ko.observableArray([]);
    self.UserAllAccessPassResults = ko.observableArray([]);
    self.currentindex = ko.observable(0);
    self.ProductPurchaseDetails = ko.observableArray([]);
    self.AllAccessEndDate = ko.observable(null);
    self.AllAccessValidEndDate = ko.observable(null);
    self.UserID = ko.observable();
    self.EncryptedMEID = ko.observable();
    self.IsRevisit = ko.observable();
    self.SessionTimeout = ko.observable();

    var mappingOptions = {
        'ExpiryDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD, YYYY");
            }
        },
        'CourseCompletedDate': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD, YYYY");
            }
        },
         'Date': {
            create: function (options) {
                if (options.data != null)
                    return moment(options.data).format("MMM DD, YYYY");
            }
         }
        ,
         'AllAccessEndDate': {
             create: function (options) {
                 if (options.data != null)
                     return moment(options.data).format("MMM DD, YYYY");
             }
         }
    }
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model.ProductPurchaseDetails.ProductPurchaseDetails, mappingOptions, self.ProductPurchaseDetails);
            ko.mapping.fromJS(model.MyEducationDetailResults, mappingOptions, self.MyEducationDetailProgressResults);
            ko.mapping.fromJS(model.MyEducationDetailCompletedResults, mappingOptions, self.MyEducationDetailCompletedResults);
            ko.mapping.fromJS(model.UserAllAccessPassResults, mappingOptions, self.UserAllAccessPassResults);
            if (model.UserAllAccessPassResults != null)
                self.AllAccessValidEndDate(model.UserAllAccessPassResults.AllAccessValidEndDate);
            else
                self.AllAccessValidEndDate(null);
            self.ProTotalItemCount(model.ProductPurchaseDetails.TotalCount);
            self.iTotalItemCount(model.TotalInprocessItemCount);
            self.TotalItemCount(model.TotalItemCount);
            self.currentindex((model.Skip / ipagingSettings.ipageSize) + 1);
            self.Pager().CurrentPage(1);
            self.iPager().iCurrentPage(self.currentindex());
            self.ProPager().ProCurrentPage(self.currentindex());
            hideloderDiv();        
        }
    };

    self.OpenCertificationContent = function (_data) {       
        $.post("/MyEducation/GetCertificationContentByID", {
            _certificationTitleOptionID: _data.CertificationTitleOptionID()
        },
       function (_data) {
           $("#DivPrivacyPolicyLegalPopUp").animate({ scrollTop: 0 }, "slow");
           $('#lableCommonHead').text('Certification Content');         
           $("#DivPrivacyPolicyLegalPopUp").html('');
           $("#DivPrivacyPolicyLegalPopUp").html(_data.CertificationContent);
         
          
       });;
        $(".hide").removeClass("show-div").addClass("hide-div");
        $("#" + id).removeClass("hide-div").addClass("show-div");
    };

    self.ContinueCourse = function (myeducationProgress) {
        var MEID = myeducationProgress.EncryptedMEID()
        //var path = '/MyEducation/MyEducationModule/' + MEID;
        //window.location = path;
        $.ajax({
            url: "/MyEducation/MyEducationModuleMediaSkipByMethod",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ meID: MEID }),
            success: function (data) {
                if (data != null) {
                    if (data == "Unauthorise") {
                        window.location = '/User/Unauthorise';
                    }
                    else {
                        self.IsRevisit(data.IsRevisit);
                        self.ShowModuleMedia(data);
                    }
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
               
            }
        });
    }
    self.ShowModuleMedia = function (myeducationModule) {
        var MEMID = 0;
        if (self.IsRevisit()==true)
        {
             MEMID = myeducationModule.MyEducationModuleDetailResults[0].MEMID;
        }
        else 
        {
            for (var i = 0; i < myeducationModule.MyEducationModuleDetailResults.length; i++) {
                if (myeducationModule.MyEducationModuleDetailResults[i].Completed == false) {
                    MEMID = myeducationModule.MyEducationModuleDetailResults[i].MEMID;
                    break;
                }
            }
        }
       
       
        if (self.IsRevisit()) {
            browser = getBrowser();
            if (browser != null || browser != "") {
                self.UserID(myeducationModule.UserID);
                self.SessionTimeout(myeducationModule.SessionTimeout);
                setInterval(function () {
                    if (self.IsRevisit()) {
                        CheckSession(myeducationModule);
                    }
                }, self.SessionTimeout());
                   
                $.post("/LogSession/LogModuleOrExam", { browser: browser, newurl: 'RevisitModule', MEID: myeducationModule.MyEducationModuleDetailResults[0].MEID, UserId: self.UserID() }, function (_model) {
                    var model = $.parseJSON(_model);
                    if (model != "success") {
                        var path = '/User/Unauthorise';
                        window.location = path;
                    }
                });
            }
        }

        if (self.IsRevisit()) {
            $(window).unbind('beforeunload');
            /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
            if (browser != null || browser != "") {
                $.post("/LogSession/DeleteSession", { UserID: self.UserID(), browser: browser, MEID: myeducationModule.MyEducationModuleDetailResults[0].MEID, }, function (_Encrypttext) {
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

    function CheckSession(myeducationModule) {
        $.post("/User/CheckSession", function (data) {
            if (data != "true") {
                $(window).unbind('beforeunload');
                $.post("/LogSession/DeleteSession", { UserID: myeducationModule.UserID, browser: browser, MEID: myeducationModule.MyEducationModuleDetailResults[0].MEID, }, function (_Encrypttext) {
                    window.location = "/Home/";
                });
            }
        });
    }
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        $.ajax({
            url: "/MyEducation/GetMyEducationCompletedByUserIDPaged",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: self.Skip() }),
            success: function (data) {
                if (data != null) {
                    ko.mapping.fromJS(data.MyEducationDetailCompletedResults, {}, self.MyEducationDetailCompletedResults);
                    self.TotalItemCount(data.TotalItemCount);
                    //BlockNewTab();
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    };
    var pagingSettings = {
        pageSize: 5,
        pageSlide: 2
    };
    self.Skip = ko.observable(0);
    self.Take = ko.observable(pagingSettings.pageSize);
    self.Pager = ko.pager(self.TotalItemCount);
    self.Pager().PageSize(pagingSettings.pageSize);
    self.Pager().PageSlide(pagingSettings.pageSlide);
    self.Pager().CurrentPage(1);
    self.Pager().CurrentPage.subscribe(function () {
        var skip = pagingSettings.pageSize * (self.Pager().CurrentPage() - 1);
        var take = pagingSettings.pageSize;
        self.GetRecordsWithSkipTake(skip, take);
    });


    //========= paging code for popup grid only ===========//
    var ipagingSettings = {
        ipageSize: 5,
        ipageSlide: 2
    };
    self.iSkip = ko.observable(0);
    self.iTake = ko.observable(ipagingSettings.ipageSize);
    self.iPager = ko.ipager(self.iTotalItemCount);
    self.iPager().iPageSize(ipagingSettings.ipageSize);
    self.iPager().iPageSlide(ipagingSettings.ipageSlide);
    self.iPager().iCurrentPage(1);
    self.iPager().iCurrentPage.subscribe(function () {
        var skip = ipagingSettings.ipageSize * (self.iPager().iCurrentPage() - 1);
        var take = ipagingSettings.ipageSize;
        self.iGetRecordsWithSkipTake(skip, take);
    });
    self.iGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.iSkip(0);
            self.iTake(ipagingSettings.ipageSize);
        }
        else {
            self.iSkip(skip);
            self.iTake(take);
        }

        GetMyEducationInProgressByUserIDPaged();

    };

    //========= paging code for popup grid only ===========//
    var PropagingSetting = {
        PropageSize: 5,
        PropageSlide: 2
    };
    self.ProSkip = ko.observable(0);
    self.ProTake = ko.observable(PropagingSetting.PropageSize);
    self.ProPager = ko.Propager(self.ProTotalItemCount);

    self.ProPager().ProPageSize(PropagingSetting.PropageSize);
    self.ProPager().ProPageSlide(PropagingSetting.PropageSlide);
    self.ProPager().ProCurrentPage(1);

    self.ProPager().ProCurrentPage.subscribe(function () {
        var skip = PropagingSetting.PropageSize * (self.ProPager().ProCurrentPage() - 1);
        var take = PropagingSetting.PropageSlide;
        self.ProGetRecordsWithSkipTake(skip, take);
    });
    self.ProGetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.iSkip(0);
            self.iTake(ipagingSettings.ipageSize);
        }
        else {
            self.ProSkip(skip);
            self.ProTake(take);
        }

        GetProductPurchasePaged();
    };
   

    self.AddOneDay = function (TimeByDay) {
        var newDate = new Date(TimeByDay);
        return new Date(newDate.setDate(newDate.getDate() + 90))
    };

    self.TodayDate = ko.observable(new Date());
    function GetProductPurchasePaged() {
        $.ajax({
            url: "/MyEducation/GetProductPurchasePaged",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: self.ProSkip() }),
            success: function (data) {
                if (data != null) {
                    ko.mapping.fromJS(data.ProductPurchaseDetails, mappingOptions, self.ProductPurchaseDetails);
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    }

    // ============= paging code for invoice ends =============== //
    function GetMyEducationInProgressByUserIDPaged() {
        $.ajax({
            url: "/MyEducation/GetMyEducationInProgressByUserID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ skip: self.iSkip() }),
            success: function (data) {
                if (data != null) {
                    $.post("/CertificationProgram/EncryptQueryString", { plainText: self.iSkip() }, function (_Encrypttext) {
                        var EncryptMEMID = $.parseJSON(_Encrypttext);
                        var pageurl = '/MyEducation/MyEducationDetail/' + EncryptMEMID;
                        if (pageurl != window.location) {
                            Hash.pushState(true);
                            Hash.go(pageurl);
                         //   window.history.pushState({ path: pageurl }, '', pageurl);
                        }
                        ko.mapping.fromJS(data.MyEducationDetailResults, {}, self.MyEducationDetailProgressResults);
                        self.iTotalItemCount(data.TotalInprocessItemCount);
                        self.iPager().iCurrentPage(self.iPager().iCurrentPage());
                        BlockNewTab();
                    });
                }
            },
            error: function (data) {
                alertify.alert("Data Not Found");
            }
        });
    }


    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }
    //-----------------------Block OPen in New Tab Code  ----------------------------------//
    /*****************************Code for  Get Already open course module and exam browser in Case of Revisit Case*********************************/
    function BlockNewTab() {
        $('#divMyEducation a').each(function () {
            var href = $(this).attr('href');
            $(this).attr('href', 'javascript:void(0);');
            $(this).attr('jshref', href);
        });
        $('#divMyEducation a').bind('click', function (e) {
            e.stopImmediatePropagation();
            e.preventDefault();
            e.stopPropagation();
            var href = $(this).attr('jshref');
            if (!e.metaKey && e.ctrlKey)
                e.metaKey = e.ctrlKey;
            if (!e.metaKey) {
                if ($(this).hasClass('coursePaging'))
                    return true;
                else
                    location.href = href;
            }
            return false;
        });


    }
    $(document).ready(function () {

        BlockNewTab();
    });
    /*****************************End*********************************/

    
}
