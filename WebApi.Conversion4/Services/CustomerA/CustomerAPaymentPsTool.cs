using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebApi.Conversion4.Models.Maps;
using WebApi.Conversion4.Models.Provider;
using WebApi.ConversionNew.Models.Provider.Payment;

namespace WebApi.ConversionNew.Services.CustomerA.Payment
{
    public class CustomerAPaymentPsTool : PsTool
    {
        public override Map CreateMap()
        {
            return new CustomerAPaymentMap();
        }

        public override Document CreateNewDocument()
        {

            return new PaymentDocument(new PaymentDocumentHeader());
        }

        public override void ProcessBeforeConvertDocumentsToStandardFile()
        {
            base.ProcessBeforeConvertDocumentsToStandardFile();
        }
        public override StringBuilder BuildStandardFileHeader()
        {
            var res = new StringBuilder();
            res.AppendLine("H,PaymentNumber,PaymentAmount,PayeeId,PaymentDate,FreeFormAddressLine1,FreeFormAddressLine2,FreeFormAddressLine3,FreeFormAddressLine4,FreeFormAddressLine5");
            res.AppendLine("D,InvoiceNumber,InvoiceDate,NetAmount,UDF2,UDF1");
            return res;
        }
        public override StringBuilder ConvertDocumentsToStandardFile()
        {
            var res = new StringBuilder();
            try
            {
                res.Append(string.Join(Environment.NewLine, Documents.Select(doc => doc.ToString())));
                return res;
            }
            catch (Exception ex)
            {
                ListErrors.Add(ex.Message);
            }
            return res;
        }

        public override List<Page> FillData(byte[] inputFile)
        {
            return base.FillData(inputFile);
        }

        public override void Process()
        {
            base.Process();
        }
    }

    public class PaymentDocument : Document
    {
        public PaymentDocument(DocumentHeader header) : base(header)
        {
        }
        //public override string ToString()
        //{
        //    return $"H,{PaymentNumber.Trim()},{PaymentAmount.Trim()},{PayeeId.Trim()},{PaymentDate.Trim()},{FreeFormAddress1?.Trim().EscapeCSV()},{FreeFormAddress2?.Trim().EscapeCSV()},{FreeFormAddress3?.Trim().EscapeCSV()},{FreeFormAddress4?.Trim().EscapeCSV()},{FreeFormAddress5?.Trim().EscapeCSV()}";
        //}
        //public override string ToString() => $"D,{InvoiceNumber?.Trim().EscapeCSV()},{InvoiceDate?.Trim()},{NetAmount.Trim()},{PONumber?.Trim().EscapeCSV()},{Description?.Trim().EscapeCSV()}";
        public override string ToString()
        {
            return $"{ Header.ToString()}{Environment.NewLine}"
                + $"{string.Join(Environment.NewLine, DetailLines.Select(d => d.ToString()))}";
        }

        public override DocumentDetailLine CreateNewDocumentDetailLine()
        {
            return new PaymentDocumentDetailLine();
        }

        public override void AssignHeader(Map map, Page page, Dictionary<string, List<string>> addressPreFixs)
        {
            base.AssignHeader(map, page, addressPreFixs);
        }

        public override void AssignDetail(Map map, List<Page> pageDetails, List<Page> pageRemittances)
        {
            base.AssignDetail(map,pageDetails, pageRemittances);
        }

        public override void AssignDetailLine(Map map, MapDetail mapDetail, string dataLine, string columnHeaderName = "")
        {
            base.AssignDetailLine(map, mapDetail, dataLine, columnHeaderName);
        }

        public override void AssignDetailFromRemitFile(Map map, Page sourcePage, List<Page> pageRemittances)
        {
            base.AssignDetailFromRemitFile(map, sourcePage, pageRemittances);
        }

        public override void AssignDetailLineFromRemitFile(Map map, string dataLine, string columnHeaderName = "")
        {
            base.AssignDetailLineFromRemitFile(map, dataLine, columnHeaderName);
        }
    }
}