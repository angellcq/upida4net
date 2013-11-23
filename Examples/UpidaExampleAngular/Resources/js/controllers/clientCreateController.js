angularExample.app.controller(
		"clientCreateController",
		["$scope", "$location", "upidaService", function($scope, $location, upidaService) {
	$scope.name = null;

	$scope.onSave = function () {
		var item = {};
		item.Name = $scope.name;
		upidaService.post("client/save", item, $scope, function() {
			$location.path("client/list");
		});
	};
}]);