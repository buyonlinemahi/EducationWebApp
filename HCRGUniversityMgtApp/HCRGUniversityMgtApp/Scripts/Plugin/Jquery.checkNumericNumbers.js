
// code to check the numeric value entered in input fields

function isNumberKey(evt) {
    if (evt.which !== 0) {
        var charCode = (evt.which) ? evt.which : event.keyCode
        if (charCode > 31 && (charCode < 48 || charCode > 57))
            return false;
        else
            return true;
    }
    else {
        return true;
    }
}