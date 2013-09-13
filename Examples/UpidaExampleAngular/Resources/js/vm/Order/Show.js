var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('orderShowController', ['$scope', '$http', function ($scope, $http) {
	$scope.id = upida.utils.query("id");
	$scope.client = null;
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;
	$scope.orderItemRows = new Array();
	$scope.products = [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}];
	$scope.editLink = upida.utils.url("order/edit?id=") + $scope.id;
	$scope.editItemsLink = upida.utils.url("order/edititems?id=") + $scope.id;
	$scope.clientsLink = upida.utils.url("client/index");
	$scope.indexLink = null;

	$scope.OrderItemRow = function () {
		this.product = null;
		this.count = null;
		this.price = null;
	};

	$scope.loadOrder = function () {
		upida.utils.get("order/getfull?id=" + $scope.id, function(item) {
			$scope.shipCountry = item.ShipCountry;
			$scope.shipCity = item.ShipCity;
			$scope.shipZip = item.ShipZip;
			$scope.shipAddress = item.ShipAddress;
			$scope.total = item.Total;
			$scope.client = item.Client.Name;
			$scope.indexLink = upida.utils.url("order/index?clientId=") + item.Client.Id;
			angular.forEach(item.OrderItems, function (p, i) {
				var row = new $scope.OrderItemRow();
				row.count = p.Count;
				row.price = p.Price;
				var product = upida.utils.find($scope.products, function(m) { return p.ProductId == m.id; });
				row.product = product.name;
				$scope.orderItemRows.push(row);
			});
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
		$scope.loadOrder();
	};
}]);