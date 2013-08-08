using Upida;
using UpidaExampleStraight.Business.Util;

namespace UpidaExampleStraight.Business
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