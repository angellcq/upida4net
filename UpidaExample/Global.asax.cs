using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace UpidaExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyConfig.RegisterDependencies(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Filters.Add(new ErrorFilterAttribute());
        }
    }
}