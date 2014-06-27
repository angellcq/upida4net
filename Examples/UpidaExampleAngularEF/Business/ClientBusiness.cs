using System.Collections.Generic;
using System.Transactions;
using Upida;
using Upida.Validation;
using UpidaExampleAngularEF.Dao;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Business
{
	public class ClientBusiness : BusinessBase
	{
		private IClientDao clientDao;

		public ClientBusiness(IMapper mapper, IValidationContext validator, IClientDao clientDao)
			: base(mapper, validator)
		{
			this.clientDao = clientDao;
		}

		public virtual IList<Client> GetAll()
		{
			IList<Client> items = this.clientDao.GetAll();
			return this.mapper.FilterList(items, Levels.GRID);
		}

		public virtual void Save(Client item)
		{
			using (TransactionScope tx = new TransactionScope())
			{
				this.mapper.Map(item);
				this.clientDao.Save(item);
				tx.Complete();
			}
		}

		public virtual void Delete(int id)
		{
			var failures = this.validator.CreateFailureList();
			using (TransactionScope tx = new TransactionScope())
			{
				Client existing = this.clientDao.GetById(id);
				failures.FailIfNull(existing, null, "Client does not exist", Severity.Fatal);
				this.validator.Assert(failures);
				long count = this.clientDao.GetCount();
				failures.FailIfEqual(1, count, null, "Cannot delete the only client");
				this.validator.Assert(failures);
				this.clientDao.Delete(existing);
				tx.Complete();
			}
		}
	}
}