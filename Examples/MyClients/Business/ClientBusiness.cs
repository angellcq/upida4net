using MyClients.Dao;
using MyClients.Domain;
using NHibernate;
using System.Collections.Generic;
using Upida;
using Upida.Validation;

namespace MyClients.Business
{
	public class ClientBusiness
	{
		private IMapper mapper;
		private IValidationContext validator;
		private IClientDao clientDao;

		public ClientBusiness(IMapper mapper, IValidationContext validator, IClientDao clientDao)
		{
			this.mapper = mapper;
			this.validator = validator;
			this.clientDao = clientDao;
		}

		public virtual Client GetById(int id)
		{
			Client item = this.clientDao.GetById(id);
			return this.mapper.Filter(item, Levels.DEEP);
		}

		public virtual IList<Client> GetAll()
		{
			IList<Client> items = this.clientDao.GetAll();
			return this.mapper.FilterList(items, Levels.GRID);
		}

		public virtual void Save(Client item)
		{
			this.validator.AssertValid(item, Groups.SAVE);
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				this.mapper.Map(item);
				this.clientDao.Save(item);
				tx.Commit();
			}
		}

		public virtual void Update(Client item)
		{
			this.validator.AssertValid(item, Groups.UPDATE);
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				Client existing = this.clientDao.GetById(item.Id.Value);
				this.mapper.MapTo(item, existing);
				this.clientDao.Merge(existing);
				tx.Commit();
			}
		}

		public virtual void Delete(int id)
		{
			var failures = this.validator.CreateFailureList();
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				Client existing = this.clientDao.GetById(id);
				failures.FailIfNull(existing, null, "Client does not exist", Severity.Fatal);
				this.validator.Assert(failures);
				long count = this.clientDao.GetCount();
				failures.FailIfEqual(1, count, null, "Cannot delete the only client");
				this.validator.Assert(failures);
				this.clientDao.Delete(existing);
				tx.Commit();
			}
		}
	}
}