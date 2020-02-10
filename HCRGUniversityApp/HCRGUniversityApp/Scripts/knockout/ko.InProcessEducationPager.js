//========== pager js for invoice grid only ========//
(function (ko) {
    var inumericObservable = function (initialValue) {
        var _iactual = ko.observable(initialValue);
        var iresult = ko.dependentObservable({
            read: function () {
                return _iactual();
            },
            write: function (newValue) {
                var iparsedValue = parseFloat(newValue);
                _iactual(isNaN(iparsedValue) ? newValue : iparsedValue);
            }
        });
        return iresult;
    };

    function iPager(totalItemCount) {
        var self = this;
        self.iCurrentPage = inumericObservable(1);
        self.iTotalItemCount = ko.computed(totalItemCount);

        self.iPageSize = inumericObservable(10);
        self.iPageSlide = inumericObservable(2);

        self.iLastPage = ko.computed(function () {
            return Math.floor((self.iTotalItemCount() - 1) / self.iPageSize()) + 1;
        });

        self.iHasNextPage = ko.computed(function () {
            return self.iCurrentPage() < self.iLastPage();
        });

        self.iHasPrevPage = ko.computed(function () {
            return self.iCurrentPage() > 1;
        });

        self.iFirstItemIndex = ko.computed(function () {
            return self.iPageSize() * (self.iCurrentPage() - 1) + 1;
        });

        self.iLastItemIndex = ko.computed(function () {
            return Math.min(self.iFirstItemIndex() + self.iPageSize() - 1, self.iTotalItemCount());
        });

        self.iPages = ko.computed(function () {
            var ipageCount = self.iLastPage();
            var ipageFrom = Math.max(1, self.iCurrentPage() - self.iPageSlide());
            var ipageTo = Math.min(ipageCount, self.iCurrentPage() + self.iPageSlide());
            ipageFrom = Math.max(1, Math.min(ipageTo - 2 * self.iPageSlide(), ipageFrom));
            ipageTo = Math.min(ipageCount, Math.max(ipageFrom + 2 * self.iPageSlide(), ipageTo));

            var iresult = [];
            for (var i = ipageFrom; i <= ipageTo; i++) {
                iresult.push(i);
            }
            return iresult;
        });
    }

    ko.ipager = function (totalItemCount) {
        var ipager = new iPager(totalItemCount);
        return ko.observable(ipager);
    };
}(ko));
//============= pager js for invoice ends  ============//