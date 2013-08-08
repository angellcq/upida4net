using System;
using System.Transactions;

namespace UpidaExampleStraight.Business.Util
{
    public class TransactionFactory
    {
        public virtual Transaction Start()
        {
            return new Transaction();
        }
    }
}