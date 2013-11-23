angularExample.app.controller(
		"orderEditController",
		["$scope", "$location", "$routeParams", "upidaService", function ($scope, $location, $routeParams, upidaService) {

	$scope.id = $routeParams.id;
	$scope.clientId = null;
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;

	$scope.onSave = function () {
		var item = {};
		item.id = $scope.id;
		item.shipCountry = $scope.shipCountry;
		item.shipCity = $scope.shipCity;
		item.shipZip = $scope.shipZip;
		item.shipAddress = $scope.shipAddress;
		item.total = $scope.total;

		upidaService.post("order/update", item, $scope, function() {
			$location.path("order/view/" + $scope.id);
		});
	};

	$scope.loadOrder = function () {
		upidaService.get("order/get?id=" + $scope.id, $scope, function(item) {
			$scope.shipCountry = item.shipCountry;
			$scope.shipCity = item.shipCity;
			$scope.shipZip = item.shipZip;
			$scope.shipAddress = item.shipAddress;
			$scope.total = item.total;
			$scope.clientId = item.client.id;
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadOrder();
	});
}]);