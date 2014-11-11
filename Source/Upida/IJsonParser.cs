﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections;

namespace Upida
{
    public interface IJsonParser
    {
        T Parse<T>(JToken node) where T : Dtobase;
        object Parse(JToken node, Type type);
        IEnumerable ParseList(JToken node, Type type);
    }
}