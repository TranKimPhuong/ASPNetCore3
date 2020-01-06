using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using WebApi.ConversionBasic.Models.Maps;
using WebApi.ConversionBasic.Models.Provider;
using WebApi.ConversionBasic.Models.Provider.MasterData;
using WebApi.ConversionBasic.Services.CustomerA;
using WebApi.ConversionBasic.Ultilities.Library;

namespace WebApi.ConversionNew.Services.CustomerA.MasterData
{
    public class CustomerAMasterDataPsTool : PsTool 
    {
        public override Map CreateMap()
        {
            return new CustomerAMasterDataMap();
        }

        public override Document CreateNewDocument()
        {
            return new MasterDataDocument(new MasterDataDocumentHeader());
        }

        public override StringBuilder BuildStandardFileHeader()
        {
            //throw new NotImplementedException();
            var res = new StringBuilder();
            try
            {
                res.AppendLine("VR,VendorId,Name,Status,Is1099,MainAddressId,DefaultRemitToId,HoldStatus");
                res.AppendLine("AR,AddressName,Address1,Address2,Address3,City,StateProvince,PostalCode,PhoneNumber");

                return res;
            }
            catch (Exception ex)
            {
                ListErrors.Add(ex.Message);
            }

            return res;
        }
        public override StringBuilder ConvertDocumentsToStandardFile()
        {
            var res = new StringBuilder();
            try
            {
                var groupedDocuments = Documents.GroupBy(s => (s.Header as MasterDataDocumentHeader).VendorId);
                var preVendor = new MasterDataDocumentHeader();
                foreach (var documents in groupedDocuments)
                {
                    int i = 0;
                    foreach (var doc in documents)
                    {
                        var header = (doc.Header as MasterDataDocumentHeader);
                        if (i == 0 || string.IsNullOrWhiteSpace(header.VendorId))
                        {
                            res.Append($"VR,{header.VendorId.Trim().EscapeCSV()},{header.VendorName.Trim().EscapeCSV()},1,0,MAIN,MAIN,0");
                            res.Append(Environment.NewLine + $"AR,MAIN,{header.VendorAddress1?.Trim().EscapeCSV()},{header.VendorAddress2?.Trim().EscapeCSV()},{header.VendorAddress3?.Trim().EscapeCSV()},{header.VendorCity?.Trim().EscapeCSV()},{header.VendorState?.Trim().EscapeCSV()},{header.VendorZip?.Trim().EscapeCSV()},{header.VendorPhone?.Trim().EscapeCSV()}" + Environment.NewLine);
                        }
                        else
                        {
                            res.Append(Environment.NewLine + $"AR,REMIT TO {i},{header.VendorAddress1?.Trim().EscapeCSV()},{header.VendorAddress2?.Trim().EscapeCSV()},{header.VendorAddress3?.Trim().EscapeCSV()},{header.VendorCity?.Trim().EscapeCSV()},{header.VendorState?.Trim().EscapeCSV()},{header.VendorZip?.Trim().EscapeCSV()},{header.VendorPhone?.Trim().EscapeCSV()}" + Environment.NewLine);
                        }
                        i++;
                    }
                }

            }
            catch (Exception ex)
            {
                ListErrors.Add(ex.Message);
            }

            return res;
        }

        public override List<Page> FillData(byte[] inputFile)
        {
            var pages = new List<Page>();
            var page = new Page
            {
                FromRow = Map.DataFromLine
            };
            var i = 1;
            try
            {

                var dataTable = inputFile.ReadDataFromExcel();
                foreach (DataRow dataTableRow in dataTable.Rows)
                {
                    if (i < Map.DataFromLine) { i++; continue; }
                    foreach (var o in dataTableRow.ItemArray)
                    {
                        page.Rows.Add(o.ToString());
                    }
                    if (page.Rows.Count != 0)
                    {
                        pages.Add(page);
                    }
                    page = new Page
                    {
                        FromRow = i
                    };
                    i++;
                }

            }
            catch (Exception ex)
            {
                if (string.IsNullOrEmpty(Map.MapHeader.FieldKey))
                {
                    if (page != null)
                        ListErrors.Add(
                            $"Read file error: Line {page.FromRow} - {page.ToRow}, rows - {page.Rows.Count} : {ex.Message}{Environment.NewLine}");
                }
                else if (page != null)
                    ListErrors.Add(
                        $"Read file error: Line {page.FromRow} - {page.ToRow}, {Map.MapHeader.FieldKey} - {page.Key}, Rows - {page.Rows.Count} : {ex.Message}{Environment.NewLine}");
            }
            if (pages.Count == 0)
                ListErrors.Add("The input file contains INVALID DATA");
            return pages;
        }

        public override void Process()
        {
            base.Process();
        }
 
    }

    public class MasterDataDocument : Document
    {
        public MasterDataDocument(DocumentHeader header) : base(header) {}

        public override DocumentDetailLine CreateNewDocumentDetailLine()
        {
            return new MasterDataDocumentDetailLine();
        }

        public override void AssignHeader(Map map, Page page, Dictionary<string, List<string>> addressPreFixs)
        {
            base.AssignHeader(map, page, addressPreFixs);
        }
}