﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Rik.CodeCamp.Core.Nancy
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