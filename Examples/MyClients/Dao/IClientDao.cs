using System.Collections.Generic;
using MyClients.Domain;

namespace MyClients.Dao
{
	public interface IClientDao : IDaobase<Client>
	{
		IList<Client> GetAll();
		Client GetById(int id);
	}
}