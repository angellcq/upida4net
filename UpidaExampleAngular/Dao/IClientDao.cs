using System.Collections.Generic;
using UpidaExampleAngular.Domain;

namespace UpidaExampleAngular.Dao
{
    public interface IClientDao : IDaoBase<Client>
    {
        IList<Client> GetAll();
    }
}