using Upida;
using UpidaExample.Business.Util;

namespace UpidaExample.Business
{
    public class BusinessBase
    {
        protected TransactionFactory transactionFactory;
        protected Mapper mapper;

        public BusinessBase(TransactionFactory transactionFactory, Mapper mapper)
        {
            this.transactionFactory = transactionFactory;
            this.mapper = mapper;
        }
    }
}