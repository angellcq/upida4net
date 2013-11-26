myclients.app.controller(
	"clientListController", ["$scope", "upidaService",
	function ($scope, upidaService) {

	$scope.clientRows = new Array();

	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
		this.lastname = null;
		this.age = null;
		this.logins = new Array();
	};

	$scope.loadClients = function() {
		upidaService.get("client/getall", $scope, function (items) {
			angular.forEach(items, function (p, i) {
				var row = new $scope.ClientRow(p.id);
				row.name = p.name;
				row.lastname = p.lastname;
				row.age = p.age;
				angular.forEach(p.logins, function (q, j) {
					row.logins.push(q.name);
				});
				$scope.clientRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadClients();
	});
}]);