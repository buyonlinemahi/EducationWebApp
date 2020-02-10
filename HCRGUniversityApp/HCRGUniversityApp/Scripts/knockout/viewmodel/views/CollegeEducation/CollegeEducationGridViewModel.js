function CollegeEducationGridViewModel() {
    var self = this;
    self.CollegeID = ko.observable();
    self.CourseCode = ko.observable();
    self.CourseDescription = ko.observable();
    self.CourseName = ko.observable();
    self.CoursePrice = ko.observable();
    self.CourseStartDate = ko.observable();
    self.CourseEndDate = ko.observable();
    self.CourseTime = ko.observable();
    self.CourseTitle = ko.observable();
    self.CourseUploadDate = ko.observable();
    self.CourseUploadDate2 = ko.observable();
    self.EducationID = ko.observable();
    self.EducationFormat = ko.observableArray([]);
    self.EducationFormatImagePath = ko.observableArray([]);
    self.EducationFormatDatePrioritys = ko.observableArray([]);
    self.selectedItem = ko.observable(0);
    self.Colleges = ko.observableArray();
    self.Professions = ko.observableArray();   
    self.Colleges = ko.observableArray([self.Colleges(0)]);
    self.EducationFormats = ko.observableArray();
    self.EducationFormats = ko.observableArray([self.EducationFormats(0)]);
    self.selectedItemEducationFormats = ko.observable(0);
    self.EducationdetailResults = ko.observableArray([]);
    self.TotalItemCount = ko.observable(0);
    self.PagingFilterType = ko.observable('All');
    self.ShowOnlineCourseOnly = ko.observable(false);
    this.Init = function (model) {
        if (model != null) {
            self.EducationdetailResults.removeAll();
            $.each(model.EducationDetailResults, function (index, value) {

                if ((index == 0) || (index % 2 == 0))
                    value.Count = 1;
                else
                    value.Count = 0;

                self.EducationdetailResults.push(new Educationdetail(value));
            });
            self.TotalItemCount(model.TotalCount);
            self.Pager().CurrentPage(1);
            self.ShowOnlineCourseOnly(model.ShowOnlineCourseOnly);
            if (model.TotalCount == 0)
            {
                document.getElementById('EmptyGrid').style.display = 'block';
            }
        }
     };
    self.ViewFilterRecords = function () {
        self.EducationdetailResults.removeAll();
        self.Skip(0);
        var collegeID = $('#College :selected').val();
        var educationFormatid = $('#EducationFormat :selected').val();
        var professionID = $('#Profession :selected').val();
      
        if (collegeID != '' || educationFormatid != '' || professionID != '' ) {
            if (collegeID != '' && educationFormatid == '' && professionID == '' ) {
                self.PagingFilterType('College');
                getEducationDetailBycollege(collegeID, self.Skip());
                self.Pager().CurrentPage(1);
              }
            else if (collegeID == '' && educationFormatid != '' && professionID == '') {
                self.PagingFilterType('Format');
                GetEducationCollegeByEducationFormat(educationFormatid, self.Skip());
                self.Pager().CurrentPage(1);
            }
            else if (collegeID == '' && educationFormatid == '' && professionID != '') {
                self.PagingFilterType('Profession');
                GetEducationByProfessionID(professionID, self.Skip());
                self.Pager().CurrentPage(1);
             
            }

            else {
                self.PagingFilterType('Mix');
                GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfID(collegeID, educationFormatid, professionID, self.Skip());
                self.Pager().CurrentPage(1);
            }
        }
        else {
            self.PagingFilterType('All');
            getalleducation();
            self.Pager().CurrentPage(1);
        }
    }
    function Educationdetail(value) {
        var self = this;
        self.CollegeID = ko.observable(value.CollegeID);
        self.CourseCode = ko.observable(value.CourseCode);
        self.CourseDescription = ko.observable(value.CourseDescription);
        self.CourseName = ko.observable(value.CourseName);
        self.CoursePrice = ko.observable(value.CoursePrice);
        self.CourseEndDate = ko.observable(moment(value.CourseEndDate).format("MMM DD,YYYY"));
        self.CourseStartDate = ko.observable(moment(value.CourseStartDate).format("MMM DD,YYYY"));
        self.CourseTime = ko.observable(value.CourseTime);
        self.CourseTitle = ko.observable(value.CourseTitle);
        self.CourseUploadDate = ko.observable(moment(value.CourseUploadDate).format("MMM DD,YYYY"));
        self.EducationID = ko.observable(value.EducationID);
        self.EducationFormat = ko.observableArray(value.EducationFormat);
        var viewModel = self.EducationFormat;
        self.EducationFormatImagePath = ko.observableArray([]);
        self.Count = ko.observable(value.Count);
        
        var myviewmodel = self.EducationFormatImagePath;
        for (var i = 0; i <= viewModel().length - 1; i++) {
            myviewmodel.push(new InsertNewItem(value.EducationFormat[i]));
        }
    };
    function InsertNewItem(_item) {
        var self = this;
        switch (_item.EducationFormatType) {
            case "LIVE EDUCATION":
                self.EducationFormatImagePath1 = ko.observable("/Content/img/" + _item.EducationFormatType.replace(' ', '') + ".png");
                self.EducationFormatDatePriority = ko.observable(_item.EducationPriority);
                break;
            case "ONLINE COURSES":
                self.EducationFormatImagePath1 = ko.observable("/Content/img/" + _item.EducationFormatType.replace(' ', '') + ".png");
                self.EducationFormatDatePriority = ko.observable(_item.EducationPriority);
                break;
            case "ON-SITE EDUCATION":
                self.EducationFormatImagePath1 = ko.observable("/Content/img/" + _item.EducationFormatType.replace(' ', '') + ".png");
                self.EducationFormatDatePriority = ko.observable(_item.EducationPriority);
                break;
            case "SELF STUDY EDUCATION":
                self.EducationFormatImagePath1 = ko.observable("/Content/img/" + _item.EducationFormatType.replace(' ', '') + ".png");
                self.EducationFormatDatePriority = ko.observable(_item.EducationPriority);
                break;
            case "MOBILE ACCESS":
                self.EducationFormatImagePath1 = ko.observable("/Content/img/" + _item.EducationFormatType.replace(' ', '') + ".png");
                self.EducationFormatDatePriority = ko.observable(_item.EducationPriority);
                break;
        }
    }
    function InsertEducationFormatDatePriority(_DatePriority) {
        var self = this;
        self.EducationFormatDatePriority1 = ko.observable(_DatePriority);
    }
    function EducationFormatDatePrioritysfinalItem(_DatePriority, _month, _dayweek, _day) {
        var self = this;
        self.EducationFormatDatePriorityItem = ko.observable(_DatePriority);
        self.EducationFormatDatePriorityItemMonth = ko.observable(_month);
        self.EducationFormatDatePriorityItemdayweek = ko.observable(_dayweek);
        self.EducationFormatDatePriorityItemdayDay = ko.observable(_day);
    }
    $(document).ready(function () {
        $.ajaxSetup({ cache: false });
        $.getJSON("/CollegeEducation/getColleges", self.Colleges);
        $.getJSON("/CollegeEducation/getProfession", self.Professions);
        $.getJSON("/CollegeEducation/getEducationFormat", function (data) {
            self.EducationFormats(data.slice());
            if (self.ShowOnlineCourseOnly() == true)
                self.selectedItemEducationFormats(2);
        });
    });
    function GetEducationCollegeByEducationFormat(educationFormatid, skip) {
        $.post("/CollegeEducation/GetEducationCollegeByEducationFormatID", { EducationFormatID: educationFormatid, skip: skip }, function (_data) {
            var data = $.parseJSON(_data);
            self.EducationdetailResults.removeAll();
            for (var i = 0; i < data.EducationDetailResults.length; i++) {
                if ((i == 0) || (i % 2 == 0))
                    data.EducationDetailResults[i].Count = 1;
                else
                    data.EducationDetailResults[i].Count = 0;

                self.EducationdetailResults.push(new Educationdetail(data.EducationDetailResults[i]));
            }
            self.TotalItemCount(data.TotalCount);
            if (data.TotalCount == 0) {
                document.getElementById('EmptyGrid').style.display = 'block';
            }
        });
    }
    function getEducationDetailBycollege(collegeID, skip) {
        $.post("/CollegeEducation/getEducationDetailBycollegeID", {
            collegeID: collegeID, skip: skip
        }, function (_data) {
            var data = $.parseJSON(_data);
            self.EducationdetailResults.removeAll();
            for (var i = 0; i < data.EducationDetailResults.length; i++) {
                if ((i == 0) || (i % 2 == 0))
                    data.EducationDetailResults[i].Count = 1;
                else
                    data.EducationDetailResults[i].Count = 0;
                self.EducationdetailResults.push(new Educationdetail(data.EducationDetailResults[i]));
            }
            self.TotalItemCount(data.TotalCount);
            if (data.TotalCount == 0)
            {
                document.getElementById('EmptyGrid').style.display = 'block';
            }
        });
    }

    function GetEducationByProfessionID(professionID, skip) {
        $.post("/CollegeEducation/GetEducationByProfessionID", { professionID: professionID, skip: skip }, function (_data) {
            var data = $.parseJSON(_data);
            self.EducationdetailResults.removeAll();
            for (var i = 0; i < data.EducationDetailResults.length; i++) {
                if ((i == 0) || (i % 2 == 0))
                    data.EducationDetailResults[i].Count = 1;
                else
                    data.EducationDetailResults[i].Count = 0;
                self.EducationdetailResults.push(new Educationdetail(data.EducationDetailResults[i]));
            }
            self.TotalItemCount(data.TotalCount);
            if (data.TotalCount == 0) {
                document.getElementById('EmptyGrid').style.display = 'block';
            }
        });
    }
    function GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfID(collegeID, educationFormatid,  professionID, skip) {
        $.post("/CollegeEducation/GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfID", { collegeID: collegeID, EducationFormatID: educationFormatid,  professionID: professionID, skip: skip }, function (_data) {
            var data = $.parseJSON(_data);
            self.EducationdetailResults.removeAll();
            for (var i = 0; i < data.EducationDetailResults.length; i++) {
                if ((i == 0) || (i % 2 == 0))
                    data.EducationDetailResults[i].Count = 1;
                else
                    data.EducationDetailResults[i].Count = 0;
                self.EducationdetailResults.push(new Educationdetail(data.EducationDetailResults[i]));
            }
            self.TotalItemCount(data.TotalCount);
            if (data.TotalCount == 0) {
                document.getElementById('EmptyGrid').style.display = 'block';
            }
        });
    }
    function getalleducation() {
        $.ajaxSetup({ cache: false });
        $.post("/CollegeEducation/GetAllEducation", {
            skip: self.Skip()
        }, function (_data) {
            var data = $.parseJSON(_data);
            self.EducationdetailResults.removeAll();
            for (var i = 0; i < data.EducationDetailResults.length; i++) {

                if ((i == 0) || (i % 2 == 0))
                    data.EducationDetailResults[i].Count = 1;
                else
                    data.EducationDetailResults[i].Count = 0;


                self.EducationdetailResults.push(new Educationdetail(data.EducationDetailResults[i]));
            }
            self.TotalItemCount(data.TotalCount);
        });;
    }
    this.LearnMore = function (model) {
        $.ajax({
            url: "/CertificationProgram/ShowDetail",
            data: ko.toJSON({ EducationID: self.EducationID() }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON(this),
            success: function (data) {
                $.post("/CertificationProgram/EncryptQueryString", { plainText: model.EducationID() }, function (_Encrypttext) {
                    var EncryptEducationID = $.parseJSON(_Encrypttext);
                    //var path = '/CertificationProgram/Index?eid=' + EncryptEducationID;
                    var path = '/Course/CourseDetail/' + EncryptEducationID;
                    window.location = path;
                });
            },
            error: function (data) {
                alert("Error while deleting a College - " + data);
            }
        });
    };
    self.GetRecordsWithSkipTake = function (skip, take) {
        if (skip == undefined || take == undefined) {
            self.Skip(0);
            self.Take(pagingSettings.pageSize);
        }
        else {
            self.Skip(skip);
            self.Take(take);
        }
        var collegeID = $('#College :selected').val();
        var educationFormatid = $('#EducationFormat :selected').val();
        var professionID = $('#Profession :selected').val();
      
        $.ajaxSetup({ cache: false });
        if (self.PagingFilterType() == 'All') {
            getalleducation();
        }
        else if (self.PagingFilterType() == 'College') {
            getEducationDetailBycollege(collegeID, self.Skip());
        }
        else if (self.PagingFilterType() == 'Format') {
            GetEducationCollegeByEducationFormat(educationFormatid, self.Skip());
        }
        else if (self.PagingFilterType() == 'Profession') {
            GetEducationByProfessionID(professionID, self.Skip());
        }
        
        else if (self.PagingFilterType() == 'Mix') {
            GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfID(collegeID, educationFormatid, professionID, self.Skip());
        }
    };
    var pagingSettings = {
        pageSize: 25,
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
};