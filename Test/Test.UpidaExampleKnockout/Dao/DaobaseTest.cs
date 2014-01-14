using System;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Transform;
using NUnit.Framework;
using Rhino.Mocks;
using UpidaExampleKnockout.Dao;
using UpidaExampleKnockout.Dao.Support;
using UpidaExampleKnockout.Domain;

namespace Test.UpidaExampleKnockout.Dao
{
	[TestFixture]
	public class DaobaseTest
	{
		private MockRepository mocks;
		private ISession session;
		private SessionFactoryExt sessionFactory;
		private Daobase<object> target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.session = this.mocks.Stub<ISession>();
			this.sessionFactory = this.mocks.Stub<SessionFactoryExt>(new object[] { null });
			this.target = new Daobase<object>(this.sessionFactory);
		}

		[Test]
		public void SessionTest()
		{
			this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
			this.mocks.ReplayAll();
			ISession actual = this.target.Session;
			Assert.AreEqual(this.session, actual);
		}

		[Test]
		public void SaveTest()
		{
			object input = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Save(input));
			}

			this.mocks.ReplayAll();
			this.target.Save(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void UpdateTest()
		{
			object input = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Update(input));
			}

			this.mocks.ReplayAll();
			this.target.Update(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void MergeTest()
		{
			object input = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Merge<object>(input));
			}

			this.mocks.ReplayAll();
			this.target.Merge(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void DeleteTest()
		{
			object input = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Delete(input));
			}

			this.mocks.ReplayAll();
			this.target.Delete(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void GetTest()
		{
			object input = new object();
			object expected = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Get<object>(input)).Return(expected);
			}

			this.mocks.ReplayAll();
			object actual = this.target.Get(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void LoadTest()
		{
			object input = new object();
			object expected = new object();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.Load<object>(input)).Return(expected);
			}

			this.mocks.ReplayAll();
			object actual = this.target.Load(input);
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}

		[Test]
		public void BeginTransactionTest()
		{
			ITransaction expected = this.mocks.Stub<ITransaction>();
			using (this.mocks.Ordered())
			{
				this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
				this.session.Expect((m) => m.BeginTransaction()).Return(expected);
			}

			this.mocks.ReplayAll();
			ITransaction actual = this.target.BeginTransaction();
			Assert.AreEqual(expected, actual);
			this.mocks.VerifyAll();
		}
	}
}