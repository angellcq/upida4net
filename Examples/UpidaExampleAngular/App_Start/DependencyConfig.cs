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
using UpidaExampleAngular.Business;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Dao.Support;
using UpidaExampleAngular.Validation;

namespace UpidaExampleAngular
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
			builder.RegisterType<SessionFactoryExt>().As<SessionFactoryExt>().SingleInstance();
			builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();
			builder.RegisterType<ValidationContext>().As<IValidationContext>().SingleInstance();

			builder.RegisterType<OrderDao>().As<IOrderDao>().SingleInstance();
			builder.RegisterType<ClientDao>().As<IClientDao>().SingleInstance();

			builder.RegisterType<ClientBusiness>().As<ClientBusiness>().SingleInstance();
			builder.RegisterType<OrderBusiness>().As<OrderBusiness>().SingleInstance();

			builder.RegisterType<ClientSaveValidator>().As<ClientSaveValidator>().InstancePerDependency();
			builder.RegisterType<ClientReferenceValidator>().As<ClientReferenceValidator>().InstancePerDependency();
			builder.RegisterType<OrderSaveValidator>().As<OrderSaveValidator>().InstancePerDependency();
			builder.RegisterType<OrderUpdateValidator>().As<OrderUpdateValidator>().InstancePerDependency();
			builder.RegisterType<OrderUpdateItemsValidator>().As<OrderUpdateItemsValidator>().InstancePerDependency();
			builder.RegisterType<OrderItemSaveValidator>().As<OrderItemSaveValidator>().InstancePerDependency();
			builder.RegisterType<OrderItemMergeValidator>().As<OrderItemMergeValidator>().InstancePerDependency();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}