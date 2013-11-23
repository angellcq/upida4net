using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Upida.Aspnetmvc
{
	public class UpidaJsonFormatter : JsonMediaTypeFormatter
	{
		private IJsonParser parser = new JsonParser();

		public UpidaJsonFormatter()
		{
			this.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
			this.SerializerSettings.ContractResolver = new LowerCasePropertyContractResolver();
		}

		public override async Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (typeof(Dtobase).IsAssignableFrom(type))
			{
				object data = await base.ReadFromStreamAsync(typeof(JToken), readStream, content, formatterLogger);
				return this.parser.Parse(data as JToken, type);
			}
			else
			{
				return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
			}
		}

		public class LowerCasePropertyContractResolver : DefaultContractResolver
		{
			protected override string ResolvePropertyName(string propertyName)
			{
				return string.Concat(Char.ToLower(propertyName[0]), propertyName.Substring(1));
			}
		}
	}
}