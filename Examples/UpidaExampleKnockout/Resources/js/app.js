$upida.baseUrl = "/";

$upida.onBeforeAjax = function () {
	$.blockUI({
		css: {
			border: 'none',
			padding: '15px',
			backgroundColor: '#000',
			'-webkit-border-radius': '10px',
			'-moz-border-radius': '10px',
			opacity: .3,
			color: '#fff'
		},
		ignoreIfBlocked: true
	});
};

$upida.onAfterAjax = function () {
	$.unblockUI();
};