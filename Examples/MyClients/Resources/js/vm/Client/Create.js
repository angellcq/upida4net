var app = angular.module("app", []);
upida.utils.errorkeyDirective(app);
app.controller('clientCreateController', ['$scope', '$http', function ($scope, $http) {
	$scope.name = null;
	$scope.lastname = null;
	$scope.age = null;
	$scope.loginRows = new Array();
	$scope.indexLink = upida.utils.url("client/index");

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
		data.Name = $scope.name;
		data.Lastname = $scope.lastname;
		data.Age = $scope.age;
		data.Logins = new Array();
		angular.forEach($scope.loginRows, function (p, i) {
			var item = {};
			item.Name = p.name;
			item.Password = p.password;
			item.Enabled = p.enabled;
			data.Logins.push(item);
		});
		upida.utils.post("client/save", data, function () {
			upida.utils.navigate("client/index");
		});
	};

	$scope.init = function () {
		upida.utils.bind($scope, $http);
		$scope.onAddLoginClick();
	};
}]);