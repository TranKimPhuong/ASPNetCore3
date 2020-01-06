using System.Collections.Generic;

namespace WebApi.Conversion4.Models.Maps
{
    public class MapHeader
    {
        public string FieldKey { get; set; }
        public IList<MapFieldHeader> MapFields { get; set; }
        public MapHeader()
        {
            MapFields = new List<MapFieldHeader>();
         }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="line"></param>
        /// <param name="start"></param>
        /// <param name="length">EOL: int.MaxValue - start</param>
        /// <param name="startWith">dùng để nhận biết dòng nào là header bằng một chuỗi nào đó</param>
        public void AddField(string fieldName, int line, int start, int length, string startWith = "")
        {
            var map = new MapFieldHeader(fieldName, line, start, length, startWith);
            MapFields.Add(map);
        }
        public void AddField(string fieldName, int elementIndex)
        {
            var map = new MapFieldHeader(fieldName, elementIndex);
            MapFields.Add(map);
        }
    }
}
