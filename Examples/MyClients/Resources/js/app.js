var myclients = myclients || {};
myclients.app = angular.module("myclients", ["ngRoute"]);

myclients.app.config(function ($routeProvider) {
	$routeProvider
		.when("/", {
			templateUrl: "client/list",
			controller: "clientListController"
		})
		.when("/client/list", {
			templateUrl: "client/list",
			controller: "clientListController"
		})
		.when("/client/create", {
			templateUrl: "client/create",
			controller: "clientCreateController"
		})
		.when("/client/edit/:id", {
			templateUrl: "client/edit",
			controller: "clientEditController"
		})
		.otherwise({
			templateUrl: "home/notfound"
		});
});

upida.baseUrl = "/api/";
upida.errorKeyDirectiveFactory(myclients.app);
upida.mainErrorDirectiveFactory(myclients.app);
myclients.app.factory("upidaService", ["$http", upida.serviceFactory]);