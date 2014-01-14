﻿using System;
using System.Collections.Generic;
using NUnit.Framework;
using Rhino.Mocks;
using Upida.Validation;
using UpidaExampleKnockout.Business;
using UpidaExampleKnockout.Controllers.Api;
using UpidaExampleKnockout.Domain;

namespace Test.UpidaExampleKnockout.Controllers
{
	[TestFixture]
	public class OrderControllerTest
	{
		private MockRepository mocks;
		private IValidationContext validator;
		private OrderBusiness orderBusiness;
		private OrderController target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.validator = this.mocks.Stub<IValidationContext>();
			this.orderBusiness = this.mocks.Stub<OrderBusiness>(null, null);
			this.target = new OrderController(this.validator, this.orderBusiness);
		}

		[Test]
		public void GetByClientTest()
		{
			int input = 5435;
			IList<Order> expected = new List<Order>();
			this.orderBusiness.Expect((m) => m.GetByClient(input)).Return(expected);
			this.mocks.ReplayAll();
			IList<Order> actual = this.target.GetByClient(input);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetTest()
		{
			int input = 5435;
			Order expected = new Order();
			expected.Id = input;
			this.orderBusiness.Expect((m) => m.Get(input)).Return(expected);
			this.mocks.ReplayAll();
			Order actual = this.target.Get(input);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void GetFullTest()
		{
			int input = 5435;
			Order expected = new Order();
			expected.Id = input;
			this.orderBusiness.Expect((m) => m.GetFull(input)).Return(expected);
			this.mocks.ReplayAll();
			Order actual = this.target.GetFull(input);
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void SaveTest()
		{
			Order input = new Order();
			input.Id = 4534;
			using (this.mocks.Ordered())
			{
				this.validator.Expect((m) => m.AssertValid(input, Groups.SAVE));
				this.orderBusiness.Expect((m) => m.Save(input));
			}

			this.mocks.ReplayAll();
			this.target.Save(input);
		}

		[Test]
		public void UpdateTest()
		{
			Order input = new Order();
			input.Id = 4534;
			using (this.mocks.Ordered())
			{
				this.validator.Expect((m) => m.AssertValid(input, Groups.UPDATE));
				this.orderBusiness.Expect((m) => m.Update(input));
			}

			this.mocks.ReplayAll();
			this.target.Update(input);
		}

		[Test]
		public void UpdateItemsTest()
		{
			Order input = new Order();
			input.Id = 4534;
			using (this.mocks.Ordered())
			{
				this.validator.Expect((m) => m.AssertValid(input, Groups.UPDATE_A));
				this.orderBusiness.Expect((m) => m.Update(input));
			}

			this.mocks.ReplayAll();
			this.target.UpdateItems(input);
		}
	}
}