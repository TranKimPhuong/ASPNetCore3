using System.Collections.Generic;

namespace WebApi.Conversion4.Models.Maps
{
    public class Page
    {
        /// <summary>
        /// Key in Page: nên dùng check number làm key
        /// Key in Remittance Page: là field map giữ file chính và file remittance
        /// </summary>
        public string Key { get; set; }
        public bool HasOver { get; set; }
        public List<string> Rows { get; set; }
        public int FromRow { get; set; }
        public int ToRow { get; set; }
        public string ColumnNameData { get; set; }

        public Page()
        {
            this.Rows = new List<string>();
        }
    }
}
