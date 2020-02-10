var editor1
function NewsGridViewModel() {
    var self = this;
    var IsHcrgAdmin;
    self.NewsSectionID = ko.observable();
    self.newsSectionTypes = ko.observableArray();
    self.selectedItem = ko.observable(2);
    self.NewsTitle = ko.observable();
    self.NewsID = ko.observable(0);
    self.NewsDescription = ko.observable();
    self.NewsEditorPick = ko.observable();
    self.newsoption = ko.observable();
    self.NewsVideos = ko.observable();
    self.NewsPhotoFile = ko.observable();
    self.NewsPhotoFileUpdate = ko.observable();
    self.NewsResults = ko.observableArray();
    self.NewsType = ko.observable();
    self.NewsVideoID = ko.observable();
    self.NewsVideoURL = ko.observable();
    self.NewsVideoNewsID = ko.observable();
    self.NewsPhotoResult = ko.observableArray([]);
    self.NewsPhotoID = ko.observable();
    self.NewsPhotoNewsID = ko.observable();
    self.NewsPhotopic = ko.observable();
    self.NewsPhotopicFile = ko.observable();
    self.NewsAuthor = ko.observable();
    self.scrolled = ko.observableArray([]);
    self.maxId = ko.observable(0);
    self.CurrentPage = ko.observable(0);
    self.TotolCount = ko.observable(0);
    self.pendingRequest = ko.observable(false);
    self.PagedRecords = ko.observable(0);
    self.NewsPhotoFile = ko.observable();
    self.NewsPhotoName = ko.observable();
    self.NewsPhotosArr = ko.observableArray([]);
    self.NewsSectionResults = ko.observableArray([]);
    self.NewsSectionTitle = ko.observable();
    self.NewsSectionID = ko.observable();
    self.OrganizationID = ko.observable(0);
    self.OrganizationName = ko.observable("");
    self.NewsSectionType = ko.observable('Section');

    self.SearchSelectedOrganization = ko.observable(0);
    self.AllOrganizations = ko.observableArray();
    self.AllOrganizations = ko.observableArray([self.AllOrganizations(0)]); 

    self.IsHCRGAdmin = ko.observable();
    $('#loaderDiv').hide();
    self.scrolled = function (data, event) {
        if (!self.pendingRequest()) {
            var elem = event.target;
            if (self.TotolCount() >= self.maxId()) {
                if (elem.scrollTop > (elem.scrollHeight - elem.offsetHeight - 1))
                    getAllNews(self.maxId());
            }
            else
                self.pendingRequest(true);
        }
    }
    this.Init = function (model) {
        if (model != null) {
            IsHcrgAdmin = model.IsHCRGAdmin;
            $("#loaderDiv").show();
            if (!self.pendingRequest()) {
                if (model.NewsResults.length != 0) {
                    if (model.IsHCRGAdmin == false)
                        $("#divAddNews").show();
                    else
                        $("#divOrgSearch").show();
                }
                else
                    $("#divAddNews").show();
                self.IsHCRGAdmin(model.IsHCRGAdmin);
                ko.mapping.fromJS(model.NewsResults, {}, self.NewsResults);
                ko.mapping.fromJS(model.NewsSectionResults, {}, self.NewsSectionResults);
                self.CurrentPage(self.maxId());
                $('#loaderDiv').hide();
            }
        };
        getAllOrganization();
    }


    self.SearchNewsContent = function () {
       if ($("#DrpOrgID").val() == '')
        {
           alertify.alert("Please select atleast one Organization");
           return false;
        }
        $.post("/News/GetNewsByOrganizationID/", { OrgID: $("#hdfOrgID").val() }, function (data) {
            self.NewsResults.removeAll();
            ko.mapping.fromJS(data, {}, self.NewsResults);
        });
    };
    self.SearchOrgChangedValue = function (item) {
        $("#hdfOrgID").val(item.SearchSelectedOrganization());
    }
    function getAllOrganization() {
        $.ajax({
            url: "/Client/GetAllOrganization",
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            success: function (data) {
                self.AllOrganizations.removeAll();
                self.AllOrganizations(data.slice());
            },
            error: function (data) {
                alertify.alert("Error while updating a item - " + data);
            }
        });
    }
    function getAllNews(skip) {
        if (!self.pendingRequest()) {
            $("#loaderDiv").show();
            $.post("/News/GetAllPagedNews", { skip: skip }, function (data) {
                var data = $.parseJSON(data);
                for (var i = 0; i < data.NewsRecords.length; i++) {
                    self.NewsResults.push(new InsertNewsLineItem(data.NewsRecords[i].NewsID, data.NewsRecords[i].NewsTitle, data.NewsRecords[i].NewsDescription, data.NewsRecords[i].NewsEditorPick, data.NewsRecords[i].NewsSectionID, data.NewsRecords[i].NewsType, data.NewsRecords[i].NewsDescriptionShort, data.NewsRecords[i].NewsAuthor));
                }
                self.TotolCount(data.TotalCount);
                self.CurrentPage(self.maxId());
                self.maxId(self.maxId() + data.PagedRecords);
                $('#loaderDiv').hide();
            });
        }
    }
    var x;
    var y;
    $(document).ready(function () {
        //  $.ajaxSetup({ cache: false });
        //****************Add Element*******************//
        var max_fields = 10; //maximum input boxes allowed
        var wrapper = $(".input_fields_wrap"); //Fields wrapper
        var wrapperPhoto = $(".input_fields_wrap1"); //Fields wrapper
        var add_button = $(".add_field_button"); //Add button ID
        var add_buttonPhoto = $(".add_field_button1"); //Add button ID
        x = 1; //initlal text box count
        y = 1;
        $(add_button).click(function (e) { //on add input button click
            e.preventDefault();
            if (x < max_fields) { //max input box allowed
                x++; //text box increment
                $(wrapper).append('<div class="removeDivMain"><div class="panel-body"><div class="col-md-3"><label> Slide Pics: </label> <a href="#" class="inner-content-link"><i class="glyphicon glyphicon-calendar"></i></a></div> <div class="col-md-4"><input type="file" name="NewsPhotoFile"  data-bind="value:NewsPhotoFile,jqBootstrapValidation: {}"  id="photo' + x + '"></div> <a href="#" style="Color:Red" class="remove_field">Remove</a></div></div>'); //add input box
            }
        });
        $(add_buttonPhoto).click(function (e) { //on add input button click
            e.preventDefault();
            if (y < max_fields) { //max input box allowed
                y++; //text box increment
                $(wrapperPhoto).append('<div class="removeDiv"> <div class="panel-body"> <div  class="col-md-3"><label> Slide Pics: </label> <a href="#" class="inner-content-link"><i class="glyphicon glyphicon-calendar"></i></a></div> <div class="col-md-4"><input type="file" name="NewsPhotoFileUpdate"  data-bind="value:NewsPhotoFileUpdate,jqBootstrapValidation: {}"  id="pic' + y + '"></div> <a href="#" style="Color:Red" class="remove_field1">Remove</a></div></div>'); //add input box
            }
        });
        $(wrapper).on("click", ".remove_field", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); x--;
        })
        $(wrapperPhoto).on("click", ".remove_field1", function (e) { //user click on remove text
            e.preventDefault(); $(this).parent('div').remove(); y--;
        })
    });
    function InsertNewsSectionLineItem(_NewsSectionID, _NewsSectionTitle) {
        var self = this;
        self.NewsSectionID = ko.observable(_NewsSectionID);
        self.NewsSectionTitle = ko.observable(_NewsSectionTitle);
    }
    function InsertPhotoDetailArr(_NewsPhotoFile, _NewsPhotoName) {
        var self = this;
        self.NewsPhotoFile = ko.observable(_NewsPhotoFile);
        self.NewsPhotoName = ko.observable(_NewsPhotoName);
    }
    function InsertNewsLineItem(_NewsID, _NewsTitle, _NewsDescription, _NewsEditorPick, _NewsSectionID, _NewsType, _NewsDescriptionShort, _NewsAuthor, _OrganizationName, _OrganizationID) {
        var self = this;
        self.NewsID = ko.observable(_NewsID);
        self.NewsTitle = ko.observable(_NewsTitle);
        self.NewsDescription = ko.observable(_NewsDescription);
        self.NewsEditorPick = ko.observable(_NewsEditorPick);
        self.NewsSectionID = ko.observable(_NewsSectionID);
        self.NewsType = ko.observable(_NewsType);
        self.NewsDescriptionShort = ko.observable(_NewsDescriptionShort);
        self.NewsAuthor = ko.observable(_NewsAuthor);
        self.OrganizationName = ko.observable(_OrganizationName);
        self.OrganizationID = ko.observable(_OrganizationID);
    }
    function InsertNewsPhotoLineItem(_NewsPhotoID, _NewsID, _NewsPhotos) {
        var self = this;
        self.NewsPhotoID = ko.observable(_NewsPhotoID);
        self.NewsPhotoNewsID = ko.observable(_NewsID);
        self.NewsPhotopic = ko.observable(_NewsPhotos);
    }
    function AddNews() {
        self.NewsDescription($("#hd").val())
        self.newsphoto = ko.observableArray();
        var SectionID = $('#NewsSection :selected').val();

        if ($('#chkboxNewsEditorPick').prop('checked'))
            self.NewsEditorPick(true);
        else
            self.NewsEditorPick(false);

        if ($('#Photo_radio').prop('checked'))
            self.NewsType("Photo");
        else
            self.NewsType("Video");

        $.post("/News/AddNews",
            {
                newsTitle: self.NewsTitle(),
                newsSectionID: SectionID,
                newsDescription: self.NewsDescription(),
                newsEditorPick: self.NewsEditorPick(),
                newsType: self.NewsType(),
                newsVideo: $("#NewsVideos").val() == '' ? '' : self.NewsVideos(),
                newsID: self.NewsID(),
                newsAuthor: self.NewsAuthor(),
                organizationName: self.OrganizationName(),
                organizationID: self.OrganizationID()
            }, function (model) {
            var msg = model.msg.split(',');
            if (msg[1] == "Added") {
                if (self.NewsType() == "Photo") {
                    var file = "";
                    var id = msg[0];
                    var xhr = new XMLHttpRequest();
                    var fd = new FormData();
                    var photoFile;
                    var photoName;
                    var arr = $('#cropList > div').map(function () {
                        return this.id;
                    }).get();
                    for (var i = 0; i < arr.length; i++) {
                        var pid = "#photo" + (arr[i].substring(3, arr[i].length));
                        if ($(pid).attr('src').length > 0) {
                            photoFile = $(pid).attr('src');
                            photoName = $("#label" + (arr[i].substring(3, arr[i].length))).text();
                            var photoFiles = "{photoFile:'" + photoFile + "'}";
                            var photoNames = "{photoName:'" + photoName + "'}";
                            $.ajax({
                                type: "POST",
                                url: "/News/UploadNewsPhotos",
                                data: ko.toJSON({ photoFiles: photoFile, photoNames: photoName, id: id }),
                                contentType: "application/json",
                                dataType: "json"
                            });
                        }
                    }
                }
                $("#main").scrollTop(0);
                editor1.SetText('');
                alertify.success("News Added Successfully");
                self.TotolCount(self.TotolCount() + 1);
                self.maxId(self.maxId() + 1);
                removeAddPopupContent();
                $("#file").closest('form').trigger('reset');
                $(".imageBox").css("background", "");
            }
            else {
                $("#divAddNews").hide();
                var viewModel = self.NewsResults;
                                        editor1.SetText('');
                        alertify.success("News Updated Successfully");
            }
                // to Quick fixed the reload issue,,,
            window.location = '/News/Index';
        });
        resetcontrol();
    };
    this.DeleteNews = function (model) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: "/News/DeleteNews",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ newsID: model.NewsID }),
                    success: function (responsetext) {
                        self.NewsResults.remove(function (item) { return item.NewsID == model.NewsID });
                        self.TotolCount(self.TotolCount() - 1)
                        if (self.CurrentPage() < self.TotolCount() && self.TotolCount() > self.NewsResults().length) {
                            $.post("/News/GetAllPagedNews", { skip: self.maxId() - 1, take: 1 }, function (_data) {
                                var data = $.parseJSON(_data);
                                $("#loaderDiv").show();
                                var data = $.parseJSON(_data);
                                for (var i = 0; i < data.NewsRecords.length; i++) {
                                    self.NewsResults.push(new InsertNewsLineItem(data.NewsRecords[i].NewsID, data.NewsRecords[i].NewsTitle, data.NewsRecords[i].NewsDescription, data.NewsRecords[i].NewsEditorPick, data.NewsRecords[i].NewsSectionID, data.NewsRecords[i].NewsType, data.NewsRecords[i].NewsDescriptionShort, data.NewsRecords[i].NewsAuthor));
                                }
                                self.TotolCount(data.TotalCount);
                                $('#loaderDiv').hide();
                            });
                        }
                        alertify.success(responsetext);
                    },
                    error: function (data) {
                        alert("Error while deleting a Education Format link - " + data);
                    }
                });
            }
        });
    };
    self.edit = function (News) {     
        if (IsHcrgAdmin == false)  
            $("#Addbutton").show();
        editor1.SetText(($("<div>").html(News.NewsDescription()).text()));
        self.NewsTitle(News.NewsTitle());
        self.NewsAuthor(News.NewsAuthor());
        self.NewsDescription(News.NewsDescription());
        self.NewsID(News.NewsID());
        self.NewsSectionID(News.NewsSectionID());
        self.selectedItem(News.NewsSectionID());
        self.NewsEditorPick(News.NewsEditorPick());
        self.NewsType(News.NewsType());
        self.OrganizationName(News.OrganizationName());
        self.OrganizationID(News.OrganizationID());
        $("#divAddNews").show();
        $("#Divphotovideo").hide();
        $("#Video_radio").removeAttr('Required', 'Required');
        document.getElementById("Photo_radio").required = false;

        $("#NewsVideos").removeAttr('Required', 'Required');
        $(window).scrollTop(0);
    }
    self.popVideo = function (News) {
        $.ajax({
            url: "/News/getVideoByNewsID",
            data: ko.toJSON({ newsID: News.NewsID() }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON(this),
            success: function (newsVideo) {
                self.NewsVideoID(newsVideo.NewsVideoID);
                self.NewsVideoURL(newsVideo.NewsVideos);
                self.NewsVideoNewsID(newsVideo.NewsID);
            }
        });
    }
    self.popPhoto = function (News) {
        $('#HndPhotoNewsID').val(News.NewsID());
        $.ajaxSetup({ cache: false });
        $.ajax({
            url: "/News/getPhotosByNewsID",
            data: ko.toJSON({ newsID: News.NewsID() }),
            cache: false,
            type: "POST", dataType: 'json',
            contentType: 'application/json',
            data: ko.toJSON(this),
            success: function (newsPhoto) {
                self.NewsPhotoResult.removeAll();
                var data = newsPhoto;
                for (var i = 0; i <= newsPhoto.length - 1; i++) {
                    self.NewsPhotoResult.push(new InsertNewsPhotoLineItem(data[i].NewsPhotoID, data[i].NewsID, data[i].NewsPhotos));
                }
            }
        });
    }
    self.DeletePhoto = function (Newsphoto) {
        if ($("#savedImageList > li").length > 1) {
            var newspicid = Newsphoto.NewsPhotoNewsID();
            $.ajax({
                url: "/News/DeletePhoto",
                data: ko.toJSON({ newsPhotoID: Newsphoto.NewsPhotoID() }),
                cache: false,
                type: "POST", dataType: 'json',
                contentType: 'application/json',
                data: ko.toJSON(this),
                success: function (responseText) {
                    $.post('/News/getPhotosByNewsID', { newsID: newspicid },
                    function (newsPhoto) {
                        var data = $.parseJSON(newsPhoto);
                        self.NewsPhotoResult.removeAll();
                        for (var i = 0; i <= data.length - 1; i++) {
                            self.NewsPhotoResult.push(new InsertNewsPhotoLineItem(data[i].NewsPhotoID, data[i].NewsID, data[i].NewsPhotos));
                        }
                    });
                    alertify.success("Delete Successfully");
                }
            });
        }
        else {
            alertify.alert("Minimum one photo required.");
        }
    }
    function UpdateVideo() {
        $.ajax({
            type: "POST",
            url: "/News/UpdateNewsVideo",
            data: ko.toJSON({ newsVideoID: self.NewsVideoID(), newsVideos: self.NewsVideoURL(), newsID: self.NewsVideoNewsID() }),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (responseText) {
                alertify.success(responseText);
            }
        })
    };
    function UpdateNewsPhoto() {
        if ($('#cropList1 > div').length > 0) {
            var file = "";
            self.newsphotoUpdate = ko.observableArray();
            var id = $('#HndPhotoNewsID').val();
            var photoFile;
            var photoName;
            var arr = $('#cropList1 > div').map(function () {
                return this.id;
            }).get();
            for (var i = 0; i < arr.length; i++) {
                var pid = "#photo" + (arr[i].substring(3, arr[i].length));
                if ($(pid).attr('src').length > 0) {
                    photoFile = $(pid).attr('src');
                    photoName = $("#label" + (arr[i].substring(3, arr[i].length))).text();
                    var photoFiles = "{photoFile:'" + photoFile + "'}";
                    var photoNames = "{photoName:'" + photoName + "'}";
                    $.ajax({
                        type: "POST",
                        url: "/News/UploadNewsPhotos",
                        data: ko.toJSON({ photoFiles: photoFile, photoNames: photoName, id: id }),
                        contentType: "application/json",
                        dataType: "json",
                        success: function () {
                            if (i == arr.length) {
                                $.post('/News/getPhotosByNewsID', { newsID: id },
                                function (newsPhoto) {
                                    var data = $.parseJSON(newsPhoto);
                                    self.NewsPhotoResult.removeAll();
                                    for (var i = 0; i <= data.length - 1; i++) {
                                        self.NewsPhotoResult.push(new InsertNewsPhotoLineItem(data[i].NewsPhotoID, data[i].NewsID, data[i].NewsPhotos));
                                    }
                                });
                            }
                        },
                        error: function (xhr, status, error) {
                            alert(xhr.responseText);
                        }
                    });
                }
            }
            $("#cropList1").empty();
            alertify.success("Photos Added Successfully");
            removeEditPopupContent();
            $("#file1").closest('form').trigger('reset'); // to reset the form
            $("#imageBox").css("background", ""); // to remove image from cropper div
        }
        else {
            alertify.alert("No image available to upload.");
        }
    }
    function resetcontrol() {
        self.NewsTitle("");
        self.NewsAuthor("");
        self.NewsDescription("");
        self.NewsEditorPick(false);
        self.NewsID("");
        $("#Slide_Pics").hide();
        $("#Slide_URl").hide();
        self.NewsVideos("");
        $("#chkbox").prop("checked", false);
        $("#Photo_radio").prop("checked", false);
        $("#Video_radio").prop("checked", false);
        $("#Divphotovideo").show();
    }
    self.NewsInfoFormBeforeSubmit = function (arr, $form, options) {
        AddNews();
        return false;
    }
    self.NewsPhotoInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddNewsPhoto").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            UpdateNewsPhoto();
           
        }
        return false;
    }
    self.NewsVideoInfoFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddNewsVideo").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        else {
            UpdateVideo();
        }
        return false;
    }
    function reset_form_element(e) {
        var wrapperPhoto = $(".input_fields_wrap1"); //Fields wrapper
        e.wrap('<form>').parent('form').trigger('reset');
        e.unwrap();
    }
    self.AddNewsDetailSuccess = function () {
    }
    self.NewsSectionFormBeforeSubmit = function (arr, $form, options) {
        if ($("#frmAddNewsSection").jqBootstrapValidation('hasErrors')) {
            return false;
        }
        return true;
    }
    self.AddNewsSectionDetailSuccess = function (model) {
        if (model != null) {
            var newNewsSection = $.parseJSON(model);
            var viewModel = self.NewsSectionResults;
            if (!newNewsSection.flag) {
                for (var i = 0; i <= viewModel().length - 1; i++) {
                    if (viewModel()[i].NewsSectionID() == newNewsSection.NewsSectionID) {
                        self.NewsSectionResults.splice(i, 1);
                        self.NewsSectionResults.splice(i, 0, new InsertNewsSectionLineItem(newNewsSection.NewsSectionID, newNewsSection.NewsSectionTitle, newNewsSection.NewsSectionType));
                        alertify.success("News Section Updated Successfully");
                        break;
                    }
                }
            }
            else {
                self.NewsSectionResults.push(new InsertNewsSectionLineItem(newNewsSection.NewsSectionID, newNewsSection.NewsSectionTitle, newNewsSection.NewsSectionType));
                $('html, body').scrollTop($(document).height());
                alertify.success("News Section Insert Successfully");
            }
            resetNewsSectioncontrol();
        }
    };
    function resetNewsSectioncontrol() {
        self.NewsSectionID("");
        self.NewsSectionTitle("");
        self.NewsSectionType("");
    }
    self.editNewsSection = function (NewsSection) {
        self.NewsSectionTitle(NewsSection.NewsSectionTitle());
        self.NewsSectionType(NewsSection.NewsSectionType());
        self.NewsSectionID(NewsSection.NewsSectionID());   
      
    
    }
    function InsertNewsSectionLineItem(_NewsSectionID, _SectionTitle, _SectionType) {
        var self = this;
        self.NewsSectionID = ko.observable(_NewsSectionID);
        self.NewsSectionTitle = ko.observable(_SectionTitle);
        self.NewsSectionType = ko.observable(_SectionType);
    }
    this.deleteNewsSection = function (itemToDelete) {
        alertify.confirm("Are you sure want to delete this?", function (e) {
            if (e) {
                $.ajax({
                    url: "/News/DeleteNewsSection",
                    cache: false,
                    type: "POST", dataType: 'json',
                    contentType: 'application/json',
                    data: ko.toJSON({ newsSectionID: itemToDelete.NewsSectionID() }),
                    success: function (data) {
                        self.NewsSectionResults.remove(function (item) { return item.NewsSectionID == itemToDelete.NewsSectionID })
                        alertify.success(data);
                    },
                    error: function (data) {
                        alert("Error while deleting a NewsSection - " + data);
                    }
                });
            }
        })
    };
    $('#sectionClose').click(function () {
        resetNewsSectioncontrol();
    });
}
$(".btnClose").click(function () {
    clearcontent();
});
$('#btnCloseAddPhoto').click(function () {
    if ($("#cropList").has("div").length != 0) {
        alertify.confirm("Are you sure want to remove the cropped image?", function (e) {
            if (e) {
                removeAddPopupContent();
            }
        });
    }
    else {
        removeAddPopupContent();
    }
});
// to clear add photo popup content
function removeAddPopupContent() {
    $("#cropList").empty();
    $('.EditorModal').modal('hide');
    $(".EditorModal").css("display", "none");
    $(".EditorModal").removeClass("in");
    $("#inner-content").removeClass("modal-open");
    $(".modal-backdrop").remove();
    document.getElementById("Photo_radio").checked = null;
    document.getElementById("Photo_radio").value = "";
    enableFileButtonAdd(); //enable fileupload and crop button for add popup        
}
$('#btnCloseEditPhotos').click(function () {
    if ($("#cropList1").has("div").length != 0) {
        alertify.confirm("Are you sure want to remove the cropped image?", function (e) {
            if (e) {
                removeEditPopupContent();
            }
        });
    }
    else {
        removeEditPopupContent();
    }
});
// to clear edit photo popup content
function removeEditPopupContent() {
    $('#myModalPhoto').modal('hide');
    $("#myModalPhoto").css("display", "none");
    $("#myModalPhoto").removeClass("in");
    $("#inner-content").removeClass("modal-open");
    $(".modal-backdrop").remove();
    $("#cropList1").empty();
    enableFileButtonEdit();//enable fileupload and crop button for edit popup
}
$("#btnContinueAddPhoto").click(function () {
    if ($('#cropList > div').length > 0) {
        $('.EditorModal').modal('hide');
        $(".EditorModal").css("display", "none");
        $(".EditorModal").removeClass("in");
        $("#inner-content").removeClass("modal-open");
        $(".modal-backdrop").remove();
    }
    else {
        alertify.alert("No image available to upload.");
    }
});
$("#Photo_radio").click(function () {
    $("#EditorModal1").css("display", "block");
    $("#inner-content").addClass("modal-open");
    $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
    $("#EditorModal1").addClass("in");

    $('#Slide_Pics').hide('slow');
    $('#Slide_URl').hide('slow');
    $("#NewsVideos").removeAttr('Required', 'Required');
    $("#Video_radio").removeAttr('Required', 'Required');
    $("#Photo_radio").prop("checked", true);
});
$("#Video_radio").click(function () {
    $('#Slide_Pics').show('slow');
    $('#Slide_URl').show('slow');
    $("#NewsVideos").attr('Required', 'Required');
});
$("#EditorModalPopUp").click(function () {
    $("#EditorModal").css("display", "block");
    $("#inner-content").addClass("modal-open");
    $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
    $("#EditorModal").addClass("in");
});
function clearcontent() {
    if ($("#hdNewsID").val() == "") {
        alertify.confirm("Are you sure want to clear this content?", function (e) {
            if (e) {
                $("#hd").val('');
                $("#Editor1").val('');
                var $iFrameContents = $('iframe').contents().find('div.designborder');
                $iFrameContents.html('');
                $('.EditorModal').modal('hide');
                $(".EditorModal").css("display", "none");
                $(".EditorModal").removeClass("in");
                $(".modal-backdrop").remove();
                $("#inner-content").removeClass("modal-open");
            }
        });
    }
    else {
        $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
        if ($("#hd").val().length > 60000) {
            alert("Limit Exceed.")
        }
        else {
            $('#EditorModal').modal('hide');
            $("#EditorModal").css("display", "none");
            $("#EditorModal").removeClass("in");
            $(".modal-backdrop").remove();
            $("#inner-content").removeClass("modal-open");
        }
    }
}
$("#btnSaveandContinue").click(function () {
    $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding 
    if ($("#hd").val().length > 60000) {
        alert('Limit Exceed')
    }
    else {
        $('#EditorModal').modal('hide');
        $("#EditorModal").css("display", "none");
        $("#EditorModal").removeClass("in");
        $(".modal-backdrop").remove();
        $("#inner-content").removeClass("modal-open");
    }
});
function submitform() {
    $("#hd").val($("<div>").text($("#Editor1").val()).html());//encoding    
}

function AddNews()
{ 
    window.location = '/News/Index';
}
function checkPhotoLimit() {
    if ($("#savedImageList > li").length + $("#cropList1 > div").length >= 5) {
        $('#btnCrop1').prop('disabled', true);
        $('#file1').prop('disabled', true);
    }
}
function checkImageExtension(fileExtension) {
    if (fileExtension == "jpg" || fileExtension == "png" || fileExtension == "tiff") {
        return true;
    }
    else {
        return false;
    }
}
function openEditPhotoModalPopup() {
    $("#myModalPhoto").css("display", "block");
    $("#inner-content").addClass("modal-open");
    $("#inner-content").append("<div class=\"modal-backdrop fade in\"></div");
    $("#myModalPhoto").addClass("in");
}

var imgCount;
window.onload = function () {
    var cropper;
    var cropperEdit;
    document.querySelector('#file').addEventListener('change', function () {
        var fileExtension = $(this).val().split('.')[$(this).val().split('.').length - 1].toLowerCase();
        var _URL = window.URL || window.webkitURL;
        var file, img;
        if (checkImageExtension(fileExtension)) {
            if (document.getElementById("file").files[0].size <= 8000000) {
                file = this.files[0];
                img = new Image();
                img.onload = function (e) {
                    if (this.width == 522 && this.height == 348) {// file file has exact same dimensions

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
                    else if (this.width > 522 && this.height > 348) { // when file dimensions are larger


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
                        alertify.alert("Image dimensions should be equal to or more than 522(width) X 348(height)");
                    }
                }
                img.src = _URL.createObjectURL(this.files[0]);
            }
            else {
                alertify.alert("File size exceeded maximum limit (8 MB).");
            }
        }
        else {
            alertify.alert("File Invalid");
        }
    });
    // On edit popup file change
    document.querySelector('#file1').addEventListener('change', function () {
        var fileExtension = $(this).val().split('.')[$(this).val().split('.').length - 1].toLowerCase();
        var _URL = window.URL || window.webkitURL;
        var file, img;
        if (checkImageExtension(fileExtension)) {
            if (document.getElementById("file1").files[0].size <= 8000000) {
                file = this.files[0];
                img = new Image();
                img.onload = function (e) {
                    if (this.width == 522 && this.height == 348) {// file file has exact same dimensions
                        if (!imgCount) {
                            imgCount = 1;
                        }
                        else {
                            imgCount += 1;
                        }
                        if ($("#savedImageList > li").length + $("#cropList1 > div").length >= 4) {
                            $('#btnCrop1').prop('disabled', true);
                            $('#file1').prop('disabled', true);
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
                        document.querySelector('#cropList1').innerHTML += '<div id="div' + imgCount + '" class="cropImage-outer-div"><img name="NewsPhotoFile" id="photo' + imgCount + '" src="' + imageSource + '" class="croppedImage"><span><img src="/Content/imgs/trash.png" class="remove-icon" onclick="javascript:removePhotoEdit(' + imgCount + ')"></span><label style="display:none;" id="label' + imgCount + '">' + document.getElementById('file').value.substring(0, 10) + imgCount + '.jpg</label> </div>';
                    }
                    else if (this.width > 522 && this.height > 348) { // when file dimensions are larger
                        var options =
                   {
                       imageBox: '#imageBox',
                       thumbBox: '#thumbBox',
                       spinner: '#spinner'
                   }
                        var reader = new FileReader();
                        reader.onload = function (e) {
                            options.imgSrc = e.target.result;
                            cropperEdit = new cropbox(options);
                        }
                        reader.readAsDataURL(file);
                        this.files = [];
                    }
                    else {
                        alertify.alert("Image dimensions should be equal to or more than 522(width) X 348(height)");
                    }
                };
                img.src = _URL.createObjectURL(this.files[0]);
            }
            else {
                alertify.alert("File size exceeded maximum limit (8 MB).");
            }
        }
        else {
            alertify.alert("File Invalid");
        }
    });
    //crop button on edit photos popup functionality
    document.querySelector('#btnCrop').addEventListener('click', function () {
        if ($(".imageBox").css("background-image") != 'none') {
            var img = cropper.getDataURL();
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
            document.querySelector('#cropList').innerHTML += '<div id="div' + imgCount + '" class="cropImage-outer-div"><img name="NewsPhotoFile" id="photo' + imgCount + '" src="' + img + '" class="croppedImage"><span><img src="/Content/imgs/trash.png" class="remove-icon" onclick="javascript:removePhoto(' + imgCount + ')"></span><label style="display:none;" id="label' + imgCount + '">' + document.getElementById('file').value.substring(0, 10) + imgCount + '.jpg</label></div>';
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
    //crop button on edit photos popup functionality
    document.querySelector('#btnCrop1').addEventListener('click', function () {
        if ($("#imageBox").css("background-image") != 'none') {
            var img = cropperEdit.getDataURL();
            if (!imgCount) {
                imgCount = 1;
            }
            else {
                imgCount += 1;
            }
            if ($("#savedImageList > li").length + $("#cropList1 > div").length >= 4) {
                $('#btnCrop1').prop('disabled', true);
                $('#file1').prop('disabled', true);
            }
            document.querySelector('#cropList1').innerHTML += '<div id="div' + imgCount + '" class="cropImage-outer-div"><img name="NewsPhotoFile" id="photo' + imgCount + '" src="' + img + '" class="croppedImage"><span><img src="/Content/imgs/trash.png" class="remove-icon" onclick="javascript:removePhotoEdit(' + imgCount + ')"></span><label style="display:none;" id="label' + imgCount + '">' + document.getElementById('file').value.substring(0, 10) + imgCount + '.jpg</label> </div>';
        }
    })
    document.querySelector('#btnZoomIn1').addEventListener('click', function () {
        cropperEdit.zoomIn();
    })
    document.querySelector('#btnZoomOut1').addEventListener('click', function () {
        cropperEdit.zoomOut();
    })
};
function removePhoto(i) {
    $("#div" + i).hide('slow', function () { $("#div" + i).remove(); });
    enableFileButtonAdd();
}
function removePhotoEdit(i) {
    $("#div" + i).hide('slow', function () { $("#div" + i).remove(); });
    enableFileButtonEdit();
}
function enableFileButtonAdd() {
    $('#btnCrop').prop('disabled', false);
    $('#file').prop('disabled', false);
}
function enableFileButtonEdit() {
    $('#btnCrop1').prop('disabled', false);
    $('#file1').prop('disabled', false);
}