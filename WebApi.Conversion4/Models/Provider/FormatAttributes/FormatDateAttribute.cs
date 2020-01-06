using System;
using System.Globalization;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    /// <summary>
    /// Format value with many format to MM/dd/yyyy format which required for standard file.
    /// </summary>
    public class FormatDateAttribute : FormatPropertyAttribute
    {
        public string[] Formats;
        public string OutputFormat;
        /// <param name="inputFormats">Possible formats of INPUT FILE</param>
        public FormatDateAttribute(string outputFormat, string[] inputFormats)
        {
            Formats = inputFormats;
            OutputFormat = outputFormat;
        }
        public override string Format(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            DateTime dt;
            if (DateTime.TryParseExact(value, Formats, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dt))
            {
                return dt.ToString(OutputFormat);
            }
            return value;
        }
    }
}