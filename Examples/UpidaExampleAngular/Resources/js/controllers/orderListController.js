angularExample.app.controller(
		"orderListController",
		["$scope", "$routeParams", "upidaService", function ($scope, $routeParams, upidaService) {

	$scope.clientId = $routeParams.clientId;
	$scope.orderRows = new Array();

	$scope.OrderRow = function (id) {
		this.id = id;
		this.createdOn = null;
		this.shipCountry = null;
		this.shipCity = null;
		this.shipZip = null;
		this.shipAddress = null;
		this.total = null;
	};

	$scope.loadOrders = function() {
		upidaService.get("order/getbyclient?clientId=" + $scope.clientId, $scope, function (items) {
			angular.forEach(items, function (p, i) {
				var row = new $scope.OrderRow(p.id);
				row.createdOn = p.createdOn;
				row.shipCountry = p.shipCountry;
				row.shipCity = p.shipCity;
				row.shipZip = p.shipZip;
				row.shipAddress = p.shipAddress;
				row.total = p.total;
				$scope.orderRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadOrders();
	});
}]);