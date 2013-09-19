var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('clientCreateController', ['$scope', '$http', function ($scope, $http) {
	$scope.name = null;
	$scope.indexLink = upida.utils.url("client/index");

	$scope.onSave = function () {
		var item = {};
		item.Name = $scope.name;
		upida.utils.post("client/save", item, function() {
			upida.utils.navigate("client/index");
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
	};
}]);