using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Upida;
using Upida.Aspnetmvc;

namespace MyClients
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			UpidaContext.Current.SetValidatorFactory(new AspMvcValidatorFactory());
			AreaRegistration.RegisterAllAreas();

			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			DependencyConfig.RegisterDependencies(GlobalConfiguration.Configuration);

			GlobalConfiguration.Configuration.Filters.Add(new ErrorFilterAttribute());
		}
	}
}