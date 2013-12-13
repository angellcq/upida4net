myclients.app.controller(
	"clientEditController",
	["$scope", "$location", "$routeParams", "upida", function ($scope, $location, $routeParams, upida) {

	$scope.id = $routeParams.id;
	$scope.name = null;
	$scope.lastname = null;
	$scope.age = null;
	$scope.loginRows = new Array();

	$scope.LoginRow = function () {
		this.id = null;
		this.name = null;
		this.password = null;
		this.enabled = false;
	};

	$scope.onRemoveLoginClick = function (item) {
		var index = $scope.loginRows.indexOf(item);
		$scope.loginRows.splice(index, 1);
	};

	$scope.onAddLoginClick = function () {
		var row = new $scope.LoginRow();
		$scope.loginRows.push(row);
	};

	$scope.onSave = function () {
		var data = {};
		data.id = $scope.id;
		data.name = $scope.name;
		data.lastname = $scope.lastname;
		data.age = $scope.age;
		data.logins = new Array();
		angular.forEach($scope.loginRows, function (p, i) {
			var item = {};
			item.name = p.name;
			item.password = p.password;
			item.enabled = p.enabled;
			data.logins.push(item);
		});
		upida.post("client/update", data, $scope)
		.then(function () {
			$location.path("client/list");
		});
	};

	$scope.loadClient = function () {
		upida.get("client/getbyid?id=" + $scope.id, $scope)
		.then(function (item) {
			$scope.name = item.name;
			$scope.lastname = item.lastname;
			$scope.age = item.age;
			angular.forEach(item.logins, function (p, i) {
				var row = new $scope.LoginRow();
				row.id = p.id;
				row.name = p.name;
				row.password = p.password;
				row.enabled = p.enabled;
				$scope.loginRows.push(row);
			});
		});
	};

	$scope.$on('$routeChangeSuccess', function () {
		$scope.loadClient();
	});
}]);