function FAQViewModel() {
    var self = this;
    self.FAQID = ko.observable();
    self.FAQues = ko.observable();
    self.FAQAns = ko.observable();
    self.FAQCategoryTitle = ko.observable();
    self.FaqCategoryName = ko.observable();
    self.FAQCatID = ko.observable();    
    self.FAQResults = ko.observableArray([]);
    self.FAQCategoryResults = ko.observableArray([]);
    self.selectedItem = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.OrganizationName = ko.observable();
    self.OrganizationID = ko.observable();

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]);
    $('#loaderDiv').hide();
    self.IsHCRGAdmin = ko.observable();

    this.Init = function (model) {        
        if (model != null) {
            if (!self.pendingRequest()) {
                if (model.FAQAllResults.length != 0) {
                    $("#divAddFaq").hide();

                    if (model.IsHCRGAdmin == false) {
                        $("#divAddFaq").show();
                    }
                }
                
                $("#loaderDiv").show();                
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                ko.mapping.fromJS(model.FAQAllResults, {}, self.FAQResults);
                ko.mapping.fromJS(model.FAQCategoryResults, {}, self.FAQCategoryResults);
                self.CurrentPage(self.maxId());
                //self.maxId(self.maxId() + model.PagedFAQResults.PagedRecords);
                $('#loaderDiv').hide();
            }
            getAllOrganization();
        };
    }

    function getAllOrganization() {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                self.AllOrganizations.removeAll();
                self.AllOrganizations(data.slice());
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }

    self.SearchFAQ = function () {
        if ($("#DrpOrgID").val() == '') {
            alertify.alert("Please select atleast one Organization");
            return false;
        }
        $.post("/FAQ/GetFaqByOrganizationID/", { OrgID: self.SearchSelectedOrganization() }, function (data) {            
            self.FAQResults.removeAll();
            ko.mapping.fromJS(data, {}, self.FAQResults);
        });
    };

    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1)) {
                    getAllFAQ();
                }
            }
            else {
                self.pendingRequest(true);
            }
        }
    }

    function getAllFAQ() {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/FAQ/GetAllPagedFAQs", { skip: self.maxId() }, function (data) {
                self.FAQResults.removeAll();
                var data = $.parseJSON(data);
                for (var i = 0; i < data.FAQDetails.length; i++) {
                    self.FAQResults.push(new InsertFAQLineItem(data.FAQDetails[i].FAQID, data.FAQDetails[i].FAQCategoryTitle, data.FAQDetails[i].FAQCatID, data.FAQDetails[i].FAQAns, data.FAQDetails[i].FAQues));
                }
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }
    }
    self.FAQInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddFAQ").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.FAQCategoryInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddFAQCategory").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddFAQDetailSuccess = function (model) {
        if (model != null) {
            $("#divAddFaq").hide();
            var faqCategory = $('#FaqCategory :selected').text();
            var newFAQ = $.parseJSON(model);
            var viewModel = self.FAQResults;
            if (!newFAQ.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].FAQID() == newFAQ.FAQID) {
                        self.FAQResults.splice(i, 1);
                        self.FAQResults.splice(i, 0, new InsertFAQLineItem(newFAQ.FAQID, faqCategory, newFAQ.FAQCatID, newFAQ.FAQAns, newFAQ.FAQues, newFAQ.OrganizationName, newFAQ.OrganizationID));
                        alertify.success("FAQ Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.FAQResults.splice(0, 0, new InsertFAQLineItem(newFAQ.FAQID, faqCategory, newFAQ.FAQCatID, newFAQ.FAQAns, newFAQ.FAQues,newFAQ.OrganizationName, newFAQ.OrganizationID));
                alertify.success("FAQ Insert Successfully");
                $("#main").scrollTop(0);
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetFAQcontrol();
        }
    };
    self.AddFAQCategoryDetailSuccess = function (model) {
        if (model != null) {
            var newFAQCategory = $.parseJSON(model);
            var viewModel = self.FAQCategoryResults;
            if (!newFAQCategory.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].FAQCatID() == newFAQCategory.FAQCatID) {
                        self.FAQCategoryResults.splice(i, 1);
                        self.FAQCategoryResults.splice(i, 0, new InsertFAQCategoryLineItem(newFAQCategory.FAQCatID, newFAQCategory.FAQCategoryTitle));
                        alertify.success("FAQ Category Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.FAQCategoryResults.push(new InsertFAQCategoryLineItem(newFAQCategory.FAQCatID, newFAQCategory.FAQCategoryTitle));
                $('html, body').scrollTop($(document).height());
                alertify.success("FAQ Category Insert Successfully");
            }
            resetFAQCategorycontrol();
        }
    };
    function resetFAQCategorycontrol() {
        self.FAQCatID("");
        self.FAQCategoryTitle("");
    }
    function resetFAQcontrol() {
        self.FAQID("");
        self.FAQues("");
        self.FAQAns("");
        self.FaqCategoryName("");
        self.FAQCatID("");
        self.selectedItem("");
        self.OrganizationName("");
        self.OrganizationID("");
    }
    self.edit = function (config) {
        self.FAQCatID(config.FAQCatID());
        self.FAQCategoryTitle(config.FAQCategoryTitle());
        $(window).scrollTop(0);
    }
    self.editFAQ = function (config) {
        $("#divAddFaq").show();
        self.FAQID(config.FAQID());
        self.FAQues(config.FAQues());
        self.FAQAns(config.FAQAns());
        //self.FAQCategoryTitle(config.FAQCategoryTitle());
        self.FaqCategoryName(config.FaqCategoryName());
        self.FAQCatID(config.FAQCatID());
        self.OrganizationName(config.OrganizationName());
        self.OrganizationID(config.OrganizationID());
        self.selectedItem(config.FAQCatID());
        $(window).scrollTop(0);
    }
    function InsertFAQCategoryLineItem(_FAQCatID, _FAQCategoryTitle) {
        var self = this;
        self.FAQCatID = ko.observable(_FAQCatID);
        self.FAQCategoryTitle = ko.observable(_FAQCategoryTitle);
    }
    function InsertFAQLineItem(_FAQID, _FAQCategoryTitle, _FAQCatID, _FAQAns, _FAQues, _OrganizationName, _OrganizationID) {
        var self = this;
        self.FAQID = ko.observable(_FAQID);
        self.FAQues = ko.observable(_FAQues);
        self.FAQAns = ko.observable(_FAQAns);
        self.FaqCategoryName = ko.observable(_FAQCategoryTitle);
        self.FAQCatID = ko.observable(_FAQCatID);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }
    this.DeleteFAQCategory = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: "/FAQ/DeleteFAQCategory",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ faqcategory: itemToDelete }),
                    success: function (data) {
                        self.FAQCategoryResults.remove(function (item) { return item.FAQCatID == itemToDelete.FAQCatID })
                        self.FAQResults.removeAll();
                        $.post("/FAQ/ShowAllFAQ", function (data) {
                            ko.mapping.fromJS(data.FAQResults, {}, self.FAQResults);
                        });
                        alertify.success(data);
                    },
                    error: function (data) {
                        alert("Error while deleting a item - " + data);
                    }
                });
            }
        });
    };
    this.DeleteFAQ = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: "/FAQ/DeleteFAQ",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ faq: itemToDelete }),
                    success: function (data) {
                        self.FAQResults.remove(function (item) { return item.FAQID == itemToDelete.FAQID })
                        alertify.success(data);
                        self.TotolCount(self.TotolCount() - 1)
                        if (self.CurrentPage() < self.TotolCount() && self.TotolCount() > self.FAQResults().length) {
                            $.post("/FAQ/GetAllPagedFAQs", { skip: self.maxId() - 1, take: 1 }, function (_data) {
                                var data = $.parseJSON(_data);
                                $("#loaderDiv").show();
                                var data = $.parseJSON(_data);
                                for (var i = 0; i < data.FAQDetails.length; i++) {
                                    self.FAQResults.push(new InsertFAQLineItem(data.FAQDetails[i].FAQID, data.FAQDetails[i].FaqCategoryName, data.FAQDetails[i].FAQCatID, data.FAQDetails[i].FAQAns, data.FAQDetails[i].FAQues));
                                }
                                self.TotolCount(data.TotalCount);
                                $('#loaderDiv').hide();
                            });
                        }
                    },
                    error: function (data) {
                        alert("Error while deleting a item - " + data);
                    }
                });
            }
        });
    };
}
