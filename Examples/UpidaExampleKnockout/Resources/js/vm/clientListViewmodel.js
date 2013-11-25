$(function () {
	vm = {
		clientRows: ko.observableArray([]),
		createLink: upida.url("client/create")
	};

	vm.ClientRow = function (id) {
		this.id = ko.observable(id);
		this.name = ko.observable();
		this.ordersLink = upida.url("order/list?clientId=" + id);
		this.newOrderLink = upida.url("order/create?clientId=" + id);
	};

	vm.loadClients = function() {
		upida.get("client/getall", function (items) {
			$.each(items, function (i, p) {
				var row = new vm.ClientRow(p.id);
				row.name(p.name);
				vm.clientRows.push(row);
			});
		});
	};

	upida.bind(vm);
	vm.loadClients();
});