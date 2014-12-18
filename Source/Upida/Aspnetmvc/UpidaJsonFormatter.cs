using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Upida.Impl;

namespace Upida.Aspnetmvc
{
    /// <summary>
    /// Represents JSON formatter above the standard Newtonsoft formatter
    /// </summary>
    public class UpidaJsonFormatter : JsonMediaTypeFormatter
    {
        private IJsonParser parser = new JsonParser(UpidaContext.Current);

        /// <summary>
        /// Creates an instance of the UpidaJsonFormatter
        /// </summary>
        public UpidaJsonFormatter()
        {
            this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            this.SerializerSettings.ContractResolver = new LowerCasePropertyContractResolver();
        }

        /// <summary>
        /// Parses JSON data from stream and converts it into a Dto object of the specific type.
        /// </summary>
        /// <param name="type">specific type</param>
        /// <param name="readStream">incoming JSON stream</param>
        /// <param name="content">content</param>
        /// <param name="formatterLogger">formatterLogger</param>
        /// <returns></returns>
        public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
        {
            if (typeof(Dtobase).IsAssignableFrom(type))
            {
                object data = await base.ReadFromStreamAsync(typeof(JToken), readStream, content, formatterLogger);
                return this.parser.Parse(data as JToken, type);
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                Type innerType = type.GenericTypeArguments[0];
                if (typeof(Dtobase).IsAssignableFrom(innerType))
                {
                    object data = await base.ReadFromStreamAsync(typeof(JToken), readStream, content, formatterLogger);
                    return this.parser.ParseList(data as JToken, innerType);
                }
                else
                {
                    return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
                }
            }
            else
            {
                return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
            }
        }

        internal class LowerCasePropertyContractResolver : DefaultContractResolver
        {
            protected override string ResolvePropertyName(string propertyName)
            {
                return string.Concat(Char.ToLower(propertyName[0]), propertyName.Substring(1));
            }
        }
    }
}