using System;
using System.Collections.Specialized;

namespace Upida
{
    public interface IFormParser
    {
        T Parse<T>(NameValueCollection form) where T : Dtobase;
    }
}