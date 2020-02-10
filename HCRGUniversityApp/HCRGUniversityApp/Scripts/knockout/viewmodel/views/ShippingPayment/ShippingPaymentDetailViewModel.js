
function ShippingPaymentDetailViewModel() {

 
    var self = this;

    self.BillingAddressResults = ko.observableArray([]);
    self.ShippingAddressResults = ko.observableArray([]);
    self.ShippingPaymentResults = ko.observableArray([]);

    //self.BillingAddressRecordResults = ko.observableArray([]);
    self.ShippingAddressRecordResults = ko.observableArray([]);
    self.TotalItemCountBA = ko.observable();
    self.TotalItemCountSA = ko.observable();

    self.StateResults = ko.observableArray([]);
    self.ShippingMethodResults = ko.observableArray([]);

    self.EducationShoppingCartTempResults = ko.observableArray([]);
    self.ShippingMethodRecordResults = ko.observableArray([]);

    // ShippingAddress Result 
    self.SAFirstName = ko.observable();
    self.SALastName = ko.observable();
    self.SAAddress = ko.observable();
    self.SAAddress2 = ko.observable();
    self.SACity = ko.observable();
    self.SAStateID = ko.observable();
    self.SAPhone = ko.observable();
    self.SAPostalCode = ko.observable();
    self.SABillingAddressSame = ko.observable();
    self.ShippingAddressID = ko.observable();
    self.SAUserID = ko.observable();

    self.SAselectedItem = ko.observable(0);
    self.SAState = ko.observable();
    self.SAStates = ko.observableArray();


    // BillingAddress Result 
    self.BAFirstName = ko.observable();
    self.BALastName = ko.observable();
    self.BAAddress = ko.observable();
    self.BAAddress2 = ko.observable();
    self.BACity = ko.observable();
    self.BAStateID = ko.observable();
    self.BAPhone = ko.observable();
    self.BAPostalCode = ko.observable();

    self.BillingAddressID = ko.observable();
    self.BAUserID = ko.observable();
    self.IsBAEnable = ko.observable(true);

    self.BAselectedItem = ko.observable(0);
    self.enableRadio = ko.observable();
    self.ShippingMethodAmount = ko.observable(0);
    self.amount = ko.observable(0);

    self.SAStateName = ko.observable(0);

    this.Init = function (model) {
        if (model != null) {

            $(".hideNav").hide();
            hideloderDiv();
            $(".CoursesArticles").hide();

            $(".hideTopBar").hide();
            $(".showTopBar").show();
            $(".footer").hide();
            $(".logoHome").addClass("hidediv");
            //$(".logoHomeHide").removeClass("hidediv");


            ko.mapping.fromJS(model.BillingAddressResults, {}, self);
            ko.mapping.fromJS(model.ShippingAddressResults, {}, self);
            ko.mapping.fromJS(model.ShippingPaymentResults, {}, self);

            if (model.ShippingPaymentResults != null)
                self.enableRadio(model.ShippingPaymentResults.ShippingMethodID.toString());
            else
                self.enableRadio(0);

            if (model.BillingAddressResults != null)
                $("#HFBillingAddressID").val(model.BillingAddressResults.BillingAddressID);
            else
                $("#HFBillingAddressID").val('0');

            if (model.ShippingAddressResults != null) {
                $("#HFShippingAddressID").val(model.ShippingAddressResults.ShippingAddressID);
                $("#divShippingAddressEditable").hide();
                $("#divShippingAddressReadonly").show();
              
                
            }
            else {
                $("#HFShippingAddressID").val('0');
                $("#divShippingAddressEditable").show();
                $("#divShippingAddressEditable").removeClass("hidediv");
                $("#divShippingAddressReadonly").hide();
            }

            self.StateResults(model.StateResults.slice());
            ko.mapping.fromJS(model.ShippingMethodResults, {}, self);
            if (model.ShippingPaymentResults != null) {
                if (model.ShippingPaymentResults.ShippingMethodID != 0)
                    self.ShippingMethodAmount(parseFloat(model.ShippingMethodResults.ShippingMethodAmount.toString()).toFixed(2));
                else
                    self.ShippingMethodAmount("0.00");
                $("#ShippingPaymentID").val(model.ShippingPaymentResults.ShippingPaymentID);
            }
            else {
                self.ShippingMethodAmount("0.00");
                $("#ShippingPaymentID").val("0");
            }

            self.ShippingMethodAmount();
            ko.mapping.fromJS(model.EducationShoppingCartTempResults, {}, self.EducationShoppingCartTempResults);
            ko.mapping.fromJS(model.ShippingMethodRecordResults, {}, self.ShippingMethodRecordResults);

            self.SubTotalCharges = ko.computed(function () {
                var tot = 0;
                if (self.EducationShoppingCartTempResults != null) {
                    for (var i = 0; i < self.EducationShoppingCartTempResults().length; i++) {
                        tot += parseFloat(self.EducationShoppingCartTempResults()[i].Amount() - self.EducationShoppingCartTempResults()[i].DiscountAmount());
                    }
                }
                return tot.toFixed(2);
            });

            self.TaxPercentage = ko.computed(function () {
                var tax = 0;
                if (self.EducationShoppingCartTempResults != null) {
                    for (var i = 0; i < self.EducationShoppingCartTempResults().length; i++) {
                        tax += (parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount())) * (parseFloat(self.EducationShoppingCartTempResults()[i].TaxPercentage()) / 100);
                    }
                }
                return tax.toFixed(2);
            });

            self.GrandTotalCharges = ko.computed(function () {
                var total = 0;
                if (self.EducationShoppingCartTempResults != null) {
                    for (var i = 0; i < self.EducationShoppingCartTempResults().length; i++) {
                        //total += parseFloat(self.EducationShoppingCartTempResults()[i].Amount() - self.EducationShoppingCartTempResults()[i].DiscountAmount());
                        total += ((parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount())) + ((parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount())) * (parseFloat(self.EducationShoppingCartTempResults()[i].TaxPercentage()) / 100)));
                    }
                    total += parseFloat(self.ShippingMethodAmount());
                }
                $("#HFGrandTotalCharges").val(total.toFixed(2));
                self.amount(total.toFixed(2));
                return total.toFixed(2);
            });



            self.TotalItemCountBA(model.TotalItemCountBA);
            self.TotalItemCountSA(model.TotalItemCountSA);

            self.SAStates = ko.observableArray(self.StateResults());
            self.BAStates = ko.observableArray(self.StateResults());

            if (model.BillingAddressResults != null)
                self.BAselectedItem(model.BillingAddressResults.BAStateID);

            if (model.ShippingAddressResults != null) {
                $("#HFShippingAddressID").val(model.ShippingAddressResults.ShippingAddressID);
                self.SAselectedItem(model.ShippingAddressResults.SAStateID);
                self.SAStateName(model.SAStateName);
                if (model.ShippingAddressResults.SABillingAddressSame == false) {
                    $('#chkSABillingAddressSame').prop('checked', false);
                    self.IsBAEnable(true);
                    $('#chkUseShippingAddress').prop('checked', false);
                }
                else {
                    $('#chkSABillingAddressSame').prop('checked', true);
                    self.BAFirstName(self.SAFirstName());
                    self.BALastName(self.SALastName());
                    self.BAAddress(self.SAAddress());
                    self.BAAddress2(self.SAAddress2());
                    self.BACity(self.SACity());
                    self.BAselectedItem(self.SAStateID());
                    self.BAPhone(self.SAPhone());
                    self.BAPostalCode(self.SAPostalCode());
                    self.IsBAEnable(false);
                    $('#chkUseShippingAddress').prop('checked', true);
                }

            }
            else
                $("#ShippingAddressID").val(0);

            $(document).ready(function () {
                $("#AnchorShipping").removeAttr("disabled");
                $("#AnchorBilling").prop("disabled", true);
                $("#AnchorReviewCheckout").prop("disabled", true);
            });

            if (model.ShippingTabDisable == 1) {

                $("#AnchorShipping").prop("disabled", true);
                $("#AnchorBilling").removeAttr("disabled");
                $("#AnchorBilling").click();
                $("#chkUseShippingAddress").prop("disabled", true);
                self.IsBAEnable(true);
                $('#chkUseShippingAddress').prop('checked', false);
            }
            else {
                $("#AnchorShipping").removeAttr("disabled");
                $("#AnchorBilling").prop("disabled", true);
                $("#chkUseShippingAddress").prop("disabled", false);
            }
        }
    };


    $("#AnchorShipping").click(function () {

        if (model.ShippingTabDisable == 1) {
            $("#AnchorShipping").prop("disabled", true);
            $("#AnchorBilling").removeAttr("disabled");
        }
        else {
            $("#AnchorShipping").removeAttr("disabled");
            $("#AnchorBilling").prop("disabled", true);
        }

        if ($("#inputSAFirstName").val() == "") {
            $("#inputSAFirstName").addClass("required-input");
            $("#inputSAFirstName").focus();
            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();
            return false;
        }
        else if ($("#inputSALastName").val() == "") {
            $("#inputSALastName").addClass("required-input");
            $("#inputSALastName").focus();
            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();
            return false;
        }
        else if ($("#inputSAAddress").val() == "") {
            $("#inputSAAddress").addClass("required-input");
            $("#inputSAAddress").focus();
            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();
            return false;
        }
        else if ($("#inputSAPhone").val() == "") {
            $("#inputSAPhone").addClass("required-input");
            $("#inputSAPhone").focus();
            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();
            return false;
        }
        else if ($("#inputSACity").val() == "") {
            $("#inputSACity").addClass("required-input");
            $("#inputSACity").focus();
            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();
            return false;
        }
        else if ($("#inputSAPostalCode").val() == "") {
            $("#inputSAPostalCode").addClass("required-input");
            $("#inputSAPostalCode").focus();

            $("#divShippingAddressEditable").show();
            $("#divShippingAddressReadonly").hide();

            return false;
        }
        else {

            $("#divShippingAddressEditable").hide();
            $("#divShippingAddressReadonly").show();
        }

        self.SAStateName($("#SAStateID").find("option:selected").text());
        $("#AnchorReviewCheckout").prop("disabled", true);
    });
    
    $("#AnchorBilling").click(function () {       
            if (model.ShippingTabDisable == 1) {
                $("#AnchorShipping").prop("disabled", true);
                $("#AnchorBilling").removeAttr("disabled");
        }
        else {
            $("#AnchorShipping").removeAttr("disabled");
        }
        
        $("#AnchorReviewCheckout").prop("disabled", true);
    });



    self.GoToShoppingCart = function () {
        window.location = '/ShoppingCart/';
    };

    self.SetShippingMethod = function (model) {

        var total = 0;
        if (self.EducationShoppingCartTempResults != null) {
            for (var i = 0; i < self.EducationShoppingCartTempResults().length; i++) {
                total += parseFloat(self.EducationShoppingCartTempResults()[i].Amount());
            }
            for (var i = 0; i < self.ShippingMethodRecordResults().length; i++) {
                if (self.ShippingMethodRecordResults()[i].ShippingMethodID() == $("input[name=ShippingMethodID]:checked").val()) {
                    self.ShippingMethodAmount(parseFloat(self.ShippingMethodRecordResults()[i].ShippingMethodAmount()).toFixed(2));
                }
            }
            total += parseFloat(self.ShippingMethodAmount());
            $("#HFGrandTotalCharges").val(self.ShippingMethodAmount());
            self.amount(self.ShippingMethodAmount());
        }

        self.ShippingMethodAmount(self.ShippingMethodAmount());
        //$("#AnchorShippingMethod").removeAttr("disabled");

        return true;
    };
    function CheckEnableDisableAccordion() {
        //disable the accordion link
        if (model.ShippingAddressResults == null) {
            //$("#AnchorShippingMethod").prop("disabled", true);
            $("#AnchorBilling").prop("disabled", true);
            $("#AnchorReviewCheckout").prop("disabled", true);
        }
        else if (model.ShippingMethodResults == null) {
            $("#AnchorBilling").prop("disabled", true);
            $("#AnchorReviewCheckout").prop("disabled", true);
        }
        else if (model.ShippingAddressResults.SABillingAddressSame == true) {
            $("#AnchorReviewCheckout").prop("disabled", true);
        }
        else if (model.BillingAddressResults == null) {
            $("#AnchorReviewCheckout").prop("disabled", true);
        }
        else {
            //$("#AnchorShippingMethod").removeAttr("disabled");
            $("#AnchorBilling").removeAttr("disabled");
            $("#AnchorReviewCheckout").prop("disabled", true);
        }
    }

    function hideloderDiv() {
        $("#loaderDiv").removeClass('loaderbg');
        $("#loading").hide();
    }
    function showloderDiv() {
        $("#loaderDiv").addClass('loaderbg');
        $("#loading").show();
    }

    $("#btnplaceorder").click(function (e) {
        showloderDiv();
        window.location = "/ShippingPayment/Pay";
    });



    


    $(document).ready(function () {

        // FOR SHIPPING ADDRESS

       

        $("#inputSAFirstName").change(function () {
            if ($("#inputSAFirstName").val() != "")
                $("#inputSAFirstName").removeClass("required-input");
            else
                $("#inputSAFirstName").addClass("required-input");
        });

        $("#inputSALastName").change(function () {
            if ($("#inputSALastName").val() != "")
                $("#inputSALastName").removeClass("required-input");
            else
                $("#inputSALastName").addClass("required-input");
        });
        $("#inputSAAddress").change(function () {
            if ($("#inputSAAddress").val() != "")
                $("#inputSAAddress").removeClass("required-input");
            else
                $("#inputSAAddress").addClass("required-input");
        });
        $("#inputSACity").change(function () {
            if ($("#inputSACity").val() != "")
                $("#inputSACity").removeClass("required-input");
            else
                $("#inputSACity").addClass("required-input");
        });
        $("#inputSAPostalCode").change(function () {
            if ($("#inputSAPostalCode").val() != "")
                $("#inputSAPostalCode").removeClass("required-input");
            else
                $("#inputSAPostalCode").addClass("required-input");
        });
        $("#inputSAPhone").change(function () {
            if ($("#inputSAPhone").val() != "")
                $("#inputSAPhone").removeClass("required-input");
            else
                $("#inputSAPhone").addClass("required-input");
        });


        // FOR BILLING ADDRESS

        $("#inputBAFirstName").change(function () {
            if ($("#inputBAFirstName").val() != "")
                $("#inputBAFirstName").removeClass("required-input");
            else
                $("#inputBAFirstName").addClass("required-input");
        });

        $("#inputBALastName").change(function () {
            if ($("#inputBALastName").val() != "")
                $("#inputBALastName").removeClass("required-input");
            else
                $("#inputBALastName").addClass("required-input");
        });
        $("#inputBAAddress").change(function () {
            if ($("#inputBAAddress").val() != "")
                $("#inputBAAddress").removeClass("required-input");
            else
                $("#inputBAAddress").addClass("required-input");
        });
        $("#inputBACity").change(function () {
            if ($("#inputBACity").val() != "")
                $("#inputBACity").removeClass("required-input");
            else
                $("#inputBACity").addClass("required-input");
        });
        $("#inputBAPostalCode").change(function () {
            if ($("#inputBAPostalCode").val() != "")
                $("#inputBAPostalCode").removeClass("required-input");
            else
                $("#inputBAPostalCode").addClass("required-input");
        });
        $("#inputBAPhone").change(function () {
            if ($("#inputBAPhone").val() != "")
                $("#inputBAPhone").removeClass("required-input");
            else
                $("#inputBAPhone").addClass("required-input");
        });
    });
    self.SaveShiipingAddress = function (self) {
        if ($("#inputSAFirstName").val() == "") {
            $("#inputSAFirstName").addClass("required-input");
            $("#inputSAFirstName").focus();
            return false;
        }
        else if ($("#inputSALastName").val() == "") {
            $("#inputSALastName").addClass("required-input");
            $("#inputSALastName").focus();
            return false;
        }
        else if ($("#inputSAAddress").val() == "") {
            $("#inputSAAddress").addClass("required-input");
            $("#inputSAAddress").focus();
            return false;
        }
        else if ($("#inputSAPhone").val() == "") {
            $("#inputSAPhone").addClass("required-input");
            $("#inputSAPhone").focus();
            return false;
        }
        else if ($("#inputSACity").val() == "") {
            $("#inputSACity").addClass("required-input");
            $("#inputSACity").focus();
            return false;
        }
        else if ($("#inputSAPostalCode").val() == "") {
            $("#inputSAPostalCode").addClass("required-input");
            $("#inputSAPostalCode").focus();
            return false;
        }
        
        else {

            var viewModelShippingAddressDetails = {
                SAFirstName: self.SAFirstName(),
                SALastName: self.SALastName(),
                SAAddress: self.SAAddress(),
                SAAddress2: self.SAAddress2(),
                SACity: self.SACity(),
                SAStateID: $("#SAStateID option:selected").val(),
                SAPostalCode: self.SAPostalCode(),
                SAPhone: self.SAPhone(),
                ShippingAddressID: $("#HFShippingAddressID").val(),// model.ShippingAddressResults == null ? 0 : model.ShippingAddressResults.ShippingAddressID,// == null ? 0 : $("#HFShippingAddressID").val(),
                SABillingAddressSame: $('#chkSABillingAddressSame').is(':checked') ? true : false,
                ShippingPaymentID: model.ShippingPaymentResults.ShippingPaymentID,// == null ? 0 : $("#HFShippingPaymentID").val(),
                IsSaveShippingAddress: $('#chkIsSaveShippingAddress').is(':checked') ? true : false
            }
            var plainJs = ko.toJS(viewModelShippingAddressDetails);

            $.ajax({
                url: "/ShippingPayment/SaveShiipingAddress",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (_data) {

                    if (_data != null) {
                        if (_data != 0) {
                            $("#HFShippingAddressID").val(_data[1]);
                            $("#HFShippingPaymentID").val(_data[2]);
                        }
                        $("#AnchorBilling").prop("disabled", false);
                        $("#AnchorBilling").click();
                        $("#AnchorReviewCheckout").prop("disabled", true);

                        // If SABillingAddressSame is checked then Billing address need to filled 
                        if ($('#chkSABillingAddressSame').is(':checked')) {
                            $("#AnchorBilling").prop("disabled", false);
                            $("#AnchorReviewCheckout").prop("disabled", true);
                            self.BAFirstName(self.SAFirstName());
                            self.BALastName(self.SALastName());
                            self.BAAddress(self.SAAddress());
                            self.BAAddress2(self.SAAddress2());
                            self.BACity(self.SACity());
                            //self.BAselectedItem(self.SAStateID());
                            self.BAselectedItem($("#SAStateID").val());
                            self.BAPhone(self.SAPhone());
                            self.BAPostalCode(self.SAPostalCode());
                            self.IsBAEnable(false);
                            $('#chkUseShippingAddress').prop('checked', true);
                        }
                        else {
                            //$("#AnchorShippingMethod").prop("disabled", false);
                            $("#AnchorBilling").prop("disabled", false);
                            $("#AnchorReviewCheckout").prop("disabled", true);
                            $('#chkUseShippingAddress').prop('checked', false);
                            ko.mapping.fromJS(model.BillingAddressResults, {}, self);
                            self.IsBAEnable(true);

                        }

                    }
                }
            });
        }
    } 
    self.SaveShiipingMethod = function (self) { 
        if ($("input[name=ShippingMethodID]:checked").length > 0) {
            $.ajax({
                url: "/ShippingPayment/SaveShiipingMethod",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON({ _shippingPaymentID: $("#HFShippingPaymentID").val(), _shippingMethodID: $("input[name=ShippingMethodID]:checked").val() }),
                success: function (_data) {

                    if (_data != null) {
                        if (_data != 0) {
                            if (_data[0] == "Add")
                                $("#HFShippingMethodID").val(_data[1]);
                            $("#HFShippingPaymentID").val(_data[2]);

                            self.enableRadio($("#HFShippingMethodID").val());
                            // If SABillingAddressSame is checked then Billing address need to filled 
                            if ($('#chkSABillingAddressSame').is(':checked')) {
                                $("#AnchorBilling").prop("disabled", false);
                                $("#AnchorReviewCheckout").prop("disabled", true);
                                self.BAFirstName(self.SAFirstName());
                                self.BALastName(self.SALastName());
                                self.BAAddress(self.SAAddress());
                                self.BAAddress2(self.SAAddress2());
                                self.BACity(self.SACity());
                                self.BAselectedItem(self.SAStateID());
                                self.BAPhone(self.SAPhone());
                                self.BAPostalCode(self.SAPostalCode());
                                self.IsBAEnable(false);
                                $('#chkUseShippingAddress').prop('checked', true);
                            }
                            else {
                                //$("#AnchorShippingMethod").prop("disabled", false);
                                $("#AnchorBilling").prop("disabled", false);
                                $("#AnchorReviewCheckout").prop("disabled", true);
                                $('#chkUseShippingAddress').prop('checked', false);
                                ko.mapping.fromJS(model.BillingAddressResults, {}, self);
                                self.IsBAEnable(true); 
                            }
                            $("#AnchorBilling").click();
                        }

                    }
                }
            });
        }
        else {
            alertify.alert("Please Select any Shipping Method.");
            return false;
        }
    }; 
    self.UseShippingAddress = function (self) {
        if ($("#chkUseShippingAddress").is(":checked")) {
            $("#AnchorReviewCheckout").prop("disabled", true);
            self.BAFirstName(self.SAFirstName());
            self.BALastName(self.SALastName());
            self.BAAddress(self.SAAddress());
            self.BAAddress2(self.SAAddress2());
            self.BACity(self.SACity());
            self.BAselectedItem(self.SAStateID());
            self.BAPhone(self.SAPhone());
            self.BAPostalCode(self.SAPostalCode());
            self.IsBAEnable(false);
            $("#chkSABillingAddressSame").prop('checked', true);
        }
        else {
            $("#AnchorReviewCheckout").prop("disabled", true);
            ko.mapping.fromJS(model.BillingAddressResults, {}, self);
            self.IsBAEnable(true);
            $("#chkSABillingAddressSame").prop('checked', false);
        } 
    }; 
    self.BillingAddressSame = function (self) {

        if ($("#chkSABillingAddressSame").is(":checked")) {
            $("#AnchorReviewCheckout").prop("disabled", true);
            self.BAFirstName(self.SAFirstName());
            self.BALastName(self.SALastName());
            self.BAAddress(self.SAAddress());
            self.BAAddress2(self.SAAddress2());
            self.BACity(self.SACity());
            self.BAselectedItem(self.SAStateID());
            self.BAPhone(self.SAPhone());
            self.BAPostalCode(self.SAPostalCode());
            self.IsBAEnable(false);
            $("#chkUseShippingAddress").prop('checked', true);
        }
        else {
            $("#AnchorReviewCheckout").prop("disabled", true);
            self.BAFirstName('');
            self.BALastName('');
            self.BAAddress('');
            self.BAAddress2('');
            self.BACity('');
            self.BAselectedItem(66);
            self.BAPhone('');
            self.BAPostalCode('');
            self.IsBAEnable(true);
            $("#chkUseShippingAddress").prop('checked', false);
        }
    }; 
    self.SaveBillingAddress = function (self) {

        if ($("#inputBAFirstName").val() == "") {
            $("#inputBAFirstName").addClass("required-input");
            $("#inputBAFirstName").focus();
            return false;
        }
        else if ($("#inputBALastName").val() == "") {
            $("#inputBALastName").addClass("required-input");
            $("#inputBALastName").focus();
            return false;
        }
        else if ($("#inputBAAddress").val() == "") {
            $("#inputBAAddress").addClass("required-input");
            $("#inputBAAddress").focus();
            return false;
        }
        else if ($("#inputBAPhone").val() == "") {
            $("#inputBAPhone").addClass("required-input");
            $("#inputBAPhone").focus();
            return false;
        }
        else if ($("#inputBACity").val() == "") {
            $("#inputBACity").addClass("required-input");
            $("#inputBACity").focus();
            return false;
        }
        else if ($("#inputBAPostalCode").val() == "") {
            $("#inputBAPostalCode").addClass("required-input");
            $("#inputBAPostalCode").focus();
            return false;
        } 
        else {
            var viewModelBillingAddressDetails = {
                BAFirstName: self.BAFirstName(),
                BALastName: self.BALastName(),
                BAAddress: self.BAAddress(),
                BAAddress2: self.BAAddress2(),
                BACity: self.BACity(),
                BAStateID: $("#BAStateID option:selected").val(),
                BAPostalCode: self.BAPostalCode(),
                BAPhone: self.BAPhone(),
                BillingAddressID: $("#HFBillingAddressID").val(),//model.ShippingAddressResults.IsSaveShippingAddress == true ? 0 : $("#HFBillingAddressID").val(),
                ShippingPaymentID: model.ShippingPaymentResults.ShippingPaymentID,// == null ? 0 : $("#HFShippingPaymentID").val(),
                IsSABillingAddressSame: $('#chkSABillingAddressSame').is(':checked') ? true : false,
                IsSaveBillingAddress: $('#chkIsSaveBillingAddress').is(':checked') ? true : false
            } 
            var plainJs = ko.toJS(viewModelBillingAddressDetails);
            $.ajax({
                url: "/ShippingPayment/SaveBillingAddress",
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify(plainJs),
                success: function (_data) {

                    if (_data != null) {
                        if (_data != 0) {
                            $("#HFBillingAddressID").val(_data[1]);
                            $("#HFShippingPaymentID").val(_data[2]);
                        }

                        //if (model.ShippingTabDisable == 1) {
                        //    $("#AnchorBilling").prop("disabled", false);
                        //}
                        $("#AnchorReviewCheckout").prop("disabled", false);
                        $("#AnchorReviewCheckout").click();
                    }
                }
            });
        }
    }; 
    self.ShippingAddressEditable = function () {

        $("#divShippingAddressEditable").removeClass("hidediv");
        $("#divShippingAddressEditable").show();
        $("#divShippingAddressReadonly").hide();

    }; 
    self.CallClientSidePlaceOrderEvent = function () {
        alert('Call Client Side Place Order Event');
        return false;
    }; 
}

