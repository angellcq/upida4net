using NUnit.Framework;
using Rhino.Mocks;
using Upida;
using Upida.Validation;
using UpidaExampleAngular.Validation;

namespace Test.UpidaExampleAngular.Validation
{
	[TestFixture]
	public class HandyValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Dtobase> mocked;
		private HandyValidator<Dtobase> target;
		private Dtobase data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Dtobase>>();
			this.target = new HandyValidator<Dtobase>();
			this.target.SetSelf(this.mocked);
			this.data = new Dtobase();
			this.target.SetTarget(this.data, null, null);
		}

		[Test]
		public void IsAssignedAndNotNull_TT_Test()
		{
			this.mocked.Expect((m) => m.IsAssigned()).Return(true);
			this.mocked.Expect((m) => m.IsNull()).Return(true);
			this.mocks.ReplayAll();
			bool actual = this.target.isAssignedAndNotNull();
			Assert.AreEqual(false, actual);
		}

		[Test]
		public void IsAssignedAndNotNull_TF_Test()
		{
			this.mocked.Expect((m) => m.IsAssigned()).Return(true);
			this.mocked.Expect((m) => m.IsNull()).Return(false);
			this.mocks.ReplayAll();
			bool actual = this.target.isAssignedAndNotNull();
			Assert.AreEqual(true, actual);
		}

		[Test]
		public void IsAssignedAndNotNull_FT_Test()
		{
			this.mocked.Expect((m) => m.IsAssigned()).Return(false);
			this.mocked.Expect((m) => m.IsNull()).Return(true);
			this.mocks.ReplayAll();
			bool actual = this.target.isAssignedAndNotNull();
			Assert.AreEqual(false, actual);
		}

		[Test]
		public void IsAssignedAndNotNull_FF_Test()
		{
			this.mocked.Expect((m) => m.IsAssigned()).Return(false);
			this.mocked.Expect((m) => m.IsNull()).Return(false);
			this.mocks.ReplayAll();
			bool actual = this.target.isAssignedAndNotNull();
			Assert.AreEqual(false, actual);
		}

		[Test]
		public void RequiredTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.MustBeAssigned(Errors.REQUIRED));
				this.mocked.Expect((m) => m.MustBeNotNull(Errors.REQUIRED));
			}

			this.mocks.ReplayAll();
			this.target.Required();
			this.mocks.VerifyAll();
		}

		[Test]
		public void RequiredWithMessageTest()
		{
			string input = "Random message";
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.MustBeAssigned(Errors.REQUIRED));
				this.mocked.Expect((m) => m.MustBeValidFormat(input));
				this.mocked.Expect((m) => m.MustBeNotNull(Errors.REQUIRED));
			}

			this.mocks.ReplayAll();
			this.target.Required(input);
			this.mocks.VerifyAll();
		}

		[Test]
		public void RequiredIfAssigned_TrueTest()
		{
			this.mocked.Expect((m) => m.isAssignedAndNotNull()).Return(true);
			this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));

			this.mocks.ReplayAll();
			this.target.RequiredIfAssigned(Errors.MUST_BE_NUMBER);
			this.mocks.VerifyAll();
		}

		[Test]
		public void RequiredIfAssigned_FalseTest()
		{
			this.mocked.Expect((m) => m.isAssignedAndNotNull()).Return(false);
			this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER)).Repeat.Never();

			this.mocks.ReplayAll();
			this.target.RequiredIfAssigned(Errors.MUST_BE_NUMBER);
			this.mocks.VerifyAll();
		}

		[Test]
		public void MustBeEmailTest()
		{
			string msg = "RANDOM MESSAGE";
			const string expr = @"\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b";
			this.mocked.Expect((m) => m.MustRegexpr(expr, msg));
			this.mocks.ReplayAll();
			this.target.MustBeEmail(msg);
			this.mocks.VerifyAll();
		}

		[Test]
		public void MissingFieldTest()
		{
			string field = "FIELD_NAME";
			object value = "VALUE";
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.Field(field, value));
				this.mocked.Expect((m) => m.SetSeverity(Severity.Fatal));
				this.mocked.Expect((m) => m.MustBeNotAssigned(Errors.MUST_BE_EMPTY));
			}

			this.mocks.ReplayAll();
			this.target.MissingField(field, value);
			this.mocks.VerifyAll();
		}
	}
}