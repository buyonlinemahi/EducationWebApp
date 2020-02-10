function OrganizationContactViewModel() {
    var self = this;
    self.ClientID = ko.observable();
    self.ContactID = ko.observable();
    self.Phone = ko.observable();
    self.Fax = ko.observable();
    self.EmailID = ko.observable();
    self.Address = ko.observable();
    self.Address2 = ko.observable();
    self.City = ko.observable();
    self.StateID = ko.observable();
    self.Zip = ko.observable();
    self.OperationHour = ko.observable();
    self.StartTime = ko.observable();
    self.EndTime = ko.observable();
    self.selectedItem = ko.observable(0);
    self.States = ko.observableArray();
    self.States = ko.observableArray([self.States(0)]);
    self.EncryptedOrganizationContactID = ko.observable();
    self.OrganizationID = ko.observable();

    $(".phoneMaskformat").mask("(999) 999-9999");
    
    $('#loaderDiv').hide();

    self.Init = function (model)

    
    {
        var starttime;
        var startmintue;
        if(model != null)
        {
            DropDownValues();
        if (model.StartTime != null ) // a check for edit case
        {
            starttime = model.StartTime.split(":");
            startmintue = starttime[1].split("  ");
        }
        var Endtime;
        var mintue;
        if (model.EndTime != null) // a check for edit case
        {
            Endtime = model.EndTime.split(":");
            mintue = Endtime[1].split(" ");
        }
        $("#objStarthour").val(starttime[0]);
        $("#objStartmintue").val(startmintue[0]);
        $("#objStartAP").val(startmintue[2]);
        $("#objFinalhour").val(Endtime[0]);
        $("#objFinalmintue").val(mintue[0])
        $("#objFinalAP").val(mintue[1]);

    }
        DropDownValues();       
        ko.mapping.fromJS(model, {}, self);


        

    };

    
    $(document).ready(function () {
        $.getJSON("/Common/getAllState", function (data) {
            self.States(data.slice());
            if (model != null)
                self.selectedItem(model.StateID);
        });
    });

    self.SaveContacts = function ()
    {
        var StartTime = $("#objStarthour").val() + ":" + $("#objStartmintue").val() + " " + $("#objStartAP").val();
        var Endtime = $("#objFinalhour").val() + ":" + $("#objFinalmintue").val() + " " + $("#objFinalAP").val();
        if ($("#txtPhone").val() == "") {
            $("txtPhone").prop("required", "true");
            alertify.alert("Phone cannot be empty!");
        }
         else if ($("#txtEmailID").val() == "") {
            $("txtEmailID").prop("required", "true");
            alertify.alert("EmailID  cannot be empty!");
        }
         else if ($("#OperationHour").val() == "") {
             $("OperationHour").prop("required", "true");
            alertify.alert("OperationHour cannot be empty!");
        }
          else if ($("#Address").val() == "") {
            $("Address").prop("required", "true");
            alertify.alert("Address cannot be empty!");
        }
        else if ($("#City").val() == "") {
            $("City").prop("required", "true");
            alertify.alert("City cannot be empty!");
        }
        else if ($("#StateID").val() == "") {
            $("StateID").prop("required", "true");
            alertify.alert(" StateID cannot be empty!");
        }
        else if ($("#Zip").val() == "") {
            $("Zip").prop("required", "true");
            alertify.alert("Zip cannot be empty!");            
        }       
        else if (StartTime == Endtime) {       
            alertify.alert("Start time and  end time cannot be Same!");            
        }            
        else {
            var _saveContact = {
                ClientID: self.ClientID(),
                Phone: self.Phone(),
                Fax: self.Fax(),
                EmailID: self.EmailID(),
                Address: self.Address(),
                Address2: self.Address2(),
                City: self.City(),
                StateID: $("#StateID").val(),
                Zip: self.Zip(),
                OperationHour: self.OperationHour(),
                StartTime: $("#objStarthour").val() + ":" + $("#objStartmintue").val() + "  " + $("#objStartAP").val(),
                EndTime: $("#objFinalhour").val() + ":" + $("#objFinalmintue").val() + " " + $("#objFinalAP").val(),
               EncryptedOrganizationContactID: self.EncryptedOrganizationContactID(),
                OrganizationID: self.OrganizationID()
            }
            $.ajax({
                url: "/Organization/OrganizationContact",
                type: 'post',
                dataType: 'json',
                contentType: 'application/json',
                data: JSON.stringify({ _Contact: _saveContact }),
                cache: false,
                success: function (_model) {

                    //var _model = $.parseJSON(result);
                    if (_model == "Saved Successfully" || _model == "Updated Successfully") {
                        alertify.alert(_model, function (e) {
                            if (e) {
                                redirect();
                            }
                        });
                    }
                    else {
                        alertify.alert(_model, function (e) {
                            if (e) {
                                redirect();
                            }
                        }); 
                    }
                }

            });
           
        }
    }

    function resetcontrol() {
        self.ContactID("");
        self.ClientID("");
        self.Phone("");
        self.Fax("");
        self.EmailID("");
        self.Address("");
        self.Address2('');
        self.City("");
        self.Zip("");
        self.OperationHour("");
        $("#objStarthour").val(1) ;
        $("#objStartmintue").val(0); 
        $("#objStartAP").val('AM');
        $("#objFinalhour").val(1);
        $("#objFinalmintue").val(0);
        $("#objFinalAP").val('AM');
        $("#StateID").val('Select State');
        self.EncryptedOrganizationContactID("");
    }

    function IsValidEmail(email) {
        var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
        return expr.test(email);
    };


    self.ValidateEmail = function(){
        var email = document.getElementById("txtEmailID").value;
        if (!IsValidEmail(email)) {
            alertify.alert("Invalid email address.");
            self.EmailID("");
        }       
    }

    self.addNewProduct = function () {
        window.location = '/Organization/OrganizationContact';
    }
    function redirect() {
        window.location = '/Organization/OrgContactUsIndex';
    }

    ///******************Time Drop Downs Binding*****************////////////
    function DropDownValues() {
        var $select = $(".0-60");
        for (i = 0; i <= 60; i++) {
            if (i < 10) {
                i = '0' + i; // adding leading zero
            }
            $select.append($('<option></option>').val(i).html(i))
        }

        var $select = $(".1-12");
        for (i = 1; i <= 12; i++) {
            $select.append($('<option></option>').val(i).html(i))
        }
    }
}