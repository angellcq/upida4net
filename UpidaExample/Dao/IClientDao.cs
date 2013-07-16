using System.Collections.Generic;
using UpidaExample.Domain;

namespace UpidaExample.Dao
{
    public interface IClientDao : IDaoBase<Client>
    {
        IList<Client> GetAll();
    }
}