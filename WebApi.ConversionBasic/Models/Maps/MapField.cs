namespace WebApi.ConversionBasic.Models.Maps
{
    public class MapField
    {
        public string FieldName { get; set; }
        public int ElementIndex { get; set; }

        /// <summary>
        /// Dữ liệu cần lấy ở dòng có bắt đầu bằng StartWith
        /// </summary>
        public string StartWith { get; set; }

        /// <summary>
        /// Dữ liệu cần lấy ở cột có Column Name
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// vị trí cột hoặc vị trí trên mảng
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// độ dài chuỗi giá trị
        /// </summary>
        public int Length { get; set; }

        public MapField() { }
        public MapField(string fieldName, int elementIndex)
        {
            FieldName = fieldName;
            this.ElementIndex = elementIndex;
        }
        public MapField(string fieldName, int start, int length, string startWith = "", string columnName = "")
        {
            this.FieldName = fieldName;
            this.Start = start;
            this.Length = length;
            this.StartWith = startWith;
            this.ColumnName = columnName;
        }
    }
}