var upida = upida || {};

$(function () {
	upida.vm = {
		id: upida.utils.query("id"),
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
		clientsLink: upida.utils.url("client/index"),
		indexLink: ko.observable()
	};

	upida.vm.editLink = upida.utils.url("order/edit?id=") + upida.vm.id;
	upida.vm.editItemsLink = upida.utils.url("order/edititems?id=") + upida.vm.id;

	upida.vm.OrderItemRow = function () {
		this.product = ko.observable();
		this.count = ko.observable();
		this.price = ko.observable();
	};

	upida.vm.loadOrder = function () {
		upida.utils.get("order/getfull?id=" + upida.vm.id, function(item) {
			upida.vm.shipCountry(item.shipCountry);
			upida.vm.shipCity(item.shipCity);
			upida.vm.shipZip(item.shipZip);
			upida.vm.shipAddress(item.shipAddress);
			upida.vm.total(item.total);
			upida.vm.client(item.client.name);
			upida.vm.indexLink(upida.utils.url("order/index?clientId=") + item.client.id);
			$.each(item.orderItems, function (i, p) {
				var row = new upida.vm.OrderItemRow();
				row.count(p.count);
				row.price(p.price);
				var product = upida.utils.find(upida.vm.products, function(m) { return p.productId == m.id; });
				row.product(product.name);
				upida.vm.orderItemRows.push(row);
			});
		});
	};

	upida.utils.bind(upida.vm);
	upida.vm.loadOrder();
});