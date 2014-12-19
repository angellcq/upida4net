var $upida = $upida || {};
$upida.settings = {};
$upida.settings.baseUrl = "/";
$upida.settings.errorLine = "<br />";

$upida.module = angular.module("upidamodule", []);
$upida.module.factory("upida", ["$http", "$q", function ($http, $q) {
	var service = { onBeforeAjax: null, onAfterAjax: null };

	service.url = function(link) {
	  return $upida.settings.baseUrl + link;
	};

	service.post = function(method, input, $scope) {
		service.ajaxStart();
		var deferred = $q.defer();
		$http({
			method: 'POST',
			url: service.url(method),
			data: input
		})
		.success(function(data, status) {
			service.clearErrors($scope);
			deferred.resolve(data);
			service.ajaxEnd();
		})
		.error(function(data, status) {
			deferred.reject();
			service.ajaxEnd();
			service.showErrors($scope, data);
		});
		return deferred.promise;
	};

	service.get = function(method, $scope) {
		service.ajaxStart();
		var deferred = $q.defer();
		$http({
			method: 'GET',
			url: service.url(method)
		})
		.success(function(data, status) {
			service.clearErrors($scope);
			deferred.resolve(data);
			service.ajaxEnd();
		})
		.error(function(data, status) {
			deferred.reject();
			service.ajaxEnd();
			service.showErrors($scope, data);
		});
		return deferred.promise;
	};

	service.all = function(promises) {
		return $q.all(promises);
	};

	service.ajaxCallCount = 0;

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
		angular.forEach(fail.failures, function (p, i) {
			var current = service.find(errors, function(m) { return m.key == p.key; });
			if(current) {
				current.text = current.text + $upida.settings.errorLine + p.text;
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
}]);

$upida.module.directive("upErrorBody", function () {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			scope.$watch('$$errors', function (errors, oldVal) {
				element.html("");
				if (!errors) return;
				var key = attrs.upErrorKey;
				if (errors) {
					for (var i = 0; i < errors.length; i++) {
						if (key) {
							if (key == errors[i].key) {
								element.html(errors[i].text);
								break;
							}
						}
						else {
							if (!errors[i].key) {
								element.html(errors[i].text);
								break;
							}
						}
					}
				}
			});
		}
	};
});

$upida.module.directive("upErrorCss", function () {
	return {
		restrict: 'A',
		link: function (scope, element, attrs) {
			scope.$watch('$$errors', function (errors, oldVal) {
				if (!errors) return;
				var key = attrs.upErrorKey;
				var cssClass = attrs.upErrorCss;
				element.removeClass(cssClass);
				if (errors) {
					for (var i = 0; i < errors.length; i++) {
						if (key) {
							if (key == errors[i].key) {
								element.addClass(cssClass);
								break;
							}
						}
						else {
							if (!errors[i].key) {
								element.addClass(cssClass);
								break;
							}
						}
					}
				}
			});
		}
	};
});