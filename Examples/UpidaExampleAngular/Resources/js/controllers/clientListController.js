angularExample.app.controller(
		"clientListController",
		["$scope", "$routeParams", "upida", function ($scope, $routeParams, upida) {

	$scope.clientRows = null;
	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
	};

	$scope.loadClients = function() {
		upida.get("client/getall", $scope)
		.then(function (items) {
			$scope.clientRows = new Array();
			angular.forEach(items, function (p, i) {
				var row = new $scope.ClientRow(p.id);
				row.name = p.name;
				$scope.clientRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadClients();
	});
}]);