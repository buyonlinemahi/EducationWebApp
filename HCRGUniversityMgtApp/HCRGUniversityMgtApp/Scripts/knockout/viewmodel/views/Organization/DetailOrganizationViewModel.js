function DetailOrganization(model) {
    var self = this;

    self.OrganizationID = ko.observable();
    self.EncryptedOrganizationID = ko.observable();
    self.OrganizationName = ko.observable();
    self.WebsiteName = ko.observable();
    self.ThemeID = ko.observable(5);
    self.RegisteredPaypalEmailID = ko.observable();
    self.LogoImage = ko.observable();
    self.LogoPath = ko.observable();
    self.OrganizationImageFile = ko.observable();
    self.Message = ko.observable();
    self.FaviconImageFile = ko.observable();
    self.FaviconImage = ko.observable();
    self.OrganizationResults = ko.observableArray([]);
    self.scrolled = ko.observableArray([]);
    self.pendingRequest = ko.observable(false);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.IsCertificate = ko.observable();
    self.IsWebPortal = ko.observable();
    self.FaviconImageFile = ko.observable();
    self.FaviconImage = ko.observable();
    self.SelectedUploadImage = ko.observable();
    self.LogoImageFile = ko.observable();

    self.Menus = ko.observableArray();
    self.Menus = ko.observableArray([self.Menus(0)]);
    self.MenuID = ko.observable();
    self.MenuName = ko.observable();
    self.selectedMenu = ko.observableArray();
    
    //**************Theme Drop down ****************************//
    self.ThemeTypes = [{
        ClProColorThemeID: 1,
        ClProWebsiteName: 'Theme1'
    },
    {
        ClProColorThemeID: 2,
        ClProWebsiteName: 'Theme2'
    },
    {
        ClProColorThemeID: 3,
        ClProWebsiteName: 'Theme3'
    },
    {
        ClProColorThemeID: 4,
        ClProWebsiteName: 'Theme4'
    },
    {
        ClProColorThemeID: 5,
        ClProWebsiteName: 'Default Theme'
    }
    ];
    //***************************************************//

    //------------------ cropping tool js
    function checkPhotoLimit() {
        if ($("#savedImageList > li").length + $("#cropList1 > div").length >= 5) {
            $('#btnCrop').prop('disabled', true);
            $('#file').prop('disabled', true);
        }
    }
    function checkImageExtension(fileExtension) {
        if (fileExtension == "jpg" || fileExtension == "png" || fileExtension == "tiff" || fileExtension == "jpeg") {
            return true;
        }
        else {
            return false;
        }
    }
    var cropper;
    document.querySelector('#file').addEventListener('change', function () {
        var fileExtension = $(this).val().split('.')[$(this).val().split('.').length - 1].toLowerCase();
        var _URL = window.URL || window.webkitURL;
        var file, img;
        if (checkImageExtension(fileExtension)) {
            if (document.getElementById("file").files[0].size <= 5000000) {
                file = this.files[0];
                img = new Image();
                img.onload = function (e) {
                    if (this.width == 230 && this.height == 49) {// file file has exact same dimensions

                        if (!imgCount) {
                            imgCount = 1;
                        }
                        else {
                            imgCount += 1;
                        }

                        if ($("#cropList > div").length >= 4) {
                            $('#btnCrop').prop('disabled', true);
                            $('#file').prop('disabled', true);
                        }

                        //------- converting image to bytes data -------//
                        var canvas = document.createElement("canvas");
                        var img1 = document.createElement("img");

                        img1.setAttribute('src', img.src);
                        canvas.width = img1.width;
                        canvas.height = img1.height;
                        var ctx = canvas.getContext("2d");
                        ctx.drawImage(img1, 0, 0);
                        var imageSource = canvas.toDataURL("image/jpg");
                        //-----------------------------//
                        document.querySelector('#cropList').innerHTML += '<div id="div' + imgCount + '" class="cropImage-outer-div"><img name="NewsPhotoFile" id="photo' + imgCount + '" src="' + imageSource + '" class="croppedImage"><span><img src="/Content/imgs/trash.png" class="remove-icon" onclick="javascript:removePhoto(' + imgCount + ')"></span><label style="display:none;" id="label' + imgCount + '">' + document.getElementById('file').value.substring(0, 10) + imgCount + '.jpg</label></div>';
                    }
                    else if (this.width > 230 && this.height > 49) { // when file dimensions are larger


                        var options =
                            {
                                imageBox: '.imageBox',
                                thumbBox: '.thumbBox',
                                spinner: '.spinner'
                            }
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            options.imgSrc = e.target.result;
                            cropper = new cropbox(options);
                        }
                        reader.readAsDataURL(file);
                        this.files = [];
                    }
                    else {
                        alertify.alert("Image dimensions should be equal to or more than 230(width) X 49(height)");
                    }
                }
                img.src = _URL.createObjectURL(this.files[0]);
            }
            else {
                alertify.alert("File size exceeded maximum limit (5 MB).");
            }
        }
        else {
            alertify.alert("Please upload file having extensions .jpeg/.jpg/.png/.tiff only.");
        }
    });
    document.querySelector('#btnCrop').addEventListener('click', function () {
        if ($(".imageBox").css("background-image") != 'none') {
            var img = cropper.getDataURL();                
                self.LogoImageFile(img);
                self.LogoImage(img);
                $("#myModalPhoto").modal("hide");
            //doument.querySelector('#cropList').innerHTML += '<div id="div' + imgCount + '" class="cropImage-outer-div"><img name="NewsPhotoFile" id="photo' + imgCount + '" src="' + img + '" class="croppedImage"><span><img src="/Content/imgs/trash.png" class="remove-icon" onclick="javascript:removePhoto(' + imgCount + ')"></span><label style="display:none;" id="label' + imgCount + '">' + document.getElementById('file').value.substring(0, 10) + imgCount + '.jpg</label></div>';
        }
    })
    document.querySelector('#btnZoomIn').addEventListener('click', function () {
        cropper.zoomIn();
    })
    document.querySelector('#btnZoomOut').addEventListener('click', function () {
        cropper.zoomOut();
    })
    // file button click in edit photo popup
    $('#myModalPhoto').on('shown.bs.modal', function () {
        checkPhotoLimit();
    })
    //------------------

    if (model != null) {
        self.OrganizationName(model.OrganizationName);
        self.WebsiteName(model.WebsiteName);
        if (model.ThemeID != null) {
            self.ThemeID(model.ThemeID);
        }
        else {
            self.ThemeID(5);
        }

        if (model.ClientTypeID == 1) {
            $("#txtOrganizationName").removeAttr("disabled");
          }
        else {
            $("#txtOrganizationName").attr("disabled", "disbled");
        }
        self.EncryptedOrganizationID(model.EncryptedOrganizationID);
        self.RegisteredPaypalEmailID(model.RegisteredPaypalEmailID);
        self.LogoImage(model.LogoImage);
        self.FaviconImage(model.FaviconImage);
        if (model.IsCertificate == null || model.IsCertificate == false) {
            $("#txtCertificate").prop("checked", false);
            self.IsCertificate("false");

        }
        else {
            $("#txtCertificate").prop("checked", true);
            self.IsCertificate("true");

        }
        if (model.IsWebPortal == null || model.IsWebPortal == false) {
            $("#txtWebPortal").prop("checked", false);
            self.IsWebPortal("false");
            self.ThemeID(5);
            $.getJSON("/Common/getAllMenu", function (data) {
                self.Menus(data.slice());
            });
            $("#drpThemes").prop("disabled", true);

        }
        else {
            $("#txtWebPortal").prop("checked", true);
            self.IsWebPortal("true");
            $.getJSON("/Common/getAllMenu", function (data) {
                self.Menus(data.slice());
            });
            if (model.MenuIDs != null) {
                var _menusIDS = model.MenuIDs.split(',');
                for (i = 0; i < _menusIDS.length; i++) {
                    self.selectedMenu.push(parseInt(_menusIDS[i]));
                }
            }
            $("#GroupPartial").show();
            $("#drpThemes").prop("disabled", false);
        }
        $('#LogoImageName').val(model.LogoImage);
        $('[data-toggle="popover-1"]').popover("destroy");
        $('[data-toggle="popover-1"]').popover({
            placement: 'top',
            trigger: 'hover',
            html: true,
            content: '<div class="media"><img src=  "' + $('#LogoImageName').val() + '" style="max-width:245px; max-height:250px;" /></div>'
        });

        $('#FaviconImage').val(model.FaviconImage);
        $('[data-toggle="popover"]').popover("destroy");
        $('[data-toggle="popover"]').popover({
            placement: 'top',
            trigger: 'hover',
            html: true,
            content: '<div class="media"><img src=  "' + $('#FaviconImage').val() + '" style="max-width:245px; max-height:250px;" /></div>'
        });

        if (model.FaviconImage != null) {
            $('#FaviconImageFile').removeAttr('required');
            $("#DivFaviconImage").addClass("col-md-7  col-sm-5 col-xs-8 ");
        }
        else {
            $("#DivFaviconImage").addClass("col-md-7  col-sm-8 col-xs-8 ");
        }
        if (model.LogoImage != null) {
            $('#OrganizationImageFile').removeAttr('required');
            $("#DivLogoImage").addClass("col-md-7  col-sm-5 col-xs-8 ");
        }
        else {
            $("#DivLogoImage").addClass("col-md-7  col-sm-8 col-xs-8 ");
        }

    }
    else {
        $("#DivFaviconImage").addClass("col-md-7  col-sm-8 col-xs-8 ");
        $("#DivLogoImage").addClass("col-md-7  col-sm-8 col-xs-8");
    }
    
    self.OrganizationInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmOrganizationform").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        if ($("#hdIsWebPortal").val() == "true") {
            var t = self.selectedMenu();
            if (t.length == 0) {
                alertify.alert('Atleast one menu is selected');
                return false;
            }            
        }
        return true;
    }

    self.AddOrganizationSuccess = function (model) {
        var newOrganization = model;
        if (model.Message == "Update SuccessFully") {
            alertify.success("Organization Updated Successfully");
            window.location = "/Organization/Index";
        }
        else {
            alertify.success("Organization Inserted Successfully");
            window.location = "/Organization/Index";
        }
        
    };

    self.DropDownThemesChanges = function (value) {
        $("#hdThemeID").val(self.ThemeID());
    }

    self.IsCertificateChanged = function () {
        if ($('#hdIsCertificate').val() == "false") {
            $("#txtCertificate").prop("checked", true);
            $('#hdIsCertificate').val("true");
        }
        else {
            $("#txtCertificate").prop("checked", false);
            $('#hdIsCertificate').val("false");
        }
    }
    self.IsWebPortalChanged = function () {
        if ($('#hdIsWebPortal').val() == "false") {
            $("#txtWebPortal").prop("checked", true);
            $('#hdIsWebPortal').val("true");
            $("#drpThemes").prop("disabled", false);
            $("#GroupPartial").show();
            self.selectedMenu.push(1);
        }
        else {
            $("#txtWebPortal").prop("checked", false);
            $('#hdIsWebPortal').val("false");
            self.ThemeID(5);
            $("#drpThemes").prop("disabled", true);
            $("#GroupPartial").hide();
            $("#hdnMenuIDs").val("");
            self.selectedMenu.removeAll();
        }
    }
    function reset() {
        self.OrganizationName('');
        self.WebsiteName('');
        self.ThemeID('');
        self.EncryptedOrganizationID('');
        self.RegisteredPaypalEmailID('');
        $("#file").val('');
        self.LogoImage('');
    }

    self.FileCheckValidation = function () {
        var fileInput = document.getElementById('file');
        if ($("#file").val() != '') {
            var filePath = fileInput.value;
            var Filesize = parseFloat($("#file")[0].files[0].size / 1024).toFixed(2);
            var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
            if (!allowedExtensions.exec(filePath)) {
                //alertify.alert('Please upload file having extensions .jpeg/.jpg/.png/.gif only.');
                fileInput.value = '';
                return false;
            }            
        }        
    }
    self.FileCheckValidationFavicon = function () {
        var fileInput = document.getElementById('FaviconImageFile');
        if ($("#FaviconImageFile").val() != '') {
            var filePath = fileInput.value;
            var Filesize = ($("#FaviconImageFile")[0].files[0].size / 1024);
            var allowedExtensions = /(\.ico)$/i;
            if (!allowedExtensions.exec(filePath)) {
                alertify.alert('Please upload file having extensions .ico only.');
                fileInput.value = '';
                return false;
            }
            else if (Filesize >= 5000) {
                alertify.alert('Image should be  Less Then 5MB');
                $("#FaviconImageFile").val('');
            }
        }
    }
    //check if all are selected or not on ever check box click
    self.MenuChange = function () {
        if ($("#updateGroupMenus :input[name='MenuOptions']").length == $("#updateGroupMenus :input[name='MenuOptions']:checked").length) {
            $('.selectall').prop('checked', true);
        }
        else {
            $('.selectall').prop('checked', false);
        }
        return true;
    };

}