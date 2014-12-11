using NUnit.Framework;
using Rhino.Mocks;
using System;

namespace Test.MyClients
{
    public abstract class TestBase
    {
        private MockRepository mocks;

        [SetUp]
        public void SetUpTestBase()
        {
            this.mocks = new MockRepository();
            this.SetUp();
        }

        public abstract void SetUp();

        public T Stub<T>()
        {
            return this.mocks.Stub<T>();
        }

        public IDisposable Ordered()
        {
            return this.mocks.Ordered();
        }

        public void VerifyTarget(Action targetCall)
        {
            this.mocks.ReplayAll();
            targetCall();
            this.mocks.VerifyAll();
        }

        public T VerifyTarget<T>(Func<T> targetCall)
        {
            this.mocks.ReplayAll();
            T actual = targetCall();
            this.mocks.VerifyAll();
            return actual;
        }
    }
}