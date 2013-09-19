using System.Collections.Generic;
using UpidaExampleStraight.Domain;

namespace UpidaExampleStraight.Dao
{
    public interface IClientDao : IDaobase<Client>
    {
        IList<Client> GetAll();
    }
}