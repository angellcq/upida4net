using System.Web.Http;
using Upida.Aspnetmvc;

namespace UpidaExampleAngular
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			config.Routes.MapHttpRoute(
				name: "service",
				routeTemplate: "api/{controller}/{action}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.JsonFormatter);
			GlobalConfiguration.Configuration.Formatters.Add(new UpidaJsonFormatter());
		}
	}
}