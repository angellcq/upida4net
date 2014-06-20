using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
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

		[Test]
		public void GetCountTest()
		{
			long expected = 20;
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.CreateQuery("select count(*) from Client")).Return(this.query);
				this.query.Expect((m) => m.UniqueResult<long>()).Return(expected);
			}

			mocks.ReplayAll();
			long actual = this.target.GetCount();
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetByIdTest()
		{
			int input = 25;
			Client expected = new Client() { Id = 25 };
			using (mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Get<Client>(input)).Return(expected);
			}

			mocks.ReplayAll();
			Client actual = this.target.GetById(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}
	}
}