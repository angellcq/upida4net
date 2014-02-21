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
	public class ClientControllerTest
	{
		private MockRepository mocks;
		private IValidationContext validator;
		private ClientBusiness clientBusiness;
		private ClientController target;

		[SetUp]
		public void SetUp()
		{
			this.mocks = new MockRepository();
			this.validator = this.mocks.Stub<IValidationContext>();
			this.clientBusiness = this.mocks.Stub<ClientBusiness>(null, null);
			this.target = new ClientController(this.validator, this.clientBusiness);
		}

		[Test]
		public void GetAllTest()
		{
			IList<Client> expected = new List<Client>();
			this.clientBusiness.Expect((m) => m.GetAll()).Return(expected);
			this.mocks.ReplayAll();
			IList<Client> actual = this.target.GetAll();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void SaveTest()
		{
			Client input = new Client();
			input.Id = 4534;
			using (this.mocks.Ordered())
			{
				this.validator.Expect((m) => m.AssertValid(input, Groups.SAVE));
				this.clientBusiness.Expect((m) => m.Save(input));
			}

			this.mocks.ReplayAll();
			this.target.Save(input);
		}
	}
}