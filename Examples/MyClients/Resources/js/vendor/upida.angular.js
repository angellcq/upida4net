var upida = upida || {};
upida.baseUrl = "/";

upida.serviceFactory = function($http) {
	var service = { onBeforeAjax: null, onAfterAjax: null };
	service.$http = $http;

	service.url = function(link) {
		return upida.baseUrl + link;
	};

	service.navigate = function(link) {
		window.location.replace(service.url(link));
	};

	service.getReff = function (id, version) {
		if (!service.isEmpty(id)) {
			return { id: id, version: version };
		}
	};

	service.isEmpty = function (val) {
		return undefined === val || null == val || "" === val;
	};

	service.post = function(method, input, $scope, callback) {
		service.ajaxStart();
		service.$http({
			method: 'POST',
			url: service.url(method),
			data: input
		})
		.success(function(data, status, headers, config) {
			service.clearErrors($scope);
			callback(data);
			service.ajaxEnd();
		})
		.error(function(data, status, headers, config) {
			service.ajaxEnd();
			service.showErrors($scope, data);
		});
	};

	service.get = function(method, $scope, callback) {
		service.ajaxStart();
		service.$http({
			method: 'GET',
			url: service.url(method)
		})
		.success(function(data, status, headers, config) {
			service.clearErrors($scope);
			callback(data);
			service.ajaxEnd();
		})
		.error(function(data, status, headers, config) {
			service.ajaxEnd();
			service.showErrors($scope, data);
		});
	};

	service.ajaxCallCount = 0;
	service.ajaxCallback = function(callback) {
		setTimeout(function () {
			if (0 == service.ajaxCallCount) {
				callback();
			}
			else {
				service.ajaxCallback(callback);
			}
		}, 100);
	};

	service.ajaxStart = function() {
		if(0 == service.ajaxCallCount) {
			if(service.onBeforeAjax) service.onBeforeAjax();
		}
	
		service.ajaxCallCount++;
	};

	service.ajaxEnd = function() {
		service.ajaxCallCount--;
		if (0 == service.ajaxCallCount) {
			setTimeout(function () {
				if (0 == service.ajaxCallCount) {
					if (service.onAfterAjax) service.onAfterAjax();
				}
			}, 200);
		}
	};

	service.showErrors = function($scope, fail) {
		if(!$scope) return;
		var errors = new Array();
		errors.main = fail.main;
		angular.forEach(fail.failures, function (p, i) {
			var current = service.find(errors, function(m) { return m.key == p.key; });
			if(current) {
				current.text = current.text + "<br />" + p.text;
			}
			else {
				errors.push(p);
			}
		});
		$scope.$$errors = errors;
	};

	service.clearErrors = function($scope) {
		if(!$scope) return;
		$scope.$$errors = new Array();
		$scope.$$errors.main = "";
	};

	service.find = function (obsArray, isItemFunc) {
		var foundItem = null;
		angular.forEach(obsArray, function (item, i) {
			if (true == isItemFunc(item)) {
				foundItem = item;
				return;
			}
		});
	
		return foundItem;
	};

	return service;
};

upida.mainErrorDirectiveFactory = function(app) {
	app.directive("mainerror", function () {
		return {
			restrict: 'A',
			link: function(scope, element, attrs) {
				scope.$watch('$$errors', function(errors, oldVal) {
					if(!errors) return;
					element.text(errors.main);
				});
			}
		};
	});
};

upida.errorKeyDirectiveFactory = function(app) {
	app.directive("errorkey", function () {
		return {
			restrict: 'A',
			link: function(scope, element, attrs) {
				scope.$watch('$$errors', function (errors, oldVal) {
					element.text("");
					if(!errors) return;
					var key = attrs.errorkey;
					if(errors) {
						angular.forEach(errors, function (p, i) {
							if(key == p.key) {
								element.html(p.text);
							}
						});
					}
				});
			}
		};
	});
};