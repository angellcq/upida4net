var upida = upida || {};

$(function () {
	upida.vm = {
		name: ko.observable(null),
		indexLink: upida.utils.url("client/index")
	};

	upida.vm.onSave = function () {
		var item = {};
		item.name = upida.vm.name();
		upida.utils.post("client/save", item, function() {
			window.location.replace(upida.utils.url("client/index"));
		});
	};

	upida.utils.bind(upida.vm);
});