var upida = upida || {};
upida.utils = upida.utils || {};

upida.utils.url = function(link) {
	return "/" + link;
};

upida.utils.navigate = function(link) {
	window.location.replace(upida.utils.url(link));
};

upida.utils.post = function(method, input, callback) {
	upida.utils.ajaxStart();
	$.ajax({
		url: upida.utils.url("api/" + method),
		type: "POST",
		data: angular.toJson(input),
		contentType: "application/json",
		success: function (output, status, xhr) {
			upida.utils.clearErrors();
			callback(output);
			upida.utils.ajaxEnd();
		},
		error: function (xhr, sttaus, err) {
			upida.utils.ajaxEnd();
			var fail = angular.fromJson(xhr.responseText);
			upida.utils.showErrors(fail);
		}
	});
};

upida.utils.get = function(method, callback) {
	upida.utils.ajaxStart();
	$.ajax({
		url: upida.utils.url("api/" + method),
		type: "GET",
		success: function (output, status, xhr) {
			upida.utils.clearErrors();
			callback(output);
			upida.utils.ajaxEnd();
		},
		error: function (xhr, sttaus, err) {
			upida.utils.ajaxEnd();
			var fail = angular.fromJson(xhr.responseText);
			upida.utils.showErrors(fail);
		}
	});
};

upida.utils.query = function(name)
{
	name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
	var regexS = "[\\?&]" + name + "=([^&#]*)";
	var regex = new RegExp(regexS);
	var results = regex.exec(window.location.search);
	if(results == null)
		return "";
	else
		return decodeURIComponent(results[1].replace(/\+/g, " "));
};

upida.utils.Lookup = function(id, name, version) {
	this.id = id;
	this.name = name;
	this.version = version;
};

upida.utils.getReff = function(id, version) {
	if(!upida.utils.isEmpty(id)) {
		return {Id: id, Version: version};
	}

	return null;
};

upida.utils.isEmpty = function (val) {
	return undefined === val || null == val || "" === val;
};

upida.utils.ajaxCallCount = 0;
upida.utils.ajaxCallback = function(callback) {
	setTimeout(function () {
		if (0 == upida.utils.ajaxCallCount) {
			callback();
		}
		else {
			upida.utils.ajaxCallback(callback);
		}
	}, 100);
};

upida.utils.ajaxStart = function() {
	if(0 == upida.utils.ajaxCallCount) {
		$.blockUI({ css: {
			border: 'none',
			padding: '15px',
			backgroundColor: '#000',
			'-webkit-border-radius': '10px',
			'-moz-border-radius': '10px',
			opacity: .3,
			color: '#fff'
		},
		ignoreIfBlocked: true });
	}

	upida.utils.ajaxCallCount++;
};

upida.utils.ajaxEnd = function() {
	upida.utils.ajaxCallCount--;
	if(0 == upida.utils.ajaxCallCount) {
		setTimeout(function () {
			if(0 == upida.utils.ajaxCallCount) {
				$.unblockUI();
			}
		}, 200);
	}
};

upida.scope = null;
upida.utils.bind = function(scope) {
	upida.scope = scope;
	upida.scope.errors = new Array();
};

upida.utils.showErrors = function(fail) {
	var errors = new Array();
	$.each(fail.Failures, function (i, p) {
		var current = upida.utils.find(upida.scope.errors, function(m) { return m.Key == p.Key; });
		if(current) {
			current.Text = current.Text + "<br />" + p.Text;
		}
		else {
			upida.scope.errors.push(p);
		}
	});

	upida.scope.errors = errors;
	upida.scope.$apply();
};

upida.utils.clearErrors = function(fail) {
	upida.scope.errors = new Array();
	upida.scope.$apply();
};

upida.utils.find = function (obsArray, isItemFunc) {
	var foundItem = null;
	$.each(obsArray, function (i, item) {
		if (true == isItemFunc(item)) {
			foundItem = item;
			return;
		}
	});

	return foundItem;
};