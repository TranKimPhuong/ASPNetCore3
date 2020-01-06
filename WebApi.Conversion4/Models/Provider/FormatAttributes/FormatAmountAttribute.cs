namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    public class FormatAmountAttribute : FormatPropertyAttribute
    {
        public override string Format(string value)
        {
            var tempString = value.Replace("$", string.Empty).Replace("*", string.Empty);
            decimal dec;
            if (decimal.TryParse(tempString, out dec))
                return dec.ToString("0.00");
            return value;
        }
    }
}