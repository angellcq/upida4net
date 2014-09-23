using System;
using NUnit.Framework;
using Rhino.Mocks;
using Upida.Validation;
using UpidaExampleKnockout.Domain;
using UpidaExampleKnockout.Validation;

namespace Test.UpidaExampleKnockout.Validation
{
	[TestFixture]
	public class OrderItemSaveValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<OrderItem> mocked;
		private OrderItemSaveValidator target;
		private OrderItem data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<OrderItem>>();
			this.target = new OrderItemSaveValidator();
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

				this.mocked.Expect((m) => m.Field("count", this.data.Count));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));
				this.mocked.Expect((m) => m.MustBeGreaterThan(0, Errors.GREATER_ZERO));

				this.mocked.Expect((m) => m.Field("price", this.data.Price));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_MONEY));
				this.mocked.Expect((m) => m.MustBeGreaterThan(0f, Errors.GREATER_ZERO));

				this.mocked.Expect((m) => m.Field("productId", this.data.ProductId));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));

				this.mocked.Expect((m) => m.MissingField("order", this.data.Id));
			}

			this.mocks.ReplayAll();
			this.target.Validate(null);
			this.mocks.VerifyAll();
		}

		public OrderItem BuildTargetData()
		{
			Order order = new Order();
			order.Id = 78990;
			OrderItem data = new OrderItem();
			data.Id = 3435;
			data.ProductId = 80790;
			data.Count = 780978;
			data.Price = 78.78904f;
			data.Order = order;
			return data;
		}
	}
}