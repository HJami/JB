using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace JB.models
{
   [JsonConverter(typeof(StringEnumConverter))]  
    public enum Location
    {
        Melbourne,
        Sydney,
        Brisbane,
        Perth,
        Hobart
    }
}