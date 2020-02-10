function EducationCredentialViewModel() {
    var self = this;
    self.CredentialUnit = ko.observable().extend({ numeric: 1 });
    self.CredentialProgram = ko.observable();
    self.CredentialType = ko.observable();
    self.EducationID = ko.observable();
    self.AccreditorID = ko.observable();
    self.CredentialID = ko.observable();
    self.CertificateMessage = ko.observable();
    self.IsActive = ko.observable();
    self.selectedItem = ko.observable();
    self.EducationCredentialResults = ko.observableArray([]);
    self.Accreditors = ko.observableArray([]);
    
  
    function getAllEducationCredential() {
        if (!self.pendingRequest()) {
            //$("#loaderDiv1").show();
            //$.post("/Education/ShowEducationCredential", { skip: self.maxId(), Educationid: self.EducationID() }, function (data) {
            //    var data = $.parseJSON(data);
            //    for (var i = 0; i < data.list_educationModule.length; i++) {
            //        self.EducationModuleResults.push(new InsertEducationModuleLineItem(data.list_educationModule[i].EducationID, data.list_educationModule[i].EducationModuleName, data.list_educationModule[i].EducationModuleDate, data.list_educationModule[i].EducationModuleTime, data.list_educationModule[i].EducationModuleID, data.list_educationModule[i].EducationModuleDescription, data.list_educationModule[i].EducationModuleTime, data.list_educationModule[i].EducationModuleVideoName, data.list_educationModule[i].EducationModulePDFName, data.list_educationModule[i].EducationModulePPTName, data.list_educationModule[i].EducationModuleTypeFile, data.list_educationModule[i].EducationModulePosition));
            //    }
            //    if (data.pagedEducationModule.TotalCount > 1) {
            //        self.MainModulePositions.removeAll();
            //        for (var i = data.pagedEducationModule.TotalCount + 1; i >= 1; i--) {
            //            self.MainModulePositions.push(new EducationModulePosition(i));
            //        }
            //    }
            //    else
            //        self.MainModulePositions.push(new EducationModulePosition(1));
            //    self.Modulepositioncount(data.pagedEducationModule.TotalCount + 1);
            //    self.TotolCount(data.pagedEducationModule.TotalCount);
            //    self.CurrentPage(self.maxId());
            //    self.maxId(self.maxId() + data.pagedEducationModule.PagedRecords);
            //    $('#loaderDiv1').hide();
            //});
        }
    }
    this.Init = function (model) {       
        if (model != null) {
            ko.mapping.fromJS(model.EducationCredentialResults, {}, self.EducationCredentialResults);
            ko.mapping.fromJS(model.AccreditorResults, {}, self.Accreditors);
            self.EducationID($('#hdEducationID').val());
           
        }   
    };

    self.AddEducationCredentialDetailSuccess = function (model) {
        if (model != null) {
            var newEducationCredential = $.parseJSON(model);
        
            var viewModel = self.EducationCredentialResults;
            if (!newEducationCredential.flag) {
                self.EducationCredentialResults.removeAll();
                $.post("/Education/ShowEducationCredential", { Educationid: newEducationCredential.EducationID}, function (_data) {
                    var data = $.parseJSON(_data);
                    if (data != null) {
                        ko.mapping.fromJS(data, {}, self.EducationCredentialResults);                
                    }                 
                    alertify.success("Update Successfully");
               
                });
            }
            else {
                if (self.EducationCredentialResults.length > 0)
                    {
                    self.EducationCredentialResults.removeAll();
                    }
                $.post("/Education/ShowEducationCredential", { Educationid: newEducationCredential.EducationID }, function (_data) {
                    var data = $.parseJSON(_data);
                    if (data != null) {
                        ko.mapping.fromJS(data, {}, self.EducationCredentialResults);
                    }
                    alertify.success("Added Successfully");

                });
            }
            
          
        }
        resetcontrol();
        $("#btEducationCredentialclosepopup").click();

    };

    self.EducationCredentialInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddEducationCredential").jqBootstrapValidation('hasErrors')) {
            alert(1);
            return false;
        }       
        return true;
    }

    self.EditEducationCredential = function (config) {   
       self.CredentialUnit(config.CredentialUnit());
       self.CredentialProgram(config.CredentialProgram());
       self.CertificateMessage(config.CertificateMessage());
       self.CredentialType(config.CredentialType());
       self.CredentialID(config.CredentialID());
       self.selectedItem(config.AccreditorID());
       self.IsActive(config.IsActive());
       
    }

    this.deleteEducationCredential = function (itemToDelete) {

        self.EducationID($('#hdEducationID').val());
        alertify.confirm("Are you sure you want to change the Status for this item?", function (e) {
            if (e) {
                $.ajax({
                    url: "/Education/DeleteEducationCredentialInfo",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON(itemToDelete),
                    success: function (model) {
                        self.EducationCredentialResults.removeAll();
                        $.post("/Education/ShowEducationCredential", { Educationid: self.EducationID() }, function (_data) {
                            var data = $.parseJSON(_data);
                            if (data != null) {
                                ko.mapping.fromJS(data, {}, self.EducationCredentialResults);
                            }
                            alertify.success("Status Updated Successfully");

                        });
                    },
                    error: function (model) {
                        alert("Error while deleting a item - " + model);
                    }
                });
            }
        });
   
    };

   
    $("#btncloseEducationCredential").click(function () {
        resetcontrol();
    });


  
    function resetcontrol() {
        self.CredentialUnit("");
        self.CredentialProgram("");
        self.CredentialType("");
        self.AccreditorID("");
        self.CredentialID("");
        self.CertificateMessage("");
        self.selectedItem("");
        
    }


}


