ko.bindingHandlers.ajaxForm = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).ajaxForm(value);
    }
};

ko.bindingHandlers.datepicker = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).datepicker(value);
    }
};

ko.bindingHandlers.jqBootstrapValidation = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).jqBootstrapValidation(value);
    }
};

ko.bindingHandlers.jqBootstrapCheckboxGroupValidation = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).find("input[type='checkbox']").jqBootstrapValidation(value);
    }
};

ko.bindingHandlers.bootstrapButton = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).button();
    },
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var disable = ko.utils.unwrapObservable(valueAccessor());
        if (disable)
            $(element).button('loading');
        else
            $(element).button('reset');
    }
};

ko.bindingHandlers.typeahead = {
    init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).typeahead({
            name: value.name,
            valueKey: value.valueKey,
            remote: value.remote
        }).on('typeahead:selected', function (evt, data) {
            if (value.putEntireDataToSelectedItem)
                viewModel[value.saveSelectedValueTo](data);
            else
                viewModel[value.saveSelectedValueTo](data[value.valueID]);
        });
    }
};

ko.bindingHandlers.inputmask = {
    update: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
        var value = ko.utils.unwrapObservable(valueAccessor());
        $(element).inputmask(value);
    }
};

ko.extenders.formatDate = function (target, formatString) {
    var result = ko.computed({
        read: function () {
            return target();
        },
        write: function (newValue) {
            var current = target();
            var valueToWrite = moment(newValue).format(formatString);
            if (valueToWrite !== current) {
                target(valueToWrite);
            }
            else {
                if (newValue !== current) {
                    target.notifySubscribers(valueToWrite);
                }
            }
        }
    });

    //initialize with current value  
    result(target());
    return result;
};
ko.bindingHandlers.sort = {
    init: function (element, valueAccessor) {
        var asc = false;

        element.style.cursor = 'pointer';

        element.onclick = function () {
            var value = valueAccessor();
            var prop = value.prop;
            var data = value.arr;

            asc = !asc;

            data.sort(function (left, right) {
                var rec1 = left;
                var rec2 = right;

                if (!asc) {
                    rec1 = right;
                    rec2 = left;
                }

                var props = prop.split('.');
                for (var i in props) {
                    var propName = props[i];
                    var parenIndex = propName.indexOf('()');
                    if (parenIndex > 0) {
                        propName = propName.substring(0, parenIndex);
                        rec1 = rec1[propName]();
                        rec2 = rec2[propName]();
                    } else {
                        rec1 = rec1[propName];
                        rec2 = rec2[propName];
                    }
                }

                return rec1 == rec2 ? 0 : rec1 < rec2 ? -1 : 1;
            });
        };
    }
};