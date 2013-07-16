using System;
using System.Transactions;

namespace UpidaExample.Business.Util
{
    public class TransactionFactory
    {
        public virtual Transaction Start()
        {
            return new Transaction();
        }
    }
}