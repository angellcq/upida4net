var upida = upida || {};

$(function () {
	upida.vm = {
		id: upida.utils.query("id"),
		clientId: null,
		orderItemRows: ko.observableArray([]),
		products: [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
		clientsLink: upida.utils.url("client/index"),
		indexLink: ko.observable()
	};

	upida.vm.OrderItemRow = function () {
		this.id = null;
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
		item.id = upida.vm.id;
		item.orderItems = new Array();
		$.each(upida.vm.orderItemRows(), function (i, p) {
			var orderItem = {};
			orderItem.id = p.id;
			orderItem.productId = p.productId();
			orderItem.count = p.count();
			orderItem.price = p.price();
			item.orderItems.push(orderItem);
		});
		upida.utils.post("order/updateitems", item, function() {
			upida.utils.navigate("order/show?id=" + upida.vm.id);
		});
	};

	upida.vm.loadOrder = function () {
		upida.utils.get("order/getfull?id=" + upida.vm.id, function(item) {
			upida.vm.clientId = item.client.id;
			upida.vm.indexLink(upida.utils.url("order/index?clientId=") + item.client.id);
			$.each(item.orderItems, function (i, p) {
				var row = new upida.vm.OrderItemRow();
				row.id = p.id;
				row.count(p.count);
				row.price(p.price);
				row.productId(p.productId);
				upida.vm.orderItemRows.push(row);
			});
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrder();
});