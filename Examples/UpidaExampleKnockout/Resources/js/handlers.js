$(function () {
	ko.bindingHandlers.idtext = {
		update: function (element, valueAccessor, allBindingsAccessor) {
			var text = ko.utils.unwrapObservable(valueAccessor());
			if(text) {
				text = "000000" + text;
				text = text.slice(-6);
			}

			element.innerHTML = text;
		},

		init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
		}
	};

	ko.bindingHandlers.date = {
		update: function (element, valueAccessor, allBindingsAccessor) {
			var ticks = ko.utils.unwrapObservable(valueAccessor());
			if(ticks) {
				element.innerHTML = $upida.formatDate(ticks);
			}
		},

		init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
		}
	};

	ko.bindingHandlers.datepicker = {
		update: function (element, valueAccessor, allBindingsAccessor) {
			var date = ko.utils.unwrapObservable(valueAccessor());
			if(date != undefined) {
				$(element).datepicker("setDate", date);
			}
		},
		init: function (element, valueAccessor, allBindingsAccessor, viewModel, bindingContext) {
			$(element).datepicker();
			$(element).datepicker("option", "dateFormat", "dd MM yy");
			ko.utils.registerEventHandler(element, "change", function () {
				var observable = valueAccessor();
				observable($(element).datepicker("getDate"));
			});
		}
	};

	$upida.formatDate = function(ticks) {
		var date = new Date(ticks);
		var day = date.getDate();
		var hour = date.getHours();
		var month = date.getMonth();
		var minute = date.getMinutes();
		if(day < 10) { day = "0" + day; }
		if(month < 10) { month = "0" + month; }
		if(hour < 10) { hour = "0" + hour; }
		if(minute < 10) { minute = "0" + minute; }
		return day + "-" + month + "-" + date.getFullYear() + " " + hour + ":" + minute;
	};
});