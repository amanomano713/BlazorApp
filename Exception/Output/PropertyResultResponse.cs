using System.Collections.Generic;
using Newtonsoft.Json;

namespace GOfit.MyGOfit.ExceptionMiddleware.Output
{
    public class PropertyResultResponse
    {
        public PropertyResultResponse() => Errors = new List<string>();

        [JsonProperty("property")]
        public string Property { get; set; }

        [JsonProperty("errors")]
        public IList<string> Errors { get; set; }
    }
}
