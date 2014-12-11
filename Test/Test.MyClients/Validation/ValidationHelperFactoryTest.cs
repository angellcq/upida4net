using MyClients.Validation.Impl;
using NUnit.Framework;

namespace Test.MyClients.Validation
{
    public class ValidationHelperFactoryTest : TestBase
    {
        private ValidationHelperFactory target;

        public override void SetUp()
        {
            this.target = new ValidationHelperFactory();
        }

        [Test]
        public void Test()
        {
            var actual = this.target.Get();
            Assert.That(actual, Is.Not.Null);
        }
    }
}