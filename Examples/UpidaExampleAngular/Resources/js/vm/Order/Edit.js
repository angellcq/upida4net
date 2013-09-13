var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('orderEditController', ['$scope', '$http', function ($scope, $http) {
	$scope.id = upida.utils.query("id");
	$scope.clientId = null;
	$scope.shipCountry = null;
	$scope.shipCity = null;
	$scope.shipZip = null;
	$scope.shipAddress = null;
	$scope.total = null;
	$scope.clientsLink = upida.utils.url("client/index");
	$scope.createClientLink = upida.utils.url("client/create");
	$scope.indexLink = null;

	$scope.onSave = function () {
		var item = {};
		item.Id = $scope.id;
		item.ShipCountry = $scope.shipCountry;
		item.ShipCity = $scope.shipCity;
		item.ShipZip = $scope.shipZip;
		item.ShipAddress = $scope.shipAddress;
		item.Total = $scope.total;

		upida.utils.post("order/update", item, function() {
			upida.utils.navigate("order/show?id=" + $scope.id);
		});
	};

	$scope.loadOrder = function () {
		upida.utils.get("order/get?id=" + $scope.id, function(item) {
			$scope.shipCountry = item.ShipCountry;
			$scope.shipCity = item.ShipCity;
			$scope.shipZip = item.ShipZip;
			$scope.shipAddress = item.ShipAddress;
			$scope.total = item.Total;
			$scope.clientId = item.Client.Id;
			$scope.indexLink = upida.utils.url("order/index?clientId=") + item.Client.Id;
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
		$scope.loadOrder();
	};
}]);