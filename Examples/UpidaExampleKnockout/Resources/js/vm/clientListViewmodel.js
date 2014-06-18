$(function () {
	vm = {
		clientRows: ko.observableArray([]),
		createLink: $upida.url("client/create")
	};

	vm.ClientRow = function (id) {
		this.id = ko.observable(id);
		this.name = ko.observable();
		this.ordersLink = $upida.url("order/list?clientId=" + id);
		this.newOrderLink = $upida.url("order/create?clientId=" + id);
	};

	vm.loadClients = function () {
		vm.clientRows.removeAll();
		$upida.get("api/client/getall")
		.then(function (items) {
			$.each(items, function (i, p) {
				var row = new vm.ClientRow(p.id);
				row.name(p.name);
				vm.clientRows.push(row);
			});
		});
	};

	vm.onDelete = function (item) {
		$upida.post("api/client/delete/" + item.id())
		.then(function() {
			vm.loadClients();
		});
	};

	$upida.bind(vm);
	vm.loadClients();
});