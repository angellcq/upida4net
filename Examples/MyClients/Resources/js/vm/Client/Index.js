var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('clientIndexController', ['$scope', '$http', function ($scope, $http) {
	$scope.clientRows = new Array();
	$scope.createLink = upida.utils.url("client/create");

	$scope.ClientRow = function (id) {
		this.id = id;
		this.name = null;
		this.lastname = null;
		this.age = null;
		this.logins = new Array();
		this.editLink = upida.utils.url("client/edit?id=") + this.id;
	};

	$scope.loadClients = function() {
		upida.utils.get("client/getall", function (items) {
			angular.forEach(items, function (p, i) {
				var row = new $scope.ClientRow(p.Id);
				row.name = p.Name;
				row.lastname = p.Lastname;
				row.age = p.Age;
				angular.forEach(p.Logins, function (q, j) {
					row.logins.push(q.Name);
				});
				$scope.clientRows.push(row);
			});
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
		$scope.loadClients();
	};
}]);