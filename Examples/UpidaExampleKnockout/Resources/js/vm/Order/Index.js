var upida = upida || {};

$(function () {
	upida.vm = {
		clientId: upida.utils.query("clientId"),
		orderRows: ko.observableArray([]),
		clientsLink: upida.utils.url("client/index"),
		createLink: null
	};

	upida.vm.createLink = upida.utils.url("order/create?clientId=") + upida.vm.clientId;
	upida.vm.OrderRow = function () {
		this.id = ko.observable();
		this.createdOn = ko.observable();
		this.shipCountry = ko.observable();
		this.shipCity = ko.observable();
		this.shipZip = ko.observable();
		this.shipAddress = ko.observable();
		this.total = ko.observable();
		this.viewLink = ko.computed(function () {
			return upida.utils.url("order/show?id=") + this.id();
		}, this);
		this.editLink= ko.computed(function () {
			return upida.utils.url("order/edit?id=") + this.id();
		}, this);
		this.editItemsLink= ko.computed(function () {
			return upida.utils.url("order/edititems?id=") + this.id();
		}, this);
	};

	upida.vm.loadOrders = function() {
		upida.utils.get("order/getbyclient?clientId=" + upida.vm.clientId, function (items) {
			$.each(items, function (i, p) {
				var row = new upida.vm.OrderRow();
				row.id(p.Id);
				row.createdOn(p.CreatedOn);
				row.shipCountry(p.ShipCountry);
				row.shipCity(p.ShipCity);
				row.shipZip(p.ShipZip);
				row.shipAddress(p.ShipAddress);
				row.total(p.Total);
				upida.vm.orderRows.push(row);
			});
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrders();
});