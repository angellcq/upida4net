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
		item.id = upida.vm.id;
		item.shipCountry = upida.vm.shipCountry();
		item.shipCity = upida.vm.shipCity();
		item.shipZip = upida.vm.shipZip();
		item.shipAddress = upida.vm.shipAddress();
		item.total = upida.vm.total();

		upida.utils.post("order/update", item, function() {
			upida.utils.navigate("order/show?id=" + upida.vm.id);
		});
	};

	upida.vm.loadOrder = function () {
		upida.utils.get("order/get?id=" + upida.vm.id, function(item) {
			upida.vm.shipCountry(item.shipCountry);
			upida.vm.shipCity(item.shipCity);
			upida.vm.shipZip(item.shipZip);
			upida.vm.shipAddress(item.shipAddress);
			upida.vm.total(item.total);
			upida.vm.clientId = item.client.id;
			upida.vm.indexLink(upida.utils.url("order/index?clientId=") + item.client.id);
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrder();
});