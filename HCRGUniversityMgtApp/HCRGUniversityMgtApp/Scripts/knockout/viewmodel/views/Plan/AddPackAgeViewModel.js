function AddPackAgeViewModel(model) {
    var self = this;

    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.IsPlanDetailPge = ko.observable();
    self.UsersBindingByPlanID = ko.observableArray([]);
    self.CoursesBindingByClientID = ko.observableArray([]);
    self.ClientTypeID = ko.observable();
    self.MultipleUserID = ko.observableArray([]);
    self.MultipleCourseID = ko.observableArray([]);

    self.UserPlanRecordsResult = ko.observableArray([]);
    self.CoursePlanRecordsResult = ko.observableArray([]);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.maxId = ko.observable(0);
    self.IsPlanAppliedCheck = ko.observable();
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);

    //===============Plans Drop Down Declaration=============//
    self.selectedPlan = ko.observable(0);
    self.Plans = ko.observableArray();
    self.Plans = ko.observableArray([self.Plans(0)]);
    self.PlanID = ko.observable();

    //=============Users Drop Down Declaration=================//
    self.selectedUser = ko.observable(0);
    self.Users = ko.observableArray();
    self.Users = ko.observableArray([self.Users(0)]);
    self.UserID = ko.observable();

    //=============Clients Drop Down Declaration  ====================//
    self.selectedClient = ko.observable(0);
    self.Clients = ko.observableArray();
    self.Clients = ko.observableArray([self.Clients(0)]);
    self.ClientID = ko.observable();

    //===============Courses Drop Down Declaration===================//
    self.selectedCourse = ko.observable(0);
    self.Courses = ko.observableArray();
    self.Courses = ko.observableArray([self.Courses(0)]);
    self.CourceID = ko.observable();
    //=================================================//

    if (model != null) {
        $('#loaderDiv').show();
        ko.mapping.fromJS(model.UserPlanRecords, {}, self.UserPlanRecordsResult);
        ko.mapping.fromJS(model.CoursePlanRecords, {}, self.CoursePlanRecordsResult);
        $("#hdIsPlanAppliedCheck").val(model.IsPlanAppliedCheck);
        if (model.IsPlanAppliedCheck == true) 
            $("#SaveCoursePlanBtn").prop('disabled', true);
        else 
            $("#SaveCoursePlanBtn").prop('disabled', false);

        $('#loaderDiv').hide();
        if (model.ClientTypeID == 1)
            $("#divClientID").show();
        else
            $("#divClientID").hide();

        $('#ClientID').val(model.ClientID);
        $("#hdClientID").val(model.ClientID);
        GetCourseAllByClientId(model.ClientID);
        $('#ClientID').val(model.ClientID);
        $("#hdPlanID").val(model.PlanID);

        $.getJSON("/Plan/GetAllPlans", function (data) {
            self.Plans(data.slice());
            if (model != null) {
                self.selectedPlan(model.PlanID);

                $("#hdPlanID").val(model.PlanID);
                $("#hdClientID").val(model.ClientID);
                self.PlanIDChangeFromIndex(model.PlanID);
            }
        });

    }

    //====== Plan Drop Down Chenge Event==================//
    self.PlanIDChangeFromIndex = function (item) {
        if (item) {
            $("#hdPlanID").val(item);
            GetClientByPlanId(item);
            GetUserByPlanId(item);
            if (model.IsPlanAppliedCheck == false) {
                $('#ClientID').removeAttr('disabled');
            }            
            $('#UserID').removeAttr('disabled');
            BindUserAndCoursesByPlanID(item);
        }
       
    }
    
    //============== Client Drop Down Change Event ===============//
    self.ClientIDChanged = function (item) {
        if (item.selectedClient()) {
            $("#hdClientID").val(item.selectedClient());
            GetCourseAllByClientId(item.selectedClient());
            $('#EducationID').removeAttr('disabled');
        }
        
    }

    //=============Client Drop Down Binding=================///
    function GetClientByPlanId(val) {
        var _Id = val;
        $.getJSON("/Client/GetAllClientsByPlanID", { _planID: _Id },
            function (data) {
                self.Clients(data.slice());
                if (model != null) {
                    self.selectedClient(model.ClientID);
                }
            });
    }

    //=============User Drop Down Binding=================///
    function GetUserByPlanId(val) {
        var _Id = val;
        $.ajax({
            url: "/User/GetAllUsersByPlanID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _planID: _Id }),
            success: function (model) {
                self.UsersBindingByPlanID.removeAll();
                $.each(model, function (index, value) {
                    self.UsersBindingByPlanID.push(new UserInsertByID(value));
                });
                $('#UserID').multiselect('destroy');
                $('#UserID').multiselect('refresh');

            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
       
    }

    function UserInsertByID(value) {
        var self = this;
        self.UID = ko.observable(value.UID);
        self.UserfullName = ko.computed(function () {
            return value.FirstName + " " + value.LastName;
        });

    }

    self.DeleteUsersByUserPlanID = function (model) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $('#loaderDiv').show();
                $.ajax({
                    url: "/Plan/DeleteUserByUsersPlanID",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify({ userPlanID: model.UserPlanID() }),
                    success: function (model) {
                        var PlanID = $("#hdPlanID").val();
                        BindUserAndCoursesByPlanID(PlanID);
                        GetUserByPlanId(PlanID);
                    },
                    error: function (data) {
                        alertify.alert("Error while updating a item - " + data);
                    }
                });
            }
        });
    }
    self.DeleteCoursesByCoursePlanID = function (model) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $('#loaderDiv').show();
                $.ajax({
                    url: "/Plan/DeleteCoursesByCoursePlanID",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: JSON.stringify({ coursesPlanID: model.CoursePlanID() }),
                    success: function (model) {
                        var ClientID = $("#hdClientID").val();
                        BindUserAndCoursesByPlanID($("#hdPlanID").val());
                        GetCourseAllByClientId(ClientID);

                    },
                    error: function (data) {
                        alertify.alert("Error while updating a item - " + data);
                    }
                });
            }
        });
    }
    self.ApplyPackageByPlanID = function () {
        var userPlanGridCount = self.UserPlanRecordsResult();
        var coursePlanGridCount = self.CoursePlanRecordsResult();
        if (userPlanGridCount.length > 0 && coursePlanGridCount.length > 0) {
            alertify.confirm("Are you sure want to apply the package?", function (e) {
                if (e) {
                    $.ajax({
                        url: "/Plan/AppliedPackageMyEducationRecordByPlanID",
                        cache: false,
                        type: "POST", dataType: 'json',
                        contentType: 'application/json',
                        data: JSON.stringify({ PlanID: $("#hdPlanID").val() }),
                        success: function (model) {
                            if (model == 1) {
                                window.location = "/Plan/";
                            }
                        },
                        error: function (data) {
                            alertify.alert("Error while updating a item - " + data);
                        }
                    });
                }
            });
        }
        else {
            alertify.alert("Please add atleast one user and one course");
        }
    }
    //============Cource Drop Down Binding==============//
    function GetCourseAllByClientId(val) {
        var _Id = val;
        $.ajax({
            url: "/Education/GetAllCoursesByClientID",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _clientID: _Id }),
            success: function (model) {
                $("#CourseMultipleDiv").show();
                self.CoursesBindingByClientID.removeAll();
                $.each(model, function (index, value) {
                    self.CoursesBindingByClientID.push(new CourceInsertByID(value));
                });
                $('#EducationID').multiselect('destroy');
                $('#EducationID').multiselect('refresh');

            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }

    function CourceInsertByID(value) {
        var self = this;
        self.EducationID = ko.observable(value.EducationID);
        self.CourseName = ko.observable(value.CourseName);
    }
    //=================================================//
    self.SaveUserPlan = function () {
        var Ischeck = true;
        var msg = '';
        if ($('#ClientID').val() == 0 || $('#ClientID').val() == "") {
            Ischeck = false;
            msg = 'Select atleast one client';
        }
        else if ($('#UserID').val() == null) {
            Ischeck = false;
            msg = 'Select atleast one user';
        }
        else {
            Ischeck = true;
            msg = '';
        }

        var _saveData = {
            PlanID: $('#PlanID').val(),
            ClientID: $('#ClientID').val(),
            MultipleUserID: $('#UserID').val()
        }

        if (Ischeck == true) {
            $.ajax({
                url: "/Plan/AddUserPlan",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ _userplan: _saveData }),
                cache: false,
                success: function (_model) {
                    if (_model == "User Plan Added Successfully") {                        
                        var PlanID = $("#hdPlanID").val();
                        BindUserAndCoursesByPlanID(PlanID);
                        GetUserByPlanId(PlanID);
                        alertify.alert(_model);
                    }
                    else {
                        alertify.alert(_model);
                    }
                }
            });
        }
        else {
            alertify.alert(msg);
        }
    }
    //=================================================//
    self.SaveCourcePlan = function () {
        $('#loaderDiv').show();
        var Ischeck = true;
        var msg = '';
        if ($('#ClientID').val() == 0 || $('#ClientID').val() == "") {
            Ischeck = false;
            msg = 'Select at least one client';
        }
        else if ($('#EducationID').val() == null) {
            Ischeck = false;
            msg = 'Select atleast one course';
        }
        else {
            Ischeck = true;
            msg = '';
        }
        var _saveData = {
            PlanID: $('#PlanID').val(),
            ClientID: $('#ClientID').val(),
            MultipleCourseID: $('#EducationID').val()
        }
        if (Ischeck == true) {
            $.ajax({
                url: "/Plan/AddCourcePlan",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ _coursePlan: _saveData }),
                cache: false,
                success: function (_model) {
                    if (_model == "Cource Plan Added Successfully") {
                        var ClientID = $("#hdClientID").val();
                        var PlanID = $("#hdPlanID").val();
                        BindUserAndCoursesByPlanID(PlanID);
                        GetCourseAllByClientId(ClientID);
                        alertify.alert(_model);                        
                    }
                    else {
                        alertify.alert(_model);
                    }
                }
            });
        }
        else {
            alertify.alert(msg);
        }
    }


    function ResetControls() {
        $('#PlanID').prop('selectedIndex', 0);
        $('#ClientID').prop('selectedIndex', 0);
        $('#loaderDiv').hide();
  }

    function BindUserAndCoursesByPlanID(planID) {
        $.ajax({
            url: "/Plan/GetUserAndCourseAccordingToPlanID",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _planID: planID }),
            cache: false,
            success: function (model) {
                if (model != null) {
                    self.UserPlanRecordsResult.removeAll();
                    self.CoursePlanRecordsResult.removeAll();
                    ko.mapping.fromJS(model.UserPlanRecords, {}, self.UserPlanRecordsResult);
                    ko.mapping.fromJS(model.CoursePlanRecords, {}, self.CoursePlanRecordsResult);
                    $("#hdIsPlanAppliedCheck").val(model.IsPlanAppliedCheck);
                    if (model.IsPlanAppliedCheck == true) {
                        $("#SaveCoursePlanBtn").prop('disabled', true);
                    }
                    else {
                        $("#SaveCoursePlanBtn").prop('disabled', false);                       

                    }
                   
                }
                else {
                }
                $('#loaderDiv').hide();
            }
        });
    }

    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    ko.mapping.fromJS(model, {}, self);
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }

}




