var app = angular.module("app", []);
upida.directives.errorkeyDirective(app);
app.controller('orderIndexController', ['$scope', function($scope) {
	$scope.clientId = upida.utils.query("clientId");
	$scope.orderRows = new Array();
	$scope.clientsLink = upida.utils.url("client/index");
	$scope.createLink = upida.utils.url("order/create?clientId=") + $scope.clientId;

	$scope.OrderRow = function (id) {
		this.id = id;
		this.createdOn = null;
		this.shipCountry = null;
		this.shipCity = null;
		this.shipZip = null;
		this.shipAddress = null;
		this.total = null;
		this.viewLink = upida.utils.url("order/show?id=") + this.id;
		this.editLink = upida.utils.url("order/edit?id=") + this.id;
		this.editItemsLink = upida.utils.url("order/edititems?id=") + this.id;
	};

	$scope.loadOrders = function() {
		upida.utils.get("order/getbyclient?clientId=" + $scope.clientId, function (items) {
			$.each(items, function (i, p) {
				var row = new $scope.OrderRow(p.Id);
				row.createdOn = p.CreatedOn;
				row.shipCountry = p.ShipCountry;
				row.shipCity = p.ShipCity;
				row.shipZip = p.ShipZip;
				row.shipAddress = p.ShipAddress;
				row.total = p.Total;
				$scope.orderRows.push(row);
			});
			$scope.$apply();
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope);
		$scope.loadOrders();
	};
}]);