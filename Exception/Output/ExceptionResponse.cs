using System.Collections.Generic;
using Newtonsoft.Json;

namespace GOfit.MyGOfit.ExceptionMiddleware.Output
{
    public class ExceptionResponse
    {
        public ExceptionResponse() => Validations = new List<PropertyResultResponse>();

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("exception")]
        public string Exception { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("I18nexception")]
        public string I18nException { get; set; }

        [JsonProperty("i18nentity")]
        public string I18nEntity { get; set; }

        [JsonProperty("entity")]
        public string Entity { get; set; }

        [JsonProperty("validations")]
        public IEnumerable<PropertyResultResponse> Validations { get; set; }
    }
}
