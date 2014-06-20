using System.Collections.Generic;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;
using UpidaExampleKnockout.Business;
using UpidaExampleKnockout.Dao;
using UpidaExampleKnockout.Domain;
using Upida.Validation;
using System;

namespace Test.UpidaExampleKnockout.Business
{
	[TestFixture]
	public class ClientBusinessTest
	{
		private MockRepository mocks;
		private IClientDao clientDao;
		private IMapper mapper;
		private IValidationContext validator;
		private ITransaction transaction;
		private ClientBusiness target;
		private IFailureList failures;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.clientDao = mocks.Stub<IClientDao>();
			this.mapper = mocks.Stub<IMapper>();
			this.validator = mocks.Stub<IValidationContext>();
			this.transaction = mocks.Stub<ITransaction>();
			this.target = new ClientBusiness(this.mapper, this.validator, this.clientDao);
			this.failures = mocks.Stub<IFailureList>();
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

		[Test]
		public void DeleteTest()
		{
			int input = 4235;
			long count = 3;
			Client existing = new Client();
			existing.Id = 6786;

			using (mocks.Ordered())
			{
				this.validator.Expect((m) => m.CreateFailureList()).Return(this.failures);
				this.clientDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.clientDao.Expect((m) => m.GetById(input)).Return(existing);
				this.failures.Expect((m) => m.FailIfNull(existing, null, "Client does not exist", Severity.Fatal));
				this.validator.Expect((m) => m.Assert(this.failures));
				this.clientDao.Expect((m) => m.GetCount()).Return(count);
				this.failures.Expect((m) => m.FailIfEqual(1, count, null, "Cannot delete the only client"));
				this.validator.Expect((m) => m.Assert(this.failures));
				this.clientDao.Expect((m) => m.Delete(existing));
				this.transaction.Expect((m) => m.Commit());
				this.transaction.Expect((m) => m.Dispose());
			}

			mocks.ReplayAll();
			this.target.Delete(input);
			this.mocks.VerifyAll();
		}
	}
}