using System.Collections.Generic;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Dao
{
    public interface IClientDao : IDaoBase<Client>
    {
        IList<Client> GetAll();
    }
}