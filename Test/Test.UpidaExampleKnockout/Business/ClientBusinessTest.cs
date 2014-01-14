using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;
using UpidaExampleKnockout.Business;
using UpidaExampleKnockout.Dao;
using UpidaExampleKnockout.Domain;

namespace Test.UpidaExampleKnockout.Business
{
	[TestFixture]
	public class ClientBusinessTest
	{
		private MockRepository mocks;
		private IClientDao clientDao;
		private IMapper mapper;
		private ITransaction transaction;
		private ClientBusiness target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.clientDao = mocks.Stub<IClientDao>();
			this.mapper = mocks.Stub<IMapper>();
			this.transaction = mocks.Stub<ITransaction>();
			this.target = new ClientBusiness(this.mapper, this.clientDao);
		}

		[Test]
		public void GetAllTest()
		{
			IList<Client> db = new List<Client>();
			IList<Client> filtered = new List<Client>();
			using (mocks.Ordered())
			{
				this.clientDao.Expect((m) => m.GetAll()).Return(db);
				this.mapper.Expect((m) => m.FilterList(db, Levels.GRID)).Return(filtered);
			}

			mocks.ReplayAll();
			IList<Client> actual = this.target.GetAll();
			Assert.AreEqual(filtered, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void SaveTest()
		{
			Client input = new Client();
			input.Id = 4235;

			using (mocks.Ordered())
			{
				this.clientDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.mapper.Expect((m) => m.Map(input));
				this.clientDao.Expect((m) => m.Save(input));
				this.transaction.Expect((m) => m.Commit());
				this.transaction.Expect((m) => m.Dispose());
			}

			mocks.ReplayAll();
			this.target.Save(input);
			this.mocks.VerifyAll();
		}
	}
}