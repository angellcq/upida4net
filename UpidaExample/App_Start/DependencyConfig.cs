using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Upida;
using Upida.Validation;
using UpidaExample.Business;
using UpidaExample.Business.Util;
using UpidaExample.Dao;
using UpidaExample.Dao.Support;

namespace UpidaExample
{
    public class DependencyConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            builder.Register((context) => new Configuration().Configure().BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.RegisterType<TransactionFactory>().As<TransactionFactory>().SingleInstance();
            builder.RegisterType<Mapper>().As<IMapper>().InstancePerDependency();
            builder.RegisterType<JsonParser>().As<IJsonParser>().InstancePerDependency();
            builder.RegisterType<Validator>().As<IValidator>().InstancePerDependency();

            builder.RegisterType<OrderDao>().As<IOrderDao>().InstancePerDependency();
            builder.RegisterType<ClientDao>().As<IClientDao>().InstancePerDependency();
            builder.RegisterType<OrderItemDao>().As<IOrderItemDao>().InstancePerDependency();

            builder.RegisterType<ClientBusiness>().As<ClientBusiness>().InstancePerDependency();
            builder.RegisterType<OrderBusiness>().As<OrderBusiness>().InstancePerDependency();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}