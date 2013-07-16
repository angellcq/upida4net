var upida = upida || {};

$(function () {
	upida.vm = {
		id: upida.utils.query("id"),
		clientId: null,
		shipCountry: ko.observable(null),
		shipCity: ko.observable(null),
		shipZip: ko.observable(null),
		shipAddress: ko.observable(null),
		total: ko.observable(null),
		clientsLink: upida.utils.url("client/index"),
		createClientLink: upida.utils.url("client/create"),
		indexLink: ko.observable()
	};

	upida.vm.onSave = function () {
		var item = {};
		item.Id = upida.vm.id;
		item.Client = upida.utils.getReff(upida.vm.clientId);
		item.ShipCountry = upida.vm.shipCountry();
		item.ShipCity = upida.vm.shipCity();
		item.ShipZip = upida.vm.shipZip();
		item.ShipAddress = upida.vm.shipAddress();
		item.Total = upida.vm.total();

		upida.utils.post("order/update", item, function() {
			window.location.replace(upida.utils.url("order/show?id=") + upida.vm.id);
		});
	};

	upida.vm.loadOrder = function () {
		upida.utils.get("order/get?id=" + upida.vm.id, function(item) {
			upida.vm.shipCountry(item.ShipCountry);
			upida.vm.shipCity(item.ShipCity);
			upida.vm.shipZip(item.ShipZip);
			upida.vm.shipAddress(item.ShipAddress);
			upida.vm.total(item.Total);
			upida.vm.clientId = item.Client.Id;
			upida.vm.indexLink(upida.utils.url("order/index?clientId=") + item.Client.Id);
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrder();
});