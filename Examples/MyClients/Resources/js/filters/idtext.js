myclients.app.filter("idtext", [function () {
	return function (input) {
		if(input) {
			var text = "000000" + input;
			text = text.slice(-6);
			return text;
		}
	};
}]);