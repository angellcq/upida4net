$(function () {
	vm = {
		name: ko.observable(null),
		indexLink: upida.url("client/list")
	};

	vm.onSave = function () {
		var item = {};
		item.name = vm.name();
		upida.post("api/client/save", item, function () {
			upida.navigate("client/list");
		});
	};

	upida.bind(vm);
});