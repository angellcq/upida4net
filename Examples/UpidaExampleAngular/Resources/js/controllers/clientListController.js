angularExample.app.controller(
		"clientListController",
		["$scope", "$routeParams", "upidaService", function($scope, $routeParams, upidaService) {

	$scope.clientRows = new Array();
	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
	};

	$scope.loadClients = function() {
		upidaService.get("client/getall", $scope, function (items) {
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