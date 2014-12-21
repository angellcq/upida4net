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
            var actual1 = this.target.GetNew();
            var actual2 = this.target.GetNew();
            Assert.That(actual1, Is.Not.Null);
            Assert.That(actual2, Is.Not.Null);
            Assert.That(actual1, Is.Not.EqualTo(actual2));
        }
    }
}