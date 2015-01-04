myclients.app.controller(
	"clientCreateController",
	["$scope", "$location", "upida", function ($scope, $location, upida) {

	$scope.name = null;
	$scope.lastname = null;
	$scope.age = null;
	$scope.loginRows = new Array();

	$scope.LoginRow = function () {
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
		upida.post("client/save", data)
		.then(function () {
			$location.path("client/list");
		});
	};

	$scope.$on("$routeChangeSuccess", function () {
		upida.setScope($scope);
		$scope.onAddLoginClick();
	});
}]);