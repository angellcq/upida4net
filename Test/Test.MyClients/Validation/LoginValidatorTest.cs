using MyClients.Domain;
using MyClients.Validation;
using MyClients.Validation.Impl;
using NUnit.Framework;
using Rhino.Mocks;
using Upida;

namespace Test.MyClients.Validation
{
    [TestFixture]
    public class LoginValidatorTest : TestBase
    {
        private IHelper context;
        private LoginValidator target;

        public override void SetUp()
        {
            this.context = this.Stub<IHelper>();
            this.target = new LoginValidator();
        }

        [Test]
        public void ValidateForSave()
        {
            Login data = new Login() { Id = 3333, Name = "LOGIN_NAME_A", Enabled = true, Password = "LOGIN_PWD_A", Client = new Client() };
            using (this.Ordered())
            {
                this.context.Expect((m) => m.SetField("id", data.Id));
                this.context.Expect((m) => m.Missing());

                this.context.Expect((m) => m.SetField("name", data.Name));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("password", data.Password));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("enabled", data.Enabled));
                this.context.Expect((m) => m.TrueFalse());

                this.context.Expect((m) => m.SetField("client", data.Client));
                this.context.Expect((m) => m.Missing());
            }

            this.VerifyTarget(() => this.target.ValidateForSave(data, this.context));
        }

        [Test]
        public void ValidateForMerge_Update_Test()
        {
            Login data = new Login() { Id = 3333, Name = "LOGIN_NAME_A", Enabled = true, Password = "LOGIN_PWD_A", Client = new Client() };
            using (this.Ordered())
            {
                this.context.Expect((m) => m.SetField("id", data.Id));
                this.context.Expect((m) => m.IsAssigned()).Return(true);
                this.context.Expect((m) => m.Number());

                this.context.Expect((m) => m.SetField("name", data.Name));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("password", data.Password));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("enabled", data.Enabled));
                this.context.Expect((m) => m.TrueFalse());

                this.context.Expect((m) => m.SetField("client", data.Client));
                this.context.Expect((m) => m.Missing());
            }

            this.VerifyTarget(() => this.target.ValidateForMerge(data, this.context));
        }

        [Test]
        public void ValidateForMerge_Save_Test()
        {
            Login data = new Login() { Id = 3333, Name = "LOGIN_NAME_A", Enabled = true, Password = "LOGIN_PWD_A", Client = new Client() };
            using (this.Ordered())
            {
                this.context.Expect((m) => m.SetField("id", data.Id));
                this.context.Expect((m) => m.IsAssigned()).Return(false);

                this.context.Expect((m) => m.SetField("name", data.Name));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("password", data.Password));
                this.context.Expect((m) => m.Required());
                this.context.Expect((m) => m.Text());

                this.context.Expect((m) => m.SetField("enabled", data.Enabled));
                this.context.Expect((m) => m.TrueFalse());

                this.context.Expect((m) => m.SetField("client", data.Client));
                this.context.Expect((m) => m.Missing());
            }

            this.VerifyTarget(() => this.target.ValidateForMerge(data, this.context));
        }
    }
}