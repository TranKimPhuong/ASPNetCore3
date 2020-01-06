namespace WebApi.Conversion4.Models.Maps
{
    public class MapOverflow
    {
        public string Key { get; set; }
        public int Line { get; set; }
        public bool HeaderInFirstPage { get; set; }

        public MapOverflow(string key, int line = 0, bool headerInFirstPage = false)
        {
            this.Key = key;
            this.Line = line;
            this.HeaderInFirstPage = headerInFirstPage;
        }
    }
}
