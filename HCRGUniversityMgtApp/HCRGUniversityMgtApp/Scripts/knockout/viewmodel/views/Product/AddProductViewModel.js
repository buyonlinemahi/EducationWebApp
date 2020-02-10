function ProductGridViewModel(model) {
    var self = this;

    self.ProductTotalQuantity = ko.observable(0);
    self.ProductCurrentBalance = ko.observable(0);
    self.ProductPaidCount = ko.observable(0);

    self.ProductTypeDll = ko.observableArray([{ TypeName: "Download", Type: 'Download' }, { TypeName: "Hard Copy", Type: 'Hard Copy' }]);

    if (model != null) {
        ko.mapping.fromJS(model, {}, self);

        if (model.ProductCurrentBalance == -2) {
            $("#inputProductCurrentBalance").val(0);
            $("#hfProductCurrentBalance").val(0);
            self.ProductCurrentBalance(0);
        }
        if (model.ProductTotalQuantity == null) {
            self.ProductTotalQuantity(0);
            self.ProductCurrentBalance(0);
        }
        if(model.ProductType == "Hard Copy")
        {
            $('#inputProductTotalQuantity').prop('readonly', false);
            $("#inputProductTotalQuantity").attr({
                "min": 1          // values (or variables) here
            });

            $("#pfile").hide();
            $("#ProductFile_File").removeClass('required_bdr');
        }

    }
  
   

    $("#hfProductPaidCount").val(self.ProductPaidCount());
    $("#hfProductTotalQuantity").val(self.ProductTotalQuantity());

    self.AddProductSuccess = function (model) {
        $("#ProductFile_File").removeClass('required_bdr');
        $("#ProductImage").removeClass('required_bdr');
        if (model.Message == "Successfully Saved") {
            reset();
        }
        else {
            ko.mapping.fromJS(model, {}, self);
            if (model.ProductTotalQuantity == -2) {
                $("#hfProductTotalQuantity").val(0);
                $("#hfProductCurrentBalance").val(0);
            }
        }
        $('#ProductImageName').val(model.ProductImage);
        $('[data-toggle="popover"]').popover("destroy");
        $('[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover',
            html: true,
            content: '<div class="media"><img src="/Storage/ProductImage/' + $('#ProductImageName').val() + '" style="max-width:245px; max-height:250px;" /></div>'
        });
        alertify.success(model.Message);
    };
    function reset() {
        $('#frmProduct')[0].reset();
    }
    function checkFileExt() {

    }
    self.ProductFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmProduct").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($('#hdProductID').val() == 0) {

            if ($('#ProductImage').val() == '') {
                $("#ProductImage").addClass('required_bdr');
                return false;
            }
            if (($('#ProductFile_File').val() == '') &&  ($('#ProductType').val() == 'Download'))
            {
                $("#ProductFile_File").addClass('required_bdr');
                return false;
            }
            var ext = $('#ProductImage').val().split('.').pop().toLowerCase();
            if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                alertify.alert('Only Image file is allowed in Product Image');
                return false;
            }

            if ($('#ProductType').val() == 'Download') {
                var ext = $('#ProductFile_File').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['pdf', 'xls', 'xlsx', 'doc', 'docx', 'ppt']) == -1) {
                    alertify.alert('Only .pdf, .xls, .xlsx, .doc, .docx, .ppt  Extension file is allowed in Product File');
                    return false;
                }
            }
        }
        else {

            if (($('#ProductFile_File').val() == '') && ($('#ProductType').val() == 'Download')) {
                if((arr[5].value=="") || (arr[5].value==null))
                {
                    $("#ProductFile_File").addClass('required_bdr');
                    return false;
                }
            }
            $("#ProductFile_File").removeClass('required_bdr');
            $("#ProductImage").removeClass('required_bdr');
            if ($('#ProductImage').val() != '') {
                var ext = $('#ProductImage').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['gif', 'png', 'jpg', 'jpeg']) == -1) {
                    alertify.alert('Only Image file is allowed in Product Image');
                    return false;
                }
            }
            if ($('#ProductFile_File').val() != '') {
                var ext = $('#ProductFile_File').val().split('.').pop().toLowerCase();
                if ($.inArray(ext, ['pdf', 'xls', 'xlsx', 'doc', 'docx', 'ppt']) == -1) {
                    alertify.alert('Only .pdf, .xls, .xlsx, .doc, .docx, .ppt  Extension file is allowed in Product File');
                    return false;
                }
            }
        }
        return true;
    };
    self.CheckProductTotalQuantity = function () {
        if (parseInt($("#inputProductTotalQuantity").val()) >= parseInt($("#hfProductTotalQuantity").val())) {
            var ProductCurrentBalance = $("#hfProductCurrentBalance").val();
            $("#inputProductCurrentBalance").val('')
            $("#inputProductCurrentBalance").val(parseInt(ProductCurrentBalance) + parseInt(parseInt($("#inputProductTotalQuantity").val()) - parseInt($("#hfProductTotalQuantity").val())));
        }
        else {
            var ProductTotalQuantity = $("#hfProductTotalQuantity").val();
            var ProductCurrentBalance = $("#hfProductCurrentBalance").val();

            var ProductPaidCount = $("#hfProductPaidCount").val();
            if (ProductTotalQuantity != ProductCurrentBalance) {
                if (parseInt($("#inputProductTotalQuantity").val()) >= ProductPaidCount) {
                    var balnace = ProductTotalQuantity - ProductCurrentBalance;
                    if (parseInt(balnace) > 0) {
                        $("#inputProductCurrentBalance").val('');
                        $("#inputProductCurrentBalance").val(parseInt($("#inputProductTotalQuantity").val()) - (parseInt(balnace)));
                    }
                    else
                        $("#inputProductCurrentBalance").val('0');
                }
                else {
                    alertify.alert(ProductPaidCount + '  Product already has been sold.', function (e) {
                        $("#inputProductTotalQuantity").val(ProductPaidCount);
                        $("#inputProductCurrentBalance").val('0');
                    });
                }
            }
            else {
                $("#inputProductCurrentBalance").val('')
                $("#inputProductCurrentBalance").val(parseInt(parseInt($("#inputProductTotalQuantity").val())));
            }
        }
    };
    self.CheckProductType = function () {
        DrpHardCopyDownload();   
    };

    function DrpHardCopyDownload() {
        if ($("#ProductType").val() == "Hard Copy") {
            $('#inputProductTotalQuantity').prop('readonly', false);
            $("#inputProductTotalQuantity").attr({
                "min": 1          // values (or variables) here
            });

            $("#pfile").hide();
            $("#ProductFile_File").removeClass('required_bdr');
        }
        else {
            $("#inputProductTotalQuantity").attr({
                "min": 0          // values (or variables) here
            });
            $("#inputProductCurrentBalance").val(0);
            $("#hfProductCurrentBalance").val(0);
            self.ProductCurrentBalance(0);
            self.ProductTotalQuantity(0);

            $('#inputProductTotalQuantity').prop('readonly', true);
            $("#pfile").show();
            $("#ProductFile_File").addClass('required_bdr');

            var control = $('#pfile');
            control.replaceWith(control.val('').clone(true));

            control.on({
                change: function () { console.log("Changed") },
                focus: function () { console.log("Focus") }
            });

        }
    };

}
$(document).ready(function () {   
    $('[data-toggle="popover"]').popover({
        placement: 'top',
        trigger: 'hover',
        html: true,
        content: '<div class="media"><img src="/Storage/ProductImage/' + $('#ProductImageName').val() + '" style="max-width:245px; max-height:250px;" /></div>'
    });

    

});
