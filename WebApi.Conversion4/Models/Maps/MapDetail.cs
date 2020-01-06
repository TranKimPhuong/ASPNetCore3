using System.Collections.Generic;

namespace WebApi.Conversion4.Models.Maps
{
    public class MapDetail
    {
        public int FromLine { get; set; } = 1;
        public int ToLine { get; set; } =0;
        public int DetailLength { get; set; }
        public int DetailCount { get; set; } = 1;

        public List<Condition> BreakDetailLineCondition { get; set; } = new List<Condition>();
        public List<Condition> RecognizeDetailLineCondition { get; set; } = new List<Condition>();

        public IList<MapField> MapFields { get; set; } = new List<MapField>();

        public MapDetail()
        {
        }

        public MapDetail(int fromLine, int toLine, int detailLength = 0, int detailCount = 1)
        {
            this.FromLine = fromLine;
            this.ToLine = toLine;
            this.DetailLength = detailLength;
            this.DetailCount = detailCount;
        }

        public void AddField(string fieldName, int start, int length, string startWith = "", string columnName = "")
        {
            var map = new MapField(fieldName, start, length, startWith, columnName);
            this.MapFields.Add(map);
        }
        public void AddField(string fieldName, int elementIndex)
        {
            var map = new MapField(fieldName, elementIndex);
            MapFields.Add(map);
        }
    }
}
