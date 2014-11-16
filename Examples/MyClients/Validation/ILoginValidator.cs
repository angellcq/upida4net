using System.Collections.Generic;
using MyClients.Domain;

namespace MyClients.Validation
{
    public interface ILoginValidator
    {
        void ValidateForSave(IEnumerable<Login> logins, IHandyValidator parentContext);

        void ValidateForMerge(IEnumerable<Login> logins, IHandyValidator parentContext);
    }
}