var $upida = $upida || {};
$upida.baseUrl = "/";

$upida.url = function(link) {
	return $upida.baseUrl + link;
};

$upida.onBeforeAjax = null;
$upida.onAfterAjax = null;

$upida.navigate = function(link) {
	window.location.replace($upida.url(link));
};

$upida.post = function(method, input) {
	$upida.ajaxStart();
	var deferred = $.Deferred();
	$.ajax({
		url: $upida.url(method),
		type: "POST",
		data: ko.toJSON(input),
		contentType: "application/json",
		success: function (output, status, xhr) {
			$upida.clearErrors();
			deferred.resolve(output);
			$upida.ajaxEnd();
		},
		error: function (xhr, status, err) {
			$upida.ajaxEnd();
			deferred.reject();
			var fail = JSON.parse(xhr.responseText);
			$upida.showErrors(fail);
		}
	});
	return deferred.promise();
};

$upida.get = function(method) {
	$upida.ajaxStart();
	var deferred = $.Deferred();
	$.ajax({
		url: $upida.url(method),
		type: "GET",
		success: function (output, status, xhr) {
			$upida.clearErrors();
			deferred.resolve(output);
			$upida.ajaxEnd();
		},
		error: function (xhr, status, err) {
			$upida.ajaxEnd();
			deferred.reject();
			var fail = JSON.parse(xhr.responseText);
			$upida.showErrors(fail);
		}
	});
	return deferred.promise();
};

$upida.query = function(name)
{
	name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
	var regexS = "[\\?&]" + name + "=([^&#]*)";
	var regex = new RegExp(regexS);
	var results = regex.exec(window.location.search);
	if (results == null) {
		return "";
	}
	else {
		return decodeURIComponent(results[1].replace(/\+/g, " "));
	}
};

$upida.Lookup = function(id, name, version) {
	this.id = ko.observable(id);
	this.name = ko.observable(name);
	this.version = version;
};

$upida.getReff = function(id, version) {
	if(!$upida.isEmpty(id)) {
		return {id: id, version: version};
	}

	return null;
};

$upida.isEmpty = function (val) {
	return undefined === val || null == val || "" === val;
};

$upida.ajaxCallCount = 0;

$upida.ajaxStart = function() {
	if(0 == $upida.ajaxCallCount) {
		if ($upida.onBeforeAjax) {
			$upida.onBeforeAjax();
		}
	}

	$upida.ajaxCallCount++;
};

$upida.ajaxEnd = function() {
	$upida.ajaxCallCount--;
	if(0 == $upida.ajaxCallCount) {
		setTimeout(function () {
			if(0 == $upida.ajaxCallCount) {
				if ($upida.onAfterAjax) {
					$upida.onAfterAjax();
				}
			}
		}, 200);
	}
};

$upida.vm = null;
$upida.bind = function(vm) {
	$upida.vm = vm;
	$upida.vm.$$errors = ko.observableDictionary();
	$upida.vm.$$mainerror = ko.observable();
	ko.applyBindings(vm);
};

$upida.showErrors = function(fail) {
	$upida.vm.$$errors.removeAll();
	$upida.vm.$$mainerror(fail.main);
	$.each(fail.failures, function (i, p) {
		var current = $upida.vm.$$errors.get(p.key);
		if(current()) {
			current(current() + "<br />" + p.text);
		}
		else {
			$upida.vm.$$errors.push(p.key, p.text);
		}
	});
};

$upida.clearErrors = function(fail) {
	if ($upida.vm.$$errors) {
		$upida.vm.$$errors.removeAll();
		$upida.vm.$$mainerror("");
	}
};

$upida.find = function (obsArray, isItemFunc) {
	var foundItem = null;
	$.each(obsArray, function (i, item) {
		if (true == isItemFunc(item)) {
			foundItem = item;
			return;
		}
	});

	return foundItem;
};