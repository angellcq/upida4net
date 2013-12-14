$(function () {
	vm = {
		clientId: $upida.query("clientId"),
		shipCountry: ko.observable(null),
		shipCity: ko.observable(null),
		shipZip: ko.observable(null),
		shipAddress: ko.observable(null),
		total: ko.observable(null),
		orderItemRows: ko.observableArray([]),
		products: [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
		clientsLink: $upida.url("client/list"),
		indexLink: null
	};

	vm.indexLink = $upida.url("order/list?clientId=") + vm.clientId;
	vm.OrderItemRow = function () {
		this.productId = ko.observable(null);
		this.count = ko.observable(1);
		this.price = ko.observable("0.0");
	};

	vm.removeProduct = function (item) {
		vm.orderItemRows.remove(item);
	};

	vm.addProduct = function () {
		var item = new vm.OrderItemRow();
		vm.orderItemRows.push(item);
	};

	vm.onSave = function () {
		var item = {};
		item.client = $upida.getReff(vm.clientId);
		item.shipCountry = vm.shipCountry();
		item.shipCity = vm.shipCity();
		item.shipZip = vm.shipZip();
		item.shipAddress = vm.shipAddress();
		item.total = vm.total();
		item.orderItems = new Array();
		$.each(vm.orderItemRows(), function (i, p) {
			var orderItem = {};
			orderItem.productId = p.productId();
			orderItem.count = p.count();
			orderItem.price = p.price();
			item.orderItems.push(orderItem);
		});
		$upida.post("api/order/save", item)
		.then(function () {
			$upida.navigate("order/list?clientId=" + vm.clientId);
		});
	};

	$upida.bind(vm);
	vm.addProduct();
});