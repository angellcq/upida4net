angularExample.app.controller(
		"orderCreateController",
		["$scope", "$location", "$routeParams", "upida", function ($scope, $location, $routeParams, upida) {

	$scope.clientId = $routeParams.clientId;
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],

	$scope.OrderItemRow = function () {
		this.product = null;
		this.count = 1;
		this.price = "0.0";
	};

	$scope.removeProduct = function (item) {
		var index = $scope.orderItemRows.indexOf(item);
		$scope.orderItemRows.splice(index, 1);
	};

	$scope.addProduct = function () {
		var item = new $scope.OrderItemRow();
		$scope.orderItemRows.push(item);
	};

	$scope.onSave = function () {
		var item = {};
		item.client = upida.getReff($scope.clientId);
		item.shipCountry = $scope.shipCountry;
		item.shipCity = $scope.shipCity;
		item.shipZip = $scope.shipZip;
		item.shipAddress = $scope.shipAddress;
		item.total = $scope.total;
		item.orderItems = new Array();
		angular.forEach($scope.orderItemRows, function (p, i) {
			var orderItem = {};
			orderItem.productId = p.product ? p.product.id : undefined;
			orderItem.count = p.count;
			orderItem.price = p.price;
			item.orderItems.push(orderItem);
		});
		upida.post("order/save", item, $scope)
		.then(function () {
			$location.path("order/list/" + $scope.clientId);
		});
	};

	$scope.addProduct();
}]);