using System;
using Iesi.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Upida.Validation;
using UpidaExampleAngular.Domain;
using UpidaExampleAngular.Validation;

namespace Test.UpidaExampleAngular.Validation
{
	[TestFixture]
	public class OrderUpdateValidatorTest
	{
		private MockRepository mocks;
		private HandyValidator<Order> mocked;
		private OrderUpdateValidator target;
		private Order data;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.mocked = this.mocks.Stub<HandyValidator<Order>>();
			this.target = new OrderUpdateValidator();
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

				this.mocked.Expect((m) => m.MissingField("createdOn", this.data.CreatedOn));

				this.mocked.Expect((m) => m.Field("shipAddress", this.data.ShipAddress));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(5, 50, Errors.LENGTH_5_AND_50));

				this.mocked.Expect((m) => m.Field("shipCity", this.data.ShipCity));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50));

				this.mocked.Expect((m) => m.Field("shipCountry", this.data.ShipCountry));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(2, 50, Errors.LENGTH_2_AND_50));

				this.mocked.Expect((m) => m.Field("shipZip", this.data.ShipZip));
				this.mocked.Expect((m) => m.Required());
				this.mocked.Expect((m) => m.MustHaveLengthBetween(5, 5, Errors.LENGTH_ZIP));

				this.mocked.Expect((m) => m.Field("total", this.data.Total));
				this.mocked.Expect((m) => m.Required(Errors.MUST_BE_NUMBER));
				this.mocked.Expect((m) => m.MustBeGreaterThan(0f, Errors.GREATER_ZERO));

				this.mocked.Expect((m) => m.MissingField("orderItems", this.data.OrderItems));
				this.mocked.Expect((m) => m.MissingField("client", this.data.Client));
			}

			this.mocks.ReplayAll();
			this.target.Validate(null);
			this.mocks.VerifyAll();
		}

		public Order BuildTargetData()
		{
			Client client = new Client();
			client.Id = 4324;
			Order data = new Order();
			data.Id = 123;
			data.CreatedOn = DateTime.Now;
			data.Client = client;
			data.ShipCountry = "COUNTRY";
			data.ShipCity = "CITY";
			data.ShipAddress = "ADDRESS";
			data.ShipZip = "ZIPZIP";
			data.Total = 234.234f;
			data.OrderItems = new HashedSet<OrderItem>();
			return data;
		}
	}
}