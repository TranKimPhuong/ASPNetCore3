using Newtonsoft.Json;
using System;
using Microsoft.AspNetCore.Http;

namespace WebApi.Common.Models
{
    // Model for multpart data
    public class ConversionRequest
    {
        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String blobHeaderName { get; set; }

       // [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String blobDetailName { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String blobOutputName { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String containerName { get; set; }

        //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public String prefixPayeeID { get; set; }

        //public IFormFile FileData { get; set; }
    }
}
