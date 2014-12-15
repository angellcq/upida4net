using MyClients.Domain;
using MyClients.Validation;
using MyClients.Validation.Common;
using MyClients.Validation.Common.Impl;
using MyClients.Validation.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;

namespace Test.MyClients.Validation
{
    public class ValidationFacadeTest : TestBase
    {
        private IValidationContext context;
        private IClientValidator clientValidator;
        private IValidationContextFactory contextFactory;
        private ValidationFacade target;

        public override void SetUp()
        {
            this.context = this.Stub<IValidationContext>();
            this.clientValidator = this.Stub<IClientValidator>();
            this.contextFactory = this.Stub<IValidationContextFactory>();
            this.target = new ValidationFacade();
            this.target.ClientValidator = this.clientValidator;
            this.target.ContextFactory = this.contextFactory;
        }

        [Test]
        public void AssertClientForSaveTest()
        {
            Client data = new Client() { Id = 1111 };
            using (this.Ordered())
            {
                this.contextFactory.Expect((m) => m.Get()).Return(this.context);
                this.context.Expect((m) => m.SetTarget(data));
                this.clientValidator.Expect((m) => m.ValidateForSave(data, this.context));
                this.context.Expect((m) => m.Assert());
            }

            this.VerifyTarget(() => this.target.AssertClientForSave(data));
        }

        [Test]
        public void AssertClientForUpdateTest()
        {
            Client data = new Client() { Id = 1111 };
            using (this.Ordered())
            {
                this.contextFactory.Expect((m) => m.Get()).Return(this.context);
                this.context.Expect((m) => m.SetTarget(data));
                this.clientValidator.Expect((m) => m.ValidateForUpdate(data, this.context));
                this.context.Expect((m) => m.Assert());
            }

            this.VerifyTarget(() => this.target.AssertClientForUpdate(data));
        }

        [Test]
        public void AssertClientExists_NotExists()
        {
            using (this.Ordered())
            {
                this.contextFactory.Expect((m) => m.Get()).Return(this.context);
                this.context.Expect((m) => m.Fail("Client does not exist"));
                this.context.Expect((m) => m.Assert());
            }

            this.VerifyTarget(() => this.target.AssertClientExists(null));
        }

        [Test]
        public void AssertClientExists_Exists()
        {
            Client data = new Client() { Id = 1111 };
            this.contextFactory.Expect((m) => m.Get()).Return(this.context);
            this.VerifyTarget(() => this.target.AssertClientExists(data));
        }

        [Test]
        public void AssertMoreThanOneClient_2()
        {
            this.contextFactory.Expect((m) => m.Get()).Return(this.context);
            this.VerifyTarget(() => this.target.AssertMoreThanOneClient(2));
        }

        [Test]
        public void AssertMoreThanOneClient_1()
        {
            using (this.Ordered())
            {
                this.contextFactory.Expect((m) => m.Get()).Return(this.context);
                this.context.Expect((m) => m.Fail("Cannot delete the only client"));
                this.context.Expect((m) => m.Assert());
            }

            this.VerifyTarget(() => this.target.AssertMoreThanOneClient(1));
        }
    }
}