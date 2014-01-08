﻿using System.Collections.Generic;
using MyClients.Business;
using MyClients.Dao;
using MyClients.Domain;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;
using Upida.Validation;

namespace Test.MyClients.Business
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

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.clientDao = mocks.Stub<IClientDao>();
			this.mapper = mocks.Stub<IMapper>();
			this.validator = mocks.Stub<IValidationContext>();
			this.transaction = mocks.Stub<ITransaction>();
			this.target = new ClientBusiness(this.mapper, this.validator, this.clientDao);
		}

		[Test]
		public void GetByIdTest()
		{
			Client db = new Client();
			db.Id = 3453;
			Client filtered = new Client();
			filtered.Id = 7567;
			using (mocks.Ordered())
			{
				this.clientDao.Expect((m) => m.GetById(db.Id.Value)).Return(db);
				this.mapper.Expect((m) => m.Filter(db, Levels.DEEP)).Return(filtered);
			}

			mocks.ReplayAll();
			Client actual = this.target.GetById(db.Id.Value);
			Assert.AreEqual(filtered, actual);
			this.mocks.VerifyAll();
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
				this.validator.Expect((m) => m.AssertValid(input, Groups.SAVE));
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
		public void UpdateTest()
		{
			Client input = new Client();
			input.Id = 4235;
			Client existing = new Client();
			existing.Id = 6786;

			using (mocks.Ordered())
			{
				this.validator.Expect((m) => m.AssertValid(input, Groups.UPDATE));
				this.clientDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.clientDao.Expect((m) => m.GetById(input.Id.Value)).Return(existing);
				this.mapper.Expect((m) => m.MapTo(input, existing));
				this.clientDao.Expect((m) => m.Merge(existing));
				this.transaction.Expect((m) => m.Commit());
				this.transaction.Expect((m) => m.Dispose());
			}

			mocks.ReplayAll();
			this.target.Update(input);
			this.mocks.VerifyAll();
		}
	}
}