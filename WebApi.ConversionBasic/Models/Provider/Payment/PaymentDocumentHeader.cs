using WebApi.ConversionBasic.Models.Data.Provider.FormatAttributes;
using WebApi.ConversionBasic.Models.Provider;

namespace WebApi.ConversionNew.Models.Provider.Payment
{
    //TODO: define additional field as public properties 
    internal class PaymentDocumentHeader : DocumentHeader
    {
        public string PaymentNumber { get; set; } = string.Empty;
        [FormatDate(outputFormat: "MM/dd/yyyy", inputFormats: new[] { "M/d/y", "M-d-y" })]
        public string PaymentDate { get; set; } = string.Empty;
        [FormatAmount]
        public string PaymentAmount { get; set; } = string.Empty;
        public string PayeeId { get; set; } = string.Empty;

        public string FreeFormAddress1 { get; set; } = string.Empty;
        public string FreeFormAddress2 { get; set; } = string.Empty;
        public string FreeFormAddress3 { get; set; } = string.Empty;
        public string FreeFormAddress4 { get; set; } = string.Empty;
        public string FreeFormAddress5 { get; set; } = string.Empty;
        public string FreeFormAddress6 { get; set; } = string.Empty;

    }
}