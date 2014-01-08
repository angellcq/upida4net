using MyClients.Domain;
using MyClients.Validation;
using NUnit.Framework;
using Rhino.Mocks;

namespace Test.MyClients.Validation
{
	[TestFixture]
	public class LoginMergeValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Login> mocked;
		private LoginMergeValidator target;
		private Login data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Login>>();
			this.target = new LoginMergeValidator();
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
				this.mocked.Expect((m) => m.RequiredIfAssigned());

				this.mocked.Expect((m) => m.Field("name", this.data.Name));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20));

				this.mocked.Expect((m) => m.Field("password", this.data.Password));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(3, 20, Errors.LENGTH_3_20));

				this.mocked.Expect((m) => m.Field("enabled", this.data.Enabled));
				this.mocked.Expect((m) => m.Required(Errors.NOT_VALID_BOOL));

				this.mocked.Expect((m) => m.MissingField("client", this.data.Client));
			}

			this.mocks.ReplayAll();
			this.target.Validate(null);
			this.mocks.VerifyAll();
		}

		private Login BuildTargetData()
		{
			Login data = new Login();
			data.Id  = 123;
			data.Name = "NAME";
			data.Password = "PASSWORD";
			return data;
		}
	}
}