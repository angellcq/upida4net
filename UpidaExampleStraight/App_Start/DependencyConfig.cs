﻿using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;
using NHibernate.Cfg;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using Upida;
using Upida.Validation;
using UpidaExampleStraight.Business;
using UpidaExampleStraight.Business.Util;
using UpidaExampleStraight.Dao;
using UpidaExampleStraight.Dao.Support;

namespace UpidaExampleStraight
{
    public class DependencyConfig
    {
        public static void RegisterDependencies(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterFilterProvider();

            builder.Register((context) => new Configuration().Configure().BuildSessionFactory()).As<ISessionFactory>().SingleInstance();
            builder.RegisterType<TransactionFactory>().As<TransactionFactory>().SingleInstance();
            builder.RegisterType<Mapper>().As<Mapper>().InstancePerDependency();
            builder.RegisterType<FormParser>().As<FormParser>().InstancePerDependency();
            builder.RegisterType<Validator>().As<Validator>().InstancePerDependency();

            builder.RegisterType<OrderDao>().As<IOrderDao>().InstancePerDependency();
            builder.RegisterType<ClientDao>().As<IClientDao>().InstancePerDependency();
            builder.RegisterType<OrderItemDao>().As<IOrderItemDao>().InstancePerDependency();

            builder.RegisterType<ClientBusiness>().As<ClientBusiness>().InstancePerDependency();
            builder.RegisterType<OrderBusiness>().As<OrderBusiness>().InstancePerDependency();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}