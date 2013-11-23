angularExample.app.controller(
		"orderViewController",
		["$scope", "$routeParams", "upidaService", function ($scope, $routeParams, upidaService) {

	$scope.id = $routeParams.id;
	$scope.client = null;
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}];

	$scope.OrderItemRow = function () {
		this.product = null;
		this.count = null;
		this.price = null;
	};

	$scope.loadOrder = function () {
		upidaService.get("order/getfull?id=" + $scope.id, $scope, function(item) {
			$scope.shipCountry = item.shipCountry;
			$scope.shipCity = item.shipCity;
			$scope.shipZip = item.shipZip;
			$scope.shipAddress = item.shipAddress;
			$scope.total = item.total;
			$scope.client = item.client.name;
			$scope.clientId = item.client.id;
			angular.forEach(item.orderItems, function (p, i) {
				var row = new $scope.OrderItemRow();
				row.count = p.count;
				row.price = p.price;
				var product = upidaService.find($scope.products, function(m) { return p.productId == m.id; });
				row.product = product.name;
				$scope.orderItemRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadOrder();
	});
}]);