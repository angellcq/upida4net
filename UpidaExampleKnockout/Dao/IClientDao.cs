using System.Collections.Generic;
using UpidaExampleKnockout.Domain;

namespace UpidaExampleKnockout.Dao
{
    public interface IClientDao : IDaoBase<Client>
    {
        IList<Client> GetAll();
    }
}