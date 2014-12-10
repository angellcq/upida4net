using MyClients.Domain;

namespace MyClients.Validation
{
    public interface ILoginValidator
    {
        void ValidateForSave(Login target, IHelper context);
        void ValidateForMerge(Login target, IHelper context);
    }
}