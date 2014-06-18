using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using NUnit.Framework;
using Rhino.Mocks;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Dao.Support;
using UpidaExampleAngular.Domain;

namespace Test.UpidaExampleAngular.Dao
{
	[TestFixture]
	public class OrderDaoTest
	{
		private MockRepository mocks;
		private ISession session;
		private SessionFactoryExt sessionFactory;
		private IQuery query;
		private IOrderDao target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.session = this.mocks.Stub<ISession>();
			this.sessionFactory = this.mocks.Stub<SessionFactoryExt>(new object[] { null });
			this.query = this.mocks.Stub<IQuery>();
			this.target = new OrderDao(this.sessionFactory);
		}

		[Test]
		public void GetByClientTest()
		{
			int input = 3245;
			IList<Order> expected = new List<Order>();
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.CreateQuery("from Order o where o.Client.Id = :clientId")).Return(this.query);
				this.query.Expect((m) => m.SetParameter<int>("clientId", input)).Return(this.query);
				this.query.Expect((m) => m.SetResultTransformer(Transformers.DistinctRootEntity)).Return(this.query);
				this.query.Expect((m) => m.List<Order>()).Return(expected);
			}

			mocks.ReplayAll();
			IList<Order> actual = this.target.GetByClient(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetByIdTest()
		{
			int input = 3245;
			Order expected = new Order();
			expected.Id = input;
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.CreateQuery("from Order o inner join fetch o.Client left outer join fetch o.OrderItems where o.Id = :id")).Return(this.query);
				this.query.Expect((m) => m.SetParameter<int>("id", input)).Return(this.query);
				this.query.Expect((m) => m.SetResultTransformer(Transformers.DistinctRootEntity)).Return(this.query);
				this.query.Expect((m) => m.UniqueResult<Order>()).Return(expected);
			}

			mocks.ReplayAll();
			Order actual = this.target.GetById(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetCountTest()
		{
			int input = 500;
			long expected = 20;
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.CreateQuery("select count(*) from Order o where o.Client.Id = :clientId")).Return(this.query);
				this.query.Expect((m) => m.SetParameter<int>("clientId", input)).Return(this.query);
				this.query.Expect((m) => m.UniqueResult<long>()).Return(expected);
			}

			mocks.ReplayAll();
			long actual = this.target.GetCount(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}
	}
}