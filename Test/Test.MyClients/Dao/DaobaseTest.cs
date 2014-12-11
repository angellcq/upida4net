using MyClients.Dao.Support;
using MyClients.Database;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.MyClients.Dao
{
    [TestFixture]
    public class DaobaseTest : TestBase
    {
        private ISession session;
        private ISessionFactoryEx sessionFactory;
        private Daobase<object> target;

        public override void SetUp()
        {
            this.session = this.Stub<ISession>();
            this.sessionFactory = this.Stub<ISessionFactoryEx>();
            this.target = new Daobase<object>();
            this.target.SessionFactory = this.sessionFactory;
        }

        [Test]
        public void SaveTest()
        {
            object input = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Save(input));
            }

            this.VerifyTarget(() => this.target.Save(input));
        }

        [Test]
        public void UpdateTest()
        {
            object input = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Update(input));
            }

            this.VerifyTarget(() => this.target.Update(input));
        }

        [Test]
        public void MergeTest()
        {
            object input = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Merge<object>(input));
            }

            this.VerifyTarget(() => this.target.Merge(input));
        }

        [Test]
        public void DeleteTest()
        {
            object input = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Delete(input));
            }

            this.VerifyTarget(() => this.target.Delete(input));
        }

        [Test]
        public void GetTest()
        {
            object input = new object();
            object expected = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Get<object>(input)).Return(expected);
            }

            object actual = this.VerifyTarget(() => this.target.Get(input));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoadTest()
        {
            object input = new object();
            object expected = new object();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.Load<object>(input)).Return(expected);
            }

            object actual = this.VerifyTarget(() => this.target.Load(input));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BeginTransactionTest()
        {
            ITransaction expected = this.Stub<ITransaction>();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.BeginTransaction()).Return(expected);
            }

            ITransaction actual = this.VerifyTarget(() => this.target.BeginTransaction());
            Assert.AreEqual(expected, actual);
        }
    }
}