
function UserSubscriptionDetailViewModel() {
    var self = this;

    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
        }
    }

    self.getIndividualAccessTermsOfService = function () {
        $.post("/UserSubscription/getAllAccessTermsOfService", {
        },
        function (_data) {
            $("#divAllAccessTermsOfService").animate({ scrollTop: 0 }, "slow");
            var data = $.parseJSON(_data);
            $("#IndividualAccessAllAccessPass").html("Individual Access");
            $("#divAllAccessTermsOfService").html(data);
        });;
    };
    self.getAllAccessTermsOfService = function () {
        $.post("/UserSubscription/getAllAccessTermsOfService", {
        },
        function (_data) {
            $("#divAllAccessTermsOfService").animate({ scrollTop: 0 }, "slow");
            var data = $.parseJSON(_data);
            $("#IndividualAccessAllAccessPass").html("All-Access Pass");
            $("#divAllAccessTermsOfService").html(data);
        });;
    };

    self.getEnterprisePackage = function () {
        $.post("/UserSubscription/getEnterprisePackage", {
        },
        function (_data) {
            $("#divEnterpriseTermsOfService").animate({ scrollTop: 0 }, "slow");
            var data = $.parseJSON(_data);
            $("#divEnterpriseTermsOfService").html(data);
        });;
    };

    self.OpenOnlineCourse = function () {

        window.location = '/CollegeEducation/?id=oc';

    };
    self.AddEnterprisePackageRegistor = function () {
        window.location = '/EnterprisePackageRegister/';
    };

    self.AddAllAccessPass = function () {
        $.post("/UserSubscription/AddAllAccessPass", {}, 
        function (_data) {
            var data = $.parseJSON(_data);
            if ((data == "Add") || (data == "Re-enrolled"))
                window.location = '/ShoppingCart/';
            else if (data == "AlreadyExists")
                alertify.alert("You have already registered.");
            else if (data == "AlreadyInCart")
                alertify.alert("Already in Shopping Cart.", function (e) {
                    window.location = '/ShoppingCart/';
                });
            
            else
                window.location = '/User/Index?ReturnUrl=%2fUserSubscription/';
        });
    };
}