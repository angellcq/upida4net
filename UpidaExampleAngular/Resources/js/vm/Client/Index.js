var app = angular.module("app", []);
upida.directives.errorkeyDirective(app);
app.controller('clientIndexController', ['$scope', function($scope) {
	$scope.clientRows = new Array();
	$scope.createLink = upida.utils.url("client/create");

	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
		this.ordersLink = upida.utils.url("order/index?clientId=") + this.id;
		this.newOrderLink = upida.utils.url("order/create?clientId=") + this.id;
	};

	$scope.loadClients = function() {
		upida.utils.get("client/getall", function (items) {
			$.each(items, function (i, p) {
				var row = new $scope.ClientRow(p.Id);
				row.name = p.Name;
				$scope.clientRows.push(row);
			});
			$scope.$apply();
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope);
		$scope.loadClients();
	};
}]);