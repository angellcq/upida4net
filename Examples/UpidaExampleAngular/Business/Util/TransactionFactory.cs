using System;
using System.Transactions;

namespace UpidaExampleAngular.Business.Util
{
    public class TransactionFactory
    {
        public virtual Transaction Start()
        {
            return new Transaction();
        }
    }
}