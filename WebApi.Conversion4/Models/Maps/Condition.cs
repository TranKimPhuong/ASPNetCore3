namespace WebApi.Conversion4.Models.Maps
{
    public class Condition
    {
        public string Text { get; set; }
        public int Line { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public string Operator { get; set; }
        public int ElementIndex { get; set; }
        public Condition(string text, int line = 0, int start = 0, int length = 0, string @operator = "==")
        {
            this.Text = text;
            this.Line = line;
            this.Start = start;
            this.Length = length;
            this.Operator = @operator;
        }
        public Condition(string text, int elementIndex = 0)
        {
            this.Text = text;
            this.ElementIndex = elementIndex;
        }
    }
}
