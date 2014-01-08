using System;
using MyClients.Validation;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;
using Upida.Validation;

namespace Test.MyClients.Validation
{
	[TestFixture]
	class HandyValidatorTest
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
		public void RequiredTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.MustBeAssigned(Errors.REQUIRED));
				this.mocked.Expect((m) => m.MustBeNotNull(Errors.REQUIRED));
			}

			this.mocks.ReplayAll();
			this.target.Required();
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
		}

		[Test]
		public void RequiredIfAssigned_TrueTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.IsAssigned()).Return(true);
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));
			}

			this.mocks.ReplayAll();
			this.target.RequiredIfAssigned();
			this.mocks.VerifyAll();
		}

		[Test]
		public void RequiredIfAssigned_FalseTest()
		{
			using (this.mocks.Ordered())
			{
				this.mocked.Expect((m) => m.IsAssigned()).Return(false);
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER)).Repeat.Never();
			}

			this.mocks.ReplayAll();
			this.target.RequiredIfAssigned();
			this.mocks.VerifyAll();
		}
	}
}