﻿using NHibernate;
using System.Collections.Generic;
using Upida;
using Upida.Validation;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Business
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