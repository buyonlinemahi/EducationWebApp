ko.extenders.numeric = function (target, precision) {
    var result = ko.computed({
        read: target,
        write: function (newValue) {
            target(isNaN(newValue) || (newValue == "") ? '' : parseFloat(Math.round(newValue * 100) / 100).toFixed(precision));
        }
    }).extend({ notify: 'always' });
    result(target());
    return result;
};