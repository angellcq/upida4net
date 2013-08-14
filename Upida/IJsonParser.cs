using Newtonsoft.Json.Linq;
using System;

namespace Upida
{
    public interface IJsonParser
    {
        T Parse<T>(JToken node) where T : Dtobase;
    }
}