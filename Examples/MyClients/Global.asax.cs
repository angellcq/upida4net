using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Upida;
using Upida.Validation;

namespace MyClients
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            UpidaContext.Current().SetTypeValidatorFactory(new AspMvcTypeValidatorFactory());
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            DependencyConfig.RegisterDependencies(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Filters.Add(new ErrorFilterAttribute());
        }
    }
}