using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using UpidaExampleAngular.Domain;
using UpidaExampleAngular.Validation;

namespace Test.UpidaExampleAngular.Validation
{
	[TestFixture]
	public class ClientSaveValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Client> mocked;
		private ClientSaveValidator target;
		private Client data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Client>>();
			this.target = new ClientSaveValidator();
			this.target.SetSelf(this.mocked);
			this.data = this.BuildTargetData();
			this.target.SetTarget(this.data, null, null);
		}

		[Test]
		public void ValidateTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.MissingField("id", this.data.Id));

				this.mocked.Expect((m) => m.Field("name", data.Name));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50));
				this.mocked.Expect((m) => m.MustBeEmail(Errors.EMAIL));
			}

			this.mocks.ReplayAll();
			this.target.Validate(null);
			this.mocks.VerifyAll();
		}

		public Client BuildTargetData()
		{
			Client data = new Client();
			data.Id = 123;
			data.Name = "Client Name";
			data.Orders = new HashSet<Order>();
			return data;
		}
	}
}