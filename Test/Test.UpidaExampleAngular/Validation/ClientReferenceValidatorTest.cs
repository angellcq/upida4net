using NUnit.Framework;
using Rhino.Mocks;
using System.Collections.Generic;
using UpidaExampleAngular.Domain;
using UpidaExampleAngular.Validation;

namespace Test.UpidaExampleAngular.Validation
{
	[TestFixture]
	public class ClientReferenceValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Client> mocked;
		private ClientReferenceValidator target;
		private Client data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Client>>();
			this.target = new ClientReferenceValidator();
			this.target.SetSelf(this.mocked);
			this.data = this.BuildTargetData();
			this.target.SetTarget(this.data, null, null);
		}

		[Test]
		public void ValidateTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.Field("id", this.data.Id));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));

				this.mocked.Expect((m) => m.MissingField("name", this.data.Name));
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