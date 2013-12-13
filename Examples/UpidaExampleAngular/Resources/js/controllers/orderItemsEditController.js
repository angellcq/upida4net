angularExample.app.controller(
		'orderItemsEditController',
		["$scope", "$location", "$routeParams", "upida", function ($scope, $location, $routeParams, upida) {

	$scope.id = $routeParams.id;
	$scope.clientId = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}];

	$scope.OrderItemRow = function () {
		this.id = null;
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
		item.id = $scope.id;
		item.orderItems = new Array();
		angular.forEach($scope.orderItemRows, function (p, i) {
			var orderItem = {};
			orderItem.id = p.id;
			orderItem.productId = p.product ? p.product.id : undefined;
			orderItem.count = p.count;
			orderItem.price = p.price;
			item.orderItems.push(orderItem);
		});
		upida.post("order/updateitems", item, $scope)
		.then(function () {
			$location.path("order/show/" + $scope.id);
		});
	};

	$scope.loadOrder = function () {
		upida.get("order/getfull?id=" + $scope.id, $scope)
		.then(function (item) {
			$scope.clientId = item.client.id;
			angular.forEach(item.orderItems, function (p, i) {
				var row = new $scope.OrderItemRow();
				row.id = p.id;
				row.count = p.count;
				row.price = p.price;
				row.product = upida.find($scope.products, function (m) { return p.productId == m.id; });
				$scope.orderItemRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadOrder();
	});
}]);