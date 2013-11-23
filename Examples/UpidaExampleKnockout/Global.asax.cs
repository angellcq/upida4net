using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Upida;
using Upida.Aspnetmvc;
using Upida.Validation;

namespace UpidaExampleKnockout
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            UpidaContext.Current().SetValidatorFactory(new AspMvcValidatorFactory());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyConfig.RegisterDependencies(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Filters.Add(new ErrorFilterAttribute());
        }
    }
}