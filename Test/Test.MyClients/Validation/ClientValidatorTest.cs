using MyClients.Domain;
using MyClients.Validation;
using MyClients.Validation.Common;
using MyClients.Validation.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;

namespace Test.MyClients.Validation
{
    [TestFixture]
    public class ClientValidatorTest : TestBase
    {
        private IValidationContext context;
        private ILoginValidator loginValidator;
        private ClientValidator target;

        public override void SetUp()
        {
            this.context = this.Stub<IValidationContext>();
            this.loginValidator = this.Stub<ILoginValidator>();
            this.target = new ClientValidator();
            this.target.LoginValidator = this.loginValidator;
        }

        [Test]
        public void ValidateForSave()
        {
            Login a = new Login() { Id = 3333, Name = "LOGIN_NAME_A", Enabled = true, Password = "LOGIN_PWD_A", Client = new Client() { Id = 888 } };
            Login b = new Login() { Id = 4444, Name = "LOGIN_NAME_B", Enabled = false, Password = "LOGIN_PWD_B", Client = new Client() { Id = 999 } };
            Client data = new Client() { Id = 1111, Age = 2222, Name = "NAME", Lastname = "LASTNAME", Logins = new ListAndSet<Login>() { a, b } };
            using (this.Ordered())
            {
                this.context.Expect((m) => m.SetField("id", data.Id));
                this.context.Expect((m) => m.Missing());

                this.context.Expect((m) => m.SetField("name", data.Name));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("lastname", data.Lastname));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("age", data.Age));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Number());
                this.context.Expect((m) => m.MustBeGreaterThan(0, "must be greater than zero"));

                this.context.Expect((m) => m.SetField("logins", data.Logins));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.MustHaveCountBetween(1, 5, "must be at least one login"));

                this.context.Expect((m) => m.AddNested());
                this.context.Expect((m) => m.SetIndex(0));
                this.context.Expect((m) => m.SetTarget(a));
                this.loginValidator.Expect((m) => m.ValidateForSave(a, this.context));

                this.context.Expect((m) => m.SetIndex(1));
                this.context.Expect((m) => m.SetTarget(b));
                this.loginValidator.Expect((m) => m.ValidateForSave(b, this.context));
                this.context.Expect((m) => m.RemoveNested());
            }

            this.VerifyTarget(() => this.target.ValidateForSave(data, this.context));
        }

        [Test]
        public void ValidateForUpdateTest()
        {
            Login a = new Login() { Id = 3333, Name = "LOGIN_NAME_A", Enabled = true, Password = "LOGIN_PWD_A", Client = new Client() { Id = 888 } };
            Login b = new Login() { Id = 4444, Name = "LOGIN_NAME_B", Enabled = false, Password = "LOGIN_PWD_B", Client = new Client() { Id = 999 } };
            Client data = new Client() { Id = 1111, Age = 2222, Name = "NAME", Lastname = "LASTNAME", Logins = new ListAndSet<Login>() { a, b } };
            using (this.Ordered())
            {
                this.context.Expect((m) => m.SetField("id", data.Id));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Number());

                this.context.Expect((m) => m.SetField("name", data.Name));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("lastname", data.Lastname));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("age", data.Age));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Number());
                this.context.Expect((m) => m.MustBeGreaterThan(0, "must be greater than zero"));

                this.context.Expect((m) => m.SetField("logins", data.Logins));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.MustHaveCountBetween(1, 5, "must be at least one login"));

                this.context.Expect((m) => m.AddNested());
                this.context.Expect((m) => m.SetIndex(0));
                this.context.Expect((m) => m.SetTarget(a));
                this.loginValidator.Expect((m) => m.ValidateForMerge(a, this.context));

                this.context.Expect((m) => m.SetIndex(1));
                this.context.Expect((m) => m.SetTarget(b));
                this.loginValidator.Expect((m) => m.ValidateForMerge(b, this.context));
                this.context.Expect((m) => m.RemoveNested());
            }

            this.VerifyTarget(() => this.target.ValidateForUpdate(data, this.context));
        }
    }
}