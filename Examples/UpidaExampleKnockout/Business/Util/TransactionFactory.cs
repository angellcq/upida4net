using System;
using System.Transactions;

namespace UpidaExampleKnockout.Business.Util
{
    public class TransactionFactory
    {
        public virtual Transaction Start()
        {
            return new Transaction();
        }
    }
}