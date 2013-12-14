$(function () {
	vm = {
		id: $upida.query("id"),
		clientId: null,
		shipCountry: ko.observable(null),
		shipCity: ko.observable(null),
		shipZip: ko.observable(null),
		shipAddress: ko.observable(null),
		total: ko.observable(null),
		clientsLink: $upida.url("client/list"),
		createClientLink: $upida.url("client/create"),
		indexLink: ko.observable()
	};

	vm.onSave = function () {
		var item = {};
		item.id = vm.id;
		item.shipCountry = vm.shipCountry();
		item.shipCity = vm.shipCity();
		item.shipZip = vm.shipZip();
		item.shipAddress = vm.shipAddress();
		item.total = vm.total();

		$upida.post("api/order/update", item)
		.then(function () {
			$upida.navigate("order/show?id=" + vm.id);
		});
	};

	vm.loadOrder = function () {
		$upida.get("api/order/get?id=" + vm.id)
		.then(function (item) {
			vm.shipCountry(item.shipCountry);
			vm.shipCity(item.shipCity);
			vm.shipZip(item.shipZip);
			vm.shipAddress(item.shipAddress);
			vm.total(item.total);
			vm.clientId = item.client.id;
			vm.indexLink($upida.url("order/list?clientId=") + item.client.id);
		});
	};

	$upida.bind(vm);
	vm.loadOrder();
});