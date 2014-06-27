angularExample.app.controller(
		"clientCreateController",
		["$scope", "$location", "upida", function ($scope, $location, upida) {
	$scope.name = null;

	$scope.onSave = function () {
		var item = {};
		item.name = $scope.name;
		upida.post("client/save", item, $scope)
		.then(function () {
			$location.path("client/list");
		});
	};
}]);