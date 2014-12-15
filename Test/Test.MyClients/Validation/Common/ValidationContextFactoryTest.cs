using MyClients.Validation.Common.Impl;
using NUnit.Framework;

namespace Test.MyClients.Validation
{
    public class ValidationContextFactoryTest : TestBase
    {
        private ValidationContextFactory target;

        public override void SetUp()
        {
            this.target = new ValidationContextFactory();
        }

        [Test]
        public void Test()
        {
            var actual = this.target.Get();
            Assert.That(actual, Is.Not.Null);
        }
    }
}