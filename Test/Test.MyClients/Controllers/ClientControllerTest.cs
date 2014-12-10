using System.Collections.Generic;
using MyClients.Service;
using MyClients.Controllers.API;
using MyClients.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.MyClients.Controllers
{
    [TestFixture]
    public class ClientControllerTest
    {
        private MockRepository mocks;
        private IClientService clientService;
        private ClientController target;

        [SetUp]
        public void SetUp()
        {
            this.mocks = new MockRepository();
            this.clientService = this.mocks.Stub<IClientService>();
            this.target = new ClientController();
            this.target.ClientService = this.clientService;
        }

        [Test]
        public void GetByIdTest()
        {
            int input = 234;
            Client expected = new Client();
            expected.Id = input;
            this.clientService.Expect((m) => m.GetById(input)).Return(expected);
            mocks.ReplayAll();
            Client actual = this.target.GetById(input);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllTest()
        {
            IList<Client> expected = new List<Client>();
            this.clientService.Expect((m) => m.GetAll()).Return(expected);
            mocks.ReplayAll();
            IList<Client> actual = this.target.GetAll();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SaveTest()
        {
            Client input = new Client();
            input.Id = 4235;
            this.clientService.Expect((m) => m.Save(input));
            mocks.ReplayAll();
            this.target.Save(input);
            this.clientService.VerifyAllExpectations();
        }

        [Test]
        public void UpdateTest()
        {
            Client input = new Client();
            input.Id = 4235;
            this.clientService.Expect((m) => m.Update(input));
            mocks.ReplayAll();
            this.target.Update(input);
            this.clientService.VerifyAllExpectations();
        }

        [Test]
        public void DeleteTest()
        {
            int input = 4235;
            this.clientService.Expect((m) => m.Delete(input));
            mocks.ReplayAll();
            this.target.Delete(input);
            this.clientService.VerifyAllExpectations();
        }
    }
}