var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('orderEditItemsController', ['$scope', '$http', function ($scope, $http) {
	$scope.id = upida.utils.query("id");
	$scope.clientId = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}];
	$scope.clientsLink = upida.utils.url("client/index");
	$scope.indexLink = null;

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
		item.Id = $scope.id;
		item.OrderItems = new Array();
		angular.forEach($scope.orderItemRows, function (p, i) {
			var orderItem = {};
			orderItem.Id = p.id;
			orderItem.ProductId = p.product ? p.product.id : undefined;
			orderItem.Count = p.count;
			orderItem.Price = p.price;
			item.OrderItems.push(orderItem);
		});
		upida.utils.post("order/updateitems", item, function() {
			upida.utils.navigate("order/show?id=" + $scope.id);
		});
	};

	$scope.loadOrder = function () {
		upida.utils.get("order/getfull?id=" + $scope.id, function(item) {
			$scope.clientId = item.Client.Id;
			$scope.indexLink = upida.utils.url("order/index?clientId=") + item.Client.Id;
			angular.forEach(item.OrderItems, function (p, i) {
				var row = new $scope.OrderItemRow();
				row.id = p.Id;
				row.count = p.Count;
				row.price = p.Price;
				row.product = upida.utils.find($scope.products, function(m) { return p.ProductId == m.id; });
				$scope.orderItemRows.push(row);
			});
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
		$scope.loadOrder();
	};
}]);