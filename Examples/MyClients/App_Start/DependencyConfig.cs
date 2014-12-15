using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using MyClients.Dao;
using MyClients.Dao.Impl;
using MyClients.Database;
using MyClients.Database.Impl;
using MyClients.Service;
using MyClients.Service.Impl;
using MyClients.Validation;
using MyClients.Validation.Common;
using MyClients.Validation.Common.Impl;
using MyClients.Validation.Impl;
using NHibernate;
using NHibernate.Cfg;
using Upida;
using Upida.Impl;

namespace MyClients
{
    public class DependencyConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired(PropertyWiringOptions.None);
            builder.RegisterFilterProvider();

            builder.Register((context) => new Configuration().Configure().BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.RegisterType<SessionFactoryEx>().As<ISessionFactoryEx>().SingleInstance();
            builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();

            builder.RegisterType<ClientDao>().As<IClientDao>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.None);
            builder.RegisterType<ClientService>().As<IClientService>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.None);

            builder.RegisterType<ValidationContextFactory>().As<IValidationContextFactory>().SingleInstance();
            builder.RegisterType<ValidationFacade>().As<IValidationFacade>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.None);
            builder.RegisterType<ClientValidator>().As<IClientValidator>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.None);
            builder.RegisterType<LoginValidator>().As<ILoginValidator>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.None);

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}