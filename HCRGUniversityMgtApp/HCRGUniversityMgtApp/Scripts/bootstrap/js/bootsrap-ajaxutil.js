var ajaUtildefaultContentType = "application/x-www-form-urlencoded; charset=UTF-8";
var AjaxUtil = {
    post: function (_url, _dataType, _data, _contentType, _cache) {
        return $.ajax({
            url: _url,
            type: 'post', //'post',
            data: _data,
            dataType: _dataType,
            cache: _cache,
            contentType: (typeof _contentType === 'undefined') ? ajaUtildefaultContentType : _contentType
        });
    },

    get: function (_url, _dataType, _data, _contentType, _cache) {
        return $.ajax({
            url: _url,
            type: 'get', //'get',
            data: _data,
            dataType: _dataType,
            cache: _cache,
            contentType: (typeof _contentType === 'undefined') ? ajaUtildefaultContentType : _contentType
        });
    }
}