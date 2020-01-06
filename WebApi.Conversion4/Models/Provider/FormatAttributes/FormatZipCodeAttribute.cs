using System.Text.RegularExpressions;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    public class FormatZipCodeAttribute : FormatPropertyAttribute
    {
        public override string Format(string value)
        {
            if (!value.Contains("-") && value.Length > 7 && Regex.Matches(value, @"[a-zA-Z]").Count == 0)
            {
                value = value.Insert(5, "-");
            }
            return value;
        }
    }
}