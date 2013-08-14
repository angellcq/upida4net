using Upida;
using UpidaExample.Business.Util;

namespace UpidaExample.Business
{
    public class BusinessBase
    {
        protected TransactionFactory transactionFactory;
        protected IMapper mapper;

        public BusinessBase(TransactionFactory transactionFactory, IMapper mapper)
        {
            this.transactionFactory = transactionFactory;
            this.mapper = mapper;
        }
    }
}