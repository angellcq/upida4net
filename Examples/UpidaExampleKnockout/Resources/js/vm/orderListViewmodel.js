$(function () {
	vm = {
		clientId: $upida.query("clientId"),
		orderRows: ko.observableArray([]),
		clientsLink: $upida.url("client/list"),
		createLink: null
	};

	vm.createLink = $upida.url("order/create?clientId=") + vm.clientId;
	vm.OrderRow = function (id) {
		this.id = ko.observable(id);
		this.createdOn = ko.observable();
		this.shipCountry = ko.observable();
		this.shipCity = ko.observable();
		this.shipZip = ko.observable();
		this.shipAddress = ko.observable();
		this.total = ko.observable();
		this.viewLink = $upida.url("order/show?id=") + id;
		this.editLink = $upida.url("order/edit?id=") + id;
		this.editItemsLink = $upida.url("order/edititems?id=") + id;
	};

	vm.loadOrders = function () {
		vm.orderRows.removeAll();
		$upida.get("api/order/getbyclient?clientId=" + vm.clientId)
		.then(function (items) {
			$.each(items, function (i, p) {
				var row = new vm.OrderRow(p.id);
				row.createdOn(p.createdOn);
				row.shipCountry(p.shipCountry);
				row.shipCity(p.shipCity);
				row.shipZip(p.shipZip);
				row.shipAddress(p.shipAddress);
				row.total(p.total);
				vm.orderRows.push(row);
			});
		});
	};

	vm.onDelete = function (item) {
		$upida.post("api/order/delete/" + item.id())
		.then(function() {
			vm.loadOrders();
		});
	};

	$upida.bind(vm);
	vm.loadOrders();
});