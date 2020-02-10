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
ko.bindingHandlers.enableAll = {
    update: function (elem, valueAccessor) {
        var enabled = ko.utils.unwrapObservable(valueAccessor());
        ko.utils.arrayForEach(elem.getElementsByTagName('select'), function (i) {
            i.disabled = !enabled;
        });

     
    }
};