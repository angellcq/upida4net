using System.Collections.Generic;
using MyClients.Controllers.API;
using MyClients.Domain;
using MyClients.Service;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.MyClients.Controllers
{
    [TestFixture]
    public class ClientControllerTest : TestBase
    {
        private IClientService clientService;
        private ClientController target;

        public override void SetUp()
        {
            this.clientService = this.Stub<IClientService>();
            this.target = new ClientController();
            this.target.ClientService = this.clientService;
        }

        [Test]
        public void GetByIdTest()
        {
            Client expected = new Client() { Id = 4444 };
            this.clientService.Expect((m) => m.GetById(1111)).Return(expected);
            Client actual = this.VerifyTarget(() => this.target.GetById(1111));
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetAllTest()
        {
            IList<Client> expected = new List<Client>();
            this.clientService.Expect((m) => m.GetAll()).Return(expected);
            IList<Client> actual = this.VerifyTarget(() => this.target.GetAll());
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SaveTest()
        {
            Client input = new Client() { Id = 1111 };
            this.clientService.Expect((m) => m.Save(input));
            this.VerifyTarget(() => this.target.Save(input));
        }

        [Test]
        public void UpdateTest()
        {
            Client input = new Client() { Id = 4444 };
            this.clientService.Expect((m) => m.Update(input));
            this.VerifyTarget(() => this.target.Update(input));
        }

        [Test]
        public void DeleteTest()
        {
            this.clientService.Expect((m) => m.Delete(1111));
            this.VerifyTarget(() => this.target.Delete(1111));
        }
    }
}