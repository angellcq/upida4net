using System.Collections.Generic;
using MyClients.Dao.Impl;
using MyClients.Database;
using MyClients.Domain;
using NHibernate;
using NHibernate.Transform;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.MyClients.Dao
{
    [TestFixture]
    public class ClientDaoTest : TestBase
    {
        private ISession session;
        private ISessionFactoryEx sessionFactory;
        private IQuery query;
        private ClientDao target;

        public override void SetUp()
        {
            this.session = this.Stub<ISession>();
            this.sessionFactory = this.Stub<ISessionFactoryEx>();
            this.query = this.Stub<IQuery>();
            this.target = new ClientDao();
            this.target.SessionFactory = this.sessionFactory;
        }

        [Test]
        public void GetByIdTest()
        {
            int input = 5672;
            Client expected = new Client();
            expected.Id = input;
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.CreateQuery("from Client client left outer join fetch client.Logins where client.Id = :id")).Return(this.query);
                this.query.Expect((m) => m.SetParameter<int>("id", input)).Return(this.query);
                this.query.Expect((m) => m.SetResultTransformer(Transformers.DistinctRootEntity)).Return(this.query);
                this.query.Expect((m) => m.UniqueResult<Client>()).Return(expected);
            }

            Client actual = this.VerifyTarget(() => this.target.GetById(input));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllTest()
        {
            IList<Client> expected = new List<Client>();
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.CreateQuery("from Client client left outer join fetch client.Logins")).Return(this.query);
                this.query.Expect((m) => m.SetResultTransformer(Transformers.DistinctRootEntity)).Return(this.query);
                this.query.Expect((m) => m.List<Client>()).Return(expected);
            }

            IList<Client> actual = this.VerifyTarget(() => this.target.GetAll());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetCountTest()
        {
            long expected = 20;
            using (this.Ordered())
            {
                this.sessionFactory.Expect((m) => m.GetCurrentSession()).Return(this.session);
                this.session.Expect((m) => m.CreateQuery("select count(*) from Client")).Return(this.query);
                this.query.Expect((m) => m.UniqueResult<long>()).Return(expected);
            }

            long actual = this.VerifyTarget(() => this.target.GetCount());
            Assert.AreEqual(expected, actual);
        }
    }
}