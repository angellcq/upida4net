﻿using System.Collections.Generic;
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
	public class OrderBusinessTest
	{
		private MockRepository mocks;
		private IOrderDao orderDao;
		private IMapper mapper;
		private ITransaction transaction;
		private OrderBusiness target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.orderDao = this.mocks.Stub<IOrderDao>();
			this.mapper = this.mocks.Stub<IMapper>();
			this.transaction = this.mocks.Stub<ITransaction>();
			this.target = new OrderBusiness(this.mapper, this.orderDao);
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
	}
}