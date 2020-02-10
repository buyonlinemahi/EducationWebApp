function NewUsersViewModel() {
    var self = this;
    self.NewUsersList = ko.observableArray([]);    
    self.uTotalItemCount = ko.observable(0);
    
    self.getNewUsers = function (_skip) {
        $.post("/User/GetNewUsers", { skip: _skip }, function (_data) {
            var model = $.parseJSON(_data);            
            ko.mapping.fromJS(model.Users, {}, self.NewUsersList);
            self.uTotalItemCount(model.TotalCount);

        });
    };
    self.getNewUsers(0);

    self.clearUser = function (_uid) {
        
        alertify.confirm('Clear this record?', function (data) {
            if (data)
            {
                $.post("/User/ClearUser", { uid: _uid() }, function (_data) {
                    alertify.success(_data);
                    self.getNewUsers(self.uSkip());
                });
            }
        });
    };

    //========= paging code for new user grid only ===========//

    self.uGetRecordsWithSkipTake = function (skip, take) {        
        if (skip == undefined || take == undefined) {
            self.uSkip(0);
            self.uTake(upagingSetting.upageSize);
        }
        else {
            self.uSkip(skip);
            self.uTake(take);
        }
        self.getNewUsers(self.uSkip());
    }

    var upagingSetting = {
        upageSize: 10,
        upageSlide: 2
    };
    self.uSkip = ko.observable(0);
    self.uTake = ko.observable(upagingSetting.upageSize);
    self.uPager = ko.upager(self.uTotalItemCount);

    self.uPager().uPageSize(upagingSetting.upageSize);
    self.uPager().uPageSlide(upagingSetting.upageSlide);
    self.uPager().uCurrentPage(1);

    self.uPager().uCurrentPage.subscribe(function () {
        var skip = upagingSetting.upageSize * (self.uPager().uCurrentPage() - 1);
        var take = upagingSetting.upageSlide;
        self.uGetRecordsWithSkipTake(skip, take);
    });

   
}




//========== pager js for new user grid only ========//
(function (ko) {
    var unumericObservable = function (initialValue) {
        var _actual = ko.observable(initialValue);

        var result = ko.dependentObservable({
            read: function () {
                return _actual();
            },
            write: function (newValue) {
                var parsedValue = parseFloat(newValue);
                _actual(isNaN(parsedValue) ? newValue : parsedValue);
            }
        });

        return result;
    };

    function uPager(totalItemCount) {
        var self = this;
        self.uCurrentPage = unumericObservable(1);

        self.uTotalItemCount = ko.computed(totalItemCount);

        self.uPageSize = unumericObservable(10);
        self.uPageSlide = unumericObservable(2);

        self.uLastPage = ko.computed(function () {
            return Math.floor((self.uTotalItemCount() - 1) / self.uPageSize()) + 1;
        });

        self.uHasNextPage = ko.computed(function () {
            return self.uCurrentPage() < self.uLastPage();
        });

        self.uHasPrevPage = ko.computed(function () {
            return self.uCurrentPage() > 1;
        });

        self.uFirstItemIndex = ko.computed(function () {
            return self.uPageSize() * (self.uCurrentPage() - 1) + 1;
        });

        self.uLastItemIndex = ko.computed(function () {
            return Math.min(self.uFirstItemIndex() + self.uPageSize() - 1, self.uTotalItemCount());
        });

        self.uPages = ko.computed(function () {
            var upageCount = self.uLastPage();
            var upageFrom = Math.max(1, self.uCurrentPage() - self.uPageSlide());
            var upageTo = Math.min(upageCount, self.uCurrentPage() + self.uPageSlide());
            upageFrom = Math.max(1, Math.min(upageTo - 2 * self.uPageSlide(), upageFrom));
            upageTo = Math.min(upageCount, Math.max(upageFrom + 2 * self.uPageSlide(), upageTo));

            var result = [];
            for (var i = upageFrom; i <= upageTo; i++) {
                result.push(i);
            }
            return result;
        });
    }

    ko.upager = function (totalItemCount) {
        var upager = new uPager(totalItemCount);
        return ko.observable(upager);
    };
}(ko));
//============= pager js for new user ends  ============//