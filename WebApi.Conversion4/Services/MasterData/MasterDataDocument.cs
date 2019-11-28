using System.Collections.Generic;
using WebApi.Conversion4.Models.Data.Maps;
using WebApi.Conversion4.Models.Data.Provider;

namespace WebApi.Conversion4.Services.MasterData
{
    internal class MasterDataDocument : Document
    {
        internal MasterDataDocument(DocumentHeader header) : base(header)
        {
        }

        internal override DocumentDetailLine CreateNewDocumentDetailLine()
        {
            return new MasterDataDocumentDetailLine();
        }

        internal override void AssignHeader(Map map, Page page, Dictionary<string, List<string>> addressPreFixs)
        {
            base.AssignHeader(map, page, addressPreFixs);
        }

        internal override void AssignDetail(Map map, List<Page> pageDetails,  List<Page> pageRemittances)
        {
            return;
        }
        protected override void AssignDetailLine(Map map, MapDetail mapDetail, string dataLine, string columnHeaderName = "")
        {
            return;
        }
    }
}