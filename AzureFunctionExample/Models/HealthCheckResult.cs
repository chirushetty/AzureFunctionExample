using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureFunctionExample.Models
{
    public class HealthCheckResult
    {
        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CheckStatus Status { get; set; }

        public string Message { get; set; }
    }
}
