namespace WebApi.Conversion4.Models.Maps
{
    public class MapFieldHeader : MapField
    {

        /// <summary>
        /// Dữ liệu cần lấy ở dòng thứ mấy
        /// </summary>
        public int Line { get; set; }

        public MapFieldHeader(string fieldName, int line, int start, int length, string startWith = "")
        {
            this.FieldName = fieldName;
            this.Line = line;
            this.Start = start;
            this.Length = length;
            this.StartWith = startWith;
        }

        /// <summary>
        /// use base () to Call a different constructor than the base constructor
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="elementIndex"></param>
        public MapFieldHeader(string fieldName, int elementIndex) : base(fieldName, elementIndex)
        {
        }
    }
}
