using System.Collections.Generic;
using NHibernate;
using Upida;
using UpidaExampleKnockout.Dao;
using UpidaExampleKnockout.Domain;
using Upida.Validation;

namespace UpidaExampleKnockout.Business
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
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				this.mapper.Map(item);
				this.clientDao.Save(item);
				tx.Commit();
			}
		}

		public virtual void Delete(int id)
		{
			var failures = this.validator.CreateFailureList();
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				Client existing = this.clientDao.GetById(id);
				failures.FailIf(null == existing, "Client does not exist", Severity.Fatal);
				this.validator.Assert(failures);
				long count = this.clientDao.GetCount();
				failures.FailIf(1 == count, "Cannot delete the only client");
				this.validator.Assert(failures);
				this.clientDao.Delete(existing);
				tx.Commit();
			}
		}
	}
}