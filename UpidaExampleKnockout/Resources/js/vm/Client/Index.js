var upida = upida || {};

$(function () {
	upida.vm = {
		clientRows: ko.observableArray([]),
		createLink: upida.utils.url("client/create")
	};
	
	upida.vm.ClientRow = function () {
		this.id = ko.observable();
		this.name = ko.observable();
		this.ordersLink = ko.computed(function () {
			return upida.utils.url("order/index?clientId=") + this.id();
		}, this);
		this.newOrderLink = ko.computed(function () {
			return upida.utils.url("order/create?clientId=") + this.id();
		}, this);
	};
	
	upida.vm.loadClients = function() {
		upida.utils.get("client/getall", function (items) {
			$.each(items, function (i, p) {
				var row = new upida.vm.ClientRow();
				row.id(p.Id);
				row.name(p.Name);
				upida.vm.clientRows.push(row);
			});
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadClients();
});