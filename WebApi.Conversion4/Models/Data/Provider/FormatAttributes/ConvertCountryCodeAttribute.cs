using WebApi.Conversion4.Models.Library;

namespace WebApi.Conversion4.Models.Data.Provider.FormatAttributes
{
    internal class ConvertCountryCodeAttribute : FormatPropertyAttribute
    {
        internal override string Format(string value)
        {
            return CountryCodeConverter.ConvertToThreeLetterISORegionName(value);
        }
    }
}