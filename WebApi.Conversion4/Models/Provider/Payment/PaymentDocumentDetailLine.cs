
using WebApi.Conversion4.Models.Data.Provider.FormatAttributes;
using WebApi.Conversion4.Models.Provider;

namespace WebApi.ConversionNew.Models.Provider.Payment
{
    //TODO: define additional field as public properties 
    internal class PaymentDocumentDetailLine : DocumentDetailLine
    {
        public string InvoiceNumber { get; set; } = string.Empty;
        [FormatDate(outputFormat: "MM/dd/yyyy", inputFormats: new[] { "M/d/y", "M-d-y" })]
        public string InvoiceDate { get; set; } = string.Empty;
        [FormatAmount]
        public string GrossAmount { get; set; } = string.Empty;
        [FormatAmount]
        public string DiscountAmount { get; set; } = string.Empty;
        [FormatAmount]
        public string NetAmount { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PONumber { get; set; } = string.Empty;

    }
}