var angularExample = angularExample || {};
angularExample.app = angular.module("angularExample", ["ngRoute"]);

angularExample.app.config(function ($routeProvider) {
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
		.when("/order/list/:clientId", {
			templateUrl: "order/list",
			controller: "orderListController"
		})
		.when("/order/create/:clientId", {
			templateUrl: "order/create",
			controller: "orderCreateController"
		})
		.when("/order/edit/:id", {
			templateUrl: "order/edit",
			controller: "orderEditController"
		})
		.when("/order/edititems/:id", {
			templateUrl: "order/edititems",
			controller: "orderItemsEditController"
		})
		.when("/order/show/:id", {
			templateUrl: "order/show",
			controller: "orderShowController"
		})
		.otherwise({
			templateUrl: "home/notfound"
		});
});

upida.baseUrl = "/api/";
upida.errorKeyDirectiveFactory(angularExample.app);
upida.mainErrorDirectiveFactory(angularExample.app);
angularExample.app.factory("upidaService", ["$http", upida.serviceFactory]);