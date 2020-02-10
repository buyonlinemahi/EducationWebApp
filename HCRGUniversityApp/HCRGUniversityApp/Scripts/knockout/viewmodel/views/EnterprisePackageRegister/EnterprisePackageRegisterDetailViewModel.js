
function EnterprisePackageRegisterDetailViewModel() {
    var self = this;

    self.EPRName = ko.observable();
    self.EPRID = ko.observable();
    self.EPRPhone = ko.observable();
    self.EPREmail = ko.observable();
    self.EPRCompany = ko.observable();

    this.Init = function (model) {
        if (model != null) {
            ko.mapping.fromJS(model, {}, self);
        }
    }




    self.AddEnterprisePackageRegisterInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEnterprisePackageRegister").jqBootstrapValidation('hasErrors')) {
            return false;
        }
    }

    self.AddEnterprisePackageRegisterDetailSuccess = function (model) {
        var data = $.parseJSON(model);
        alertify.alert(data, function (e) {
            if (e) {
                //window.location = '/MyEducation/';
            }
        });
    }

    $(document).ready(function () {
        $('.phonemask').mask('(999)-999-9999');
    });
}


