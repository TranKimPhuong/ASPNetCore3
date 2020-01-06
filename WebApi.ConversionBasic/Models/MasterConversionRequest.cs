using Microsoft.AspNetCore.Http;


namespace WebApi.ConversionBasic.Models
{
    public class MasterConversionRequest
    {
        public IFormFile FileData { get; set; }
        public string CaseAction { get; set; }
        //public string VendorCreate { get; set; }
        //public string AddressCreate { get; set; }
        //public string VendorUpdate { get; set; }
        //public string AddressUpdate { get; set; }
        public string ContainerName { get; set; } = string.Empty;
        public string BlobName { get; set; } = string.Empty;
        public string Decrypt { get; set; }
        public string SiteName { get; set; }
        public string isGetFromBlob { get; set; } = string.Empty;
    }
}
