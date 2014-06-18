angularExample.app.controller(
		"clientListController",
		["$scope", "$routeParams", "upida", function ($scope, $routeParams, upida) {

	$scope.clientRows = null;
	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
	};

	$scope.loadClients = function () {
		$scope.clientRows = new Array();
		upida.get("client/getall", $scope)
		.then(function (items) {
			angular.forEach(items, function (p, i) {
				var row = new $scope.ClientRow(p.id);
				row.name = p.name;
				$scope.clientRows.push(row);
			});
		});
	};

	$scope.onDelete = function (id) {
		upida.post("client/delete/" + id, null, $scope)
		.then(function () {
			$scope.loadClients();
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadClients();
	});
}]);