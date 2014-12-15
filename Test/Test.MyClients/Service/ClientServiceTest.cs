using MyClients.Service.Impl;
using MyClients.Dao;
using MyClients.Domain;
using NHibernate;
using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using MyClients.Validation.Impl;
using MyClients.Validation;
using MyClients.Validation.Common;

namespace Test.MyClients.Business
{
    [TestFixture]
    public class ClientBusinessTest
    {
        private MockRepository mocks;
        private IClientDao clientDao;
        private IMapper mapper;
        private IValidationFacade validator;
        private ITransaction transaction;
        private ClientService target;
        private IFailureList failures;

        [SetUp]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.clientDao = mocks.Stub<IClientDao>();
            this.mapper = mocks.Stub<IMapper>();
            this.validator = mocks.Stub<IValidationFacade>();
            this.transaction = mocks.Stub<ITransaction>();
            this.target = new ClientService();
            this.target.Mapper = this.mapper;
            this.target.Validator = this.validator;
            this.target.ClientDao = this.clientDao;
            this.failures = mocks.Stub<IFailureList>();
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
                this.validator.Expect((m) => m.AssertClientForSave(input));
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
                this.validator.Expect((m) => m.AssertClientForUpdate(input));
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

        [Test]
        public void DeleteTest()
        {
            int input = 4235;
            long count = 3;
            Client existing = new Client();
            existing.Id = 6786;

            using (mocks.Ordered())
            {
                this.clientDao.Expect((m) => m.BeginTransaction()).Return(this.transaction);
                this.clientDao.Expect((m) => m.GetById(input)).Return(existing);
                this.validator.Expect((m) => m.AssertClientExists(existing));
                this.clientDao.Expect((m) => m.GetCount()).Return(count);
                this.validator.Expect((m) => m.AssertMoreThanOneClient(count));
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