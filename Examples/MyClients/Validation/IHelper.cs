using System;
using Upida.Validation;

namespace MyClients.Validation
{
    public interface IHelper : IValidator
    {
        void Required();
        void Missing();
        void Number();
        void Date();
        void Float();
        void Email();
        void Text();
        void TrueFalse();
    }
}