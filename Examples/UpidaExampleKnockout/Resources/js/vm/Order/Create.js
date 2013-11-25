var upida = upida || {};

$(function () {
	upida.vm = {
		clientId: upida.utils.query("clientId"),
		shipCountry: ko.observable(null),
		shipCity: ko.observable(null),
		shipZip: ko.observable(null),
		shipAddress: ko.observable(null),
		total: ko.observable(null),
		orderItemRows: ko.observableArray([]),
		products: [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
		clientsLink: upida.utils.url("client/index"),
		indexLink: null
	};

	upida.vm.indexLink = upida.utils.url("order/index?clientId=") + upida.vm.clientId;
	upida.vm.OrderItemRow = function () {
		this.productId = ko.observable(null);
		this.count = ko.observable(1);
		this.price = ko.observable("0.0");
	};

	upida.vm.removeProduct = function (item) {
		upida.vm.orderItemRows.remove(item);
	};

	upida.vm.addProduct = function () {
		var item = new upida.vm.OrderItemRow();
		upida.vm.orderItemRows.push(item);
	};

	upida.vm.onSave = function () {
		var item = {};
		item.client = upida.utils.getReff(upida.vm.clientId);
		item.shipCountry = upida.vm.shipCountry();
		item.shipCity = upida.vm.shipCity();
		item.shipZip = upida.vm.shipZip();
		item.shipAddress = upida.vm.shipAddress();
		item.total = upida.vm.total();
		item.orderItems = new Array();
		$.each(upida.vm.orderItemRows(), function (i, p) {
			var orderItem = {};
			orderItem.productId = p.productId();
			orderItem.count = p.count();
			orderItem.price = p.price();
			item.orderItems.push(orderItem);
		});
		upida.utils.post("order/save", item, function() {
			upida.utils.navigate("order/index?clientId=" + upida.vm.clientId);
		});
	};

	upida.utils.bind(upida.vm);
});