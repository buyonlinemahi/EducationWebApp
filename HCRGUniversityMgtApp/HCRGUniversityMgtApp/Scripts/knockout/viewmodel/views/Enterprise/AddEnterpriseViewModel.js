function AddEnterpriseViewModel() {
    var self = this;
    self.EnterpriseID = ko.observable();
    self.EnterpriseClientName = ko.observable();
    self.EnterpriseAddress = ko.observable();
    self.EnterpriseCity = ko.observable();
    self.EnterpriseStateID = ko.observable();
    self.EnterpriseZip = ko.observable();
    self.EnterprisePhone = ko.observable();
    self.EnterpriseEmail = ko.observable();
    self.EnterpriseCourseStartPrice = ko.observable();
    self.EnterpriseCourseEndPrice = ko.observable();
    self.EnterpriseProgram = ko.observable(false);
    self.EnterpriseEndDate = ko.observable();
    self.EnterpriseNumberUser = ko.observable();



    self.EnterpriseStateselectedItem = ko.observable(0);
    self.EnterpriseStates = ko.observable();
    self.EnterpriseStates = ko.observableArray();
    self.StateResults = ko.observableArray([]);


    this.Init = function (model) {
        if (model != null) {

            self.StateResults(model.StateResults.slice());

            self.EnterpriseStates = ko.observableArray(self.StateResults());
            ko.mapping.fromJS(model.Enterprise, {}, self);
            if (model.Enterprise!=null)
            self.EnterpriseEndDate(moment(model.Enterprise.EnterpriseEndDate).format("MM/DD/YYYY"));
        }
    }


    self.AddEnterpriseFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEnterprise").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#spnaEnterpriseStartPrice").html("");
        $("#spnaEnterpriseEndPrice").html("");

        var _startPrice = $('#inputEnterpriseCourseStartPrice').val();
        if ((_startPrice != "") && (_startPrice != "0")) {
            var _endPrice = $('#inputEnterpriseCourseEndPrice').val();
            if ((_endPrice != "") && (_endPrice != "0")) {
                if (parseInt(_startPrice) > parseInt(_endPrice)) {
                    $('#inputEnterpriseCourseStartPrice').val('');
                    $("#spnaEnterpriseStartPrice").html("Enterprise Start Price is less then End Price amount.");
                    $('#inputEnterpriseCourseStartPrice').focus();
                    return false;
                }
            }
        }

        var _endPrice = $('#inputEnterpriseCourseEndPrice').val();
        if ((_endPrice != "") && (_endPrice != "0")) {
            var _startPrice = $('#inputEnterpriseCourseStartPrice').val();
            if ((_startPrice != "") && (_startPrice != "0")) {
                if (parseInt(_endPrice) < parseInt(_startPrice)) {
                    $('#inputEnterpriseCourseEndPrice').val('');
                    $("#spnaEnterpriseEndPrice").html("Enterprise End Price is less then Start Price amount.");
                    $('#inputEnterpriseCourseEndPrice').focus();
                    return false;
                }
            }
        }

        return true;
    }
    self.AddEnterpriseDetailSuccess = function (model) {
        $("#loaderDiv").show();
        if (model == "Add Sucessfully.") {
            alertify.alert("Added Sucessfully.", function (e) {
                $('#loaderDiv').hide();
            });
        }
        else {
            alertify.alert("Updated Sucessfully.", function (e) {
                $('#loaderDiv').hide();
            });
        }
    }

    $("#inputEnterpriseCourseStartPrice").change(function () {
        $("#spnaEnterpriseStartPrice").html('');
        var _startPrice = $('#inputEnterpriseCourseStartPrice').val();
        if ((_startPrice != "") && (_startPrice != "0")) {
            var _endPrice = $('#inputEnterpriseCourseEndPrice').val();
            if ((_endPrice != "") && (_endPrice != "0")) {
                if (parseInt(_startPrice) > parseInt(_endPrice)) {
                    $('#inputEnterpriseCourseStartPrice').val('');
                    $("#spnaEnterpriseStartPrice").html("Enterprise Start Price is less then End Price amount.");
                    $('#inputEnterpriseCourseStartPrice').focus();
                    return false;
                }
            }
        }
    });

    $("#inputEnterpriseCourseEndPrice").change(function () {
        $("#spnaEnterpriseEndPrice").html('');
        var _endPrice = $('#inputEnterpriseCourseEndPrice').val();
        if ((_endPrice != "") && (_endPrice != "0")) {
            var _startPrice = $('#inputEnterpriseCourseStartPrice').val();
            if ((_startPrice != "") && (_startPrice != "0")) {
                if (parseInt(_endPrice) < parseInt(_startPrice)) {
                    $('#inputEnterpriseCourseEndPrice').val('');
                    $("#spnaEnterpriseEndPrice").html("Enterprise End Price is more then Start Price amount.");
                    $('#inputEnterpriseCourseEndPrice').focus();
                    return false;
                }

            }
        }
    });
};