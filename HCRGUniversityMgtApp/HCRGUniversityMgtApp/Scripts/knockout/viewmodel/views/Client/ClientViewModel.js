function ClientViewModel(model) {
    var self = this;

    //**************Client Model ****************************//
    self.ClientID = ko.observable();
    self.EncryptedClientID = ko.observable();
    self.OrganizationID = ko.observable();
    self.FirstName = ko.observable();
    self.LastName = ko.observable();
    self.EmailID = ko.observable();
    self.Address = ko.observable();
    self.Address2 = ko.observable();
    self.City = ko.observable();
    self.StateID = ko.observable();
    self.Zip = ko.observable();
    self.Phone = ko.observable();
    self.Password = ko.observable();   
    self.IsActive = ko.observable();
    self.IsApproved = ko.observable();
    self.IsEmailSent = ko.observable();
    self.ClientTypeID = ko.observable();

    self.ClientResults = ko.observableArray();
    self.Clients = ko.observableArray();

    self.selectedState = ko.observable(0);
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);

    self.selectedOrganization = ko.observable(0);
    self.Organizations = ko.observableArray();
    self.Organizations = ko.observableArray([self.Organizations(0)]);

    self.selectedClient = ko.observable(0);
    self.ClientTypes = ko.observableArray();
    self.ClientTypes = ko.observableArray([self.ClientTypes(0)]);

    ko.mapping.fromJS(model, {}, self);

    self.bindModelData = function (model) {
        self.selectedOrganization(model.OrganizationID);
        self.selectedState(model.StateID);
        self.selectedClient(model.ClientTypeID);
        ko.mapping.fromJS(model, {}, self);
    };
    $.getJSON("/Common/getAllState", function (data) {
        self.States(data.slice());
    });

    $.getJSON("/Client/GetAllOrganization", function (data) {
        self.Organizations(data.slice());

    });

    $.getJSON("/Common/getAllClientType", function (data) {
        self.ClientTypes(data.slice());

    });

   
    self.SaveClientDetails = function () {
        if ($("#OrganizationID").val() == 0)
        {
            $("OrganizationID").prop("required", "true");
            alert("Select Organization!");

        }
        else if ($("#txtFirstName").val() == "")
        {
            $("txtFirstName").prop("required", "true");
            alert(" First Name cannot be empty!");
        }
        else if ($("#txtLastName").val() == "")
        {
            $("txtLastName").prop("required", "true");
            alert("Last Name  cannot be empty!");
        }
        else if ($("#txtEmail").val() == "")
        {
            $("txtEmail").prop("required", "true");
            alert("Email cannot be empty!");
        }
        else if ($("#txtAddress").val() == "")
        {
            $("txtAddress").prop("required", "true");
            alert("Address cannot be empty!");
        }
        else if ($("#txtCity").val() == "")
        {
            $("txtCity").prop("required", "true");
            alert("City cannot be empty!");
        }
        else if ($("#StateID").val() == 0)
        {
            $("StateID").prop("required", "true");
            alert("Select State!");
        }
        else if ($("#txtZip").val() == "")
        {
            $("txtZip").prop("required", "true");
            alert(" Zip cannot be empty!");
        }
        else if ($("#txtPhone").val() == "")
        {
            $("txtPhone").prop("required", "true");
            alert("Phone cannot be empty!");
        }
        else if ($("#ClientTypeID").val() == 0) {
            $("ClientTypeID").prop("required", "true");
            alert("Select Client Type!");
        }
        else {
            var _saveClient = {
                OrganizationID: $("#OrganizationID").val(),
                ClientID: self.ClientID(),
                FirstName: self.FirstName(),
                LastName: self.LastName(),
                EmailID: self.EmailID(),
                Address: self.Address(),
                Address2: self.Address2(),
                City: self.City(),
                StateID: $("#StateID").val(),
                ClientTypeID: $("#ClientTypeID").val(),
                Zip: self.Zip(),
                Phone: self.Phone(),
                IsApproved: self.IsApproved(),
                IsActive: self.IsActive()
            }            
            
            $.ajax({
                url: "/Client/SaveClient",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ _client: _saveClient }),
                cache: false,
                success: function (_model) {
                    $("#ClientModal").modal("hide");
                    ResetControls();
                    alertify.alert(_model);
                    //if (_model == "Add Sucessfully." || _model == "Update Successfully" || _model == "The email you are trying to use already exists. We cannot create this account. If you think this is a mistake, please contact us at support@allgatelearning.com") {                        
                    //    alertify.alert(_model, function (e) {
                    //        if (_model == "Add Sucessfully." || _model == "Update Successfully") {                                                                
                    //            
                    //        }
                    //    });                        
                    //}
                    //else {
                    //    alertify.alert(_model);
                    //}
                }
            });
        }

        }

    function ResetControls() {
        $('#OrganizationID').prop('selectedIndex', 0);
        self.FirstName("");
        self.LastName("");
        self.EmailID("");
        self.Address("");
        self.Address2('');
        self.City("");
        $('#StateID').prop('selectedIndex', 0);
        self.Zip("");
        self.Phone("");
        $("#ClientModal").modal('hide');
        self.ClientID("");
        self.selectedClient("");
    }

    self.updateClient = function (model) {
        window.location = '/Client/ClientEdit?ClientID=' + model.EncryptedClientID();
       
    }

}
