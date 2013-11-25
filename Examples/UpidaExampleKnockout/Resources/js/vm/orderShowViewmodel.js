$(function () {
	vm = {
		id: upida.query("id"),
		client: ko.observable(),
		shipCountry: ko.observable(),
		shipCity: ko.observable(),
		shipZip: ko.observable(),
		shipAddress: ko.observable(),
		total: ko.observable(),
		orderItemRows: ko.observableArray([]),
		products: [{id:1, name:'product A'}, {id:2, name:'product B'}, {id:3, name:'product C'}, {id:4, name:'product D'}, {id:5, name:'product E'}],
		editLink: null,
		editItemsLink: null,
		clientsLink: upida.url("client/list"),
		indexLink: ko.observable()
	};

	vm.editLink = upida.url("order/edit?id=") + vm.id;
	vm.editItemsLink = upida.url("order/edititems?id=") + vm.id;

	vm.OrderItemRow = function () {
		this.product = ko.observable();
		this.count = ko.observable();
		this.price = ko.observable();
	};

	vm.loadOrder = function () {
		upida.get("order/getfull?id=" + vm.id, function(item) {
			vm.shipCountry(item.shipCountry);
			vm.shipCity(item.shipCity);
			vm.shipZip(item.shipZip);
			vm.shipAddress(item.shipAddress);
			vm.total(item.total);
			vm.client(item.client.name);
			vm.indexLink(upida.url("order/list?clientId=") + item.client.id);
			$.each(item.orderItems, function (i, p) {
				var row = new vm.OrderItemRow();
				row.count(p.count);
				row.price(p.price);
				var product = upida.find(upida.vm.products, function(m) { return p.productId == m.id; });
				row.product(product.name);
				vm.orderItemRows.push(row);
			});
		});
	};

	upida.bind(vm);
	vm.loadOrder();
});