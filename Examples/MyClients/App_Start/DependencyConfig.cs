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
using MyClients.Validation;
using MyClients.Dao.Support;
using MyClients.Dao;
using MyClients.Business;

namespace MyClients
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
			builder.RegisterType<SessionFactoryExt>().SingleInstance();
			builder.RegisterType<Mapper>().As<IMapper>().SingleInstance();
			builder.RegisterType<ValidationContext>().As<IValidationContext>().SingleInstance();

			builder.RegisterType<ClientDao>().As<IClientDao>().SingleInstance();
			builder.RegisterType<ClientBusiness>().As<ClientBusiness>().SingleInstance();

			builder.RegisterType<ClientSaveValidator>().As<ClientSaveValidator>().InstancePerDependency();
			builder.RegisterType<ClientUpdateValidator>().As<ClientUpdateValidator>().InstancePerDependency();
			builder.RegisterType<LoginSaveValidator>().As<LoginSaveValidator>().InstancePerDependency();
			builder.RegisterType<LoginMergeValidator>().As<LoginMergeValidator>().InstancePerDependency();

			var container = builder.Build();
			DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
			GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
		}
	}
}