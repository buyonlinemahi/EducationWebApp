

//========== pager js for   Pro  grid only ========//
(function (ko) {
	var PronumericObservable = function (initialValue) {
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

	function ProPager(totalItemCount) {
		var self = this;
		self.ProCurrentPage = PronumericObservable(1);

		self.ProTotalItemCount = ko.computed(totalItemCount);

		self.ProPageSize = PronumericObservable(10);
		self.ProPageSlide = PronumericObservable(2);

		self.ProLastPage = ko.computed(function () {
			return Math.floor((self.ProTotalItemCount() - 1) / self.ProPageSize()) + 1;
		});

		self.ProHasNextPage = ko.computed(function () {
			return self.ProCurrentPage() < self.ProLastPage();
		});

		self.ProHasPrevPage = ko.computed(function () {
			return self.ProCurrentPage() > 1;
		});

		self.ProFirstItemIndex = ko.computed(function () {
			return self.ProPageSize() * (self.ProCurrentPage() - 1) + 1;
		});

		self.ProLastItemIndex = ko.computed(function () {
			return Math.min(self.ProFirstItemIndex() + self.ProPageSize() - 1, self.ProTotalItemCount());
		});

		self.ProPages = ko.computed(function () {
			var PropageCount = self.ProLastPage();
			var PropageFrom = Math.max(1, self.ProCurrentPage() - self.ProPageSlide());
			var PropageTo = Math.min(PropageCount, self.ProCurrentPage() + self.ProPageSlide());
			PropageFrom = Math.max(1, Math.min(PropageTo - 2 * self.ProPageSlide(), PropageFrom));
			PropageTo = Math.min(PropageCount, Math.max(PropageFrom + 2 * self.ProPageSlide(), PropageTo));

			var result = [];
			for (var i = PropageFrom; i <= PropageTo; i++) {
				result.push(i);
			}
			return result;
		});
	}

	ko.Propager = function (totalItemCount) {
		var Propager = new ProPager(totalItemCount);
		return ko.observable(Propager);
	};
}(ko));
//============= pager js for Pro ends  ============//








////========== pager js for invoice grid only ========//
//(function (ko) {
//	var PronumericObservable = function (initialValue) {
//		var _Proactual = ko.observable(initialValue);
//		var Proresult = ko.dependentObservable({
//			read: function () {
//				return _Proactual();
//			},
//			write: function (newValue) {
//				var ProparsedValue = parseFloat(newValue);
//				_Proactual(isNaN(ProparsedValue) ? newValue : ProparsedValue);
//			}
//		});
//		return Proresult;
//	};

//	function ProPager(totalItemCount) {
//		var self = this;
//		self.ProCurrentPage = PronumericObservable(1);
//		self.ProTotalItemCount = ko.computed(totalItemCount);

//		self.ProPageSize = PronumericObservable(10);
//		self.ProPageSlide = PronumericObservable(2);

//		self.ProLastPage = ko.computed(function () {
//			return Math.floor((self.ProTotalItemCount() - 1) / self.ProPageSize()) + 1;
//		});

//		self.ProHasNextPage = ko.computed(function () {
//			return self.ProCurrentPage() < self.ProLastPage();
//		});

//		self.ProHasPrevPage = ko.computed(function () {
//			return self.ProCurrentPage() > 1;
//		});

//		self.ProFirstItemIndex = ko.computed(function () {
//			return self.ProPageSize() * (self.ProCurrentPage() - 1) + 1;
//		});

//		self.ProLastItemIndex = ko.computed(function () {
//			return Math.min(self.ProFirstItemIndex() + self.ProPageSize() - 1, self.ProTotalItemCount());
//		});

//		self.ProPages = ko.computed(function () {
//			var PropageCount = self.ProLastPage();
//			var PropageFrom = Math.max(1, self.ProCurrentPage() - self.ProPageSlide());
//			var PropageTo = Math.min(PropageCount, self.ProCurrentPage() + self.ProPageSlide());
//			PropageFrom = Math.max(1, Math.min(PropageTo - 2 * self.ProPageSlide(), PropageFrom));
//			PropageTo = Math.min(PropageCount, Math.max(PropageFrom + 2 * self.ProPageSlide(), PropageTo));

//			var Proresult = [];
//			for (var i = PropageFrom; i <= PropageTo; i++) {
//				Proresult.push(i);
//			}
//			return Proresult;
//		});
//	}

//	ko.Propager = function (totalItemCount) {
//		var Propager = new ProPager(totalItemCount);
//		return ko.observable(Propager);
//	};
//}(ko));
////============= pager js for invoice ends  ============//