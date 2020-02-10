function LegalGridViewModel() {
    var self = this;
    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
            $("#divdisplay").html(model.LegalContent);
            // $("#divdisplay").html(model.HomePageContent);
        }
    };
    self.LegalDetailSuccess = function (model) {
        if (model != null) {
            alertify.success("Updated Successfully");
            var data = $.parseJSON(model);
            $("#divdisplay").html(data.LegalContent);
        }
    };

    self.LegalInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmLegal").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        $("#hd").val($("<div>").text($("#EditorLegal").val()).html());//encoding
        return true;
    };
    $("#EditorModalPopUp").click(function () {
        $("#EditorModal").css("display", "block");
        $("#inner-content").addClass("modal-open");
        $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
        $("#EditorModal").addClass("in");
    });

    $(".btnClose").click(function () {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    });

}
function submitform() {
    $("#hd").val($("<div>").text($("#EditorLegal").val()).html());//encoding
}