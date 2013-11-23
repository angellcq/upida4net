using System.Collections.Generic;
using NHibernate;
using Upida;
using UpidaExampleAngular.Dao;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Business
{
	public class ClientBusiness : BusinessBase
	{
		private IClientDao clientDao;

		public ClientBusiness(IMapper mapper, IClientDao clientDao)
			: base(mapper)
		{
			this.clientDao = clientDao;
		}

		public IList<Client> GetAll()
		{
			IList<Client> items = this.clientDao.GetAll();
			return this.mapper.FilterList(items, Levels.GRID);
		}

		public void Save(Client item)
		{
			using (ITransaction tx = this.clientDao.BeginTransaction())
			{
				this.mapper.Map(item);
				this.clientDao.Save(item);
				tx.Commit();
			}
		}
	}
}