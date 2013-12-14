$(function () {
	vm = {
		id: $upida.query("id"),
		clientId: null,
		orderItemRows: ko.observableArray([]),
		products: [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
		clientsLink: $upida.url("client/list"),
		indexLink: ko.observable()
	};

	vm.OrderItemRow = function () {
		this.id = null;
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
		item.id = vm.id;
		item.orderItems = new Array();
		$.each(vm.orderItemRows(), function (i, p) {
			var orderItem = {};
			orderItem.id = p.id;
			orderItem.productId = p.productId();
			orderItem.count = p.count();
			orderItem.price = p.price();
			item.orderItems.push(orderItem);
		});
		$upida.post("api/order/updateitems", item)
		.then(function () {
			$upida.navigate("order/show?id=" + vm.id);
		});
	};

	vm.loadOrder = function () {
		$upida.get("api/order/getfull?id=" + vm.id)
		.then(function (item) {
			vm.clientId = item.client.id;
			vm.indexLink($upida.url("order/list?clientId=") + item.client.id);
			$.each(item.orderItems, function (i, p) {
				var row = new vm.OrderItemRow();
				row.id = p.id;
				row.count(p.count);
				row.price(p.price);
				row.productId(p.productId);
				vm.orderItemRows.push(row);
			});
		});
	};

	$upida.bind(vm);
	vm.loadOrder();
});