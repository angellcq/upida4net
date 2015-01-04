myclients.app.controller(
	"clientListController", ["$scope", "upida", function ($scope, upida) {

	$scope.clientRows;

	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
		this.lastname = null;
		this.age = null;
		this.logins = new Array();
	};

	$scope.loadClients = function () {
		$scope.clientRows = new Array();
		upida.get("client/getall")
		.then(function (items) {
			angular.forEach(items, function (p, i) {
				var row = new $scope.ClientRow(p.id);
				row.name = p.name;
				row.lastname = p.lastname;
				row.age = p.age;
				angular.forEach(p.logins, function (q, j) {
					row.logins.push({ id: q.id, name: q.name });
				});
				$scope.clientRows.push(row);
			});
		});
	};

	$scope.onDelete = function (clientId) {
		upida.post("client/delete/" + clientId, null)
		.then(function () {
			$scope.loadClients();
		});
	};

	$scope.$on("$routeChangeSuccess", function () {
		upida.setScope($scope);
		$scope.loadClients();
	});
}]);