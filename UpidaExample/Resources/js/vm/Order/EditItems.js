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
		item.Id = upida.vm.id;
		item.OrderItems = new Array();
		$.each(upida.vm.orderItemRows(), function (i, p) {
			var orderItem = {};
			orderItem.Id = p.id;
			orderItem.ProductId = p.productId();
			orderItem.Count = p.count();
			orderItem.Price = p.price();
			item.OrderItems.push(orderItem);
		});
		upida.utils.post("order/updateitems", item, function() {
		    window.location.replace(upida.utils.url("order/show?id=") + upida.vm.id);
		});
	};

	upida.vm.loadOrder = function () {
		upida.utils.get("order/getfull?id=" + upida.vm.id, function(item) {
		    upida.vm.clientId = item.Client.Id;
			upida.vm.indexLink(upida.utils.url("order/index?clientId=") + item.Client.Id);
			$.each(item.OrderItems, function (i, p) {
				var row = new upida.vm.OrderItemRow();
				row.id = p.Id;
				row.count(p.Count);
				row.price(p.Price);
				row.productId(p.ProductId);
				upida.vm.orderItemRows.push(row);
			});
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrder();
});