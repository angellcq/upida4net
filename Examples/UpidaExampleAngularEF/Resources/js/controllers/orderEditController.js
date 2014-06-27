angularExample.app.controller(
		"orderEditController",
		["$scope", "$location", "$routeParams", "upida", function ($scope, $location, $routeParams, upida) {

	$scope.id = $routeParams.id;
	$scope.clientId = null;
	$scope.clientName = null;
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

		upida.post("order/update", item, $scope)
		.then(function () {
			$location.path("order/show/" + $scope.id);
		});
	};

	$scope.loadOrder = function () {
		upida.get("order/get?id=" + $scope.id, $scope)
		.then(function (item) {
			$scope.shipCountry = item.shipCountry;
			$scope.shipCity = item.shipCity;
			$scope.shipZip = item.shipZip;
			$scope.shipAddress = item.shipAddress;
			$scope.total = item.total;
			$scope.clientId = item.client.id;
			$scope.clientName = item.client.name;
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadOrder();
	});
}]);