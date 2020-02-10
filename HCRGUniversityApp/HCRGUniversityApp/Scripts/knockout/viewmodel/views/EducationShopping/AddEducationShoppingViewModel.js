function AddEducationShoppingGridViewModel() {
    var self = this;
    self.Couponcode = ko.observable();
    self.Charges = ko.observable().extend({ numeric: 2 });
    self.EducationShoppingCartResults = ko.observableArray([]);
    self.EducationShoppingCartTempResults = ko.observableArray([]);
    self.DiscountCouponResults = ko.observableArray([]);
    this.Init = function (model) {
        if (model != null) {
            $.each(model.DiscountCouponResults, function (index, value) {
                self.DiscountCouponResults.push(new DiscountCoupon(value));
            });
            $.each(model.EducationShoppingCartTempResults, function (index, value) {
                self.EducationShoppingCartResults.push(new self.EducationShoppingCart(value, self.DiscountCouponResults()));
            });
        }
    };

    ko.mapping.fromJS(model.EducationShoppingCartTempResults, {}, self.EducationShoppingCartTempResults);

    

    self.TotalCharges = ko.computed(function () {
        var tot = 0;
        if (self.EducationShoppingCartTempResults != null) {
            for (var i = 0; i < self.EducationShoppingCartTempResults().length; i++) {
                tot += parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount());
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
                total += ((parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount())) + ((parseFloat(self.EducationShoppingCartTempResults()[i].Amount()) - parseFloat(self.EducationShoppingCartTempResults()[i].DiscountAmount())) * (parseFloat(self.EducationShoppingCartTempResults()[i].TaxPercentage()) / 100)));
            }
        }
        return total.toFixed(2);
    });

    self.EducationShoppingCart = function (value, DiscountCouponArr) {
        this.UserID = ko.observable(value.UserID);
        this.Quantity = ko.observable(value.Quantity);
        this.Price = ko.observable(value.Price).extend({ numeric: 2 });
        this.EducationType = ko.observable(value.EducationType);
        this.TempID = ko.observable(value.TempID);
        this.Date = ko.observable(moment(value.Date).format("MM/DD/YYYY"));
        this.PName = ko.observable(value.PName);
        this.EducationID = ko.observable(value.EducationID);
        this.CoupanID = ko.observable(value.CoupanID);
        this.EducationTypeID = ko.observable(value.EducationTypeID);
        this.DiscountedAmount = ko.observable('0').extend({ numeric: 2 });
        this.DiscountAmount = ko.observable('0').extend({ numeric: 2 });
        this.CredentialID = ko.observable(value.CredentialID);
        this.Total = ko.observable();
        this.EduorProID = ko.observable(value.EduorProID);
        this.CartType = ko.observable(value.CartType);
        this.TaxPercentage = ko.observable(value.TaxPercentage);
        if (value.CoupanID != null) {
            for (var i = 0; i < DiscountCouponArr.length; i++) {
                if (DiscountCouponArr[i].CouponType() == "Percent" && DiscountCouponArr[i].CouponEducationID() == value.EduorProID) {
                    discountamount = ((DiscountCouponArr[i].CouponDiscount()) / 100) * parseFloat(value.Amount);
                    this.DiscountedAmount = ko.observable(discountamount).extend({ numeric: 2 });
                    self.DiscountCouponResults()[i].PName(value.PName);
                    self.DiscountCouponResults()[i].CouponDiscountedAmount(this.DiscountedAmount());
                    break;
                }
                else if (DiscountCouponArr[i].CouponType() == "Fixed" && DiscountCouponArr[i].CouponEducationID() == value.EduorProID) {
                    this.DiscountedAmount = ko.observable(DiscountCouponArr[i].CouponDiscount()).extend({ numeric: 2 });
                    self.DiscountCouponResults()[i].PName(value.PName);
                    self.DiscountCouponResults()[i].CouponDiscountedAmount(this.DiscountedAmount());
                    break;
                }
            }
        }
        else {
            this.DiscountedAmount('0');
        }
        this.Amount = ko.observable(value.Amount).extend({ numeric: 2 });
        this.TotalPrice = ko.observable(value.Amount - this.DiscountedAmount()).extend({ numeric: 2 });
        this.isDisable = ko.observable(false);
    }.bind(this);
    function DiscountCoupon(value) {
        var self = this;
        self.CoupanValid = ko.observable(value.CoupanValid);
        self.CouponCode = ko.observable('"' + value.CouponCode + '"');
        self.CouponDiscount = ko.observable(value.CouponDiscount);
        self.CouponEducationID = ko.observable(value.CouponEducationID);
        self.CouponExpiryDate = ko.observable(moment(value.CouponExpiryDate).format("MM/DD/YYYY"));
        self.CouponID = ko.observable(value.CouponID);
        self.CouponType = ko.observable(value.CouponType);
        self.PName = ko.observable();
        self.CouponDiscountedAmount = ko.observable();
    };
    function NewDiscountCoupon(CoupanID, CouponCode, PName, CouponDiscountedAmount, CouponEducationID) {
        var self = this;
        self.CouponID = ko.observable(CoupanID);
        self.CouponCode = ko.observable('"' + CouponCode + '"');
        self.PName = ko.observable(PName);
        self.CouponDiscountedAmount = ko.observable(CouponDiscountedAmount);
        self.CouponEducationID = ko.observable(CouponEducationID);
    };

    self.AddtocartDetailSuccess = function () {

    };

    self.AddtocartBeforeSubmit = function () {

    };
    this.deleteCartItem = function (itemToDelete) {

        $.ajax({
            url: "/ShoppingCart/DeleteCartItem",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON({ id: itemToDelete.TempID(), cartType: itemToDelete.CartType(), _productID: itemToDelete.EduorProID() }),
            success: function (data) {
                if (data == 'Item cannot be delete , as it is already in process.') {
                    alertify.alert("Item cannot be delete, as it is already in process.", function (e) {
                        return false;
                    });
                }
                else {
                    self.EducationShoppingCartResults.remove(function (item) { return item.TempID == itemToDelete.TempID && item.CartType == itemToDelete.CartType })
                    var total = 0;
                    var tax = 0;
                    var tot = 0;
                    if (self.EducationShoppingCartResults != null) {
                        for (var i = 0; i < self.EducationShoppingCartResults().length; i++) {
                            tot += parseFloat(self.EducationShoppingCartResults()[i].Amount()) - parseFloat(self.EducationShoppingCartResults()[i].DiscountAmount());
                            tax += (parseFloat(self.EducationShoppingCartResults()[i].Amount()) - parseFloat(self.EducationShoppingCartResults()[i].DiscountAmount())) * (parseFloat(self.EducationShoppingCartResults()[i].TaxPercentage()) / 100);
                            total += ((parseFloat(self.EducationShoppingCartResults()[i].Amount()) - parseFloat(self.EducationShoppingCartResults()[i].DiscountAmount())) + ((parseFloat(self.EducationShoppingCartResults()[i].Amount()) - parseFloat(self.EducationShoppingCartResults()[i].DiscountAmount())) * (parseFloat(self.EducationShoppingCartResults()[i].TaxPercentage()) / 100)));
                        }
                    }
                    $("#spanTotalCharges").html('$' + tot.toFixed(2));
                    $("#spanTaxPercentage").html('$' + tax.toFixed(2));
                    $("#spanGrandTotalCharges").html('$' + total.toFixed(2));

                    $("#shoingCartItemCountDiv").text(self.EducationShoppingCartResults().length);
                    self.DiscountCouponResults.remove(function (remove) {

                        return remove.CouponID() == itemToDelete.CoupanID();
                    })
                }
            },
            error: function (data) {
                //alert("Error while deleting a College - " + data);
            }
        });
    };
    self.DeleteDiscountCouponOnCartItemDelete = function (couponId) {
        var count=0;
        for (var i = 0; i < self.DiscountCouponResults().length; i++) {
            if (couponId == self.DiscountCouponResults()[i].CouponID()) {
                count = count + 1;
            }
            if (count == 1) {
                self.DiscountCouponResults.remove(function (remove) {
                    return remove.CouponID() == couponId;
                });
            }
        }
    }

    this.Applycoupon = function (model) {
        if ($('#Couponcode').val() == "" || $('#Couponcode').val() == null) {
            alertify.alert("Please enter a Coupon code");
            return false;
        }
        var AlreadyApplyCoupon = ko.utils.arrayFirst(self.DiscountCouponResults(), function (line) {
            return line.CouponCode().replace(/['"]+/g, '') === self.Couponcode();
        });
        if (!AlreadyApplyCoupon) {
            $.ajax({
                url: "/ShoppingCart/ApplyCoupon",
                data: ko.toJSON({ Couponcode: self.Couponcode() }),
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (newmodel) {
                    var discountamount = 0;
                    var count = 0;
                    if (self.EducationShoppingCartResults != null) {
                        for (var i = 0; i < self.EducationShoppingCartResults().length; i++) {
                            if (newmodel.CouponType == "Fixed" && newmodel.CouponEducationID == self.EducationShoppingCartResults()[i].EducationID()) {
                                self.EducationShoppingCartResults()[i].DiscountedAmount(newmodel.CouponDiscount);
                                self.EducationShoppingCartResults()[i].CoupanID(newmodel.CouponID);
                                self.EducationShoppingCartResults()[i].TotalPrice(self.EducationShoppingCartResults()[i].Amount() - newmodel.CouponDiscount);
                                count = count + 1;
                                var matchingLine = ko.utils.arrayFirst(self.DiscountCouponResults(), function (line) {
                                    return line.CouponEducationID() === newmodel.CouponEducationID;
                                });
                                if (matchingLine) {
                                    self.DiscountCouponResults.remove(function (remove) {
                                        return remove.CouponEducationID() == newmodel.CouponEducationID;
                                    });
                                    self.DiscountCouponResults.push(new NewDiscountCoupon(newmodel.CouponID, newmodel.CouponCode, self.EducationShoppingCartResults()[i].PName(), self.EducationShoppingCartResults()[i].DiscountedAmount(), newmodel.CouponEducationID));
                                } else {
                                    self.DiscountCouponResults.push(new NewDiscountCoupon(newmodel.CouponID, newmodel.CouponCode, self.EducationShoppingCartResults()[i].PName(), self.EducationShoppingCartResults()[i].DiscountedAmount(), newmodel.CouponEducationID));
                                }

                                //$.post("/ShoppingCart/UpdateEducationToShoppingCart", {
                                //    EducationTypeID: self.EducationShoppingCartResults()[i].EducationTypeID(), EducationID: newmodel.CouponEducationID, PName: self.EducationShoppingCartResults()[i].PName(), Date: self.EducationShoppingCartResults()[i].Date(), TempID: self.EducationShoppingCartResults()[i].TempID(), EducationType: self.EducationShoppingCartResults()[i].EducationType(), Price: self.EducationShoppingCartResults()[i].Price(), Quantity: self.EducationShoppingCartResults()[i].Quantity(), Amount: self.EducationShoppingCartResults()[i].Amount(), UserID: self.EducationShoppingCartResults()[i].UserID(), CouponID: newmodel.CouponID, CredentialID: self.EducationShoppingCartResults()[i].CredentialID()
                                //}, function (_data) {
                                //});

                                var _c = i;
                                $.post("/ShoppingCart/UpdateEducationToShoppingCart", {
                                    CouponID: newmodel.CouponID,
                                    EducationTypeID: self.EducationShoppingCartResults()[_c].EducationTypeID(),
                                    UserID: self.EducationShoppingCartResults()[_c].UserID(),
                                    CourseName: self.EducationShoppingCartResults()[_c].PName(),
                                    Date: self.EducationShoppingCartResults()[_c].Date(),
                                    EducationShoppingTempID: self.EducationShoppingCartResults()[_c].TempID(),
                                    EducationType: self.EducationShoppingCartResults()[_c].EducationType(),
                                    Price: self.EducationShoppingCartResults()[_c].Price(),
                                    Quantity: self.EducationShoppingCartResults()[_c].Quantity(),
                                    Amount: self.EducationShoppingCartResults()[_c].Amount(),
                                    EducationID: newmodel.CouponEducationID,
                                    CredentialID: self.EducationShoppingCartResults()[_c].CredentialID()
                                }, function (_data) {
                                });

                            }
                            else if (newmodel == "Coupan not valid") {
                                count = count + 1;
                                alertify.alert("Invalid Coupon Code");
                                break;
                            }
                            else if
                                (newmodel.CouponType == "Percent" && newmodel.CouponEducationID == self.EducationShoppingCartResults()[i].EduorProID()) {
                                discountamount = ((newmodel.CouponDiscount) / 100) * parseFloat(self.EducationShoppingCartResults()[i].Amount());
                                self.EducationShoppingCartResults()[i].DiscountedAmount(discountamount);
                                self.EducationShoppingCartResults()[i].CoupanID(newmodel.CouponID);
                                self.EducationShoppingCartResults()[i].TotalPrice(self.EducationShoppingCartResults()[i].Amount() - discountamount);
                                count = count + 1;
                                var matchingLine = ko.utils.arrayFirst(self.DiscountCouponResults(), function (line) {
                                    return line.CouponEducationID() === newmodel.CouponEducationID;
                                });

                                if (matchingLine) {
                                    self.DiscountCouponResults.remove(function (remove) {
                                        return remove.CouponEducationID() == newmodel.CouponEducationID;
                                    });
                                    self.DiscountCouponResults.push(new NewDiscountCoupon(newmodel.CouponID, newmodel.CouponCode, self.EducationShoppingCartResults()[i].PName(), self.EducationShoppingCartResults()[i].DiscountedAmount(), newmodel.CouponEducationID));
                                } else {
                                    self.DiscountCouponResults.push(new NewDiscountCoupon(newmodel.CouponID, newmodel.CouponCode, self.EducationShoppingCartResults()[i].PName(), self.EducationShoppingCartResults()[i].DiscountedAmount(), newmodel.CouponEducationID));
                                }
                                var _c = i;
                                $.post("/ShoppingCart/UpdateEducationToShoppingCart", {
                                    CouponID: newmodel.CouponID,
                                    EducationTypeID: self.EducationShoppingCartResults()[_c].EducationTypeID(),
                                    UserID: self.EducationShoppingCartResults()[_c].UserID(),
                                    CourseName: self.EducationShoppingCartResults()[_c].PName(),
                                    Date: self.EducationShoppingCartResults()[_c].Date(),
                                    EducationShoppingTempID: self.EducationShoppingCartResults()[_c].TempID(),
                                    EducationType: self.EducationShoppingCartResults()[_c].EducationType(),
                                    Price: self.EducationShoppingCartResults()[_c].Price(),
                                    Quantity: self.EducationShoppingCartResults()[_c].Quantity(),
                                    Amount: self.EducationShoppingCartResults()[_c].Amount(),
                                    EducationID: newmodel.CouponEducationID,
                                    CredentialID: self.EducationShoppingCartResults()[_c].CredentialID()
                                }, function (_data) {
                                });
                            };
                        }
                        //self.EducationShoppingCartResults.remove(function (item) { return item.TempID == itemToDelete.TempID && item.CartType == itemToDelete.CartType })
                        var total = 0;
                        if (self.EducationShoppingCartResults != null) {
                            for (var i = 0; i < self.EducationShoppingCartResults().length; i++) {
                                total += parseFloat(self.EducationShoppingCartResults()[i].Amount()) - parseFloat(self.EducationShoppingCartResults()[i].DiscountedAmount());
                            }
                        }
                        $("#spanGrandTotalCharges").html('$' + total.toFixed(2));


                    }
                    self.Couponcode("");
                    if (count == 0) {
                        alertify.alert("Coupon not valid for this cart");
                    }
                },
                error: function (data) {
                    alertify.alert("Error Occur");
                }
            });
        }
        else {
            alertify.alert("Already Applied this Coupon");
        }
    };

    self.GoToShippingPayment = function () {
        if (self.EducationShoppingCartResults()[0] != null) {
            $.ajax({
                type: "POST",
                url: "/ShoppingCart/CheckProductQuantityStock",
                data: ko.toJSON({ myList: self.EducationShoppingCartResults() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (responseText) {
                    if (responseText == 'ProductOutOfStock')
                        alertify.alert("Product(s) are out of stock.", function (e) {
                           window.location = '/ShoppingCart/';
                        });
                    else if (responseText == 'Item(s) are already being in Process.')
                        alertify.alert("Item(s) are already being in Process.", function (e) {
                            return false;
                        });
                    else
                        window.location = '/ShippingPayment/';
                }
            })
        }
        else {
            alertify.alert("Shopping Cart is empty, please add any Course.", function (e) {
                if (e) {
                    window.location = '/CollegeEducation/';
                }
            });
        };

    };
    this.Checkout = function () {
        var self = this;
        $.ajax({
            type: "POST",
            url: "/ShoppingCart/Checkout",
            data: ko.toJSON({ myList: self.EducationShoppingCartResults() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (responseText) {
                if (responseText == "Added Successfully")
                    window.location = '/MyEducation/';
                else
                    window.location = '/CollegeEducation/';
            }
        })
    };



}
function ShowEducationContentGridViewModel(model) {
    $("#Diveducationcontent").html(model.EducationDetailPageResults.PContent);
}
