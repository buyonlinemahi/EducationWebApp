function GroupViewModel() {
    var self = this;
    self.Menus = ko.observableArray();
    self.Menus = ko.observableArray([self.Menus(0)]);
    self.selectedMenu = ko.observable(0);
    self.MenuID = ko.observable();
    self.MenuName = ko.observable();
    self.selectedMenu = ko.observableArray();    
    self.UserMenuGroupID = ko.observable();
    self.OldGroupName = ko.observable();
    self.OldSelectedMenu = ko.observableArray();
    self.UserMenuPermissionID = ko.observable();

    //check if all are selected or not on ever check box click
    self.MenuChange = function (id) {      
            if ($("input[name='MenuOptions']:checked").length == $("input[name='MenuOptions']").length) {
                $('.selectall').prop('checked', true);
            }
            else {
                $('.selectall').prop('checked', false);
            }               
        return true;
    };

    //after form success
    self.AddGroupSuccess = function () {
        if (self.UserMenuGroupID() != null) {
            alertify.alert('Group Updated Successfully', function () { window.location = "/Group/"; });
        }
        else {
            alertify.alert('Group Added Successfully', function () { window.location = "/Group/"; }); 
        }
        
    };

    // check conditions for group add or update and for data redundancy.
    self.CheckAndSubmit = function () {        
        if ($("#txtGroupName").val().trim() == "")
        {
            alertify.alert('Enter Group Name.');
            $("#txtGroupName").val("");
            return false;
        }

        self.selectedMenu.sort().join(',');// to sort the observable arrey of menus.        
        if (self.UserMenuGroupID() != null) {            
            if (self.selectedMenu() != self.OldSelectedMenu()) {
                $.post("/Group/CheckUserGroupMenu/", { menuIds: self.selectedMenu().join(',') }, function (data) {
                    if (data != true) {
                         
                        alertify.alert(data);
                        return false;
                    }
                    else {
                        $("#frmAddUserGroup").submit();
                    }
                });
            }
            else {
                $("#frmAddUserGroup").submit();
            }
        }
        else {
            $.post("/Group/CheckUserGroupName/", { userGroupName: self.MenuName() }, function (data) {
                if (data != true) {
                    alertify.alert(data);
                    return false;
                }
                else {
                    $.post("/Group/CheckUserGroupMenu/", { menuIds: self.selectedMenu().join(',') }, function (data) {
                        if (data != true) {
                            alert(data);
                            return false;
                        }
                        else {
                            $("#frmAddUserGroup").submit();
                        }
                    });
                }
            });
        }
    }

    //to select all checkboxes
    $('.selectall').click(function () {
        self.selectedMenu([]);
        if ($(this).is(':checked')) {
            for (i = 0; i < self.Menus().length; i++) {
                self.selectedMenu.push(parseInt(self.Menus()[i].MenuID));
            }
        }
        else {
            self.selectedMenu.push(1);
        }
    });

    self.init = function (model)
    {
        $.getJSON("/Common/getAllMenu", function (data) {
            self.Menus(data.slice());
            if (model.UserMenuGroup != null) {
                $("#pageHeading").html("Edit Group");
                $("#txtGroupName").attr("disabled", "disabled");
                self.MenuName(model.UserMenuGroup.UserMenuGroupName);
                self.UserMenuPermissionID(model.UserMenuPermission.UserMenuPermissionID);
                self.OldGroupName(model.UserMenuGroup.UserMenuGroupName);
                self.UserMenuGroupID(model.UserMenuGroup.UserMenuGroupID);
                arr = model.UserMenuPermission.MenuIDs.split(',');
                for (i = 0; i < arr.length; i++) {
                    self.selectedMenu.push(parseInt(arr[i]));
                    self.OldSelectedMenu.push(parseInt(arr[i]));
                }
                self.MenuChange();
            }
            else {
                self.selectedMenu.push(1);
            }
        });                
    };
}