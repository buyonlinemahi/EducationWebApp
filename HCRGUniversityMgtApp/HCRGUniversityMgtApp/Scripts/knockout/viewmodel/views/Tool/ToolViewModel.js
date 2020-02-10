function ToolViewModel(model) {
    var self = this;
  
    self.inputdata = ko.observable();
    self.EncryptedText = ko.observable();

    self.ConvertFieldValue = function () {
        $.ajax({
            url: "/Tool/Index",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({  _InputData:  $('#txtInput').val() }),
            cache: false, 
            success: function (_model) {
                $('#txtEncryptedText').val(_model)
            }
        });
        
    }

    self.DecryptFieldValue = function () {
        $.ajax({
            url: "/Tool/DecryptValues",
            type: 'post',
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify({ _InputData: $('#txtEncryptedText').val() }),
            cache: false,
            success: function (_model) {
                $('#txtDecryptText').val(_model)
            }
        });
      
    }


}


