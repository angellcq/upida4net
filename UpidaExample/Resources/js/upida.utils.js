var upida = upida || {};

$(function () {
	upida.utils = {};
	upida.utils.url = function(link) {
		return "/" + link;
	};

	upida.utils.post = function(method, input, callback) {
		upida.utils.ajaxStart();
		$.ajax({
			url: upida.utils.url("api/" + method),
			type: "POST",
			data: ko.toJSON(input),
			contentType: "application/json",
			success: function (output, status, xhr) {
				upida.utils.clearErrors();
				callback(output);
				upida.utils.ajaxEnd();
			},
			error: function (xhr, sttaus, err) {
				upida.utils.ajaxEnd();
				var fail = JSON.parse(xhr.responseText);
				upida.utils.showErrors(fail);
			}
		});
	};

	upida.utils.get = function(method, callback) {
		upida.utils.ajaxStart();
		$.ajax({
		    url: upida.utils.url("api/" + method),
			type: "GET",
			success: function (output, status, xhr) {
				upida.utils.clearErrors();
				callback(output);
				upida.utils.ajaxEnd();
			},
			error: function (xhr, sttaus, err) {
				upida.utils.ajaxEnd();
				var fail = JSON.parse(xhr.responseText);
				upida.utils.showErrors(fail);
			}
		});
	};

	upida.utils.query = function(name)
	{
		name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
		var regexS = "[\\?&]" + name + "=([^&#]*)";
		var regex = new RegExp(regexS);
		var results = regex.exec(window.location.search);
		if(results == null)
			return "";
		else
			return decodeURIComponent(results[1].replace(/\+/g, " "));
	};

	upida.utils.Lookup = function(id, name) {
		this.id = ko.observable(id);
		this.name = ko.observable(name);
	};

	upida.utils.getReff = function(id) {
		return {Id: id};
	};

	upida.utils.isEmpty = function (val) {
		return undefined === val || null == val || "" === val;
	};
	
	upida.utils.ajaxCallCount = 0;
	upida.utils.ajaxCallback = function(callback) {
		setTimeout(function () {
			if (0 == upida.utils.ajaxCallCount) {
				callback();
			}
			else {
				upida.utils.ajaxCallback(callback);
			}
		}, 100);
	};

	upida.utils.ajaxStart = function() {
		if(0 == upida.utils.ajaxCallCount) {
			$.blockUI({ css: {
				border: 'none',
				padding: '15px',
				backgroundColor: '#000',
				'-webkit-border-radius': '10px',
				'-moz-border-radius': '10px',
				opacity: .3,
				color: '#fff'
			},
			ignoreIfBlocked: true });
		}

		upida.utils.ajaxCallCount++;
	};

	upida.utils.ajaxEnd = function() {
		upida.utils.ajaxCallCount--;
		if(0 == upida.utils.ajaxCallCount) {
			setTimeout(function () {
				if(0 == upida.utils.ajaxCallCount) {
					$.unblockUI();
				}
			}, 200);
		}
	};

	upida.vm = null;
	upida.utils.bind = function (vm) {
		upida.vm = vm;
		upida.vm.errors = ko.observableDictionary();
		ko.applyBindings(vm);
	};

	upida.utils.showErrors = function(fail) {
		upida.vm.errors.removeAll();
		$.each(fail.Failures, function (i, p) {
			var current = upida.vm.errors.get(p.Key);
			if(current()) {
				current(current() + "<br />" + p.Text);
			}
			else {
				upida.vm.errors.push(p.Key, p.Text);
			}
		});
	};

	upida.utils.clearErrors = function(fail) {
		if(upida.vm.errors) {
			upida.vm.errors.removeAll();
		}
	};

	upida.utils.find = function (obsArray, isItemFunc) {
		var foundItem = null;
		$.each(obsArray, function (i, item) {
			if (true == isItemFunc(item)) {
				foundItem = item;
				return;
			}
		});
		
		return foundItem;
	};
});