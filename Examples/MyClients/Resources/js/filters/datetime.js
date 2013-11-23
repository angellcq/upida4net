myclients.app.filter("datetime", [function (utils) {
	return function (input) {
		var date = new Date(input);
		var minutes = date.getMinutes(); if(minutes < 10) minutes = "0" + minutes;
		var hours = date.getHours(); if(hours < 10) hours = "0" + hours;
		var month = date.getMonth(); if(month < 10) month = "0" + month;
		var day = date.getDate(); if(day < 10) day = "0" + day;
		return date.getFullYear() + "-" + month + "-" + day + " " + hours + ":" + day;
	};
}]);