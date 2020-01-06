using System.Collections.Generic;

namespace WebApi.ConversionBasic.Models.Maps
{
    public class MapRemittance
    {
        /// <summary>
        /// Field dung lam Key de nhan biet remitance thuoc hearder nao
        /// </summary>
        public string LinkFieldName { get; set; }

        /// <summary>
        /// Field nhan biet nam o line nao trong remittance
        /// </summary>
        public int LinkFieldLine { get; set; }

        /// <summary>
        /// Field nhan biet bat dau o cot thu may remittance
        /// </summary>
        public int LinkFieldStart { get; set; }

        /// <summary>
        /// Field nhan biet cua remittance co do dai bao nhieu
        /// </summary>
        public int LinkFieldLength { get; set; }

        /// <summary>
        /// Bắt đầu đọc dữ liệu từ dòng thứ mấy trog file
        /// </summary>
        public int DataFromLine { get; set; }

        /// <summary>
        /// Vị trí dòng kết thúc 1 page
        /// </summary>
        public int BreakPageLine { get; set; }

        /// <summary>
        /// Dấu hiệu nhận biết kết thúc 1 page
        /// </summary>
        public List<Condition> BreakPageCondition { get; set; } = new List<Condition>();

        /// <summary>
        /// Dấu hiệu nhận biết bắt đầu 1 page
        /// </summary>
        public List<Condition> NewPageCondition { get; set; } = new List<Condition>();

        /// <summary>
        /// Line bat dau cua cac detail line trong remittance
        /// </summary>
        public int DetailFromLine { get; set; }

        /// <summary>
        /// Line ket thuc cua cac detail line trong remittance
        /// </summary>
        public int DetailToLine { get; set; }

        public int DetailLength { get; set; }
        public int DetailCount { get; set; }

        /// <summary>
        /// Chuoi danh dau het 1 list detail line
        /// </summary>
        public List<Condition> BreakDetailLineCondition { get; set; } = new List<Condition>();
        public List<Condition> RecognizeDetailLineCondition { get; set; } = new List<Condition>();

        /// <summary>
        /// Nếu null thì lấy theo file chính (MapDetail)
        /// </summary>
        public IList<MapField> MapFields { get; set; } = new List<MapField>();
        public MapOverflow MapOverflow { get; set; }
        public MapRemittance()
        {

        }
        public void AddField(string fieldName, int start, int length, string startWith = "", string columnName = "")
        {
            var map = new MapField(fieldName, start, length, startWith, columnName);
            this.MapFields.Add(map);
        }

        public MapRemittance(
            string linkFieldName,
            int linkFieldLine, int linkfieldStart, int linkfieldLength,
            int detailFromLine, int detailToLine = 0,
            int detailLength = 0, int detailCount = 1,
            string breakDetailLineKey = "", int breakDetailLineKeyStart = 0, int breakDetailLineKeyLegnth = 0,
            int dataFromLine = 1,
            int breakPageLine = 0, string breakPageKey = "", int breakPageKeyStart = 0, int breakPageKeyLegnth = 0)
        {
            this.LinkFieldName = linkFieldName;
            this.LinkFieldStart = linkfieldStart;
            this.LinkFieldLength = linkfieldLength;
            this.LinkFieldLine = linkFieldLine;
            this.DetailFromLine = detailFromLine;
            this.DetailToLine = detailToLine;
            this.DetailLength = detailLength;
            this.DetailCount = detailCount;

            this.DataFromLine = dataFromLine;
            this.BreakPageLine = breakPageLine;

        }
    }
}
