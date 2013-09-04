var app = angular.module("app", []);
upida.directives.errorkeyDirective(app);
app.controller('orderCreateController', ['$scope', function($scope) {
	$scope.clientId = upida.utils.query("clientId"),
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
	$scope.clientsLink = upida.utils.url("client/index"),
	$scope.indexLink = upida.utils.url("order/index?clientId=") + $scope.clientId;

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
		item.Client = upida.utils.getReff($scope.clientId);
		item.ShipCountry = $scope.shipCountry;
		item.ShipCity = $scope.shipCity;
		item.ShipZip = $scope.shipZip;
		item.ShipAddress = $scope.shipAddress;
		item.Total = $scope.total;
		item.OrderItems = new Array();
		$.each($scope.orderItemRows, function (i, p) {
			var orderItem = {};
			orderItem.ProductId = p.product ? p.product.id : undefined;
			orderItem.Count = p.count;
			orderItem.Price = p.price;
			item.OrderItems.push(orderItem);
		});
		upida.utils.post("order/save", item, function() {
			upida.utils.navigate("order/index?clientId=" + $scope.clientId);
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope);
		$scope.addProduct();
	};
}]);