var upida = upida || {};
upida.directives = upida.directives || {};

upida.directives.errorkeyDirective = function(app) {
	app.directive("errorkey", function () {
		return {
			restrict: 'A',
			link: function(scope, element, attrs) {
				scope.$watch('errors', function(oldVal, errors) {
					element.text("");
					var key = attrs.errorkey;
					if(errors) {
						$.each(errors, function (i, p) {
							if(key == p.Key) {
								element.text(p.Text);
							}
						});
					}
				});
			}
		};
	});
};