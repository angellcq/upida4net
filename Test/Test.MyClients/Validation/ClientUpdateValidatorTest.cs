using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Iesi.Collections.Generic;
using MyClients.Domain;
using MyClients.Validation;
using NUnit.Framework;
using Rhino.Mocks;
using Upida.Validation;

namespace Test.MyClients.Validation
{
	[TestFixture]
	public class ClientUpdateValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Client> mocked;
		private ClientUpdateValidator target;
		private Client data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Client>>();
			this.target = new ClientUpdateValidator();
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

				this.mocked.Expect((m) => m.Field("name", this.data.Name));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20));

				this.mocked.Expect((m) => m.Field("lastname", this.data.Lastname));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20));

				this.mocked.Expect((m) => m.Field("age", this.data.Age));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));
				this.mocked.Expect((m) => m.MustBeGreaterThan(0, Errors.GREATER_ZERO));

				this.mocked.Expect((m) => m.Field("logins", this.data.Logins));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveCountBetween(1, 5, Errors.NUMBER_OF_LOGINS));
				this.mocked.Expect((m) => m.Stop());
				this.mocked.Expect((m) => m.NestedList<Login>(Groups.MERGE, null));
			}

			this.mocks.ReplayAll();
			this.target.Validate(null);
			this.mocks.VerifyAll();
		}

		private Client BuildTargetData()
		{
			Client data = new Client();
			data.Id = 123;
			data.Age = 23;
			data.Name = "NAME";
			data.Lastname = "LAST NAME";
			data.Logins = new HashedSet<Login>();
			return data;
		}
	}
}