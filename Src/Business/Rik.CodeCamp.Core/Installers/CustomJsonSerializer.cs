using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rik.CodeCamp.Core.Installers
{
    public sealed class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Formatting = Formatting.Indented;
        }
    }
}
