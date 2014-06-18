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
	public class OrderBusinessTest
	{
		private MockRepository mocks;
		private IOrderDao orderDao;
		private IMapper mapper;
		private IValidationContext validator;
		private ITransaction transaction;
		private OrderBusiness target;
		private IFailureList failures;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.orderDao = this.mocks.Stub<IOrderDao>();
			this.mapper = this.mocks.Stub<IMapper>();
			this.validator = this.mocks.Stub<IValidationContext>();
			this.transaction = this.mocks.Stub<ITransaction>();
			this.target = new OrderBusiness(this.mapper, this.validator, this.orderDao);
			this.failures = mocks.Stub<IFailureList>();
		}

		[Test]
		public void GetByClientTest()
		{
			int input = 3432;
			IList<Order> db = new List<Order>();
			IList<Order> filtered = new List<Order>();
			using (this.mocks.Ordered())
			{
				this.orderDao.Expect((m) => m.GetByClient(input)).Return(db);
				this.mapper.Expect((m) => m.FilterList(db, Levels.GRID)).Return(filtered);
			}

			this.mocks.ReplayAll();
			IList<Order> actual = this.target.GetByClient(input);
			Assert.AreEqual(filtered, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetTest()
		{
			int input = 3432;
			Order db = new Order();
			db.Id = input;
			Order filtered = new Order();
			filtered.Id = 5754;
			using (this.mocks.Ordered())
			{
				this.orderDao.Expect((m) => m.GetById(input)).Return(db);
				this.mapper.Expect((m) => m.Filter(db, Levels.DEEP)).Return(filtered);
			}

			this.mocks.ReplayAll();
			Order actual = this.target.Get(input);
			Assert.AreEqual(filtered, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetFullTest()
		{
			int input = 3432;
			Order db = new Order();
			db.Id = input;
			Order filtered = new Order();
			filtered.Id = 5754;
			using (this.mocks.Ordered())
			{
				this.orderDao.Expect((m) => m.GetById(input)).Return(db);
				this.mapper.Expect((m) => m.Filter(db, Levels.FULL)).Return(filtered);
			}

			this.mocks.ReplayAll();
			Order actual = this.target.GetFull(input);
			Assert.AreEqual(filtered, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void SaveTest()
		{
			Order input = new Order();
			input.Id = 4235;

			using (mocks.Ordered())
			{
				this.orderDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.mapper.Expect((m) => m.Map(input));
				this.orderDao.Expect((m) => m.Save(input));
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
			Order input = new Order();
			input.Id = 4235;
			Order existing = new Order();
			existing.Id = 9707;

			using (mocks.Ordered())
			{
				this.orderDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.orderDao.Expect((m) => m.Load(input.Id)).Return(existing);
				this.mapper.Expect((m) => m.MapTo(input, existing));
				this.orderDao.Expect((m) => m.Update(existing));
				this.transaction.Expect((m) => m.Commit());
				this.transaction.Expect((m) => m.Dispose());
			}

			mocks.ReplayAll();
			this.target.Update(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void DeleteTest_Ok()
		{
			int input = 4235;
			long count = 3;
			Order existing = new Order();
			existing.Id = 6786;
			existing.Client = new Client() { Id = 123 };

			using (mocks.Ordered())
			{
				this.validator.Expect((m) => m.CreateFailureList()).Return(this.failures);
				this.orderDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.orderDao.Expect((m) => m.GetById(input)).Return(existing);
				this.failures.Expect((m) => m.FailIf(false, "Order does not exist", Severity.Fatal));
				this.validator.Expect((m) => m.Assert(this.failures));
				this.orderDao.Expect((m) => m.GetCount(existing.Client.Id.Value)).Return(count);
				this.failures.Expect((m) => m.FailIf(false, "Cannot delete the only order in the client"));
				this.validator.Expect((m) => m.Assert(this.failures));
				this.orderDao.Expect((m) => m.Delete(existing));
				this.transaction.Expect((m) => m.Commit());
				this.transaction.Expect((m) => m.Dispose());
			}

			mocks.ReplayAll();
			this.target.Delete(input);
			this.mocks.VerifyAll();
		}

		[Test]
		[ExpectedException(typeof(Exception), ExpectedMessage = "EXPECTED")]
		public void DeleteTest_NotExisting()
		{
			int input = 4235;

			using (mocks.Ordered())
			{
				this.validator.Expect((m) => m.CreateFailureList()).Return(this.failures);
				this.orderDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.orderDao.Expect((m) => m.GetById(input)).Return(null);
				this.failures.Expect((m) => m.FailIf(true, "Order does not exist", Severity.Fatal)).Throw(new Exception("EXPECTED"));
			}

			mocks.ReplayAll();
			this.target.Delete(input);
			this.mocks.VerifyAll();
		}

		[Test]
		[ExpectedException(typeof(Exception), ExpectedMessage = "EXPECTED")]
		public void DeleteTest_CountOne()
		{
			int input = 4235;
			Order existing = new Order();
			existing.Id = 6786;
			existing.Client = new Client() { Id = 123 };

			using (mocks.Ordered())
			{
				this.validator.Expect((m) => m.CreateFailureList()).Return(this.failures);
				this.orderDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
				this.orderDao.Expect((m) => m.GetById(input)).Return(existing);
				this.failures.Expect((m) => m.FailIf(false, "Order does not exist", Severity.Fatal));
				this.validator.Expect((m) => m.Assert(this.failures));
				this.orderDao.Expect((m) => m.GetCount(existing.Client.Id.Value)).Return(1);
				this.failures.Expect((m) => m.FailIf(true, "Cannot delete the only order in the client")).Throw(new Exception("EXPECTED"));
			}

			mocks.ReplayAll();
			this.target.Delete(input);
			this.mocks.VerifyAll();
		}
	}
}