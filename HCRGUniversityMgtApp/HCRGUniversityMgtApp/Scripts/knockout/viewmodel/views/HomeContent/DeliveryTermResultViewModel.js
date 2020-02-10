function DeliveryTermViewModel() {
    var self = this;

    self.DeliveryTermID = ko.observable();
    self.DeliveryTermContents = ko.observable();
    self.DescriptionShort = ko.observable();
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);

 
    self.DeliveryTermData = ko.observableArray([]);

    this.Init = function (model) {
        if (model != null) {
            if (!self.pendingRequest()) {
                $("#loaderDiv").show();
                $.each(model.DeliveryTermResults, function (index, value) {
                    self.DeliveryTermData.push(new DeliveryConditon(value));
                });

                self.TotolCount(model.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + model.PagedRecords);
                $('#loaderDiv').hide();
            }
        }
    };

    self.DeliveryTermsDetailSuccess = function (model) {
        if (model != null) {
            var newDeliveryCondtion = $.parseJSON(model);
            var viewModel = self.DeliveryTermData;
            if (!newDeliveryCondtion.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].DeliveryTermID() == newDeliveryCondtion.DeliveryTermID) {
                        self.DeliveryTermData.splice(i, 1);
                        self.DeliveryTermData.splice(i, 0, new InsertDeliveryConditionLineItem(newDeliveryCondtion.DeliveryTermID, newDeliveryCondtion.DeliveryTermContents, newDeliveryCondtion.DescriptionShort));
                        alertify.success(" Delivery Term Conditon Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.DeliveryTermData.splice(i, 0, new InsertDeliveryConditionLineItem(newDeliveryCondtion.DeliveryTermID, newDeliveryCondtion.DeliveryTermContents, newDeliveryCondtion.DescriptionShort));
                $("#main").scrollTop(0);
                alertify.success(" Delivery Term Condition Insert Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
            }
            resetcontrol();
        }
    };


    self.DeliveryTermInfoFormBeforeSubmit = function (arr, $form, options) {

        var string = arr[1].value;
        var Rplacedstring = string.replace('&amp;', "");
        Rplacedstring = Rplacedstring.replace('nbsp;', "");
        if (Rplacedstring == " " || Rplacedstring == "") {
            alertify.alert("Content cannot be empty!");
            return false;
        }
        if ($("#frmDeliveryTerm").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorDelivery").val()).html());//encoding
        return true;
    };

    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });


    function InsertDeliveryConditionLineItem(_DeliveryTermID, _DeliveryTermContents, _DescriptionShort) {
        var self = this;
        self.DeliveryTermID = ko.observable(_DeliveryTermID);
        self.DeliveryTermContents = ko.observable(_DeliveryTermContents);
        self.DescriptionShort = ko.observable(_DescriptionShort);
    }
    
    self.EditDeliveryConditionLineItems = function (config) {
        self.DeliveryTermID(config.DeliveryTermID());
        self.DeliveryTermContents(config.DeliveryTermContents());
        var editor1 = document.getElementById("EditorDelivery").editor;
        editor1.SetText(($("<div>").html(config.DeliveryTermContents()).text()));
        $(window).scrollTop(0);
        $("#EditorModalPopUp").click();
    }
    function DeliveryConditon(value) {
        var self = this;
        self.DeliveryTermID = ko.observable(value.DeliveryTermID);
        self.DeliveryTermContents = ko.observable(value.DeliveryTermContents);
        self.DescriptionShort = ko.observable(value.DescriptionShort);
    };


    function resetcontrol() {
        self.DeliveryTermID("");
        self.DeliveryTermContents("");
        var editor1 = document.getElementById("EditorDelivery").editor;
        editor1.SetText("");
    }


    $(".btnClose").click(function () {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorDelivery").val()).html());//encoding
}