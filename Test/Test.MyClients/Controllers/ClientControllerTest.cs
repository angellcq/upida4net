using System.Collections.Generic;
using MyClients.Business;
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
		private ClientBusiness clientBusiness;
		private ClientController target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.clientBusiness = this.mocks.Stub<ClientBusiness>(null, null, null);
			this.target = new ClientController(this.clientBusiness);
		}

		[Test]
		public void GetByIdTest()
		{
			int input = 234;
			Client expected = new Client();
			expected.Id = input;
			this.clientBusiness.Expect((m) => m.GetById(input)).Return(expected);
			mocks.ReplayAll();
			Client actual = this.target.GetById(input);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void getAllTest()
		{
			IList<Client> expected = new List<Client>();
			this.clientBusiness.Expect((m) => m.GetAll()).Return(expected);
			mocks.ReplayAll();
			IList<Client> actual = this.target.GetAll();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void SaveTest()
		{
			Client input = new Client();
			input.Id = 4235;
			this.clientBusiness.Expect((m) => m.Save(input));
			mocks.ReplayAll();
			this.target.Save(input);
			this.clientBusiness.VerifyAllExpectations();
		}

		[Test]
		public void UpdateTest()
		{
			Client input = new Client();
			input.Id = 4235;
			this.clientBusiness.Expect((m) => m.Update(input));
			mocks.ReplayAll();
			this.target.Update(input);
			this.clientBusiness.VerifyAllExpectations();
		}
	}
}