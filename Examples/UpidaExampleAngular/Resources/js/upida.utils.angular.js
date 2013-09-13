var upida = upida || {};
upida.utils = upida.utils || {};

upida.utils.url = function(link) {
	return "/" + link;
};

upida.utils.navigate = function(link) {
	window.location.replace(upida.utils.url(link));
};

upida.utils.post = function(method, input, callback) {
	upida.utils.ajaxStart();
	upida.$http({
		method: 'POST',
		url: "/api" + upida.utils.url(method),
		data: input
	})
	.success(function(data, status, headers, config) {
		upida.utils.clearErrors();
		callback(data);
		upida.utils.ajaxEnd();
	})
	.error(function(data, status, headers, config) {
		upida.utils.ajaxEnd();
		upida.utils.showErrors(data);
	});
};

upida.utils.get = function(method, callback) {
	upida.utils.ajaxStart();
	upida.$http({
		method: 'GET',
		url: "/api" + upida.utils.url(method)
	})
	.success(function(data, status, headers, config) {
		upida.utils.clearErrors();
		callback(data);
		upida.utils.ajaxEnd();
	})
	.error(function(data, status, headers, config) {
		upida.utils.ajaxEnd();
		upida.utils.showErrors(data);
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

upida.utils.Lookup = function(id, name, version) {
	this.id = id;
	this.name = name;
	this.version = version;
};

upida.utils.getReff = function(id, version) {
	if(!upida.utils.isEmpty(id)) {
		return {Id: id, Version: version};
	}

	return null;
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
		// Lock screen
	}

	upida.utils.ajaxCallCount++;
};

upida.utils.ajaxEnd = function() {
	upida.utils.ajaxCallCount--;
	if(0 == upida.utils.ajaxCallCount) {
		setTimeout(function () {
			if(0 == upida.utils.ajaxCallCount) {
				// Unlock screen
			}
		}, 200);
	}
};

upida.$scope = null;
upida.$http = null;
upida.utils.bind = function(scope, http) {
	upida.$scope = scope;
	upida.$http = http;
	upida.$scope.errors = new Array();
};

upida.utils.showErrors = function(fail) {
	var errors = new Array();
	angular.forEach(fail.Failures, function (p, i) {
		var current = upida.utils.find(upida.$scope.errors, function(m) { return m.Key == p.Key; });
		if(current) {
			current.Text = current.Text + "<br />" + p.Text;
		}
		else {
			upida.$scope.errors.push(p);
		}
	});

	upida.$scope.errors = errors;
};

upida.utils.clearErrors = function(fail) {
	upida.$scope.errors = new Array();
};

upida.utils.find = function (obsArray, isItemFunc) {
	var foundItem = null;
	angular.forEach(obsArray, function (item, i) {
		if (true == isItemFunc(item)) {
			foundItem = item;
			return;
		}
	});

	return foundItem;
};

upida.utils.errorkeyDirective = function (app) {
	app.directive("errorkey", function () {
		return {
			restrict: 'A',
			link: function(scope, element, attrs) {
				scope.$watch('errors', function(oldVal, errors) {
					element.text("");
					var key = attrs.errorkey;
					if(errors) {
						angular.forEach(errors, function (p, i) {
							if(key == p.Key) {
								element.text(p.Text);
							}
						});
					}
				});
			}
		};
	});
};