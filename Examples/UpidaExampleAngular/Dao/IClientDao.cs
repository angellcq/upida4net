using System.Collections.Generic;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Dao
{
	public interface IClientDao : IDaobase<Client>
	{
		IList<Client> GetAll();
	}
}