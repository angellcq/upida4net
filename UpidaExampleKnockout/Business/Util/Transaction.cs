using System;
using System.Transactions;

namespace UpidaExampleKnockout.Business.Util
{
    public class Transaction : IDisposable
    {
        private TransactionScope scope;

        public Transaction()
        {
            this.scope = new TransactionScope();
        }

        public virtual void Commit()
        {
            this.scope.Complete();
        }

        public virtual void Dispose()
        {
            this.scope.Dispose();
        }
    }
}