function PrivacyPolicyLegalViewModel() {
    var self = this;

    $("#btntermsconditions").click(function () {
        $("#DivPrivacyPolicyLegalPopUp").html('');
        $.ajax({
            type: "POST",
            url: "/Resource/TermsCondition",
            contentType: "application/json; charset=utf-8",          
            success: function (responseText) {                
                $("#DivPrivacyPolicyLegalPopUp").html(responseText.TermsConditionContent);
            }
        })
    });

    $("#btnprivacypolicy").click(function () {
        $("#DivPrivacyPolicyLegalPopUp").html('');
        $.ajax({
            type: "POST",
            url: "/Resource/PrivacyPolicy",
            contentType: "application/json; charset=utf-8",
            success: function (responseText) {              
                $("#DivPrivacyPolicyLegalPopUp").html(responseText.PrivacyPolicyContent);
            }
        })
    });

    //$("#btnPolicyOpenPopUp").click(function () {
    //    $("#DivPrivacyPolicyLegalPopUp").html('');
    //    $.ajax({
    //        type: "POST",
    //        url: "/Resource/Policy",
    //        contentType: "application/json; charset=utf-8",
    //        success: function (responseText) { 
    //            $("#DivPrivacyPolicyLegalPopUp").html(responseText.PolicyContent);
    //        }
    //    })
    //});

}