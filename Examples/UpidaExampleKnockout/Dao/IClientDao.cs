using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao
{
	public interface IClientDao : IDaobase<Client>
	{
		IList<Client> GetAll();
	}
}