using System.Collections.Generic;
using UpidaExampleAngularEF.Domain;

namespace UpidaExampleAngularEF.Dao
{
	public interface IClientDao : IDaobase<Client>
	{
		IList<Client> GetAll();
		Client GetById(int id);
		long GetCount();
		void Save(Client item);
		void Delete(Client item);
	}
}