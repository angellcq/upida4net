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
	public class ClientDaoTest
	{
		private MockRepository mocks;
		private ISession session;
		private SessionFactoryExt sessionFactory;
		private IQuery query;
		private IClientDao target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.session = this.mocks.Stub<ISession>();
			this.sessionFactory = this.mocks.Stub<SessionFactoryExt>(new object[] { null });
			this.query = this.mocks.Stub<IQuery>();
			this.target = new ClientDao(this.sessionFactory);
		}

		[Test]
		public void GetAllTest()
		{
			IList<Client> expected = new List<Client>();
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.CreateQuery("from Client")).Return(this.query);
				this.query.Expect((m) => m.List<Client>()).Return(expected);
			}

			mocks.ReplayAll();
			IList<Client> actual = this.target.GetAll();
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}
	}
}